using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentServer.OuterNetwork.Read;
using AgentServer.OuterNetwork.Write;

namespace AgentServer.OuterNetwork
{
    public class OuterNetworkOpcode
    {
        public static Dictionary<short, Type> Recv = new Dictionary<short, Type>();
        public static Dictionary<Type, short> Send = new Dictionary<Type, short>();

        public static Dictionary<short, string> SendNames = new Dictionary<short, string>();

        public static void Init()
        {
            #region Recv

            Recv.Add(unchecked((short)0x8000), typeof(RpAuth));
            Recv.Add(unchecked((short)0x8016), typeof(RpServerList));
            Recv.Add(unchecked((short)0x800C), typeof(RpSelectSrv));

            #endregion


            #region Send

            Send.Add(typeof(SpAuth), unchecked((short)0x8001));
            Send.Add(typeof(SpServerList), unchecked((short)0x8017));
            Send.Add(typeof(SpSelectSrv), unchecked((short)0x8064));

            #endregion

            SendNames = Send.ToDictionary(p => p.Value, p => p.Key.Name);
        }
    }
}
