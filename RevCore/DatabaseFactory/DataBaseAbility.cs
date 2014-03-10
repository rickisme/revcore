using Data.Enums.SkillEngine;
using Data.Structures.Player;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFactory
{
    public class DataBaseAbility : Database
    {
        /// <summary>
        /// Get Player Abilities list
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static Abilities GetPlayerAbility(int playerId, SkillType skillType)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Abilities abilities = new Abilities();


                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GAME_PLAYER_ABILITY_GET";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_PlayerId", playerId);
                    command.Parameters.AddWithValue("@in_AbilityType", skillType);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int abilityId = reader.GetInt32(1);
                            int abilityLevel = reader.GetInt32(2);

                            abilities.AddAbility(abilityId, abilityLevel);
                        }
                    }
                }
                connection.Close();
                return abilities;
            }
        }

        /// <summary>
        /// Add or Update player abilities 
        /// </summary>
        /// <param name="player"></param>
        public static void SavePlayerAbility(Player player, SkillType skillType, bool update = true)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Abilities Abilities = new Abilities();
                switch (skillType)
                {
                    case SkillType.Ascension:
                        Abilities = player.AscensionAbilities;
                        break;
                    case SkillType.Basic:
                        Abilities = player.Abilities;
                        break;
                }

                if (!update)
                {
                    foreach (var ability in Abilities.Values)
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "GAME_PLAYER_ABILITY_ADD";
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@in_PlayerId", player.PlayerId);
                            command.Parameters.AddWithValue("@in_AbilityId", ability.Key);
                            command.Parameters.AddWithValue("@in_AbilityLevel", ability.Value);
                            command.Parameters.AddWithValue("@in_AbilityType", skillType);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    foreach (var ability in Abilities.Values)
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "GAME_PLAYER_ABILITY_UPDATE";
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@in_PlayerId", player.PlayerId);
                            command.Parameters.AddWithValue("@in_AbilityId", ability.Key);
                            command.Parameters.AddWithValue("@in_AbilityLevel", ability.Value);
                            command.Parameters.AddWithValue("@in_AbilityType", skillType);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
            }
        }
    }
}
