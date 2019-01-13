using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class HomeController : Controller
    {
        ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        public ActionResult Index()
        {
            //Validar Inicio de Sesión
            GeneralFunctions Function = new GeneralFunctions();
            List<tbUsuario> Usuario = Function.getUserID();
            //
            if (Usuario.Count > 0)
            {
                ViewData["SubTitle"] = "Welcome in ASP.NET MVC 5 INSPINIA SeedProject ";
                ViewData["Message"] = "It is an application skeleton for a typical MVC 5 project. You can use it to quickly bootstrap your webapp projects.";

                return View();
            }
            else
                return RedirectToAction("Index","Login");
        }

        public ActionResult Minor()
        {
            ViewData["SubTitle"] = "Simple example of second view";
            ViewData["Message"] = "Data are passing to view by ViewData from controller";

            return View();
        }
    }
}