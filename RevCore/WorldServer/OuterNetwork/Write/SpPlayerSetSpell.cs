using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerSetSpell : OuterNetworkSendPacket
    {
        protected int SpellId;
        protected byte Command;
        protected byte SpellLevel;

        public SpPlayerSetSpell(int spellId, byte cmd, byte spelllvl)
        {
            SpellId = spellId;
            Command = cmd;
            SpellLevel = spelllvl;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, SpellId);
            WriteC(writer, Command);
            WriteC(writer, SpellLevel);
            WriteH(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
        }
    }
}
