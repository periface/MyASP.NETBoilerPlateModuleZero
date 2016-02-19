using System.Web.Mvc;

namespace SimpleCms.Web.Areas.AdminCms.Controllers
{
    public class TagsController : AdminController
    {
        // GET: Admin/Tags
        public ActionResult Index()
        {
            return View();
        }
    }
}