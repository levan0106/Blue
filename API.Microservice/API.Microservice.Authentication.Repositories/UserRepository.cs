using API.Core.Logging;
using API.Microservice.Authentication.Models;
using API.Microservice.Authentication.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Microservice.Authentication.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void UpdateRefreshToken(string userName, string refreshToken, string clientId, string token=null)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters();
                parameters.Add("@i_ClientId", clientId);
                parameters.Add("@i_UserId", userName);
                parameters.Add("@i_Token", token);
                parameters.Add("@i_RefreshToken", refreshToken);
                ExecuteNoneQuery("Insert_RefreshToken", parameters);
            }
            catch (Exception e)
            {
                LogManager.LogError("Update Refresh Token", e);
            }
        }

        public bool ValidateRefreshToken(string userName, string refreshToken, string clientId, string token = null)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters();
                parameters.Add("@i_ClientId", clientId);
                parameters.Add("@i_UserId", userName);
                parameters.Add("@i_Token", token);
                parameters.Add("@i_RefreshToken", refreshToken);
                UserModel result = ExecuteQuery<UserModel>("Validate_RefreshToken", parameters).FirstOrDefault();
                return result != null;
            }
            catch (Exception e)
            {
                LogManager.LogError("Validate Refresh Token", e);
                return false;
            }
        }

        public bool ValidateUserInfo(string userName, string userPassword, string clientId)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters();
                parameters.Add("@i_ClientId", clientId);
                parameters.Add("@i_UserId", userName);
                parameters.Add("@i_UserPassword", userPassword);
                UserModel result = ExecuteQuery<UserModel>("Validate_UserInfo", parameters).FirstOrDefault();
                return result != null;
            }
            catch (Exception e)
            {
                LogManager.LogError("Validate User Info", e);
                return false;
            }
        }
    }
}
