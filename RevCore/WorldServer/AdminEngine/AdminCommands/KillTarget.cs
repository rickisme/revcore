using Data.Enums;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Player;
using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.AdminEngine.AdminCommands
{
    class KillTarget : ACommand
    {
        public override void Process(IConnection connection, string[] msg)
        {
            try
            {
                Player player = connection.Player;
                Creature creature = player.Target;
                if (creature != null) 
                {
                    creature.LifeStats.MinusHp(creature.MaxHp + 1);

                    AiLogic.OnAttack(player, creature);
                    AiLogic.OnAttacked(creature, player, creature.MaxHp + 1);
                }
            }
            catch (Exception e)
            {
                Print(connection, "Wrong syntax!");
                Print(connection, "Syntax: @kill");
                Log.Warn(e.ToString());
            }
        }
    }
}
