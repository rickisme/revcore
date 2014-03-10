using Data.Enums;
using Data.Structures.Player;
using DatabaseFactory;
using Global.Interfaces;
using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using WorldServer.Extensions;
using WorldServer.OuterNetwork;
using WorldServer.OuterNetwork.Write;
using WorldServer.SkillEngine;

namespace WorldServer.Services
{
    internal class StorageService : IStorageService
    {
        private IDFactory IDFactory;

        public StorageService()
        {
            IDFactory = new IDFactory(1000000);
        }

        public long UidRegister(StorageItem Item)
        {
            return IDFactory.Register(Item.UID);
        }

        public void Action()
        {

        }

        public void AddStartItemsToPlayer(Player player)
        {
            AddItem(player, player.Inventory, new StorageItem { ItemId = 1000000902, Amount = 1, State = ItemState.NEW });

            switch (player.PlayerData.Class)
            {
                case PlayerClass.Blademan:
                    AddItem(player, player.Inventory, new StorageItem { ItemId = 100200001, Amount = 1, State = ItemState.NEW });
                    break;
                case PlayerClass.Swordman:
                    AddItem(player, player.Inventory, new StorageItem { ItemId = 200200001, Amount = 1, State = ItemState.NEW });
                    break;
                case PlayerClass.Spearman:
                    AddItem(player, player.Inventory, new StorageItem { ItemId = 300200001, Amount = 1, State = ItemState.NEW });
                    break;
                case PlayerClass.Bowman:
                    AddItem(player, player.Inventory, new StorageItem { ItemId = 400200001, Amount = 1, State = ItemState.NEW });
                    break;
                case PlayerClass.Medic:
                    AddItem(player, player.Inventory, new StorageItem { ItemId = 500200001, Amount = 1, State = ItemState.NEW });
                    break;
                case PlayerClass.Ninja:
                    AddItem(player, player.Inventory, new StorageItem { ItemId = 600200001, Amount = 1, State = ItemState.NEW });
                    break;
                case PlayerClass.Busker:
                    AddItem(player, player.Inventory, new StorageItem { ItemId = 700200001, Amount = 1, State = ItemState.NEW });
                    break;
                case PlayerClass.Hanbi:
                    AddItem(player, player.Inventory, new StorageItem { ItemId = 100204001, Amount = 1, State = ItemState.NEW });
                    break;
            }
            DataBaseStorage.SavePlayerStorage(player.PlayerId, player.Inventory);
        }

        public void ShowPlayerStorage(Player player, StorageType storageType, bool shadowUpdate = true, int offset = 0)
        {
            switch (storageType)
            {
                case StorageType.Inventory:
                    new SpWeightMoney(player).Send(player.Connection);
                    new SpInventoryInfo(player.Inventory).Send(player.Connection);
                    //DataBaseStorage.SavePlayerStorage(player.PlayerId, player.Inventory);
                    break;
                case StorageType.CharacterWarehouse:

                    break;
                case StorageType.Trade:
                    // Updates are sent by the controller
                    break;
            }
        }

        public bool AddItem(Player player, Storage storage, int itemId, int count, int slot = -1)
        {
            if (count < 0)
                return false;

            if (slot > (storage.StorageType == StorageType.Inventory ? storage.Size + 20 : storage.Size))
                return false;

            if ((storage.StorageType == StorageType.Inventory && slot < 20 && slot != -1))
                return false;

            if (slot < -1)
                return false;

            lock (storage.ItemsLock)
            {
                int stackSize = 99;

                if (storage.IsFull())
                {
                    SystemMessages.InventoryIsFull.Send(player.Connection);
                    return false;
                }

                if (slot != -1)
                {
                    // Certain slot + Stackable
                    if (storage.Items.ContainsKey(slot))
                    {
                        //new SpChatMessage(player, string.Format("You Cant Put Item {0} In Inventory!!", ItemTemplate.Factory(itemId).Name), ChatType.Info).Send(player.Connection);
                        return false;
                    }

                    storage.Items.Add(slot, new StorageItem { ItemId = itemId, Amount = count, State = ItemState.NEW });
                }
                else
                {
                    #region Any slot + Stackable
                    Dictionary<int, StorageItem> itemsById = storage.GetItemsById(itemId);

                    int canBeAdded =
                        itemsById.Values.Where(storageItem => storageItem.Amount < stackSize).Sum(
                            storageItem => stackSize - storageItem.Amount);

                    if (canBeAdded >= count)
                    {
                        foreach (var storageItem in itemsById.Values)
                        {
                            int added = Math.Min(stackSize - storageItem.Amount, count);
                            storageItem.Amount += added;
                            storageItem.State = ItemState.UPDATE;
                            count -= added;
                            if (count == 0)
                                break;
                        }
                    }
                    else
                    {
                        if (storage.IsFull() || count > GetFreeSlots(storage).Count * stackSize)
                        {
                            new SpChatMessage(player, "Inventory Full!!", ChatType.House, true).Send(player.Connection);
                            return false;
                        }

                        foreach (var storageItem in itemsById.Values)
                        {
                            int added = Math.Min(stackSize - storageItem.Amount, count);
                            storageItem.Amount += added;
                            storageItem.State = ItemState.UPDATE;
                            count -= added;
                        }
                        while (count > 0)
                        {
                            int added = Math.Min(stackSize, count);
                            StorageItem item = new StorageItem { ItemId = itemId, Amount = added, State = ItemState.NEW };
                            storage.Items.Add(storage.GetFreeSlot(), item);
                            count -= added;
                        }
                    }
                    #endregion
                }
            }

            ShowPlayerStorage(player, storage.StorageType);
            return true;
        }

