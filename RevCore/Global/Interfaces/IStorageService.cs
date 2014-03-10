using Data.Enums;
using Data.Structures.Player;
using System.Collections.Generic;

namespace Global.Interfaces
{
    public interface IStorageService : IComponent
    {
        void AddStartItemsToPlayer(Player player);
        void ShowPlayerStorage(Player player, StorageType storageType, bool shadowUpdate = true, int offset = 0);
        bool AddItem(Player player, Storage storage, int itemId, int itemCounter, int slot = -1);
        bool AddItem(Player player, Storage storage, StorageItem item);
        bool RemoveItem(Player player, Storage storage, int slot, int counter);
        bool RemoveItemById(Player player, Storage storage, int itemId, int counter);
        void ReplaceItem(Player player, Storage storage, MoveItemArgs Args, bool showStorage = true);
        List<int> GetFreeSlots(Storage storage);
        bool AddMoneys(Player player, Storage storage, long counter);
        bool RemoveMoney(Player player, Storage storage, long counter);
        bool ContainsItem(Storage storage, int itemId, long counter);
        StorageItem GetItemById(Storage storage, int id);
        StorageItem GetItemBySlot(Storage storage, int slot);
        int GetTotalWeight(Player player, Storage storage);
        long UidRegister(StorageItem Item);
        void UseItem(Player player, int Type, int Position, long ItemId, long ItemCount);
    }
}
