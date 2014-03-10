using Data.Enums;
using Data.Structures.Player;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFactory
{
    public class DataBaseStorage : Database
    {
        /// <summary>
        /// Get Player storages, default is Inventory
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Storage GetPlayerStorage(int PID, StorageType type = StorageType.Inventory)
        {
            Storage storage = new Storage();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GAME_PLAYER_MONEY_GET";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_PID", PID);

                    command.Parameters.AddWithValue("@out_Money", MySqlDbType.Int64);
                    command.Parameters["@out_Money"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    storage.Money = (int)command.Parameters["@out_Money"].Value;
                }

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GAME_INVENTORY_GET";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_PID", PID);
                    command.Parameters.AddWithValue("@in_TYPE", (int)type);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            StorageItem item = new StorageItem()
                            {
                                UID = reader.GetInt64(0),
                                ItemId = reader.GetInt32(2),
                                Amount = reader.GetInt32(3),
                                Magic0 = reader.GetInt32(4),
                                Magic1 = reader.GetInt32(5),
                                Magic2 = reader.GetInt32(6),
                                Magic3 = reader.GetInt32(7),
                                Magic4 = reader.GetInt32(8),
                                BonusMagic1 = reader.GetInt32(9),
                                BonusMagic2 = reader.GetInt32(10),
                                BonusMagic3 = reader.GetInt32(11),
                                BonusMagic4 = reader.GetInt32(12),
                                BonusMagic5 = reader.GetInt32(13),
                                LimitTime = reader.GetInt32(17),
                                Upgrade = reader.GetInt32(18),
                                Quality = reader.GetInt32(19),
                                Lock = reader.GetInt32(20),
                            };

                            item.UID = Global.Global.StorageService.UidRegister(item);

                            if (reader.GetInt32(14) == 0)
                            {
                                storage.Items.Add(reader.GetInt32(15), item);
                            }
                            else
                            {
                                if (!storage.EquipItems.ContainsKey(reader.GetInt32(15)))
                                    storage.EquipItems.Add(reader.GetInt32(15), item);
                                else
                                    storage.EquipItems[reader.GetInt32(15)] = item;
                            }
                        }
                    }
                }
                connection.Close();
            }

            return storage;
        }

        /// <summary>
        /// Save Player Character Storage
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="storage"></param>
        public static void SavePlayerStorage(int PID, Storage storage)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                //using (MySqlCommand command = connection.CreateCommand())
                //{
                //    command.CommandText = "GAME_INVENTORY_CLEAR";
                //    command.CommandType = System.Data.CommandType.StoredProcedure;
                //    command.Parameters.AddWithValue("@in_PID", PID);
                //    command.ExecuteNonQuery();
                //}

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GAME_PLAYER_MONEY_UPDATE";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_PID", PID);
                    command.Parameters.AddWithValue("@in_AMOUNT", storage.Money);
                    command.ExecuteNonQuery();
                }

                foreach (var storageitem in storage.DeleteItems)
                {
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        long uid = storageitem.Key;
                        var item = storageitem.Value;
                        if (item != null)
                        { 
                            command.CommandText = "GAME_INVENTORY_DELETE";
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@in_UID", item.UID);
                            command.Parameters.AddWithValue("@in_PID", PID);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                foreach (var storageitem in storage.Items)
                {
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        int slot = storageitem.Key;
                        var item = storageitem.Value;

                        if (item != null)
                        {
                            switch (item.State)
                            {
                                case ItemState.UPDATE:
                                    command.CommandText = "GAME_INVENTORY_UPDATE";
                                    break;
                                case ItemState.NEW:
                                    command.CommandText = "GAME_INVENTORY_ADD";
                                    break;
                            }

                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@in_UID", item.UID);
                            command.Parameters.AddWithValue("@in_PID", PID);
                            command.Parameters.AddWithValue("@in_ID", item.ItemId);
                            command.Parameters.AddWithValue("@in_Amount", item.Amount);
                            command.Parameters.AddWithValue("@in_Magic0", item.Magic0);
                            command.Parameters.AddWithValue("@in_Magic1", item.Magic1);
                            command.Parameters.AddWithValue("@in_Magic2", item.Magic2);
                            command.Parameters.AddWithValue("@in_Magic3", item.Magic3);
                            command.Parameters.AddWithValue("@in_Magic4", item.Magic4);
                            command.Parameters.AddWithValue("@in_BonusMagic1", item.BonusMagic1);
                            command.Parameters.AddWithValue("@in_BonusMagic2", item.BonusMagic2);
                            command.Parameters.AddWithValue("@in_BonusMagic3", item.BonusMagic3);
                            command.Parameters.AddWithValue("@in_BonusMagic4", item.BonusMagic4);
                            command.Parameters.AddWithValue("@in_BonusMagic5", item.BonusMagic5);
                            command.Parameters.AddWithValue("@in_Equiped", 0);
                            command.Parameters.AddWithValue("@in_Slot", slot);
                            command.Parameters.AddWithValue("@in_InventoryType", storage.StorageType);
                            command.Parameters.AddWithValue("@in_LimitTime", item.LimitTime);
                            command.Parameters.AddWithValue("@in_Upgrade", item.Upgrade);
                            command.Parameters.AddWithValue("@in_Quality", item.Quality);
                            command.Parameters.AddWithValue("@in_Lock", item.Lock);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                if (storage.StorageType == StorageType.Inventory)
                {
                    foreach (var storageitem in storage.EquipItems)
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            int slot = storageitem.Key;
                            var item = storageitem.Value;

                            if (item != null)
                            {
                                switch (item.State)
                                {
                                    case ItemState.UPDATE:
                                        command.CommandText = "GAME_INVENTORY_UPDATE";
                                        break;
                                    case ItemState.NEW:
                                        command.CommandText = "GAME_INVENTORY_ADD";
                                        break;
                                }

                                command.CommandType = System.Data.CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@in_UID", item.UID);
                                command.Parameters.AddWithValue("@in_PID", PID);
                                command.Parameters.AddWithValue("@in_ID", item.ItemId);
                                command.Parameters.AddWithValue("@in_Amount", item.Amount);
                                command.Parameters.AddWithValue("@in_Magic0", item.Magic0);
                                command.Parameters.AddWithValue("@in_Magic1", item.Magic1);
                                command.Parameters.AddWithValue("@in_Magic2", item.Magic2);
                                command.Parameters.AddWithValue("@in_Magic3", item.Magic3);
                                command.Parameters.AddWithValue("@in_Magic4", item.Magic4);
                                command.Parameters.AddWithValue("@in_BonusMagic1", item.BonusMagic1);
                                command.Parameters.AddWithValue("@in_BonusMagic2", item.BonusMagic2);
                                command.Parameters.AddWithValue("@in_BonusMagic3", item.BonusMagic3);
                                command.Parameters.AddWithValue("@in_BonusMagic4", item.BonusMagic4);
                                command.Parameters.AddWithValue("@in_BonusMagic5", item.BonusMagic5);
                                command.Parameters.AddWithValue("@in_Equiped", 1);
                                command.Parameters.AddWithValue("@in_Slot", slot);
                                command.Parameters.AddWithValue("@in_InventoryType", storage.StorageType);
                                command.Parameters.AddWithValue("@in_LimitTime", item.LimitTime);
                                command.Parameters.AddWithValue("@in_Upgrade", item.Upgrade);
                                command.Parameters.AddWithValue("@in_Quality", item.Quality);
                                command.Parameters.AddWithValue("@in_Lock", item.Lock);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }

                connection.Close();
            }
        }
    }
}
