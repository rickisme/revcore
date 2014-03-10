using Data.Interfaces;
using Global.Interfaces;
using System;
using System.Collections.Generic;
using Utilities;
using WorldServer.AdminEngine.AdminCommands;
using WorldServer.AdminEngine.DevelopCommands;
using WorldServer.AdminEngine.UserCommands;

namespace WorldServer.AdminEngine
{
    public class AdminEngine : IAdminEngine
    {
        public Dictionary<string, ACommand> DevelopCommandList = new Dictionary<string, ACommand>();
        public Dictionary<string, ACommand> AdminCommandList = new Dictionary<string, ACommand>();
        public Dictionary<string, ACommand> UserCommandList = new Dictionary<string, ACommand>();

        public Dictionary<IConnection, WaitMessageHandle> WaitValueHandles = new Dictionary<IConnection, WaitMessageHandle>();

        public AdminEngine()
        {
            DevelopCommandList.Add("fsc", new FakeSendPacket());

            AdminCommandList.Add("additem", new AddItem());
            AdminCommandList.Add("addskill", new AddSkill());
            AdminCommandList.Add("goto", new Goto());
            AdminCommandList.Add("kill", new KillTarget());
            AdminCommandList.Add("removeitem", new RemoveItem());
            AdminCommandList.Add("set", new Set());

            UserCommandList.Add("help", new Help());
            UserCommandList.Add("info", new Info());
        }

        public bool ProcessChatMessage(Data.Interfaces.IConnection connection, string message)
        {
            if (WaitValueHandles.ContainsKey(connection))
            {
                WaitMessageHandle handle = WaitValueHandles[connection];
                handle.SendValue(message);
                return true;
            }

            if (message.Length < 2)
                return false;

            switch (message[0]) 
            {
                case '@':
                    {
                        if (IsDev(connection.Player))
                        {
                            string cmd = message.Substring(1).Split(' ')[0];
                            if (DevelopCommandList.ContainsKey(cmd))
                            {
                                try
                                {
                                    DevelopCommandList[cmd].Process(connection, message.Substring(cmd.Length + 1).Trim().Split(' '));
                                }
                                catch (Exception ex)
                                {
                                    Log.WarnException("DevelopCommand: Process:", ex);
                                }
                                return true;
                            }

                            if (AdminCommandList.ContainsKey(cmd))
                            {
                                try
                                {
                                    AdminCommandList[cmd].Process(connection, message.Substring(cmd.Length + 1).Trim().Split(' '));
                                }
                                catch (Exception ex)
                                {
                                    Log.WarnException("AdminCommand: Process:", ex);
                                }
                                return true;
                            }
                        }

                        if (IsGM(connection.Player))
                        {
                            string cmd = message.Substring(1).Split(' ')[0];
                            if (AdminCommandList.ContainsKey(cmd))
                            {
                                try
                                {
                                    AdminCommandList[cmd].Process(connection, message.Substring(cmd.Length + 1).Trim().Split(' '));
                                }
                                catch (Exception ex)
                                {
                                    Log.WarnException("AdminCommand: Process:", ex);
                                }
                                return true;
                            }
                        }
                        break;
                    }
                case '!':
                case '.':
                    {
                        string cmd = message.Substring(1).Split(' ')[0];
                        if (UserCommandList.ContainsKey(cmd))
                        {
                            try
                            {
                                UserCommandList[cmd].Process(connection, message.Substring(cmd.Length + 1).Trim().Split(' '));
                            }
                            catch (Exception ex)
                            {
                                Log.WarnException("UserCommand: Process:", ex);
                            }
                            return true;
                        }
                        break;
                    }
            }

            return false;
        }

        public bool IsGM(Data.Structures.Player.Player player)
        {
            return Properties.Settings.Default.GM_LIST.Contains(player.Account.name);
        }

        public bool IsDev(Data.Structures.Player.Player player)
        {
            return Properties.Settings.Default.DEV_LIST.Contains(player.Account.name);
        }

        public void Action()
        {
            
        }
    }
}
