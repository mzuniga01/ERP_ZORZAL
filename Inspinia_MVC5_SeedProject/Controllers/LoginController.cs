using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using Microsoft.Owin.Security;


namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class LoginController : Controller
    {
        ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        // GET: Login
        public ActionResult Index()
        {
                Session["UserLogin"] = null;    
                return View();
        }

        [HttpPost]
        public ActionResult Index(tbUsuario Login, string txtPassword)
        {
            try
            { 
                var Usuario = db.UDP_Acce_Login(Login.usu_NombreUsuario, txtPassword).ToList();
                if(Usuario.Count>0)
                {
                    foreach (UDP_Acce_Login_Result UserLogin in Usuario)
                        Session["UserLogin"] = UserLogin.usu_Id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("usu_NombreUsuario", "Usuario o Password incorrecto");
                    return View(Login);
                }
            }
            catch(Exception Ex)
            {
                Ex.Message.ToString();
                return View(Login);
            }
        }

        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Login");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult SinAcceso()
        {
            //Validar Inicio de Sesión
            GeneralFunctions Function = new GeneralFunctions();
            if (Function.GetUserLogin())
                return View();
            else
                return RedirectToAction("Index", "Login");
        }
    }
}