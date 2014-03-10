using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Global.Interfaces
{
    public interface IScriptEngine : IComponent
    {
        void Init();

        void SendWelComeMessage(Player player);

        void UseItem(Player player, long itemId, int position, long itemCount);
    }
}
