using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Structures.Template;

namespace Data.Structures.Npc
{
    public class Npc : Creature.Creature
    {
        public int NpcId;

        public SpawnTemplate SpawnTemplate;

        public NpcTemplate NpcTemplate;

        public Npc ParentNpc;
        public List<Npc> Childs = new List<Npc>();

        public List<Npc> NamesList;

        public override long UID
        {
            get
            {
                return base.UID;
            }
            set
            {
                base.UID = value;
            }
        }

        public override int GetLevel()
        {
            return NpcTemplate.Level;
        }

        public override void Release()
        {
            Instance.NpcUID.Release(UID);
        }
    }
}
