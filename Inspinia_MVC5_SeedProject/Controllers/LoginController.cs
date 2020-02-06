using System;
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
        Helpers Function = new Helpers();
        Helpers Help = new Helpers();

        // GET: Login
        public ActionResult Index()
        {
            Help.fCerrarSesion();
            return View();
        }

        [HttpPost]
        public ActionResult Index(tbUsuario Login, string txtPassword)
        {
            try
            {
                var Usuario = db.UDP_Acce_Login(Login.usu_NombreUsuario, txtPassword).ToList();
                //Paso 1: Validar si el usuario existe.
                if (Usuario.Count > 0)
                {
                    foreach (UDP_Acce_Login_Result UserLogin in Usuario)
                    {
                        //Paso 2: Validar que el usuario esté activo.
                        if(UserLogin.usu_EsActivo)
                        {
                            //Paso 3: Validar las sesiones validas.
                            //usu_SesionesValidas == 0 - Necesita contraseña porque ya expiró
                            //usu_SesionesValidas == 1 - Necesita cambiar contraseña
                            //usu_SesionesValidas == 9 - Todo OK
                            if (UserLogin.usu_SesionesValidas == 0)
                            {
                                ModelState.AddModelError("usu_NombreUsuario", "Su contraseña expiró, contacte al Administrador");
                                return View(Login);
                            }
                            if (UserLogin.usu_SesionesValidas == 1)
                            {
                                return RedirectToAction("ModificarPass/" + UserLogin.emp_Id, "Usuario");
                            }
                            
                            //Si todo esta bien, recuperar la informacion del usuario.
                            var Listado = db.SDP_Acce_GetUserRols(UserLogin.usu_Id, "").ToList(); //Accesos
                            var ListadoRol = db.SDP_Acce_GetRolesAsignados(UserLogin.usu_Id).ToList(); //Rol
                            Session["UserNombreUsuario"] = UserLogin.usu_NombreUsuario;
                            Session["UserNombresApellidos"] = UserLogin.usu_Nombres + " " + UserLogin.usu_Apellidos;
                            Session["UserLogin"] = UserLogin.usu_Id;
                            Session["UserLoginEsAdmin"] = UserLogin.usu_EsAdministrador;
                            Session["UserLoginSesion"] = UserLogin.usu_SesionesValidas;
                            Session["UserEstado"] = UserLogin.usu_EsActivo;
                            //Si el usuario no es admin, recuperar la información del rol y sus accesos
                            if (!UserLogin.usu_EsAdministrador)
                            {
                                foreach (SDP_Acce_GetRolesAsignados_Result Rol in ListadoRol)
                                    Session["UserRolEstado"] = Rol.rol_Estado;
                                Session["UserLoginRols"] = Listado;
                                Session["UserRol"] = ListadoRol.Count();
                            }
                            
                        }
                        else
                        {
                            //Si el usuario no es activo que muestre mensaje y retorne al login una vez mas.
                            ModelState.AddModelError("usu_NombreUsuario", "Usuario inactivo, contacte al Administrador");
                            return View(Login);
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
                Function.InsertBitacoraErrores("Login", Ex.Message.ToString(), "Index");
                return View(Login);
            }
        }

        public ActionResult CerrarSesion()
        {
            Help.fCerrarSesion();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult SinAcceso()
        {
            //Validar Inicio de Sesión
            if (Help.GetUserLogin())
                return View();
            else
                return RedirectToAction("Index", "Login");
        }

        public ActionResult NotFound()
        {
            //Validar Inicio de Sesión
            if (Help.GetUserLogin())
                return View();
            else
                return RedirectToAction("Index", "Login");
        }

        public ActionResult SinRol()
        {
            //Validar Inicio de Sesión
            if (Help.GetUserLogin())
                return View();
            else
                return RedirectToAction("Index", "Login");
        }
    }
}