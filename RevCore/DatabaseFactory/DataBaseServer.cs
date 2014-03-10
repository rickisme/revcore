using Data.Structures.Template.Servers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFactory
{
    public class DataBaseServer : Database
    {
        /// <summary>
        /// Get Server Info by ID
        /// </summary>
        /// <param name="svId"></param>
        /// <returns></returns>
        public static Server GetServerInfo(int svId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Server srv = new Server();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SRV_SERVER_INFO_GET";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@svId", svId);
                    var reader = command.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            srv.id = reader.GetInt32(0);
                            srv.name = reader.GetString(1);
                            srv.ip = reader.GetString(2);
                        }
                    }
                    reader.Close();
                }
                connection.Close();

                return srv;
            }
        }

        /// <summary>
        /// Get Server Channel info by server id
        /// </summary>
        /// <param name="svId"></param>
        /// <returns></returns>
        public static List<Channel> GetServerChannel(int svId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                List<Channel> list = new List<Channel>();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SRV_CHANNEL_INFO_GET";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@worldId", svId);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Channel ch = new Channel()
                            {
                                wid = reader.GetInt32(0),
                                id = reader.GetInt32(1),
                                name = reader.GetString(2),
                                port = reader.GetInt32(3),
                                max_user = reader.GetInt32(4),
                                type = reader.GetInt32(5)
                            };
                            list.Add(ch);
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
