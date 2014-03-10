using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using WorldServer.OuterNetwork.Read;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.OuterNetwork
{
    public class OuterNetworkOpcode
    {
        public static Dictionary<short, Type> Recv = new Dictionary<short, Type>();
        public static Dictionary<Type, short> Send = new Dictionary<Type, short>();

        public static Dictionary<short, string> RecvNames = new Dictionary<short, string>();
        public static Dictionary<short, string> SendNames = new Dictionary<short, string>();

        public static void Init()
        {
            #region Recv
            Recv.Add(unchecked((short)0x0001), typeof(RpAuth));
            Recv.Add(unchecked((short)0x0003), typeof(RpExitGame));
            Recv.Add(unchecked((short)0x0005), typeof(RpEnterGame));
            Recv.Add(unchecked((short)0x0007), typeof(RpMove));
            Recv.Add(unchecked((short)0x0008), typeof(RpChatMessage));
            Recv.Add(unchecked((short)0x0009), typeof(RpAttack));
            Recv.Add(unchecked((short)0x000B), typeof(RpItemPickUp));
            Recv.Add(unchecked((short)0x000E), typeof(RpItemDelete));
            Recv.Add(unchecked((short)0x0010), typeof(RpPlayerList));
            Recv.Add(unchecked((short)0x0014), typeof(RpCreatePlayer));
            Recv.Add(unchecked((short)0x0016), typeof(RpSettingOption));
            Recv.Add(unchecked((short)0x001A), typeof(RpInventoryMove));
            Recv.Add(unchecked((short)0x0024), typeof(RpPlayerAction));
            // 30 zudui
            Recv.Add(unchecked((short)0x0038), typeof(RpCheckName));
            Recv.Add(unchecked((short)0x003A), typeof(RpUseItem));
            Recv.Add(unchecked((short)0x003C), typeof(RpUseSkill));
            Recv.Add(unchecked((short)0x0043), typeof(RpLevelUpAbility));
            Recv.Add(unchecked((short)0x0045), typeof(RpLearnSkill));
            Recv.Add(unchecked((short)0x0048), typeof(RpPlayerDiedDialog));
            Recv.Add(unchecked((short)0x0056), typeof(RpPlayerLogout));
            Recv.Add(unchecked((short)0x008F), typeof(RpUNK008F));
            Recv.Add(unchecked((short)0x0090), typeof(RpOpenDialog));
            //97 jiaoyi
            Recv.Add(unchecked((short)0x00B0), typeof(RpUNK00B0));
            // b1 00 | 01 00 00
            Recv.Add(unchecked((short)0x00D4), typeof(RpOpenOnlineShop));
            Recv.Add(unchecked((short)0x1088), typeof(RpTargetSelected));
            Recv.Add(unchecked((short)0x1638), typeof(RpUNK1638));
            Recv.Add(unchecked((short)0x1720), typeof(RpPremiumRevive));
            // 1779 gaobai
            Recv.Add(unchecked((short)0x1800), typeof(RpFindTeammate));
            #endregion


            #region Send
            Send.Add(typeof(SpAuth), unchecked((short)0x0002));
            Send.Add(typeof(SpExit), unchecked((short)0x0004));
            Send.Add(typeof(SpPlayerRunning), unchecked((short)0x0006));
            Send.Add(typeof(SpAttack), unchecked((short)0x000A));
            Send.Add(typeof(SpNpcAttack), unchecked((short)0x000C));
            Send.Add(typeof(SpItemPickupMsg), unchecked((short)0x000D));
            Send.Add(typeof(SpItemDelete), unchecked((short)0x000F));
            Send.Add(typeof(SpPlayerList), unchecked((short)0x0011));
            Send.Add(typeof(SpCreatePlayer), unchecked((short)0x0015));
            Send.Add(typeof(SpInventoryMove), unchecked((short)0x001B));
            Send.Add(typeof(SpUNK0020), unchecked((short)0x0020));
            Send.Add(typeof(SpPlayerAction), unchecked((short)0x0024));
            Send.Add(typeof(SpCheckName), unchecked((short)0x0039));
            Send.Add(typeof(SpUseItem), unchecked((short)0x003B));
            Send.Add(typeof(SpPlayerSetSpell), unchecked((short)0x003D));
            Send.Add(typeof(SpLearnSkillMessage), unchecked((short)0x0046));
            Send.Add(typeof(SpPlayerLogout), unchecked((short)0x0057));
            Send.Add(typeof(SpPlayerRemove), unchecked((short)0x0063));
            Send.Add(typeof(SpPlayerInfo), unchecked((short)0x0064));
            Send.Add(typeof(SpPlayerMove), unchecked((short)0x0065));
            Send.Add(typeof(SpChatMessage), unchecked((short)0x0066));
            Send.Add(typeof(SpNpcSpawn), unchecked((short)0x0067));
            Send.Add(typeof(SpNpcDespawn), unchecked((short)0x0068));
            Send.Add(typeof(SpPlayerHpMpSp), unchecked((short)0x0069));
            Send.Add(typeof(SpPlayerExpAndPointUp), unchecked((short)0x006A));
            Send.Add(typeof(SpPlayerStats), unchecked((short)0x006B));
            Send.Add(typeof(SpPlayerSkillNormal), unchecked((short)0x006C));
            Send.Add(typeof(SpInventoryInfo), unchecked((short)0x0071));
            Send.Add(typeof(SpDropInfo), unchecked((short)0x0072));
            Send.Add(typeof(SpDropRemove), unchecked((short)0x0073));
            Send.Add(typeof(SpNpcMove), unchecked((short)0x0074));
            Send.Add(typeof(SpEquipInfo), unchecked((short)0x0076));
            Send.Add(typeof(SpPlayerLevelUp), unchecked((short)0x0077));
            Send.Add(typeof(SpSetLocation), unchecked((short)0x0079));
            Send.Add(typeof(SpWeightMoney), unchecked((short)0x007C));
            Send.Add(typeof(SpItemEffect), unchecked((short)0x007F));
            Send.Add(typeof(SpServerTime), unchecked((short)0x0080));
            Send.Add(typeof(SpInventoryQuest), unchecked((short)0x0081));
            Send.Add(typeof(SpQuestList), unchecked((short)0x0085));
            Send.Add(typeof(SpCreatureDied), unchecked((short)0x0088));
            Send.Add(typeof(SpPlayerSkillEffect), unchecked((short)0x0089));
            Send.Add(typeof(SpOpenShop), unchecked((short)0x0091));
            Send.Add(typeof(SpPlayerQuickInfo), unchecked((short)0x00A0));
            Send.Add(typeof(SpShoutMessage), unchecked((short)0x00CA));
            Send.Add(typeof(SpOpenOnlineShop), unchecked((short)0x00D5));
            Send.Add(typeof(SpBindPoint), unchecked((short)0x100B));
            Send.Add(typeof(SpPlayerDPoint), unchecked((short)0x1059));
            Send.Add(typeof(SpUNK1639), unchecked((short)0x1639));
            Send.Add(typeof(SpPremiumRevive), unchecked((short)0x1721));
            Send.Add(typeof(SpCraftList), unchecked((short)0x1733));
            #endregion

            RecvNames = Recv.ToDictionary(p => p.Key, p => p.Value.Name);
            SendNames = Send.ToDictionary(p => p.Value, p => p.Key.Name);
        }
    }
}
