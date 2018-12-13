using API.Core.Entities;
using API.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Microservice.Image.Repositories
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
