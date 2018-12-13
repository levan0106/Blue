using API.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace API.Core.Common
{
    public abstract class BaseApiController:Controller
    {
        public CurrentUserModel CurrentUser
        {
            get
            {
                if (HttpContext != null)
                {
                   // HttpContext.Items.TryGetValue("ClientId", out object clientId);
                   // HttpContext.Items.TryGetValue("UserId", out object userId);
                    return new CurrentUserModel()
                    {
                        ClientId = HttpContext.User.FindFirst(ClaimTypes.Webpage).Value,
                        UserId = HttpContext.User.Identity.Name
                    };
                }
                return null;
            }
        }
        public BaseApiController()
        {
        }
    }
}
