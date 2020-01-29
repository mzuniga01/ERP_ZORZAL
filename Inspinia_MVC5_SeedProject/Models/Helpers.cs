using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class Helpers
    {
        ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        public const bool AnuladoFactura = true;
        public const bool EsImpreso = true;
        public const int EstadoImpreso = 2;
        public const int RTN = 3;
        public const int ID = 2;
        public const int rol_Id = 2; //Rol de cajero

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

        //Estados Exoneración 
        public const bool ExoneracionActiva = true;
        public const bool ExoneracionInactiva = false;

        //Reportes
        public const int rptVentasFechas = 195;
        public const int rptVentasCajaFechas = 196;
        public const int rptFacturasPendientesPago = 197;
        public const int rptVentasConsumidorFinal = 198;
        public const int rptNotasCreditoEntreFechas = 199;
        public const int rptAnalisisMora = 200;
        public const int rptSolicitudesCreditoAprobar = 202;
        public const int rptCuponesDescuentoFechas = 205;

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

        //Listas
        public List<cMinorista> MinoristaList()
        {
            List<cMinorista> list = new List<cMinorista>();

            list.Add(new cMinorista()
            {
                ID_MINORISTA = "1",
                DESCRIPCION = "Si"
            });
            list.Add(new cMinorista()
            {
                ID_MINORISTA = "0",
                DESCRIPCION = "No"
            });
            return list;
        }
        public List<cActivo> EstadoList()
        {
            List<cActivo> list = new List<cActivo>();

            list.Add(new cActivo()
            {
                ID_ACTIVO = "1",
                DESCRIPCION = "Si"
            });
            list.Add(new cActivo()
            {
                ID_ACTIVO = "0",
                DESCRIPCION = "No"
            });
            return list;
        }
        public List<cTipoCuenta> TipoCuentaList()
        {
            List<cTipoCuenta> list = new List<cTipoCuenta>();

            list.Add(new cTipoCuenta()
            {
                ID_TIPOCUENTA = 1,
                DESCRIPCION = "Ahorro"
            });
            list.Add(new cTipoCuenta()
            {
                ID_TIPOCUENTA = 0,
                DESCRIPCION = "Cheques"
            });
            return list;
        }
        public List<Genero> GeneroList()
        {
            List<Genero> list = new List<Genero>();

            list.Add(new Genero()
            {
                ID_GENERO = "H",
                DESCRIPCION = "Hombre"
            });
            list.Add(new Genero()
            {
                ID_GENERO = "M",
                DESCRIPCION = "Mujer"
            });
            return list;
        }
        public List<Nacionalidad> NacionalidadList()
        {
            List<Nacionalidad> list = new List<Nacionalidad>();

            list.Add(new Nacionalidad()
            {
                DESCRIPCION = "Hondureña",
            });
            list.Add(new Nacionalidad()
            {
                DESCRIPCION = "Mexicano",
            });
            list.Add(new Nacionalidad()
            {
                DESCRIPCION = "EstadoUnidense"
            });
            return list;
        }
        public List<cDepartamento> DepartamentoList()
        {
            List<cDepartamento> list = new List<cDepartamento>();

            list.Add(new cDepartamento()
            {
                DESCRIPCION = "Olancho",
            });
            list.Add(new cDepartamento()
            {
                DESCRIPCION = "Atlántida",
            });
            list.Add(new cDepartamento()
            {
                DESCRIPCION = "La Ceiba"
            });
            list.Add(new cDepartamento()
            {
                DESCRIPCION = "Choluteca"
            });
            list.Add(new cDepartamento()
            {
                DESCRIPCION = "Cortes"
            });

            return list;
        }
        public List<DenominacionList> DenominacionList()
        {
            List<DenominacionList> list = new List<DenominacionList>();

            list.Add(new DenominacionList()
            {
                ID_TipoDenominacion = 1,
                Tipo_Denominacion = "Billete"
            });
            list.Add(new DenominacionList()
            {
                ID_TipoDenominacion = 2,
                Tipo_Denominacion = "Moneda"
            });


            return list;
        }
        public static List<TipoPagos> TPList()
        {
            List<TipoPagos> list = new List<TipoPagos>();

            list.Add(new TipoPagos()
            {
                ID_TP = 1,
                DESCRIPCION_TP = "Efectivo"
            });
            list.Add(new TipoPagos()
            {
                ID_TP = 2,
                DESCRIPCION_TP = "Tarjeta Crédito/Débito"
            });
            return list;
        }

        //Seguridad
        public bool Sesiones(string sPantalla)
        {
            int UserID = 0;
            bool Retorno = false;
            byte Sesion = 0;

            try
            {
                UserID = (int)HttpContext.Current.Session["UserLogin"];
                Sesion = (byte)HttpContext.Current.Session["UserLoginSesion"];
                if (Sesion > 1)
                    Retorno = true;
                //else
                //{
                //    var list = (IEnumerable<SDP_Acce_GetUserRols_Result>)HttpContext.Current.Session["UserLoginRols"];
                //    var BuscarList = list.Where(x => x.obj_Referencia == sPantalla);
                //    int Conteo = BuscarList.Count();
                //    if (Conteo > 0)
                //        Retorno = true;
                //}
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                Retorno = false;
            }
            return Retorno;
        }
        public bool GetUserRols(string sPantalla)
        {
            int UserID = 0;
            bool EsAdmin = false;
            bool Retorno = false;

            try
            {
                UserID = (int)HttpContext.Current.Session["UserLogin"];
                EsAdmin = (bool)HttpContext.Current.Session["UserLoginEsAdmin"];
                if (EsAdmin)
                {
                    Retorno = true;
                }
                else
                {
                    var list = (IEnumerable<SDP_Acce_GetUserRols_Result>)HttpContext.Current.Session["UserLoginRols"];
                    var BuscarList = list.Where(x => x.obj_Referencia == sPantalla);
                    int Conteo = BuscarList.Count();
                    if (Conteo > 0)
                        Retorno = true;
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                Retorno = false;
            }
            return Retorno;

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
                return UsuarioList;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return UsuarioList;
            }
        }
        public bool GetUserLogin()
        {
            bool state = false;
            int user = 0;
            try
            {
                user = (int)HttpContext.Current.Session["UserLogin"];
                if (user != 0)
                    state = true;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                state = false;
            }
            return state;
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
                Ex.Message.ToString();
            }
            return user;
        }
        public DateTime DatetimeNow()
        {
            DateTime dt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-6)).DateTime;
            return dt;
        }
        public void InsertBitacoraErrores(string sPantalla, string biteMensajeError, string biteAccion)
        {
            IEnumerable<object> List = null;
            string UserName = (string)HttpContext.Current.Session["UserName"];
            try
            {
                List = db.UDP_Acce_tbBitacoraErrores_Insert(sPantalla, UserName, DatetimeNow(), biteMensajeError, biteAccion);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
        }
        public bool GetRol()
        {
            bool state = false;
            bool EsAdmin = false;
            int Rol = 0;
            try
            {
                Rol = (int)HttpContext.Current.Session["UserRol"];
                EsAdmin = (bool)HttpContext.Current.Session["UserLoginEsAdmin"];
                if (EsAdmin)
                    state = true;
                else
                {
                    if (Rol != 0)
                        state = true;
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                state = false;
            }
            return state;
        }
        public bool EsPersonaNatural(int clte_Id)
        {
            bool Retorno = false;
            try
            {
                var Cliente = (from vCliente in db.tbCliente where vCliente.clte_Id == clte_Id select vCliente.clte_EsPersonaNatural).FirstOrDefault();
                if (Cliente)
                {
                    Retorno = true;
                }
                else
                {
                    Retorno = false;
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                Retorno = false;
            }
            return Retorno;
        }
    }
}