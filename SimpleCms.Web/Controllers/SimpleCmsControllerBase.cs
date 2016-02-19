using System;
using System.Linq;
using System.Web.Mvc;
using Abp.Dependency;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using SimpleCms.ModuleCms.Themes;
using SimpleCms.Web.ViewEngines;

namespace SimpleCms.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class SimpleCmsControllerBase : AbpController
    {
        private const string KeySession = "Theme";
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
        /// <summary>
        /// Theme resolver
        /// </summary>
        private void InsertViewEngine()
        {
            string activeThemeName;
            if (Session[KeySession] == null)
            {
                var container = IocManager.Instance;
                var instance = container.Resolve<IThemeService>().GetCurrentActiveThemeFromTenant(GetTenancyNameByUrl());
                if (instance == null)
                {
                    Session[KeySession] = "";
                    return;
                }
                activeThemeName = instance.UniqueFolderId;
                Session[KeySession] = activeThemeName;
            }
            else
            {
                activeThemeName = (string)Session[KeySession];
            }
            try
            {
                System.Web.Mvc.ViewEngines.Engines.Insert(0, new SystemThemeViewEngine(activeThemeName));
            }
            catch (Exception)
            {
                System.Web.Mvc.ViewEngines.Engines.Clear();
                System.Web.Mvc.ViewEngines.Engines.Insert(0, new SystemThemeViewEngine(activeThemeName));
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