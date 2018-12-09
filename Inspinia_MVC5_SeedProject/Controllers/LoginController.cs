using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
                return View();
        }

        [HttpPost]
        public ActionResult Index(tbFactura Login)
        {
            string user = Login.fact_Codigo.ToString();
            if (Login.fact_Codigo == "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}