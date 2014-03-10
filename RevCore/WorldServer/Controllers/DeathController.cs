using Data.Interfaces;
using Data.Structures.Player;
using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace WorldServer.Controllers
{
    class DeathController : IController
    {
        public Player Player;

        public void Start(Player player)
        {
            Player = player;
            Player.Target = null;
            Player.LifeStats.Kill();
            PlayerLogic.PleyerDied(player);
        }

        public void Release()
        {
            Player.LifeStats.Rebirth();
            Player = null;
        }

        public void Action()
        {

        }
    }
}
