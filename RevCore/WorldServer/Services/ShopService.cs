using Data.Structures.Player;
using DatabaseFactory;
using Global.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.Services
{
    internal class ShopService : IShopService
    {
        public void Action()
        {
        
        }
        public Dictionary<int, ShopItem> GetShopItemByNpcId(int npcId) 
        {
            return DataBaseNpc.GetShopItemByNpcId(npcId);
        }
        public ShopItem GetShopItemBySlot(int npcId, int slot) 
        {
            return DataBaseNpc.GetShopItemByNpcId(npcId)[slot];
        }

        public void OpenOnlineShop(Player player, int type) 
        {
            switch (type) 
            {
                case 1:
                    new SpOpenOnlineShop(type).Send(player);
                    break;
                case 2: 
                    break;
            }
        }
    }
}
