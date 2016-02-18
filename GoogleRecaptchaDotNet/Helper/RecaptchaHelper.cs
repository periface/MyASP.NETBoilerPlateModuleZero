using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GoogleRecaptchaDotNetMvc.Helper
{
    public static class RecaptchaHelper
    {
        public static IHtmlString RecaptchaInput(this HtmlHelper helper)
        {
            //
            //<div class="g-recaptcha" data-sitekey="6Lco7g0TAAAAAPXI_PHY0uoHPRgOQAZFq2wOStk9"></div>
            //<script src = 'https://www.google.com/recaptcha/api.js' ></ script >
            //
            var secret = WebConfigurationManager.AppSettings["RecaptchaGoogleSiteKey"];
            var sb = new StringBuilder();
            var div = new TagBuilder("div");
            div.MergeAttribute("class", "g-recaptcha");
            div.MergeAttribute("data-sitekey", secret);
            sb.Append(div);
            var script = new TagBuilder("script");
            script.MergeAttribute("src", "https://www.google.com/recaptcha/api.js");
            sb.Append(script);
            sb.Append("<p class='help-block'>" + helper.ViewBag.Message + "</p>");
            return new HtmlString(sb.ToString());
        }
    }
}
