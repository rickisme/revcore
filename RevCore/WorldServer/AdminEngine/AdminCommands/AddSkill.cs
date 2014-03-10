using Data.Interfaces;
using Data.Structures.Player;
using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace WorldServer.AdminEngine.AdminCommands
{
    public class AddSkill: ACommand
    {
        public override void Process(IConnection connection, string[] msg)
        {
            try
            {
                Player player = connection.Player;
                int skill_id = int.Parse(msg[0]);

                PlayerLogic.PlayerLearnSkill(player, skill_id);
            }
            catch (Exception e)
            {
                Print(connection, "Wrong syntax!");
                Print(connection, "Syntax: @addskill {skill_id}");
                Log.Warn(e.ToString());
            }
        }

    }
}
