using API.Microservice.Authentication.Models;
using System;
using System.Collections.Generic;

namespace API.Microservice.Authentication.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<UserModel>
    {
        bool ValidateUserInfo(string userName, string userPassword, string clientId);
        bool ValidateRefreshToken(string userName, string refreshToken, string clientId, string token = null);
        void UpdateRefreshToken(string userName, string refreshToken, string clientId, string token = null);
    }
}
