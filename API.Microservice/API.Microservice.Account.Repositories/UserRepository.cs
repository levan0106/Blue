using API.Core.Entities;
using API.Core.Logging;
using API.Microservice.Account.Models;
using API.Microservice.Account.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace API.Microservice.Account.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public bool Add(UserModel entity, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_UserId", entity.UserId);
                parameters.Add("@i_UserName", entity.UserName);
                parameters.Add("@i_UserMobile", entity.UserMobile);
                parameters.Add("@i_UserEmail", entity.UserEmail);
                parameters.Add("@i_FaceBookUrl", entity.FaceBookUrl);
                parameters.Add("@i_LinkedInUrl", entity.LinkedInUrl);
                parameters.Add("@i_TwitterUrl", entity.TwitterUrl);
                parameters.Add("@i_PersonalWebUrl", entity.PersonalWebUrl);

                ExecuteNoneQuery("Add_User", parameters);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Add User: ", ex);
                throw ex;
            }
        }

        public bool Delete(int id, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_UserRecId", id);
                ExecuteNoneQuery("Delete_User", parameters);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogError("User delete by id", ex);
                return false;
                throw;
            }
        }

        public IEnumerable<UserModel> Get(CurrentUserModel currentUser)
        {
            CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
            return ExecuteQuery<UserModel>("Get_User", parameters).ToList();

        }

        public UserModel Get(int id, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_UserRecId", id);
                UserModel result = ExecuteQuery<UserModel>("Get_UserById", parameters).FirstOrDefault();
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool Update(int id, UserModel entity, CurrentUserModel currentUser)
        {
            throw new NotImplementedException();
        }
    }
}
