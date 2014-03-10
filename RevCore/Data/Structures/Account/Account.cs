using Data.Interfaces;
using System.Collections.Generic;
using Utilities;

namespace Data.Structures.Account
{
    public class Account
    {
        public IConnection Connection;
        public int SessionID { get; set; }

        public int id { get; set; }
        public string name { get; set; }
        public string passwd { get; set; }
        public int isgm { get; set; }
        public int activated { get; set; }
        public int membership { get; set; }
        public string last_ip { get; set; }
        public int cash { get; set; }

        public List<Player.Player> Players = new List<Player.Player>();

        public DelayedAction ExitAction;
    }
}
