using Data.Enums;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.AdminEngine.DevelopCommands
{
    public class FakeSendPacket : ACommand
    {
        public override void Process(IConnection connection, string[] msg)
        {
            try
            {
                short opcode = BitConverter.ToInt16(msg[0].ToBytes(), 0);
                new SpTest(opcode, msg[1]).Send(connection);
            }
            catch (Exception e)
            {
                Print(connection, "Wrong syntax!");
                Print(connection, "Syntax: @fsc {hexdata}");
                Log.Warn(e.ToString());
            }
        }
    }
}
