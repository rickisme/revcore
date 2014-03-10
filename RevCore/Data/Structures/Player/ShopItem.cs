using Data.Structures.Template;
using Data.Structures.Template.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Structures.Player
{
    public class ShopItem
    {
        public int NpcId = 0;
        public int ItemSlot = 0;
        public long ItemId = 0;
        public int Amount = 0;
        public long Money = 0;

        public int Magic0 = 0;
        public int Magic1 = 0;
        public int Magic2 = 0;
        public int Magic3 = 0;
        public int Magic4 = 0;

        public int MagicNum 
        {
            get 
            {
                int num = 0;
                if (Magic0 != 0)
                {
                    num++;
                }

                if (Magic1 != 0)
                {
                    num++;
                }

                if (Magic2 != 0)
                {
                    num++;
                }

                if (Magic3 != 0)
                {
                    num++;
                }

                if (Magic4 != 0)
                {
                    num++;
                }
                return num;
            }
        }

        public ItemTemplate ItemTemplate
        {
            get { return ItemTemplate.Factory(ItemId); }
        }

        public NpcTemplate NpcTemplate
        {
            get { return NpcTemplate.Factory(NpcId); }
        }
    }
}
