using Data.Enums;
using Data.Enums.SkillEngine;
using Data.Interfaces;
using DatabaseFactory;
using Global.Interfaces;
using Utilities;
using WorldServer.OuterNetwork;

namespace WorldServer.Services
{
    public class AccountService : IAccountService
    {
        public static IDFactory IdFactory = new IDFactory(0);

        public void Authorized(IConnection connection, string accountName, string password)
        {
            connection.Account = DataBaseAccount.GetAccountByName(accountName);
            connection.Account.Connection = connection;
            connection.Account.Players = DataBasePlayer.GetPlayers(connection.Account, Properties.Settings.Default.SERVER_ID);
            connection.Account.SessionID = (int)IdFactory.GetNext();

            for (int i = 0; i < connection.Account.Players.Count; i++)
            {
                connection.Account.Players[i].Inventory = DataBaseStorage.GetPlayerStorage(connection.Account.Players[i].PlayerId, StorageType.Inventory);
                connection.Account.Players[i].Abilities = DataBaseAbility.GetPlayerAbility(connection.Account.Players[i].PlayerId, SkillType.Basic);
                connection.Account.Players[i].AscensionAbilities = DataBaseAbility.GetPlayerAbility(connection.Account.Players[i].PlayerId, SkillType.Ascension);
                connection.Account.Players[i].Skills = DataBaseSkill.GetPlayerSkill(connection.Account.Players[i].PlayerId, SkillType.Basic);
                connection.Account.Players[i].PassiveSkills = DataBaseSkill.GetPlayerSkill(connection.Account.Players[i].PlayerId, SkillType.Passive);
                connection.Account.Players[i].AscensionSkills = DataBaseSkill.GetPlayerSkill(connection.Account.Players[i].PlayerId, SkillType.Ascension);
                connection.Account.Players[i].Quests = DataBaseQuest.GetPlayerQuest(connection.Account.Players[i].PlayerId);
            }

            DataBaseAccount.SaveLastIP(connection.Account.id, (connection as OuterNetworkConnection).IpAddress);
            Global.Global.FeedbackService.OnAuthorized(connection);
        }

        public void AbortExitAction(IConnection connection)
        {
            if (connection.Account.ExitAction != null)
            {
                connection.Account.ExitAction.Abort();
                connection.Account.ExitAction = null;
            }
        }

        public void Action()
        {

        }
    }
}
