using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace API.Core.Infrastructure
{
    public class BaseFactory
    {
        private static string _connectionString;
        protected string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        private static Enums.DataBase _database;
        protected Enums.DataBase Database
        {
            get { return _database; }
            set { _database = value; }
        }
        protected IDbConnection GetConnection()
        {
            DatabaseConnectionFactory.DataBase = Database;
            DatabaseConnectionFactory.ConnectionString = ConnectionString ?? "Data Source=localhost;Initial Catalog=Blue.Account;Integrated Security=True";
            return DatabaseConnectionFactory.GetConnection();
        }

        //public bool DoExecuteNoneQuery(string storeName, DynamicParameters parameters)
        //{
        //    try
        //    {
        //        using (var connection = GetConnection())
        //        {
        //            SqlMapper.Execute(connection, storeName, param: parameters, commandType: CommandType.StoredProcedure);
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //        throw ex;
        //    }
        //}
        //public IEnumerable<T> DoExecuteQuery<T>(string storeName, DynamicParameters parameters) where T : class
        //{
        //    try
        //    {
        //        using (var connection = GetConnection())
        //        {
        //            IEnumerable<T> result = SqlMapper.Query<T>(connection, storeName, parameters, commandType: CommandType.StoredProcedure);
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //        throw ex;
        //    }
        //}
    }
}
