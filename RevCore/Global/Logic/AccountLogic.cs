using Data.Interfaces;
using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace Global.Logic
{
    public class AccountLogic : Global
    {
        public const int LogoutTimeout = 0;

        public static void ClientDisconnected(IConnection connection)
        {
            if (connection.Account != null && connection.Player != null)
            {
                Player player = connection.Player;
                new DelayedAction(() => PlayerLogic.PlayerEndGame(player), LogoutTimeout * 1000);
            }
        }

        public static void ExitPlayer(IConnection connection)
        {
            AccountService.AbortExitAction(connection);

            connection.Account.ExitAction = new DelayedAction(
                () =>
                {
                    FeedbackService.Exit(connection);
                    PlayerLogic.PlayerEndGame(connection.Player);
                }, LogoutTimeout * 1000);
        }

        public static void LogoutPlayer(IConnection connection)
        {
            connection.Account.ExitAction = new DelayedAction(
                () =>
                {
                    FeedbackService.Logout(connection);
                    PlayerLogic.PlayerEndGame(connection.Player);
                }, LogoutTimeout * 1000);
        }
    }
}
