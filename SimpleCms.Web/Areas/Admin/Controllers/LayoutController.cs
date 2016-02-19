using System.Linq;
using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Threading;
using SimpleCms.ModuleCms.SiteConfiguration;
using SimpleCms.Sessions;
using SimpleCms.Web.Controllers;
using SimpleCms.Web.Models.Layout;

namespace SimpleCms.Web.Areas.Admin.Controllers
{
    public class LayoutController : AdminController
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ISiteService _siteService;
        public LayoutController(IUserNavigationManager userNavigationManager, ILocalizationManager localizationManager, ISessionAppService sessionAppService, IMultiTenancyConfig multiTenancyConfig, ISiteService siteService)
        {
            _userNavigationManager = userNavigationManager;
            _localizationManager = localizationManager;
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
            _siteService = siteService;
        }
        [ChildActionOnly]
        public PartialViewResult AdminTopMenu(string activeMenu = "")
        {
            //Custom fix, url navigation is not detecting my active menu
            var activeMenuItem = activeMenu;
            if (Request.Url != null)
            {
                if (string.IsNullOrEmpty(activeMenuItem))
                {
                    var url = Request.Url.AbsoluteUri;
                    activeMenuItem = url.Split('/').Last();
                }
            }
            var model = new TopMenuViewModel()
            {
                MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("ZeroMenu", AbpSession.UserId)),
                ActiveMenuItemName = activeMenuItem
            };

            return PartialView("_menuAdmin", model);
        }
        [ChildActionOnly]
        public PartialViewResult LangSelectionMenu()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _localizationManager.CurrentLanguage,
                Languages = _localizationManager.GetAllLanguages()
            };
            return PartialView("_langSelectionAdmin", model);
        }
        [ChildActionOnly]
        public PartialViewResult SiteInformationHeader()
        {
            var siteInformation = _siteService.GetCurrentInfo();
            return PartialView("_siteInformationHeader", siteInformation);
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

            return PartialView("_UserMenuOrLoginLinkAdmin", model);
        }
    }
}