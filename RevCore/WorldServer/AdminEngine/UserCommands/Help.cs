using Data.Enums;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.AdminEngine.UserCommands
{
    public class Help : ACommand
    {
        public override void Process(IConnection connection, string[] msg)
        {
            try
            {
                Print(connection, "======================================");
                Print(connection, "Help List");
                Print(connection, "======================================");
                Print(connection, " ");
                Print(connection, "======================================");
            }
            catch (Exception e)
            {
                Print(connection, "Wrong syntax!");
                Print(connection, "Syntax: @help");
                Log.Warn(e.ToString());
            }
        }
    }
}
