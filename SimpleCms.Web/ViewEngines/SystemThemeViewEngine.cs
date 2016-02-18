using System.Web.Mvc;
using Abp.Dependency;
using Abp.Runtime.Session;
using SimpleCms.ModuleCms.Themes;

namespace SimpleCms.Web.ViewEngines
{
    public class SystemThemeViewEngine : RazorViewEngine
    {
        public SystemThemeViewEngine(string parsedTenancyName)
        {
            var container = IocManager.Instance;
            var instance = container.Resolve<IThemeService>().GetCurrentActiveThemeFromTenant(parsedTenancyName);
            if (instance == null) return;
            var activeThemeName = instance.UniqueFolderId;
            ViewLocationFormats = new[]
            {
                "~/Views/Themes/" + activeThemeName + "/{1}/{0}.cshtml",
                "~/Views/Themes/" + activeThemeName + "/Shared/{0}.cshtml"
            };
            PartialViewLocationFormats = new[]
            {
                "~/Views/Themes/" + activeThemeName + "/{1}/{0}.cshtml",
                "~/Views/Themes/" + activeThemeName + "/Shared/{0}.cshtml"
            };
            AreaViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/Themes/" + activeThemeName + "/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Themes/" + activeThemeName + "/Shared/{0}.cshtml"
            };
            AreaPartialViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/Themes/" + activeThemeName + "/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Themes/" + activeThemeName + "/Shared/{0}.cshtml"
            };
        }
    }
}