using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Threading;
using SimpleCms.ModuleCms.SiteConfiguration;
using SimpleCms.ModuleCms.SiteConfiguration.Dto;
using SimpleCms.Sessions;
using SimpleCms.Web.Models.Layout;

namespace SimpleCms.Web.Controllers
{
    public class LayoutController : SimpleCmsControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ISiteService _siteService;
        public LayoutController(
            IUserNavigationManager userNavigationManager,
            ILocalizationManager localizationManager,
            ISessionAppService sessionAppService,
            IMultiTenancyConfig multiTenancyConfig, ISiteService siteService)
        {
            _userNavigationManager = userNavigationManager;
            _localizationManager = localizationManager;
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
            _siteService = siteService;
        }


        [ChildActionOnly]
        public PartialViewResult TopMenu(string activeMenu = "")
        {
            var model = new TopMenuViewModel
            {
                MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.UserId)),
                ActiveMenuItemName = activeMenu
            };

            return PartialView("_TopMenu", model);
        }

        [ChildActionOnly]
        public PartialViewResult LanguageSelection()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _localizationManager.CurrentLanguage,
                Languages = _localizationManager.GetAllLanguages()
            };

            return PartialView("_LanguageSelection", model);
        }
        [ChildActionOnly]
        public PartialViewResult GetSiteInformation()
        {
            var info = _siteService.GetCurrentInfo(ActiveTenantId);
            //Todo: If null return principalWebSiteInfo
            if (info == null)
            {
                return PartialView("_siteInformationHeaderClient", new SiteInfoDto()
                {
                    SiteTitle = "Cinotam"
                });
            }
            return PartialView("_siteInformationHeaderClient",info);
        }

        [ChildActionOnly]
        public PartialViewResult RenderMySiteName()
        {
            var info = _siteService.GetCurrentInfo(ActiveTenantId);
            //Todo: If null return principalWebSiteInfo
            if (info == null)
            {
                return PartialView("_mySiteName","Cinotam");
            }
            return PartialView("_mySiteName",info.SiteTitle);
        }
        [ChildActionOnly]
        public PartialViewResult RenderMySiteLogo()
        {
            var info = _siteService.GetCurrentInfo(ActiveTenantId);
            //Todo: If null return principalWebSiteInfo
            if (info == null)
            {
                return PartialView("_mySiteName", new SiteInfoDto()
                {
                    SiteTitle = "Cinotam"
                });
            }
            return PartialView("_mySiteLogo", info.SiteLogo);
        }
        [ChildActionOnly]
        public PartialViewResult RenderMySiteSlogan()
        {
            var info = _siteService.GetCurrentInfo(ActiveTenantId);
            //Todo: If null return principalWebSiteInfo
            if (info == null)
            {
                return PartialView("_mySiteName", new SiteInfoDto()
                {
                    SiteTitle = "Cinotam"
                });
            }
            return PartialView("_mySiteSlogan", info.SiteSlogan);
        }
        [ChildActionOnly]
        public PartialViewResult UserMenuOrLoginLink()
        {
            UserMenuOrLoginLinkViewModel model;

            if (AbpSession.UserId.HasValue)
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    LoginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations()),
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                };
            }
            else
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled
                };
            }

            return PartialView("_UserMenuOrLoginLink", model);
        }
    }
}