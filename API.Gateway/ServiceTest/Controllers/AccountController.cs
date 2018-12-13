using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceTest.Models;

namespace ServiceTest.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var jwt = GetJwt(model);
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Append("token", jwt.AccessToken, option);
                Response.Cookies.Append("refresh-token", jwt.RefreshToken, option);

                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        [HttpGet]
        public IActionResult RefreshToken(string token, string refreshToken)
        {
            var jwt = RefreshJwt(token, refreshToken);
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append("token", jwt.AccessToken, option);
            Response.Cookies.Append("refresh-token", jwt.RefreshToken, option);

            if (jwt != null)
            {
                return new ObjectResult(new
                {
                    token = jwt.AccessToken,
                    refreshToken = jwt.RefreshToken,
                    status = 200,
                    statusText = "ok"
                });
            }
            else
            {
                return new ObjectResult(new
                {
                    status = 401,
                    statusText = "Refresh fail"
                });
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                Username = "tung",
                Password = "123",
                ClientId = 88
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            Response.Cookies.Delete("refresh-token");
            return RedirectToAction("Index", "Home");
        }
        private static JWT GetJwt(LoginViewModel user)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:3000");
            client.DefaultRequestHeaders.Clear();

            var res2 = client.GetAsync($"/api/auth?name={user.Username}&pwd={user.Password}&client={user.ClientId}").Result;

            dynamic jwt = JsonConvert.DeserializeObject(res2.Content.ReadAsStringAsync().Result);

            return new JWT { AccessToken = jwt.access_token, RefreshToken = jwt.refresh_token };
        }
        private static JWT RefreshJwt(string token, string refreshToken)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:3000");
            client.DefaultRequestHeaders.Clear();

            var res2 = client.GetAsync($"/api/auth/refresh?token={token}&refreshtoken={refreshToken}").Result;

            dynamic jwt = JsonConvert.DeserializeObject(res2.Content.ReadAsStringAsync().Result);

            return new JWT { AccessToken = jwt.access_token, RefreshToken = jwt.refresh_token };
        }
    }
    public class JWT
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}