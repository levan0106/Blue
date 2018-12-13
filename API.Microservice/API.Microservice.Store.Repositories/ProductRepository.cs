using API.Microservice.Store.Repository.Interfaces;
using API.Microservice.Store.Entities;
using System;
using System.Collections.Generic;
using API.Core.Logging;
using System.Linq;
using API.Core.Entities;

namespace API.Microservice.Store.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public bool Add(ProductModel entity, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Name", entity.Name);
                parameters.Add("@i_Description", entity.Description);
                parameters.Add("@i_Image", entity.Image);
                parameters.Add("@i_Price", entity.Price);
                parameters.Add("@i_Deposit", entity.Deposit);
                parameters.Add("@i_AvailableFromDate", entity.AvailableFromDate);
                parameters.Add("@i_AvailableToDate", entity.AvailableToDate);
                parameters.Add("@i_Location", entity.LocationId);
                parameters.Add("@i_Shipping", entity.Shipping);
                parameters.Add("@i_Address", entity.Address);

                ExecuteNoneQuery("Add_Product", parameters);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Add Product: ", ex);
                throw ex;
            }
        }

        public bool Delete(int id, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Id", id);
                ExecuteNoneQuery("Delete_Product", parameters);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Product delete by id", ex);
                return false;
                throw;
            }
        }

        public IEnumerable<ProductModel> Get(CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                IEnumerable<ProductModel> result = ExecuteQuery<ProductModel>("Get_Product", parameters);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Product get all", ex);
                throw;
            }
        }

        public ProductModel Get(int id, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Id", id);
                ProductModel result = ExecuteQuery<ProductModel>("Get_ProductById", parameters).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Product get by id", ex);
                throw;
            }
        }

        public bool Update(int id, ProductModel entity, CurrentUserModel currentUser)
        {
            try
            {
                CustomDynamicParameters parameters = new CustomDynamicParameters(currentUser);
                parameters.Add("@i_Id", id);
                parameters.Add("@i_Name", entity.Name);
                parameters.Add("@i_Description", entity.Description);
                parameters.Add("@i_Image", entity.Image);
                parameters.Add("@i_Price", entity.Price);
                parameters.Add("@i_Deposit", entity.Deposit);
                parameters.Add("@i_AvailableFromDate", entity.AvailableFromDate);
                parameters.Add("@i_AvailableToDate", entity.AvailableToDate);
                parameters.Add("@i_Location", entity.LocationId);
                parameters.Add("@i_Shipping", entity.Shipping);
                parameters.Add("@i_Address", entity.Address);

                ExecuteNoneQuery("Update_Product", parameters);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogError("Update Product: ", ex);
                throw ex;
            }
        }
    }
}
