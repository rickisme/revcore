using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Global.Interfaces
{
    public interface ITeamService : IComponent
    {
        void Init();

        void Find(Player player, int type, int map_id, int level);
    }
}
