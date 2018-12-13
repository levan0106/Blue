using API.Core.Repository.Interfaces;
using System;

namespace API.Microservice.Store.Repository.Interfaces
{
    public interface IRepositoryBase<T> : IRepository<T> where T : class
    {
    }
}
