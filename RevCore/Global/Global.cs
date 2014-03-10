using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Utilities;
using Global.Interfaces;
using Data.Enums;

namespace Global
{
    public class Global
    {
        //Services:

        public static IAccountService AccountService;

        public static IAiService AiService;

        public static IChatService ChatService;

        public static IControllerService ControllerService;

        public static IFeedbackService FeedbackService;

        public static IMapService MapService;

        public static IObserverService ObserverService;

        public static IPlayerService PlayerService;

        public static ITeamService TeamService;

        public static ISkillsLearnService SkillsLearnService;

        public static IStatsService StatsService;

        public static IShopService ShopService;

        public static IStorageService StorageService;

        public static ITeleportService TeleportService;

        public static IVisibleService VisibleService;

        //Engines:
        public static IScriptEngine ScriptEngine;

        public static IAdminEngine AdminEngine;

        public static ISkillEngine SkillEngine;

        public static IQuestEngine QuestEngine;

        //

        protected static bool ShutdownIsStart = false;

        protected static bool ServerIsWork = true;

        protected static Thread MapServiceLoopThread;

        public static CountryCode CountryCode;

        public static void InitMainLoop()
        {
            MapServiceLoopThread = new Thread(MapServiceLoop);
            MapServiceLoopThread.Start();
        }

        public static void ShutdownServer()
        {
            if (ShutdownIsStart)
                return;

            ShutdownIsStart = true;

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("          Starting shootdown hook");
            Console.WriteLine("-------------------------------------------");

            //FeedbackService.ShowShutdownTicks();

            ServerIsWork = false;
        }

        protected static void MainLoop()
        {
            while (ServerIsWork)
            {
                try
                {
                    //Services:

                    AccountService.Action();
                    AiService.Action();
                    ChatService.Action();
                    ControllerService.Action();
                    FeedbackService.Action();
                    ObserverService.Action();
                    PlayerService.Action();
                    SkillsLearnService.Action();
                    StatsService.Action();
                    StorageService.Action();
                    TeleportService.Action();
                    VisibleService.Action();

                    //Engines:

                    AdminEngine.Action();
                    SkillEngine.Action();

                    //Others:

                    DelayedAction.CheckActions();
                }
                catch (Exception ex)
                {
                    Log.ErrorException("MainLoop:", ex);
                }

                Thread.Sleep(10);
            }
        }

        protected static void MapServiceLoop()
        {
            while (true)
            {
                try
                {
                    MapService.Action();
                }
                catch (Exception ex)
                {
                    Log.ErrorException("MapServiceLoop:", ex);
                }

                Thread.Sleep(1);
            }
        }
    }
}
