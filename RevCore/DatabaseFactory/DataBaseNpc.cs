using Data.Structures.Player;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFactory
{
    public class DataBaseNpc : Database
    {
        public static Dictionary<int, ShopItem> GetShopItemByNpcId(int npcId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Dictionary<int, ShopItem> list = new Dictionary<int, ShopItem>();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GAME_NPC_SHOP_GET";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_NpcId", npcId);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ShopItem ch = new ShopItem()
                            {
                                NpcId = reader.GetInt32(1),
                                ItemId = reader.GetInt64(2),
                                ItemSlot = reader.GetInt32(3),
                                Amount = reader.GetInt32(4),
                                Money = reader.GetInt64(5),
                                Magic0 = reader.GetInt32(6),
                                Magic1 = reader.GetInt32(7),
                                Magic2 = reader.GetInt32(8),
                                Magic3 = reader.GetInt32(9),
                                Magic4 = reader.GetInt32(10)
                            };
                            list.Add(ch.ItemSlot, ch);
                        }
                    }
                    reader.Close();
                }
                connection.Close();

                return list;
            }
        }
    }
}
