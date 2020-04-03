using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sell.web.IService;

namespace sell.web.Controllers
{
    public class HomeController : Controller
    {
        public IUserInfoSercice _userInfoService { get; set; }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
