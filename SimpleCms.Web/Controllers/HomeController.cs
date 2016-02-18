using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using SimpleCms.ModuleCms.Services;
using SimpleCms.ModuleCms.SiteConfiguration;
using SimpleCms.ModuleEcommerce.Services;

namespace SimpleCms.Web.Controllers
{
    public class HomeController : SimpleCmsControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}