using Data.Interfaces;
using Data.Structures.Npc;
using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace WorldServer.AdminEngine.UserCommands
{
    public class Info : ACommand
    {
        public override void Process(IConnection connection, string[] msg)
        {
            try
            {
                Player player = connection.Player;
                Print(connection, "======================================");
                Print(connection, "Info List");
                Print(connection, "======================================");
                Print(connection, "Name: " + player.PlayerData.Name);
                Print(connection, "======================================");
                Print(connection, "Target Info List");
                Print(connection, "======================================");
                if (player.Target is Npc)
                {
                    Npc npc = player.Target as Npc;
                    Print(connection, "Target Name: " + npc.NpcTemplate.Name);
                    Print(connection, "Target Level: " + npc.GetLevel());
                    Print(connection, "Target MaxHp: " + npc.MaxHp);
                    Print(connection, "Target MaxMp: " + npc.MaxMp);
                }

                if (player.Target is Player)
                {
                    Player target = player.Target as Player;
                    Print(connection, "Target Name: " + target.PlayerData.Name);
                    Print(connection, "Target Level: " + target.GetLevel());
                    Print(connection, "Target MaxHp: " + target.MaxHp);
                    Print(connection, "Target MaxMp: " + target.MaxMp);
                }

                Print(connection, "======================================");
            }
            catch (Exception e)
            {
                Print(connection, "Wrong syntax!");
                Print(connection, "Syntax: @info");
                Log.Warn(e.ToString());
            }
        }
    }
}
