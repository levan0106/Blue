using API.Core.Entities;
using API.Microservice.Image.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Microservice.Image.Repository.Interfaces
{
    public interface IImageRepository:IRepositoryBase<ImageModel>
    {
        IEnumerable<ImageModel> GetByCategory(string type, CurrentUserModel currentUser);
        IEnumerable<ImageModel> GetByCategoryId(int id, string type, CurrentUserModel currentUser);
    }
}
