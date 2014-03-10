using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpOpenOnlineShop : OuterNetworkSendPacket
    {
        protected int type;

        public SpOpenOnlineShop(int type) 
        {
            this.type = type;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            switch (type)
            {
                case 1:
                    {
                        WriteC(writer, type);
                        WriteH(writer, Properties.Settings.Default.SERVER_ID);
                        WriteH(writer, Properties.Settings.Default.SERVER_ID);
                        WriteS(writer, Properties.Settings.Default.SHOP_URL);
                        break;
                    }
                case 2:
                    {
                        WriteC(writer, type);
                        break;
                    }
            }
        }
    }
}