        public bool AddItem(Player player, Storage storage, StorageItem item)
        {
            item.UID = IDFactory.GetNext();

            lock (storage.ItemsLock)
            {
                if (item.ItemId == 0)
                {
                    Log.Debug("Item UID[{0}]: ItemId = {1}", item.UID, item.ItemId);
                    return false;
                }

                int maxStack = CanStack(item) ? 99 : 1;
                int canStacked = 1;

                if (maxStack > 1)
                {
                    for (int i = 0; i < storage.Size; i++)
                    {
                        if (!storage.Items.ContainsKey(i)) 
                            continue;

                        if (storage.Items[i].ItemId == item.ItemId)
                        {
                            canStacked += maxStack - storage.Items[i].Amount;

                            if (canStacked >= item.Amount)
                                break;
                        }
                    }
                }

                if (canStacked < item.Amount && GetFreeSlots(storage).Count < 1)
                    return false;

                if (canStacked > 0)
                {
                    for (int i = 0; i < storage.Size; i++)
                    {
                        if (!storage.Items.ContainsKey(i)) continue;

                        if (storage.Items[i].ItemId == item.ItemId)
                        {
                            int put = maxStack - storage.Items[i].Amount;
                            if (item.Amount < put)
                                put = item.Amount;

                            storage.Items[i].Amount += put;
                            storage.Items[i].State = ItemState.UPDATE;
                            item.Amount -= put;

                            if (item.Amount <= 0)
                                break;
                        }
                    }
                }

                if (item.Amount > 0) 
                { 
                    storage.Items.Add(storage.GetFreeSlot(), item);
                }
                ShowPlayerStorage(player, storage.StorageType);
                return true;
            }
        }

        public bool RemoveItem(Player player, Storage storage, int slot, int counter)
        {
            if (counter < 0)
                return false;

            lock (storage.ItemsLock)
            {
                if (!storage.Items.ContainsKey(slot) || storage.Items[slot].Amount < counter)
                    return false;

                if (storage.Items[slot].Amount == counter)
                {
                    storage.Items[slot].State = ItemState.DELETE;
                    storage.DeleteItems.Add(storage.Items[slot].UID, storage.Items[slot]);
                    storage.Items.Remove(slot);
                }
                else if (storage.Items[slot].Amount > counter)
                {
                    storage.Items[slot].Amount -= counter;
                    storage.Items[slot].State = ItemState.UPDATE;
                }
                ShowPlayerStorage(player, storage.StorageType, false);
            }
            return true;
        }

        public bool RemoveItemById(Player player, Storage storage, int itemId, int counter)
        {
            //todo rework
            for (int slot = 0; slot <= player.Inventory.Size; slot++)
                if (player.Inventory.Items.ContainsKey(slot))
                {
                    if (player.Inventory.Items[slot].ItemId == itemId)
                    {
                        if (player.Inventory.Items[slot].Amount <= counter)
                        {
                            player.Inventory.Items[slot].State = ItemState.DELETE;
                            storage.DeleteItems.Add(player.Inventory.Items[slot].UID, player.Inventory.Items[slot]);
                            player.Inventory.Items.Remove(slot);
                        }
                        else
                        {
                            player.Inventory.Items[slot].Amount -= counter;

                            player.Inventory.Items[slot].State = ItemState.UPDATE;
                        }
                        ShowPlayerStorage(player, storage.StorageType);
                        return true;
                    }                        
                }
            return false;
        }

