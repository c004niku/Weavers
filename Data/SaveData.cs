using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Data
{
    public static class SaveData
    {
        public static int Save(string sqlQuery)
        {
            int userId = 0;
            using (var connection = ConnectionManager.Inst.CreateNewConnection())
            {
                connection.Open();

                var command = new MySqlCommand(sqlQuery);
                command.Connection = connection;

                userId = command.ExecuteNonQuery();

                connection.Close();
            }

            return userId;
        }


        public static int SaveAndGetId(string sqlQuery)
        {
            object userId = null;
            using (var connection = ConnectionManager.Inst.CreateNewConnection())
            {
                connection.Open();

                var command = new MySqlCommand(sqlQuery);
                command.Connection = connection;

                userId = command.ExecuteScalar();

                connection.Close();
            }

            return Convert.ToInt32(userId);
        }

    }
}