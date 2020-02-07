using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;

namespace ERP_GMEDINA.Models
{
    public class Helpers
    {
        ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }
        //Cerrar sesion
        public void fCerrarSesion()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D);
            HttpContext.Current.Response.Expires = -1500;
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            AuthenticationManager.SignOut();
            HttpContext.Current.Session["UserNombreUsuario"] = null;
            HttpContext.Current.Session["UserNombresApellidos"] = null;
            HttpContext.Current.Session["UserLogin"] = null;
            HttpContext.Current.Session["UserLoginEsAdmin"] = null;
            HttpContext.Current.Session["UserLoginSesion"] = null;
            HttpContext.Current.Session["UserLoginRols"] = null;
            HttpContext.Current.Session["UserRol"] = null;
            HttpContext.Current.Session["UserRolEstado"] = null;
            HttpContext.Current.Session["UserEstado"] = null;
        }

        public bool GetUserAccesoRol(string sPantalla)
        {
            bool Retorno = false;
            try
            {
                if (!Convert.ToBoolean(HttpContext.Current.Session["UserLoginEsAdmin"]))
                {
                    var list = (IEnumerable<SDP_Acce_GetUserRols_Result>)HttpContext.Current.Session["UserLoginRols"];
                    var BuscarList = list.Where(x => x.obj_Referencia == sPantalla);
                    int Conteo = BuscarList.Count();
                    if (Conteo > 0)
                        Retorno = true;
                }
                else
                    Retorno = true;
                    
            }
            catch (Exception Ex)
            {
                InsertBitacoraErrores("Helpers", Ex.Message.ToString(), "GetUserAccesoRol");
            }
            return Retorno;
        }

        public void ValidarUsuario(string sPantalla, out int SesionesValidas, out bool UsuarioEstado, out bool EsAdmin, out int UsuarioRol, out bool AccesoPantalla)
        {
            UsuarioEstado = false;
            SesionesValidas = -1;
            EsAdmin = false;
            UsuarioRol = 0;
            AccesoPantalla = false;

            try
            {
                SesionesValidas = Convert.ToInt32(HttpContext.Current.Session["UserLoginSesion"]);
                UsuarioEstado = Convert.ToBoolean(HttpContext.Current.Session["UserEstado"]);
                EsAdmin = Convert.ToBoolean(HttpContext.Current.Session["UserLoginEsAdmin"]);
                UsuarioRol = Convert.ToInt32(HttpContext.Current.Session["UserRol"]);
                AccesoPantalla = GetUserAccesoRol(sPantalla);
            }
            catch (Exception Ex)
            {
                InsertBitacoraErrores("Sesiones", Ex.Message.ToString(), "Helpers");
            }
        }

        public bool GetUserLogin()
        {
            bool Estado = false;
            int user = 0;
            try
            {
                user = (int)HttpContext.Current.Session["UserLogin"];
                if (user != 0)
                    Estado = true;
            }
            catch (Exception Ex)
            {
                InsertBitacoraErrores("GetUserLogin", Ex.Message.ToString(), "Helpers");
            }
            return Estado;
        }

        public string InsertBitacoraErrores(string sPantalla, string biteMensajeError, string biteAccion)
        {
            IEnumerable<object> List = null;
            string msj = "";
            try
            {
                List = db.UDP_Acce_tbBitacoraErrores_Insert(sPantalla, "", DatetimeNow(), biteMensajeError, biteAccion);
                foreach (UDP_Acce_tbBitacoraErrores_Insert_Result Res in List)
                    msj = Res.MensajeError;
            }
            catch (Exception Ex)
            {
                msj = Ex.Message.ToString();
            }
            return msj;

        }

        public List<tbUsuario> getUserInformation()
        {
            int user = 0;
            List<tbUsuario> UsuarioList = new List<tbUsuario>();
            try
            {
                user = (int)HttpContext.Current.Session["UserLogin"];
                if (user != 0)
                {
                    UsuarioList = db.tbUsuario.Where(s => s.usu_Id == user).ToList();
                }
            }
            catch (Exception Ex)
            {
                InsertBitacoraErrores("Helpers", Ex.Message.ToString(), "getUserInformation");
            }
            return UsuarioList;
        }

        public int GetUser()
        {
            int user = 0;
            try
            {
                user = (int)HttpContext.Current.Session["UserLogin"];
            }
            catch (Exception Ex)
            {
                InsertBitacoraErrores("Helpers", Ex.Message.ToString(), "GetUser");
            }
            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime DatetimeNow()
        {
            DateTime dt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-6)).DateTime;
            return dt;
        }

        public const bool AnuladoFactura = true;
        public const int RTN = 3;
        public const int ID = 2;

        //Estados Cliente
        public const bool ClienteActivo = true;

        public const bool ClienteCredito = false;
        public const bool ClienteInactivo = false;
        public const bool ClienteExonerado = false;

        //Lista Precios
        public const bool ListaPrecioActivo = true;

        public const bool ListaPrecioInactivo = false;

        //Estados Pedido
        public const int Pendiente = 1;

        public const int Facturado = 2;

        //Estado Solicitud Credito
        public const int SolicitudPendiente = 1;

        public const int SolicitudAprobado = 2;
        public const int SolicitudDenegado = 3;

        ///ESTADO ENTRADA
        public const int EntradaAnulada = 1;

        public const int EntradaEmitida = 2;
        public const int EntradaInactivada = 4;
        public const int EntradaAplicada = 1;

        //estado movimiento
        public const int EntradaEstadoAnulada = 3;

        //Salida
        public const int sal_Aplicada = 1;

        public const int sal_Emitida = 2;
        public const int sal_Anulada = 3;
        public const int sal_Inactiva = 4;
        public const int sal_Activa = 5;

        public const int sal_Impresa = 6;
        public const bool sal_EsAnulada = true;
        public const int sal_Prestamo = 1;

        public const int sal_Venta = 2;
        public const int sal_Devolucion = 3;
        public const int esfac_Pagada = 3;
        public const int esfac_PagoPendiente = 4;
        public const bool fact_EsAnulada = true;
        public const string sal_Estado = "Emitida";


        ///ESTADO OBJETO
        public const bool ObjetoActivo = true;

        public const bool ObjetoInactivo = false;

        //Estado Rol
        public const bool RolActivo = true;//1

        public const bool RolInactivo = false;//0

        //Inventario Físico
        public const int InvFisicoActivo = 1;

        public const int InvFisicoConciliado = 2;

        public const int InvFisicoReconteo = 3;

        //BODEGA
        public const int BodegaActivo = 1;

        public const int BodegaInactivo = 0;

        //Empleado
        public const bool EmpleadoActivo = true;//1

        public const bool EmpleadoInactivo = false;//0

        //Estados Producto
        public const bool ProductoActivo = true;

        public const bool ProductoInactivo = false;

        //Estado Categoria
        public const int CategoriaActivo = 1;

        public const int CategoriaInactivo = 2;

        //Estado Subcategoria
        public const int SubcategoriaActivo = 1;

        public const int SubcategoriaInactivo = 2;

        //Estado Box
        public const int vbox_Abrierta = 1;

        public const int vbox_Cerrada = 2;

        public const string box_Abrierta = "Abrierta";

        public const string box_Cerrada = "Cerrada";



    }
}