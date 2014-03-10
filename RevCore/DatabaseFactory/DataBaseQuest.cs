using Data.Enums.SkillEngine;
using Data.Structures.Player;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFactory
{
    public class DataBaseQuest : Database
    {
        /// <summary>
        /// Get Player Quests list
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static Quests GetPlayerQuest(int playerId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Quests Quests = new Quests();


                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GAME_QUEST_GET";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_PlayerId", playerId);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int questId = reader.GetInt32(1);
                            int questStep = reader.GetInt32(2);

                            Quests.AddQuest(questId, questStep);
                        }
                    }
                }
                connection.Close();
                return Quests;
            }
        }

        /// <summary>
        /// Add or Update player Quests 
        /// </summary>
        /// <param name="player"></param>
        public static void SavePlayerQuest(Player player, bool update = true)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Quests Quests = new Quests();

                if (!update)
                {
                    foreach (var Quest in Quests.Values)
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "GAME_QUEST_ADD";
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@in_PlayerId", player.PlayerId);
                            command.Parameters.AddWithValue("@in_QuestId", Quest.Key);
                            command.Parameters.AddWithValue("@in_QuestStep", Quest.Value);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    foreach (var ability in Quests.Values)
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "GAME_QUEST_UPDATE";
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@in_PlayerId", player.PlayerId);
                            command.Parameters.AddWithValue("@in_QuestId", ability.Key);
                            command.Parameters.AddWithValue("@in_QuestStep", ability.Value);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
            }
        }
    }
}
