using Data.Structures.Account;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFactory
{
    public class DataBaseAccount : Database
    {
        /// <summary>
        /// Get User Account
        /// </summary>
        /// <param name="name"></param>
        /// <param name="passwd"></param>
        /// <returns></returns>
        public static Account GetAccount(string name, string passwd)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Account account = new Account();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AUTH_ACCOUNT_GET";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_name", name);
                    command.Parameters.AddWithValue("@in_passwd", passwd);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            account.id = reader.GetInt32(0);
                            account.name = reader.GetString(1);
                            account.passwd = reader.GetString(2);
                            account.isgm = reader.GetInt32(3);
                            account.activated = reader.GetInt32(4);
                            account.membership = reader.GetInt32(5);
                            account.last_ip = reader.GetString(6);
                            account.cash = reader.GetInt32(7);
                        }
                    }
                    else
                    {
                        //account = NewAccount(name, passwd);
                    }
                    reader.Close();
                }
                connection.Close();

                return account;
            }
        }

        /// <summary>
        /// Create new Account
        /// </summary>
        /// <param name="name"></param>
        /// <param name="passwd"></param>
        /// <returns></returns>
        public static Account NewAccount(string name, string passwd)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Account account = new Account();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AUTH_ACCOUNT_NEW";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_name", name);
                    command.Parameters.AddWithValue("@in_passwd", passwd);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            account.id = reader.GetInt32(0);
                            account.name = reader.GetString(1);
                            account.passwd = reader.GetString(2);
                            account.isgm = reader.GetInt32(3);
                            account.activated = reader.GetInt32(4);
                            account.membership = reader.GetInt32(5);
                            account.last_ip = reader.GetString(6);
                            account.cash = reader.GetInt32(7);
                        }
                    }
                    reader.Close();
                }
                connection.Close();

                return account;
            }
        }

        /// <summary>
        /// Get User Account info by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Account GetAccountByName(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Account account = new Account();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AUTH_ACCOUNT_BYNAME_GET";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_name", name);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            account.id = reader.GetInt32(0);
                            account.name = reader.GetString(1);
                            account.passwd = reader.GetString(2);
                            account.isgm = reader.GetInt32(3);
                            account.activated = reader.GetInt32(4);
                            account.membership = reader.GetInt32(5);
                            account.last_ip = reader.GetString(6);
                            account.cash = reader.GetInt32(7);
                        }
                    }
                    reader.Close();
                }
                connection.Close();

                return account;
            }
        }

        /// <summary>
        /// Save Last User Connected IP
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ip"></param>
        public static void SaveLastIP(int uid, string ip)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AUTH_ACCOUNT_LASTIP_UPDATE";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@in_uid", uid);
                    command.Parameters.AddWithValue("@in_ipaddress", ip);

                    command.Parameters.AddWithValue("@out_success", MySqlDbType.Int32);
                    command.Parameters["@out_success"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    //bool success = (bool)command.Parameters["@out_success"].Value;
                }
                connection.Close();
            }
        }
    }
}
