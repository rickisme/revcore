using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldServer.InnerNetwork.Write;

namespace WorldServer.InnerNetwork
{
    public class InnerNetworkOpcode
    {
        public static Dictionary<short, Type> Recv = new Dictionary<short, Type>();
        public static Dictionary<Type, short> Send = new Dictionary<Type, short>();

        public static Dictionary<short, string> SendNames = new Dictionary<short, string>();

        public static void Init()
        {
            #region Recv

            #endregion


            #region Send

            Send.Add(typeof(SpRegisteredServer), unchecked((short)0x0001));

            #endregion

            SendNames = Send.ToDictionary(p => p.Value, p => p.Key.Name);
        }
    }
}
