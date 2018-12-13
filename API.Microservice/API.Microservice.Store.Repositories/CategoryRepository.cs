using API.Core.Entities;
using API.Core.Logging;
using API.Microservice.Store.Entities;
using API.Microservice.Store.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.Microservice.Store.Repositories
{
    public class CategoryRepository : RepositoryBase, ICategoryRepository
    {
        public bool Add(CategoryModel entity, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Name", entity.Name);
                parameters.Add("@i_Description", entity.Description);
                parameters.Add("@i_Image", entity.Image);
                parameters.Add("@i_Location", entity.LocationId);

                ExecuteNoneQuery("Add_Category", parameters);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Add Category: ", ex);
                throw ex;
            }
        }

        public bool Delete(int id, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Id", id);
                ExecuteNoneQuery("Delete_Category", parameters);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Category delete by id", ex);
                return false;
                throw;
            }
        }

        public IEnumerable<CategoryModel> Get(CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                IEnumerable<CategoryModel> result = ExecuteQuery<CategoryModel>("Get_Category", parameters);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Category get all", ex);
                throw;
            }
        }

        public CategoryModel Get(int id, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Id", id);
                CategoryModel result = ExecuteQuery<CategoryModel>("Get_CategoryById", parameters).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Category get by id", ex);
                throw;
            }
        }

        public bool Update(int id, CategoryModel entity, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Id", id);
                parameters.Add("@i_Name", entity.Name);
                parameters.Add("@i_Description", entity.Description);
                parameters.Add("@i_Image", entity.Image);
                parameters.Add("@i_Location", entity.LocationId);

                ExecuteNoneQuery("Update_Category", parameters);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Update Category: ", ex);
                throw ex;
            }
        }
    }
}
