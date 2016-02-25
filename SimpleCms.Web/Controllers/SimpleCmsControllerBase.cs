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
        private const string LastTenant = "LastTenant";
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
            //Init theme
            InsertViewEngine();
        }
        /// <summary>
        /// Theme resolver
        /// </summary>
        private void InsertViewEngine()
        {
            ClearViewEngine(); //We clear the current view engine
            string activeThemeName;
            //If there is not a current theme working, we load it
            if (Session[KeySession] == null)
            {
                var tenantName = GetTenancyNameByUrl();
                Session[LastTenant] = tenantName;
                var instance = _themeService.GetCurrentActiveThemeFromTenant(tenantName);
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
                //There is a current theme in memory now we check if we are in the correct tenant

                if ((string) Session[LastTenant] != GetTenancyNameByUrl())
                {
                    //We are not in the correct tenant
                    ClearViewEngine(); //Clear the engine and load the theme again!
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
                    //we are in the current tenant we jus load the theme from memory
                    activeThemeName = (string)Session[KeySession];
                }
            }
            //After the theme is resolved and is not null we inject it in the view engine
            if (!string.IsNullOrEmpty(activeThemeName))
            {
                System.Web.Mvc.ViewEngines.Engines.Insert(0, new SystemThemeViewEngine(activeThemeName));
            }
        }
        private static void ClearViewEngine()
        {
            System.Web.Mvc.ViewEngines.Engines.Clear();
            System.Web.Mvc.ViewEngines.Engines.Insert(0, new DefaultViewEngine());
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
        public string HostWithNoTenantName()
        {
            var hostName = string.Empty;
            if (Request.Url != null)
            {
                hostName= Request.Url.Host.Split(".").Last();
            }
            return hostName;
        }
        public bool IsNotExistentTenancy
        {
            get
            {
                //Gets the parsed name
                var name = GetTenancyNameByUrl();
                var hostName = HostWithNoTenantName();
                //Checks if a tenancy name was detected
                var hasName = Request.Url != null && !name.StartsWith(hostName);
                //Finds the name in the database
                var instance = AsyncHelper.RunSync(() => _tenancyService.GetTenantByName(name));
                //if tenancy was not found && the user send a tenancy name then he was looking for an unexisting tenant
                return instance == null && hasName;
            }
        }
        /// <summary>
        /// Detects if the site url belongs to a tenant website
        /// <para>Not available in AdminController since tenantId can be obtained from te AbpSession</para>
        /// </summary>
        public bool IsHostSite
        {
            get
            {
                var name = GetTenancyNameByUrl();
                var instance =AsyncHelper.RunSync(()=>_tenancyService.GetTenantByName(name));
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