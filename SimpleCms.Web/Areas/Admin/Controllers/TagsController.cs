using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Application.Features;

namespace SimpleCms.Web.Areas.Admin.Controllers
{
    public class TagsController : Controller
    {
        // GET: Admin/Tags
        public ActionResult Index()
        {
            return View();
        }
    }
}