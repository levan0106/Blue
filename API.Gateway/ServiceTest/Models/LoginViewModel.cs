using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceTest.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        [Display(Name = "Client")]
        public int ClientId { get; set; }
    }
}
