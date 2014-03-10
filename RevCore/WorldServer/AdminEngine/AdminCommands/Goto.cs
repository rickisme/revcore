using Data.Enums;
using Data.Structures.Player;
using Data.Structures.World;
using System;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.AdminEngine.AdminCommands
{
    public class Goto : ACommand
    {
        public override void Process(Data.Interfaces.IConnection connection, string[] msg)
        {
            try
            {
                Player player = connection.Player;
                int mapId = int.Parse(msg[0]);

                WorldPosition Position = Data.Data.BindPoints[mapId][0];
                Global.Global.TeleportService.ForceTeleport(player, Position);
            }
            catch (Exception e)
            {
                Alert(connection, "Wrong syntax!");
                Alert(connection, "Syntax: @goto {map_id}");
                Log.Warn(e.ToString());
            }
        }
    }
}
