using API.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(CurrentUserModel currentUser);
        T Get(int id, CurrentUserModel currentUser);
        bool Add(T entity, CurrentUserModel currentUser);
        bool Delete(int id, CurrentUserModel currentUser);
        bool Update(int id, T entity, CurrentUserModel currentUser);
    }
}
