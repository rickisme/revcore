using Data.Enums;
using Data.Structures.Account;
using Data.Structures.Player;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFactory
{
    public class DataBasePlayer : Database
    {
        /// <summary>
        /// Check player name exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CheckNameResult CheckName(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                int result = 0;
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GAME_PLAYER_NAME_CHECK";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_name", name);

                    command.Parameters.AddWithValue("@out_result", MySqlDbType.Int32);
                    command.Parameters["@out_result"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    result = (int)command.Parameters["@out_result"].Value;
                }
                connection.Close();
                return (CheckNameResult)result;
            }
        }

        /// <summary>
        /// Save Player Character
        /// </summary>
        /// <param name="player"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public static int SavePlayer(Player player, bool create = false)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                int id = 0;
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (create)
                    {
                        command.CommandText = "GAME_PLAYER_CREATE";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@in_AccountID", player.Account.id);
                        command.Parameters.AddWithValue("@in_ServerID", player.ServerId);
                        command.Parameters.AddWithValue("@in_Name", player.PlayerData.Name);
                        command.Parameters.AddWithValue("@in_Level", player.GetLevel());
                        command.Parameters.AddWithValue("@in_Job", (int)player.PlayerData.Class);
                        command.Parameters.AddWithValue("@in_Title", player.PlayerData.Title);
                        command.Parameters.AddWithValue("@in_Map", player.Position.MapId);
                        command.Parameters.AddWithValue("@in_X", player.Position.X);
                        command.Parameters.AddWithValue("@in_Y", player.Position.Y);
                        command.Parameters.AddWithValue("@in_Z", player.Position.Z);
                        command.Parameters.AddWithValue("@in_Gender", (int)player.PlayerData.Gender);
                        command.Parameters.AddWithValue("@in_Forces", player.PlayerData.Forces);
                        command.Parameters.AddWithValue("@in_Hair", player.PlayerData.HairStyle);
                        command.Parameters.AddWithValue("@in_Color", player.PlayerData.HairColor);
                        command.Parameters.AddWithValue("@in_Face", player.PlayerData.Face);
                        command.Parameters.AddWithValue("@in_Voice", player.PlayerData.Voice);
                        command.Parameters.AddWithValue("@in_Hp", player.LifeStats.Hp);
                        command.Parameters.AddWithValue("@in_Mp", player.LifeStats.Mp);
                        command.Parameters.AddWithValue("@in_Sp", player.LifeStats.Sp);
                        command.Parameters.AddWithValue("@in_Exp", player.Exp);
                        command.Parameters.AddWithValue("@in_SkillPoint", player.SkillPoint);
                        command.Parameters.AddWithValue("@in_AbilityPoint", player.AbilityPoint);
                        command.Parameters.AddWithValue("@in_AscensionPoint", player.AscensionPoint);
                        command.Parameters.AddWithValue("@in_HonorPoint", player.HonorPoint);
                        command.Parameters.AddWithValue("@in_KarmaPoint", player.KarmaPoint);


                        command.Parameters.AddWithValue("@out_Id", MySqlDbType.Int32);
                        command.Parameters["@out_Id"].Direction = System.Data.ParameterDirection.Output;
                        command.ExecuteNonQuery();

                        id = (int)command.Parameters["@out_Id"].Value;
                    }
                    else
                    {
                        command.CommandText = "GAME_PLAYER_SAVE";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@in_Pid", player.PlayerId);
                        command.Parameters.AddWithValue("@in_Level", player.GetLevel());
                        command.Parameters.AddWithValue("@in_Job", (int)player.PlayerData.Class);
                        command.Parameters.AddWithValue("@in_Title", player.PlayerData.Title);
                        command.Parameters.AddWithValue("@in_Map", player.Position.MapId);
                        command.Parameters.AddWithValue("@in_X", player.Position.X);
                        command.Parameters.AddWithValue("@in_Y", player.Position.Y);
                        command.Parameters.AddWithValue("@in_Z", player.Position.Z);
                        command.Parameters.AddWithValue("@in_Forces", player.PlayerData.Forces);
                        command.Parameters.AddWithValue("@in_Hair", player.PlayerData.HairStyle);
                        command.Parameters.AddWithValue("@in_Color", player.PlayerData.HairColor);
                        command.Parameters.AddWithValue("@in_Face", player.PlayerData.Face);
                        command.Parameters.AddWithValue("@in_Voice", player.PlayerData.Voice);
                        command.Parameters.AddWithValue("@in_Hp", player.LifeStats.Hp);
                        command.Parameters.AddWithValue("@in_Mp", player.LifeStats.Mp);
                        command.Parameters.AddWithValue("@in_Sp", player.LifeStats.Sp);
                        command.Parameters.AddWithValue("@in_Exp", player.Exp);
                        command.Parameters.AddWithValue("@in_SkillPoint", player.SkillPoint);
                        command.Parameters.AddWithValue("@in_AbilityPoint", player.AbilityPoint);
                        command.Parameters.AddWithValue("@in_AscensionPoint", player.AscensionPoint);
                        command.Parameters.AddWithValue("@in_HonorPoint", player.HonorPoint);
                        command.Parameters.AddWithValue("@in_KarmaPoint", player.KarmaPoint);
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
                return id;
            }
        }

        /// <summary>
        /// Get Account Player Characters
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="ServerId"></param>
        /// <returns></returns>
        public static List<Player> GetPlayers(Account Account, int ServerId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                List<Player> list = new List<Player>();
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GAME_PLAYER_GET_ALL";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_AccID", Account.id);
                    command.Parameters.AddWithValue("@in_SrvID", ServerId);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Player p = new Player();
                            p.Account = Account;
                            p.PlayerId = reader.GetInt32(0);
                            p.ServerId = reader.GetInt32(2);
                            p.PlayerData.Name = reader.GetString(3);
                            p.Level = reader.GetInt32(4);
                            p.PlayerData.Class = (PlayerClass)reader.GetInt32(5);
                            p.PlayerData.Title = reader.GetInt32(6);
                            p.Position.MapId = reader.GetInt32(7);
                            p.Position.X = reader.GetFloat(8);
                            p.Position.Y = reader.GetFloat(9);
                            p.Position.Z = reader.GetFloat(10);
                            p.PlayerData.Gender = (Gender)reader.GetInt32(11);
                            p.PlayerData.Forces = reader.GetInt32(12);
                            p.PlayerData.HairStyle = reader.GetInt32(13);
                            p.PlayerData.HairColor = reader.GetInt32(14);
                            p.PlayerData.Face = reader.GetInt32(15);
                            p.PlayerData.Voice = reader.GetInt32(16);
                            p.GameStats = Global.Global.StatsService.InitStats(p);
                            p.LifeStats.Hp = reader.GetInt32(17);
                            p.LifeStats.Mp = reader.GetInt32(18);
                            p.LifeStats.Sp = reader.GetInt32(19);
                            p.Exp = reader.GetInt64(20);
                            p.SkillPoint = reader.GetInt32(21);
                            p.AbilityPoint = reader.GetInt32(22);
                            p.AscensionPoint = reader.GetInt32(23);
                            p.HonorPoint = reader.GetInt32(24);
                            p.KarmaPoint = reader.GetInt32(25);

                            list.Add(p);
                        }
                    }
                }
                connection.Close();
                return list;
            }
        }
    }
}
