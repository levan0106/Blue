using API.Core.Repository.Interfaces;
using System;

namespace API.Microservice.Image.Repository.Interfaces
{
    public interface IRepositoryBase<T> : IRepository<T> where T : class
    {
    }
}
