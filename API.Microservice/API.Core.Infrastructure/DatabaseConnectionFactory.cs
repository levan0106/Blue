using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace API.Core.Infrastructure
{
    public class DatabaseConnectionFactory: IDisposable
    {
        public static string ConnectionString;
        public static Enums.DataBase DataBase;
        public static IDbConnection GetConnection()
        {
            if (DataBase == Enums.DataBase.mysql)
            {
                var connection = new MySqlConnection(ConnectionString);
                if (!connection.Ping())
                {
                    connection.Open();
                }
                return connection;
            }
            else
            {
                var connection = new SqlConnection(ConnectionString);
                connection.Open();
                return connection;
            }
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
