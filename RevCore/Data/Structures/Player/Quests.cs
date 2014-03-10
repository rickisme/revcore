using Data.Structures.QuestEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Structures.Player
{
    public class Quests : Dictionary<int, KeyValuePair<int, int>>
    {
        public Quests()
            : base()
        {

        }

        public void AddQuest(int questid, int step)
        {
            this.Add(questid, new KeyValuePair<int, int>(questid, step));
        }

        public KeyValuePair<int, int> GetQuestByQuestId(int questid)
        {
            return this[questid];
        }
    }
}
