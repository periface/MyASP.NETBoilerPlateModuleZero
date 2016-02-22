using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using GoogleRecaptchaDotNetMvc;
using GoogleRecaptchaDotNetMvc.Attribute;
using SimpleCms.ModuleCms.Services;
using SimpleCms.ModuleCms.SiteConfiguration;
using SimpleCms.ModuleEcommerce.Services;

namespace SimpleCms.Web.Controllers
{
    public class HomeController : SimpleCmsControllerBase
    {
       
        public ActionResult Index()
        {
            ViewBag.Message = IsHostSite ? "Greetings from host!" : "Greetings from tenant!";
            return View();
        }

        [CheckRecaptcha]
        [HttpPost]
        public ActionResult Index(string text)
        {
            return View("_ok");
        }
	}
}