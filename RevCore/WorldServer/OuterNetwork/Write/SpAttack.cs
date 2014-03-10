using Data.Enums;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using Data.Structures.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpAttack : OuterNetworkSendPacket
    {
        protected Creature Creature;
        protected Attack Attack;

        public SpAttack(Creature creature, Attack attack)
        {
            Creature = creature;
            Attack = attack;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteH(writer, (int)Creature.Target.UID);
            WriteH(writer, ((Creature as Player).PlayerData.Class == PlayerClass.Bowman) ? 100 : 0); // skill id
            WriteD(writer, Attack.Results[0]); // Damage 1

            WriteD(writer, Attack.Results[1]); // Damage 2
            WriteD(writer, Attack.Results[2]); // Damage 3
            WriteD(writer, Attack.Results[3]); // Damage 4
            WriteD(writer, Attack.Results[4]); // Damage 5
            WriteD(writer, Attack.Args.SkillId); // skill id

            WriteD(writer, Attack.AttackAction); // IdEffect
            WriteF(writer, Creature.Position.X);
            WriteF(writer, Creature.Position.Z);
            WriteF(writer, Creature.Position.Y);
            WriteC(writer, 0);
            WriteC(writer, (byte)Attack.Count); // The number of attacks

            WriteH(writer, 0);
            WriteD(writer, Creature.Target.LifeStats.Hp); // Finally blood
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
        }
    }
}
