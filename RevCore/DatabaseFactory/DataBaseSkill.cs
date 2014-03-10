using Data.Enums.SkillEngine;
using Data.Structures.Player;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFactory
{
    public class DataBaseSkill : Database
    {
        /// <summary>
        /// Get Player Skills list
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static Skills GetPlayerSkill(int playerId, SkillType skillType)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Skills skills = new Skills();


                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GAME_PLAYER_SKILL_GET";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_PlayerId", playerId);
                    command.Parameters.AddWithValue("@in_SkillType", skillType);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int skillId = reader.GetInt32(1);
                            int skillLevel = reader.GetInt32(2);

                            skills.AddSkill(skillId, skillLevel);
                        }
                    }
                }
                connection.Close();
                return skills;
            }
        }

        public static void AddPlayerSkill(Player player, int skillid, int level, SkillType skillType, int skillSlot)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GAME_PLAYER_SKILL_ADD";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_PlayerId", player.PlayerId);
                    command.Parameters.AddWithValue("@in_SkillId", skillid);
                    command.Parameters.AddWithValue("@in_SkillLevel", level);
                    command.Parameters.AddWithValue("@in_SkillType", skillType);
                    command.Parameters.AddWithValue("@in_SkillSlot", skillSlot);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// Update player skills 
        /// </summary>
        /// <param name="player"></param>
        public static void SavePlayerSkill(Player player, SkillType skillType)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Skills Skills = new Skills();
                switch (skillType)
                {
                    case SkillType.Ascension:
                        Skills = player.AscensionSkills;
                        break;
                    case SkillType.Basic:
                        Skills = player.Skills;
                        break;
                    case SkillType.Passive:
                        Skills = player.PassiveSkills;
                        break;
                }

                foreach (var skill in Skills.Values)
                {
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = "GAME_PLAYER_SKILL_UPDATE";
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@in_PlayerId", player.PlayerId);
                            command.Parameters.AddWithValue("@in_SkillId", skill.Key);
                            command.Parameters.AddWithValue("@in_SkillLevel", skill.Value);
                            command.Parameters.AddWithValue("@in_SkillType", skillType);
                            command.ExecuteNonQuery();
                        }
                        catch(Exception e)
                        {
                            
                        }
                        finally
                        {
                        
                        }
                    }
                }

                connection.Close();
            }
        }
    }
}
