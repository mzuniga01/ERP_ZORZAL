using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models;

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
        public ActionResult Index(tbTiposPago Login)
        {
            string user = Login.tpago_Codigo.ToString();
            if (Login.tpago_Descripcion == "Admin")
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