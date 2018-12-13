﻿using System;
using API.Core.Entities;

namespace API.Microservice.Account.Models
{
    public class UserModel: BaseModel
    {
        public int UserRecId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public string UserEmail { get; set; }
        public string FaceBookUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string PersonalWebUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
