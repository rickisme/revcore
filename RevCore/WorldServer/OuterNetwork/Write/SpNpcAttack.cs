using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using Data.Structures.World;
using System.IO;

namespace WorldServer.OuterNetwork.Write
{
    public class SpNpcAttack : OuterNetworkSendPacket
    {
        protected Npc Npc;
        protected Attack Attack;

        public SpNpcAttack(Npc npc, Attack attack)
        {
            Npc = npc;
            Attack = attack;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteH(writer, (int)(Npc.Target as Player).UID);
            WriteH(writer, 0); // skill id
            WriteD(writer, Attack.Results[0]); // Damage 1

            WriteD(writer, Attack.Results[1]); // Damage 2
            WriteD(writer, Attack.Results[2]); // Damage 3
            WriteD(writer, Attack.Results[3]); // Damage 4
            WriteD(writer, Attack.Results[4]); // Damage 5
            WriteD(writer, 0); // skill id

            WriteD(writer, Attack.AttackAction); // FLD_EFFERT
            WriteF(writer, Npc.Position.X);
            WriteF(writer, Npc.Position.Z);
            WriteF(writer, Npc.Position.Y);
            WriteC(writer, 4);
            WriteC(writer, (byte)Attack.Count); // The number of attacks

            WriteH(writer, 0);
            WriteD(writer, Npc.Target.LifeStats.Hp); // Finally blood
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);

            writer.Seek(2, SeekOrigin.Begin);
            WriteH(writer, (int)Npc.UID);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
