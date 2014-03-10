using Data.Structures.Player;

namespace WorldServer.OuterNetwork.Write
{
    public class SpInventoryInfo : OuterNetworkSendPacket
    {
        protected Storage Storage;

        public SpInventoryInfo(Storage s)
        {
            Storage = s;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, 1);
            WriteD(writer, 0);

            for (int i = 0; i < 66; i++)
            {
                if (Storage.Items.ContainsKey(i))
                    WriteItemInfo(writer, Storage.GetItem(i));
                else
                    WriteB(writer, new byte[88]);
            }
        }
    }
}
