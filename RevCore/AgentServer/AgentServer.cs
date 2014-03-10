using AgentServer.InnerNetwork;
using AgentServer.OuterNetwork;
using AgentServer.Services;
using Data.Structures.Template.Servers;
using Global.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Utilities;

namespace AgentServer
{
    internal class AgentServer
    {
        public static IAccountService AccountService;

        public static InnerNetworkListener InnerNetwork;

        public static OuterNetworkListener OuterNetwork;

        public static List<Server> SvrListInfo = new List<Server>();

        public static void Main(string[] args)
        {
            Console.Title = "AgentServer";
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

            MainAgentServerLoop();

            StopServer();

            Process.GetCurrentProcess().Kill();
        }

        private static void RunServer()
        {
            Stopwatch sw = Stopwatch.StartNew();

            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            Console.WriteLine("----===== Revolution AgentServer =====----\n\n"
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
            Console.WriteLine("\n-------------------------------------------\n");

            InnerNetworkOpcode.Init();
            OuterNetworkOpcode.Init();

            InnerNetwork = new InnerNetworkListener("*", 22323, 20);
            InnerNetwork.BeginListening();

            OuterNetwork = new OuterNetworkListener("*", 16000, 1000);
            OuterNetwork.BeginListening();

            InnerNetworkConnection.SendAllThread.Start();
            OuterNetworkConnection.SendAllThread.Start();

            sw.Stop();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("           Server start in {0}", (sw.ElapsedMilliseconds / 1000.0).ToString("0.00s"));
            Console.WriteLine("-------------------------------------------");
        }

        protected static void MainAgentServerLoop()
        {
            while (true)
            {
                string cmd = Console.ReadLine();

                Thread.Sleep(10);
            }
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
