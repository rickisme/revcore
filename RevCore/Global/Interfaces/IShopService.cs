using Data.Structures.Player;
using System.Collections.Generic;

namespace Global.Interfaces
{
    public interface IShopService : IComponent
    {
        void OpenOnlineShop(Player player, int type);
        Dictionary<int, ShopItem> GetShopItemByNpcId(int npcId);
        ShopItem GetShopItemBySlot(int npcId, int slot);
    }
}
