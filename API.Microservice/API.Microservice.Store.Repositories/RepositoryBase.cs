using System;
using System.Collections.Generic;
using System.Text;
using API.Core.Entities;
using API.Core.Repositories;

namespace API.Microservice.Store.Repositories
{
    public class RepositoryBase : BaseRepository
    {
    }
    public class CustomDynamicParameters : BaseDynamicParameters
    {
        public CustomDynamicParameters(CurrentUserModel currentUser) : base(currentUser)
        {
        }
    }
}
