using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Dependency;
using SimpleCms.ModuleCms.Themes;
using SimpleCms.ModuleCms.Themes.Dto;

namespace SimpleCms.Web.Helpers
{
    public static class ThemeHelper
    {
        public static string GetActiveTheme(this HtmlHelper helper)
        {
            //This is optional
            var container = IocManager.Instance;
            var instance = container.Resolve<IThemeService>().GetCurrentActiveThemeFromTenant();
            //Should not be like this... but anyway.....
            if (instance==null) return "~/Views/Shared/_Layout.cshtml";
            var activeTheme = $"~/Views/Themes/{instance.UniqueFolderId}/Shared/_Layout.cshtml";
            return activeTheme;
        }
    }
}