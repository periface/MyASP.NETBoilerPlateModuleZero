using System.Linq;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;

namespace SimpleCms.Web.Areas
{
    /// <summary>
    /// Admin controller doesnt have the theme resolver, thats only for frontend controllers
    /// </summary>
    public class AdminController : AbpController
    {
        public string ActiveTenantName => GetTenancyNameByUrl();
        protected AdminController()
        {
            LocalizationSourceName = SimpleCmsConsts.LocalizationSourceName;
        }
        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }
        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        private string GetTenancyNameByUrl()
        {
            if (Request.Url == null) return string.Empty;
            var tenancyName = Request.Url.AbsoluteUri.Split(".").First();
            tenancyName = tenancyName.Split("//").Last();
            return tenancyName;
        }
    }
}