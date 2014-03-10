using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Structures.Player;
using Data.Enums;
using Global.Logic;

namespace WorldServer.OuterNetwork.Read
{
    public class RpCreatePlayer : OuterNetworkRecvPacket
    {
        protected PlayerData PlayerData;

        public override void Read()
        {
            PlayerData = new PlayerData();
            PlayerData.Name = Encoding.Default.GetString(ReadB(15)).Replace("\0", "");
            ReadC(); // UNK
            PlayerData.Class = (PlayerClass)ReadC();
            PlayerData.HairStyle = ReadC();
            PlayerData.HairColor = ReadC();
            PlayerData.Face = ReadC();
            PlayerData.Voice = ReadC();
            ReadC(); // UNK
            PlayerData.Gender = (Gender)ReadC();
            ReadC(); // UNK
            ReadD(); // UNK
        }

        public override void Process()
        {
            PlayerLogic.CreateCharacter(Connection, PlayerData);
        }
    }
}
