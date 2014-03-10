using Data.Enums;
using DatabaseFactory;
using Global.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Utilities;
using WorldServer.InnerNetwork;
using WorldServer.OuterNetwork;
using WorldServer.Properties;
using WorldServer.Services;

namespace WorldServer
{
    public class WorldServer : Global.Global
    {
        public static InnerNetworkClient InnerClient;

        public static List<OuterNetworkListener> OuterNetworks = new List<OuterNetworkListener>();

        public static void Main(string[] args)
        {
            Console.Title = "WorldServer";
            Console.CancelKeyPress += CancelEventHandler;

            try
            {
                RunServer();
            }
            catch (Exception ex)
            {
                Log.FatalException("Can't start server!", ex);
                return;
            }

            MainLoop();

            StopServer();

            Process.GetCurrentProcess().Kill();
        }

        private static void RunServer()
        {
            Stopwatch sw = Stopwatch.StartNew();

            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            Console.WriteLine("----===== Revolution WorldServer =====----\n\n"
                              + "Copyright (C) 2013 Revolution Team\n\n"
                              + "This program is CLOSE SOURCE project.\n"
                              + "You DON'T have any right's, if you are NOT autor\n"
                              + "or authorized representative of him.\n"
                              + "Using that program without any right's is ILLEGAL\n\n"
                              + "Authors: Jenose, IMaster\n"
                              + "Authorized representative: netgame.in.th\n\n"
                              + "-------------------------------------------");

            Log.Info("Init Services...");
            AccountService = new AccountService();
            AiService = new AiService();
            ChatService = new ChatService();
            ControllerService = new ControllerService();
            FeedbackService = new FeedbackService();
            MapService = new MapService();
            ObserverService = new ObserverService();
            PlayerService = new PlayerService();
            TeamService = new TeamService();
            SkillsLearnService = new SkillsLearnService();
            StatsService = new StatsService();
            ShopService = new ShopService();
            StorageService = new StorageService();
            TeleportService = new TeleportService();
            VisibleService = new VisibleService();

            Log.Info("Init Engines...");
            ScriptEngine = new ScriptEngine.ScriptEngine();
            AdminEngine = new AdminEngine.AdminEngine();
            SkillEngine = new SkillEngine.SkillEngine();
            QuestEngine = new QuestEngine.QuestEngine();
            Console.WriteLine("\n-------------------------------------------\n");

            GlobalLogic.ServerStart();
            Console.WriteLine("\n-------------------------------------------\n");

            CountryCode = (CountryCode)Enum.Parse(typeof(CountryCode), Settings.Default.COUNTRY_CODE);

            InnerNetworkOpcode.Init();
            OuterNetworkOpcode.Init();

            InnerClient = new InnerNetworkClient("127.0.0.1", 22323);
            InnerClient.BeginConnect();

            foreach (var channel in DataBaseServer.GetServerChannel(Settings.Default.SERVER_ID))
            {
                var OuterNetwork = new OuterNetworkListener("*", channel.port, channel.max_user);
                OuterNetwork.BeginListening();
                OuterNetworks.Add(OuterNetwork);
            }

            InnerNetworkClient.SendAllThread.Start();
            OuterNetworkConnection.SendAllThread.Start();

            sw.Stop();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("           Server start in {0}", (sw.ElapsedMilliseconds / 1000.0).ToString("0.00s"));
            Console.WriteLine("-------------------------------------------");
        }

        private static void StopServer()
        {
            //TcpServer.ShutdownServer();
            //GlobalLogic.OnServerShutdown();
        }

        protected static void UnhandledException(Object sender, UnhandledExceptionEventArgs args)
        {
            Log.FatalException("UnhandledException", (Exception)args.ExceptionObject);

            while (true)
                Thread.Sleep(1);
        }

        protected static void CancelEventHandler(object sender, ConsoleCancelEventArgs args)
        {
            while (true)
                Thread.Sleep(1);
        }
    }
}
