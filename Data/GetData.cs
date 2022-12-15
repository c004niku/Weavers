using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Weavers.Data
{

public static class GetData
    {
        public static DataSet SelectData(string sqlQuery)
        {
            DataSet dataset = new DataSet();
            using (var connection = ConnectionManager.Inst.CreateNewConnection())
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();

                connection.Open();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(sqlQuery, connection))
                {
                    DataSet result = new DataSet();
                    adapter.Fill(result);
                    return result;
                }

            }
        }

        public static object IsRecordExisting(string sqlQuery)
        {
            var obj = new object();
            using (var connection = ConnectionManager.Inst.CreateNewConnection())
            {
                connection.Open();

                var command = new MySqlCommand(sqlQuery);
                command.Connection = connection;

                obj = command.ExecuteScalar();


                connection.Close();
            }

            return obj;
        }

        public static void DeleteData(string sqlQuery)
        {
            using (var connection = ConnectionManager.Inst.CreateNewConnection())
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                connection.Open();

                var command = new MySqlCommand(sqlQuery);
                command.Connection = connection;

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }

}