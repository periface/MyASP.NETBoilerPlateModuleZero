using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleCms.Web.ViewEngines
{
    public class UserCustomThemesViewEngine : RazorViewEngine
    {
        public UserCustomThemesViewEngine(string activeThemeName, string userUniqueFolder)
        {

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
                "~Areas/{2}/Views/Themes/" + activeThemeName + "/{1}/{0}.cshtml",
                "~Areas/{2}/Views/Themes/" + activeThemeName + "/Shared/{0}.cshtml"
            };
            AreaPartialViewLocationFormats = new[]
            {
                "~Areas/{2}/Views/Themes/" + activeThemeName + "/{1}/{0}.cshtml",
                "~Areas/{2}/Views/Themes/" + activeThemeName + "/Shared/{0}.cshtml"
            };
        }
    }
}