        public void ReplaceItem(Player player, Storage storage, MoveItemArgs Args, bool showStorage = true)
        {
            lock (storage.ItemsLock)
            {
                switch (Args.IsFromInventory)
                {
                    case 1: // Move item from inventory
                        storage.Items.TryGetValue(Args.FromSlot, out Args.ItemToMove);

                        switch (Args.IsToInventory)
                        {
                            case 1: // to inventory
                                {
                                    Args.ItemToReplace = player.Inventory.GetItem(Args.ToSlot);
                                    storage.Items.Remove(Args.FromSlot);
                                    if (Args.ItemToReplace != null)
                                    {
                                        storage.Items.Remove(Args.ToSlot);
                                        storage.Items.Add(Args.FromSlot, Args.ItemToReplace);
                                    }
                                    storage.Items.Add(Args.ToSlot, Args.ItemToMove);
                                }
                                break;

                            case 0: // to Equiped
                                {
                                    if (!CanDress(player, Args.ItemToMove) && !Global.Global.AdminEngine.IsDev(player))
                                        return;

                                    storage.EquipItems.TryGetValue(Args.ToSlot, out Args.ItemToReplace);
                                    storage.Items.Remove(Args.FromSlot);
                                    storage.EquipItems.Remove(Args.ToSlot);

                                    if (Args.ItemToReplace != null)
                                        storage.Items.Add(Args.FromSlot, Args.ItemToReplace);

                                    storage.EquipItems.Add(Args.ToSlot, Args.ItemToMove);
                                }
                                break;
                        }
                        break;

                    case 0: // Move item from Equiped
                        Args.ItemToMove = player.Inventory.GetEquipItem(Args.FromSlot);

                        switch (Args.IsToInventory)
                        {
                            case 1: // to inventory
                                {
                                    Args.ItemToReplace = player.Inventory.GetItem(Args.ToSlot);

                                    storage.EquipItems.Remove(Args.FromSlot);
                                    if (Args.ItemToReplace != null)
                                    {
                                        storage.Items.Remove(Args.ToSlot);
                                        //storage.EquipItems.Add(Args.FromSlot, Args.ItemToReplace);
                                    }
                                    storage.Items.Add(Args.ToSlot, Args.ItemToMove);
                                }
                                break;

                            case 0: // to Equiped
                                // this may be not use
                                break;
                        }
                        break;
                }

                switch (Args.ItemToMove.State)
                {
                    case ItemState.NEW:
                        Args.ItemToMove.State = ItemState.NEW;
                        break;
                    case ItemState.UPDATE:
                        Args.ItemToMove.State = ItemState.UPDATE;
                        break;
                    case ItemState.DELETE:
                        break;
                }
            }

            new SpInventoryMove(Args).Send(player.Connection);

            CreatureLogic.UpdateCreatureStats(player);

            if (showStorage)
            {
                Global.Global.VisibleService.Send(player, new SpEquipInfo(player));
                ShowPlayerStorage(player, storage.StorageType);
            }
        }

        public List<int> GetFreeSlots(Storage storage)
        {
            var freeSlots = new List<int>();

            for (int i = 0; i <= storage.Size; i++)
                if (!storage.Items.ContainsKey(i))
                    freeSlots.Add(i);

            return freeSlots;
        }

        public bool AddMoneys(Player player, Storage storage, long counter)
        {
            if (counter < 0)
                return false;

            storage.Money += counter;
            ShowPlayerStorage(player, storage.StorageType);

            return true;
        }

        public bool RemoveMoney(Player player, Storage storage, long counter)
        {
            if (counter < 0)
                return false;

            if (storage.Money - counter >= 0)
                storage.Money -= counter;
            else
            {
                player.Inventory.Money = 0;
                Log.Warn("InventorService: Player {0} moneys can't be less than 0");
                ShowPlayerStorage(player, storage.StorageType);
                return false;
            }

            ShowPlayerStorage(player, storage.StorageType);
            return true;
        }

        public bool ContainsItem(Storage storage, int itemId, long counter)
        {
            lock (storage.ItemsLock)
            {
                for (int i = 0; i <= storage.Size; i++)
                    if (storage.Items.ContainsKey(i))
                    {
                        if (storage.Items[i].ItemId == itemId && storage.Items[i].Amount >= counter)
                            return true;
                    }
            }
            return false;
        }

        public StorageItem GetItemById(Storage storage, int id)
        {
            lock (storage.ItemsLock)
            {
                for (int i = 0; i <= storage.Size; i++)
                    if (storage.Items.ContainsKey(i))
                    {
                        if (storage.Items[i].ItemId == id)
                            return storage.Items[i];
                    }
            }
            return null;
        }

        public StorageItem GetItemBySlot(Storage storage, int slot)
        {
            lock (storage.ItemsLock)
            {
                if (!storage.Items.ContainsKey(slot))
                    return null;

                return storage.Items[slot];
            }
        }

        private bool CanDress(Player player, StorageItem item, bool sendErrors = false)
        {
            if (player.PlayerData.Class != (PlayerClass)item.ItemTemplate.Class)
            {
                if(sendErrors)
                    new SpChatMessage(player, "Can't Equip!!", ChatType.House, true).Send(player.Connection);

                return false;
            }

            if (item.ItemTemplate.Level > player.GetLevel() && item.ItemTemplate.JobLevel > player.GetJobLevel())
            {
                if (sendErrors)
                    new SpChatMessage(player, "Can't Equip!!", ChatType.House, true).Send(player.Connection);

                return false;
            }

            return true;
        }

        private bool CanStack(StorageItem item)
        {
            bool retval = true;

            switch (item.ItemTemplate.Category)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 21:
                    retval = false;
                    break;
            }

            return retval;
        }


        public int GetTotalWeight(Player player, Storage storage)
        {
            int totalWeight = 0;

            storage.Items.Values.ToList().Each(i => {
                totalWeight += i.ItemTemplate.Weight;
            });

            return totalWeight;
        }

        public void UseItem(Player player, int type, int position, long itemId, long itemCount) 
        {
            Global.Global.ScriptEngine.UseItem(player, itemId, position, itemCount);

            new SpUseItem(player, type, position, itemId, itemCount);
            Global.Global.VisibleService.Send(player, new SpItemEffect(itemId));
        }
    }
}
