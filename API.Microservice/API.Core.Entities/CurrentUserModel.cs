using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Entities
{
    public class CurrentUserModel
    {
        public string UserId { get; set; }
        public string ClientId { get; set; }
        public string Role { get; set; }
    }
}
