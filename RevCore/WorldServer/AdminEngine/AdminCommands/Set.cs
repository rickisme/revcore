using Data.Enums;
using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.AdminEngine.AdminCommands
{
    public class Set : ACommand
    {
        public override void Process(Data.Interfaces.IConnection connection, string[] msg)
        {
            try
            {
                Player player = connection.Player;

                Player target = player.Target as Player;
                if (target == null) 
                {
                    target = player;
                }

                string type = "";
                int value = 1;

                switch (msg.Length)
                {
                    case 2:
                        {
                            type = msg[0];
                            value = int.Parse(msg[1]);
                            break;
                        }
                    case 3:
                        {
                            target = Global.Global.VisibleService.FindTarget(player, msg[0]);
                            type = msg[1];
                            value = int.Parse(msg[2]);
                            break;
                        }
                }

                switch (type)
                {
                    case "level":
                        {
                            long exp = Data.Data.PlayerExperience[value];
                            Global.Global.PlayerService.SetExp(target, exp, null);
                            break;
                        }
                    case "exp":
                        {
                            Global.Global.PlayerService.SetExp(target, target.Exp + value, null);
                            break;
                        }
                    case "skillpoint":
                        {
                            target.SkillPoint = value;
                            Global.Global.PlayerService.SetExp(target, target.Exp, null);
                            break;
                        }
                    case "ability":
                        {
                            target.AbilityPoint = value;
                            new SpPlayerStats(target).Send(target);
                            break;
                        }
                    case "honor":
                        {
                            target.HonorPoint = value;
                            new SpPlayerStats(target).Send(target);
                            break;
                        }
                    case "karma":
                        {
                            target.KarmaPoint = value;
                            new SpPlayerStats(target).Send(target);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Print(connection, "Wrong syntax!");
                Print(connection, "Syntax: @set {level|exp|skillpoint|ability|honor|karma}{value}");
                Log.Warn(e.ToString());
            }
        }
    }
}
