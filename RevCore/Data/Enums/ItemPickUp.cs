using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Enums
{
    public enum ItemPickUp
    {
        GotMoney = 1,
        MaxWeight = 2,
        TooFarFromItem = 3,
        ItemDisappear = 4,
        NoPremiumPermision = 5,
        MaxMoney = 6,
        InventoryIsFull = 7,
        AtCafeOnly = 8, // Thai client ipbonus permision
        PickItemCanceled = 9,
        Impossible = 10,
        HerbPickFailed = 11,
        MaxItemStack = 12,
    }
}
