using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace WorldServer.OuterNetwork.Read
{
    public class RpLevelUpAbility : OuterNetworkRecvPacket
    {
        protected Dictionary<int, KeyValuePair<int, int>> ReadAbility = new Dictionary<int, KeyValuePair<int, int>>();

        protected int AbilityId;

        protected int AbilityPoint;

        public override void Read()
        {
            /*for (int i = 0; i < 14; i++)
            {
                int id = ReadH();
                int lvl = ReadH();

                ReadAbility.Add(id, new KeyValuePair<int, int>(id, lvl));
            }*/

            ReadB(60);

            AbilityId = ReadH();
            AbilityPoint = ReadD();
        }

        public override void Process()
        {
            PlayerLogic.LevelUpAbility(Connection.Player, ReadAbility, AbilityId, AbilityPoint);
        }
    }
}
