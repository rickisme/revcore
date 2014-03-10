using Data.Enums;
using Data.Enums.SkillEngine;
using Data.Structures.Account;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using Data.Structures.Template.Servers;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Utilities;

namespace DatabaseFactory
{
    public class Database
    {
        protected static string connectionString = Properties.Settings.Default.DB_CONSTR;
    }
}
