using Data.Structures.Player;
using Global.Logic;

namespace WorldServer.OuterNetwork.Read
{
    public class RpSettingOption : OuterNetworkRecvPacket
    {
        protected Settings Setting = new Settings();

        public override void Read()
        {
            Setting.PartyFriend = ReadC();
            Setting.CanTrade = ReadC();
            Setting.CanWhisper = ReadC();
            ReadC();
            Setting.CostumeType = ReadC();
            ReadC();
            Setting.HairSwitch = ReadC();
            Setting.FameSwitch = ReadC();
            Setting.PartySearch = ReadC();
            ReadC();
            Setting.WeaponSwitch = ReadC();
            ReadC();
            Setting.PetExp = ReadH();
        }

        public override void Process()
        {
            //PlayerLogic.OptionSetting(Connection.Player, Setting);
        }
    }
}
