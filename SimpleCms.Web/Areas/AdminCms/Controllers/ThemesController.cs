using System.Web.Mvc;
using Abp.UI;
using SimpleCms.ModuleCms.SiteConfiguration;
using SimpleCms.ModuleCms.Themes;
using SimpleCms.Web.Controllers;

namespace SimpleCms.Web.Areas.AdminCms.Controllers
{
    public class ThemesController : AdminController
    {
        private readonly IThemeService _themeService;
        private readonly ISiteService _siteService;
        public ThemesController(IThemeService themeService, ISiteService siteService)
        {
            _themeService = themeService;
            _siteService = siteService;
        }

        // GET: Admin/Themes
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult MyThemes()
        {
            var siteThemes = _themeService.GetTenantAsignedThemes();
            return View(siteThemes);
        }

        public ViewResult AllThemes()
        {
            var allThemes = _themeService.GetStoreThemes();
            return View(allThemes);
        }

        public JsonResult ActivateTheme(int id)
        {
            _themeService.ActivateTheme(id);
            return Json(new { ok = true });
        }

        public JsonResult GetTheme(int id)
        {
            var config = _siteService.GetCurrentConfig();
            if (config.Id == 0)
            {
                throw new UserFriendlyException("You need to configure your site firts!");
            }
            _themeService.GetTheme(id);
            return Json(new { ok = true });
        }

        
    }
}