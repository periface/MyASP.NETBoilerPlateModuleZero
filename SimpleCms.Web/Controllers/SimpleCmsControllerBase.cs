using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using SimpleCms.Web.ViewEngines;

namespace SimpleCms.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class SimpleCmsControllerBase : AbpController
    {
        /// <summary>
        /// Gets the active tenancy, usefull for anon users
        /// </summary>
        public string ActiveTenantName => GetTenancyNameByUrl();

        protected SimpleCmsControllerBase()
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

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            InsertViewEngine();
        }

        private  void InsertViewEngine()
        {
            try
            {

                System.Web.Mvc.ViewEngines.Engines.Insert(0, new SystemThemeViewEngine(GetTenancyNameByUrl()));
            }
            catch (Exception)
            {
                System.Web.Mvc.ViewEngines.Engines.Clear();
                System.Web.Mvc.ViewEngines.Engines.Insert(0, new SystemThemeViewEngine(GetTenancyNameByUrl()));
            }
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