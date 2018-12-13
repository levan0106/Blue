using API.Core.Entities;
using API.Core.Logging;
using API.Core.Repositories;
using API.Microservice.Image.Models;
using API.Microservice.Image.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Microservice.Image.Repositories
{
    public class ImageRepository : BaseRepository, IImageRepository
    {
        public bool Add(ImageModel entity, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);

                ExecuteNoneQuery("Add_Image", parameters);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Add Image: ", ex);
                throw ex;
            }
        }

        public bool Delete(int id, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Id", id);
                ExecuteNoneQuery("Delete_Image", parameters);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Image delete by id", ex);
                return false;
                throw;
            }
        }

        public IEnumerable<ImageModel> Get(CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                IEnumerable<ImageModel> result = ExecuteQuery<ImageModel>("Get_Image", parameters);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Image get all", ex);
                throw;
            }
        }

        public ImageModel Get(int id, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Id", id);
                ImageModel result = ExecuteQuery<ImageModel>("Get_ImageById", parameters).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Image get by id", ex);
                throw;
            }
        }

        public bool Update(int id, ImageModel entity, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Id", id);

                ExecuteNoneQuery("Update_Image", parameters);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Update Image: ", ex);
                throw ex;
            }
        }
        public IEnumerable<ImageModel> GetByCategory(string type, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Category", type);
                IEnumerable<ImageModel> result = ExecuteQuery<ImageModel>("Get_ImageByCategory", parameters);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Get all images by category", ex);
                throw;
            }
        }
        public IEnumerable<ImageModel> GetByCategoryId(int id, string type, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Id", id);
                parameters.Add("@i_Category", type);
                IEnumerable<ImageModel> result = ExecuteQuery<ImageModel>("Get_ImageByCategoryId", parameters);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Get all images by category id", ex);
                throw;
            }
        }
    }
}
