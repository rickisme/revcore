using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace Data.Structures.World
{
    public class MapInstance : Statistical
    {
        public int MapId;

        public bool IsEditingMode = false;

        public IDFactory NpcUID = new IDFactory(10000);
        public IDFactory DropUID = new IDFactory(20000);

        public List<Player.Player> Players = new List<Player.Player>();
        public List<Npc.Npc> Npcs = new List<Npc.Npc>();
        public List<Item> Items = new List<Item>();

        public object CreaturesLock = new object();

        public void AddNpc(Npc.Npc npc)
        {
            npc.UID = (short)NpcUID.GetNext();
            Npcs.Add(npc);
        }

        public virtual void OnMove(Player.Player Player)
        {
            
        }

        public virtual void OnNpcKill(Player.Player killer, Npc.Npc killed)
        {
            
        }

        public virtual void Release()
        {
            try
            {
                for (int i = 0; i < Npcs.Count; i++)
                    Npcs[i].Release();

                Npcs.Clear();
            }
            catch (Exception ex)
            {
                Log.WarnException("MapInstance: Dispose", ex);
            }

            try
            {
                for (int i = 0; i < Items.Count; i++)
                    Items[i].Release();

                Items.Clear();
            }
            catch (Exception ex)
            {
                Log.WarnException("MapInstance: Dispose", ex);
            }

        }
    }
}
