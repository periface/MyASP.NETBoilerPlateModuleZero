using System.ComponentModel.DataAnnotations;

namespace SimpleCms.Web.Models.Account
{
    public class LoginFormViewModel
    {
        public string ReturnUrl { get; set; }
        public bool IsMultiTenancyEnabled { get; set; }
    }
}