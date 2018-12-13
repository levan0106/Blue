using API.Core.Entities;
using System;

namespace API.Microservice.Authentication.Models
{
    public class UserModel:BaseModel
    {
        public int UserRecId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
