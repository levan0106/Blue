using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Microservice.Authentication.Repository.Interfaces;
using API.Microservice.Authentication.Models;
using API.Core.Logging;

namespace API.Microservice.Authentication.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly IOptions<Audience> _settings;
        readonly IUserRepository _userRepository;
        public AuthenticationController(IOptions<Audience> settings, IUserRepository userRepository)
        {
            this._settings = settings;
            this._userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult GetToken(string name, string pwd, string client)
        {
            if (this._userRepository.ValidateUserInfo(name, pwd, client))
            {
                return DoGenerateToken(name, client);
            }
            else
            {
                LogManager.LogDebug("Login failed !");
                LogManager.LogDebug(string.Format("UserName/Password: {0}/{1}", name, pwd));
                return ValidateFail();
            }
        }
        [HttpGet]
        public IActionResult RefreshToken(string token, string refreshToken)
        {
            if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(refreshToken))
            {
                return ValidateFail();
            }
            try
            {
                ClaimsPrincipal principal = TokenManager.GetPrincipalFromExpiredToken(_settings, token);
                string userName = principal.Identity.Name;
                string clientId = principal.FindFirstValue(ClaimTypes.Webpage);

                //Validate refresh token
                if (_userRepository.ValidateRefreshToken(userName, refreshToken, clientId))
                {
                    return DoGenerateToken(userName, clientId);
                }
                else
                {
                    LogManager.LogDebug(string.Format("Client/Token/RefreshToken: {0}/{1}/{2}", clientId, token, refreshToken));
                    return ValidateFail();
                }
            }
            catch (Exception e)
            {
                LogManager.LogDebug("Token/RefreshToken:", e);
                return ValidateFail();
            }
        }

        private IActionResult ValidateFail()
        {
            var responseJson = new
            {
                access_token = "no token",
                refresh_token = "no token",
                expires_in = (int)TimeSpan.FromMinutes(0).TotalSeconds,
                status = 401,
                statusText = "fail"
            };
            return Json(responseJson);
        }
         private IActionResult DoGenerateToken(string userName, string clientId)
        {
            string newRefreshToken = TokenManager.GenerateRefreshToken();// generate new refresh token
            string accessToken = TokenManager.GenerateToken(_settings, userName, clientId); // generate new token
            var responseJson = new
            {
                access_token = accessToken,
                refresh_token = newRefreshToken,
                expires_in = (int)TimeSpan.FromMinutes(1).TotalSeconds,
                status= 200,
                statusText="ok"
            };

            //Save refresh token to validate after
            _userRepository.UpdateRefreshToken(userName, newRefreshToken, clientId, accessToken);

            return Json(responseJson);;
        }
    }
}