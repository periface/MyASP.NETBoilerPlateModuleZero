using System;
using System.Linq;
using System.Web.Mvc;
using Abp.Dependency;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Threading;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using SimpleCms.ModuleCms.Themes;
using SimpleCms.ModuleZero.Tenancy;
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
        
        private readonly IThemeService _themeService;
        private readonly ITenancyService _tenancyService;
        protected SimpleCmsControllerBase()
        {
            var iocManager = IocManager.Instance;
            _themeService = iocManager.Resolve<IThemeService>();
            _tenancyService= iocManager.Resolve<ITenancyService>();
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
            //If is host there is not need of obtain the current theme
            if (IsHostSite) return;
            string activeThemeName;
            if (Session[KeySession] == null)
            {
                var instance = _themeService.GetCurrentActiveThemeFromTenant(GetTenancyNameByUrl());
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
            if (!string.IsNullOrEmpty(activeThemeName))
            {
                System.Web.Mvc.ViewEngines.Engines.Insert(0, new SystemThemeViewEngine(activeThemeName));

            }
        }
        /// <summary>
        /// Gets the current tenant name based on the current url
        /// <para>Test in production.</para>
        /// <para>Not available in AdminController since tenant info can be obtained from te AbpSession</para>
        /// </summary>
        /// <returns></returns>
        private string GetTenancyNameByUrl()
        {
            if (Request.Url == null) return string.Empty;
            var tenancyName = Request.Url.AbsoluteUri.Split(".").First();
            tenancyName = tenancyName.Split("//").Last();
            return tenancyName;
        }
        /// <summary>
        /// Detects if the site url belongs to a tenant website
        /// <para>Not available in AdminController since tenantId can be obtained from te AbpSession</para>
        /// </summary>
        public bool IsHostSite
        {
            get
            {
                var instance =AsyncHelper.RunSync(()=>_tenancyService.GetTenantByName(GetTenancyNameByUrl()));
                return instance == null;
            }
        }
        /// <summary>
        /// Gets the current active tenantId
        /// <para>Not available in AdminController since tenantId can be obtained from te AbpSession</para>
        /// </summary>
        public int? ActiveTenantId
        {
            get
            {
                var instance = AsyncHelper.RunSync(() => _tenancyService.GetTenantByName(GetTenancyNameByUrl()));
                return instance;
            }
        }
    }
}