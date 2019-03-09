using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project.App_Start;
using Project.Data;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        [AuthenticationFilter]
        public ActionResult Index()
        {
            return View();
        }

    }
}