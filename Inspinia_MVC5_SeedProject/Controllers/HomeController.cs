using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_GMEDINA.Controllers
{
    public class HomeController : Controller
    {
        [SessionManager("Home/Index")]
        public ActionResult Index()
        {
            return View();
        }
    
        public ActionResult Minor()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
    }
}