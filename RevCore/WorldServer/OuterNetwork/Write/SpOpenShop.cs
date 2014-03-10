using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpOpenShop : OuterNetworkSendPacket
    {
        protected int dialogId;
        protected int type;
        protected int npcId;

        public SpOpenShop(int dialogId, int type, int npcId)
        {
            this.dialogId = dialogId;
            this.type = type;
            this.npcId = npcId;
        }

        /*
         * AA55
         * 2700
         * 012C01
         * 9100
         * 1800
         * 0180
         * 0000
         * 01000000
         * 01000000
         * 00000000
         * 0000000000000000
         * 0000000000000000
         * 55AA
         */
        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, dialogId);
            WriteD(writer, dialogId);
            WriteD(writer, type);

            var shopItems = Global.Global.ShopService.GetShopItemByNpcId(npcId);

            WriteD(writer, shopItems.Count);//Count
            WriteD(writer, 0);
            WriteQ(writer, 0);

            foreach(ShopItem shopItem in shopItems.Values)
            {
                WriteQ(writer, shopItem.ItemId);
                if (shopItem.MagicNum > 0)
                {
                    WriteQ(writer, shopItem.MagicNum);
                    if (shopItem.Magic1 > 0)
                    {
                        WriteQ(writer, shopItem.Magic1);
                    }
                    if (shopItem.Magic2 > 0)
                    {
                        WriteQ(writer, shopItem.Magic2);
                    }
                    if (shopItem.Magic3 > 0)
                    {
                        WriteQ(writer, shopItem.Magic3);
                    }
                    if (shopItem.Magic4 > 0)
                    {
                        WriteQ(writer, shopItem.Magic4);
                    }
                }
                else 
                {
                    WriteQ(writer, 0);
                }
                WriteQ(writer, -1);
            }
        }
    }
}
