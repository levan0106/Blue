using API.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Microservice.Account.Repository.Interfaces
{
    public interface IRepositoryBase<T>: IRepository<T> where T:class
    {
    }
}
