using System.Collections.Generic;
using System.Linq;

namespace Data.Structures.Player
{
    public class Abilities : Dictionary<int, KeyValuePair<int, int>>
    {
        protected int slot = 0;

        public Abilities()
            : base()
        {
        }

        public void AddAbility(int id, int level)
        {
            this.Add(slot, new KeyValuePair<int, int>(id, level));
            slot++;
        }

        public bool LevelUpAbility(int id)
        {
            var remover = this
                .Where(kvp => kvp.Value.Key == id)
                .ToList();

            int level = remover
                .Select(v => v.Value.Value)
                .FirstOrDefault();

            if (level == 20)
                return false;

            int removeSlot = remover
                .Select(v => v.Key).FirstOrDefault();

            this.Remove(removeSlot);
            this.Add(removeSlot, new KeyValuePair<int, int>(id, level + 1));

            return true;
        }

        public KeyValuePair<int, int> GetAbilityBySlotId(int slot)
        {
            return this[slot];
        }
    }
}
