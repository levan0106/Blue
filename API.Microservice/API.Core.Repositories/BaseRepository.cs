using API.Core.Entities;
using API.Core.Infrastructure;
using API.Core.Logging;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace API.Core.Repositories
{
    public class BaseRepository : BaseFactory
    {
        public IConfigurationRoot Configuration { get; set; }
        public BaseRepository()
        {
            try
            {
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();


                var dataBase = Configuration.GetSection("DataBase");

                base.Database = (Enums.DataBase)Enum.Parse(typeof(Enums.DataBase), dataBase["DataBaseType"].ToLower());
                base.ConnectionString = dataBase["Connection"];
            }
            catch (Exception e)
            {

                LogManager.LogError("Construct BaseRepository ", e);
            }
        }

        public bool ExecuteNoneQuery(string storeName, BaseDynamicParameters parameters)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    SqlMapper.Execute(connection, storeName, param: parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogError("Execute None Query", ex);
                return false;
            }
        }
        public IEnumerable<T> ExecuteQuery<T>(string storeName, BaseDynamicParameters parameters) where T : class
        {
            try
            {
                using (var connection = GetConnection())
                {
                    IEnumerable<T> result = SqlMapper.Query<T>(connection, storeName, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogError("Execute Query", ex);
                return new List<T>();
            }
        }
    }
    public class BaseDynamicParameters : DynamicParameters
    {
        public BaseDynamicParameters(CurrentUserModel currentUser)
        {
            if(currentUser != null)
            {
                base.Add("@i_ClientId", currentUser.ClientId);
            }
        }
    }
}
