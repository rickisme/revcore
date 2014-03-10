using System;
using System.Collections.Generic;
using System.Linq;
using AgentServer.InnerNetwork.Read;

namespace AgentServer.InnerNetwork
{
    public class InnerNetworkOpcode
    {
        public static Dictionary<short, Type> Recv = new Dictionary<short, Type>();
        public static Dictionary<Type, short> Send = new Dictionary<Type, short>();

        public static Dictionary<short, string> SendNames = new Dictionary<short, string>();

        public static void Init()
        {
            #region Recv
            Recv.Add(unchecked((short)0x0001), typeof(RpRegisteredServer));
            #endregion


            #region Send
            
            #endregion

            SendNames = Send.ToDictionary(p => p.Value, p => p.Key.Name);
        }
    }
}
