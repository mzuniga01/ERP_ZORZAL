using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERP_GMEDINA.Attribute
{
    public class SessionManager : ActionFilterAttribute
    {
        private readonly string _screenId;
        Helpers Help = new Helpers();
        public SessionManager()
        {
        }

        public SessionManager(string screenId)
        {
            _screenId = screenId;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int SesionesValidas = -1;
            bool UsuarioEstado = false;
            bool EsAdmin = false;
            int UsuarioRol = 0;
            bool AccesoPantalla = false;

            Help.ValidarUsuario(_screenId, out SesionesValidas, out UsuarioEstado, out EsAdmin, out UsuarioRol, out AccesoPantalla);
            var valuesUsuarioInactivo = new RouteValueDictionary(new { action = "Index", controller = "Login" });//--//
            var valuesError = new RouteValueDictionary(new { action = "Index", controller = "Login" });//--//
            var valuesSinAcceso = new RouteValueDictionary(new { action = "SinAcceso", controller = "Login" });
            var valuesSinRol = new RouteValueDictionary(new { action = "SinRol", controller = "Login" });
            var valuesIndex = new RouteValueDictionary(new { action = "Index", controller = "Login" });
            var valuesCambiarPass = new RouteValueDictionary(new { action = "ModificarPass/" + HttpContext.Current.Session["UserLogin"], controller = "Usuario" });
            var valuesContraseñaExpirada = new RouteValueDictionary(new { action = "Index", controller = "Login" });//--//

            //Paso 1: Validar que la sesion no haya expirado.
            if (Help.GetUserLogin())
            {
                //Paso 1: Validar que el estado del usuario siga siendo activo.
                if (UsuarioEstado)
                {
                    //Paso 3: Validar las sesiones validas.
                    //usu_SesionesValidas == 0 - Necesita contraseña porque ya expiró
                    //usu_SesionesValidas == 1 - Necesita cambiar contraseña
                    //usu_SesionesValidas == 9 - Todo OK
                    if (SesionesValidas == 1)
                        filterContext.Result = new RedirectToRouteResult(valuesCambiarPass);
                    else if (SesionesValidas == 0)
                        filterContext.Result = new RedirectToRouteResult(valuesContraseñaExpirada);
                    else if (SesionesValidas == -1)
                        filterContext.Result = new RedirectToRouteResult(valuesError);
                    else
                    {
                        //Paso 4: Validar si el usuario no es admin.
                        if(!EsAdmin)
                        {
                            //Paso 5: Validar si el usuario tiene un rol asignado.
                            if(UsuarioRol!=0)
                            {
                                //Paso 6: Validar si el usuario tiene acceso a la pantalla u objeto.
                                if(!AccesoPantalla)
                                    filterContext.Result = new RedirectToRouteResult(valuesSinAcceso);
                            }
                            else
                                filterContext.Result = new RedirectToRouteResult(valuesSinRol);
                        }
                    }
                }
                else
                    filterContext.Result = new RedirectToRouteResult(valuesUsuarioInactivo);
            }
            else
                filterContext.Result = new RedirectToRouteResult(valuesIndex);
        }
    }
}