using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpShoutMessage : OuterNetworkSendPacket
    {
        protected List<string> Messages = new List<string>();

        public SpShoutMessage(string msg)
        {
            Messages.Add(msg);
        }

        public SpShoutMessage(List<string> msgs)
        {
            Messages = msgs;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, Messages.Count);

            foreach(var message in Messages)
            {
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteH(writer, message.Length);
                WriteB(writer, Encoding.Default.GetBytes(message));
            }
        }
    }
}
