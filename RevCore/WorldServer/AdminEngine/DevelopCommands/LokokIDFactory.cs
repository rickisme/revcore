using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace WorldServer.AdminEngine.DevelopCommands
{
    public class LokokIDFactory : ACommand
    {
        public override void Process(IConnection connection, string[] msg)
        {
            try
            {
                
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
