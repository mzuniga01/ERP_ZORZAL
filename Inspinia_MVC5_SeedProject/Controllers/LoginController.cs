﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using Microsoft.Owin.Security;


namespace ERP_GMEDINA.Controllers
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
                if (Usuario.Count > 0)
                {
                    foreach (UDP_Acce_Login_Result UserLogin in Usuario)
                    {
                        var Listado = db.SDP_Acce_GetUserRols(UserLogin.usu_Id, "").ToList();
                        var ListadoRol = db.SDP_Acce_GetRolesAsignados(UserLogin.usu_Id).ToList();
                        Session["UserRol"] = ListadoRol.Count();
                        Session["UserLogin"] = UserLogin.usu_Id;
                        Session["UserLoginRols"] = Listado;
                        Session["UserLoginEsAdmin"] = UserLogin.usu_EsAdministrador;
                        Session["UserLoginSesion"] = UserLogin.usu_SesionesValidas;
                        if (!UserLogin.usu_EsActivo)
                        {
                            ModelState.AddModelError("usu_NombreUsuario", "Usuario inactivo, contacte al Administrador");
                            return View(Login);
                        }
                        if (UserLogin.usu_SesionesValidas == 0)
                        {
                            ModelState.AddModelError("usu_NombreUsuario", "Su contraseña expiró, contacte al Administrador");
                            return View(Login);
                        }
                        if (UserLogin.usu_SesionesValidas == 1)
                        {
                            return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("usu_NombreUsuario", "Usuario o Password incorrecto");
                    return View(Login);
                }
            }
            catch (Exception Ex)
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
            Session["UserLogin"] = null;
            Session["UserLoginRols"] = null;
            Session["UserLoginEsAdmin"] = null;
            Session["UserLoginSesion"] = null;
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

        public ActionResult NotFound()
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