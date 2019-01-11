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
        ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        // GET: Login
        public ActionResult Index()
        {
                return View();
        }

        [HttpPost]
        public ActionResult Index(tbUsuario Login, string txtPassword)
        {
            try
            {
                var Usuario = db.UDP_Login(Login.usu_NombreUsuario, txtPassword).ToList();
                if(Usuario.Count>0)
                {
                    foreach (UDP_Login_Result UserLogin in Usuario)
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
                //Login.usu_Password = Convert.ToByte(usu_Password);
                return View(Login);
            }
        }

        public List<tbUsuario> getUserID()
        {
            int user = 0;
            List<tbUsuario> UsuarioList = new List<tbUsuario>();
            try
            {
                user = Convert.ToInt32(Session["UserLogin"]);
                if(user!=0)
                {
                    UsuarioList = db.tbUsuario.Where(s=> s.usu_Id==user).ToList();
                }
                return UsuarioList;
            }
            catch(Exception Ex)
            {
                Ex.Message.ToString();
                return UsuarioList;
            }
        }


    }
}