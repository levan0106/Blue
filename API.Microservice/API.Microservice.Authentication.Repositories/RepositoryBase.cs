using API.Core.Entities;
using API.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Microservice.Authentication.Repositories
{
    public class RepositoryBase: BaseRepository
    {
    }
    public class CustomDynamicParameters : BaseDynamicParameters
    {
        public CustomDynamicParameters() :base(null)
        {
        }
    }
}
