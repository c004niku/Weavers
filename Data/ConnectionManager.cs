using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Data
{
    public class ConnectionManager
    {
        private static ConnectionManager inst;
        internal MySqlConnection CreateNewConnection()
        {
            System.Configuration.Configuration rootWebConfig =
                                      System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            System.Configuration.ConnectionStringSettings connString;
            if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                connString =
                    rootWebConfig.ConnectionStrings.ConnectionStrings["DefaultConnection"];
                if (connString != null)
                {
                    return new MySqlConnection(connString.ConnectionString);
                }
                else
                {
                    return new MySqlConnection(null);
                }

            }
            return new MySqlConnection(null);

        }
        public static ConnectionManager Inst
        {
            get
            {
                if (inst == null)
                    inst = new ConnectionManager();
                return inst;
            }
        }
        public ConnectionManager()
        {
            CreateNewConnection();
        }
    }
}