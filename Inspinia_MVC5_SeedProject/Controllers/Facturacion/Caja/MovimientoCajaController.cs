﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
/// <summary>
/// //
/// </summary>
namespace ERP_GMEDINA.Controllers
{
    public class MovimientoCajaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        public ActionResult Index()
        {
            var tbArqueoCaja = db.UDV_Vent_ArqueoCaja_Select;
            return View(tbArqueoCaja.ToList());
        }



        /////////INICIO APERTURA/////////
        // GET: /MovimientoCaja/

        public ActionResult DetailsApertura(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbMovimientoCaja tbMovimientoCaja = db.tbMovimientoCaja.Find(id);
            if (tbMovimientoCaja == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbMovimientoCaja);
        }

        public ActionResult IndexApertura()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("MovimientoCaja/IndexApertura"))
                {
                    return View(db.UDV_Vent_MovimientoCaja_Apertura.ToList());
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
        ///Create Apertura
        public ActionResult CreateApertura()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("MovimientoCaja/CreateApertura"))
                {
                    int idUser = 0;
                    
                    List<tbUsuario> User = Function.getUserInformation();
                    foreach (tbUsuario Usuario in User)
                    {
                        idUser = Convert.ToInt32(Usuario.usu_Id);
                    }

                    //////Solicitud Efectivo
                    tbMovimientoCaja MovimientoCaja = new tbMovimientoCaja();
                    tbSolicitudEfectivo SolicitudEfectivo = new tbSolicitudEfectivo();
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", SolicitudEfectivo.mnda_Id);
                    ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", MovimientoCaja.usu_Id);


                    ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                    ViewBag.suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();

                    var suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                    ViewBag.UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_NombreUsuario).SingleOrDefault();
                    ViewBag.mocja_UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();
                    var Cajas = db.tbCaja.Select(s => new { cja_Id = s.cja_Id, cja_Descripcion = s.cja_Descripcion, suc_Id = s.suc_Id }).Where(x => x.suc_Id == suc_Id).ToList();
                    ViewBag.cja_Id = new SelectList(Cajas, "cja_Id", "cja_Descripcion", MovimientoCaja.cja_Id);
                    /////Vistas Parciales
                    ViewBag.SolicitudEfectivo = db.tbSolicitudEfectivo.ToList();
                    ViewBag.MovimientoCaja = db.tbMovimientoCaja.ToList();
                    Session["SolicitudEfectivo"] = null;
                    return View();
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // POST: /MovimientoCaja/CreateApertura
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateApertura([Bind(Include = "mocja_Id,cja_Id,mocja_FechaApetura,mocja_UsuarioApertura,usu_Id,mocja_UsuarioCrea,mocja_FechaCrea")] tbMovimientoCaja tbMovimientoCaja)
        {
            int idUser = 0;
            List<tbUsuario> User = Function.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }

            tbSolicitudEfectivo tbSolicitudEfectivo = new tbSolicitudEfectivo();
            /////////REMOVE////////
            ModelState.Remove("mocja_UsuarioApertura");
            ModelState.Remove("mocja_UsuarioArquea");
            ModelState.Remove("mocja_FechaArqueo");
            ModelState.Remove("mocja_FechaAceptacion");
            ModelState.Remove("mocja_UsuarioAceptacion");
            /////////VAR//////////;
            bool solef_EsApertura = true;
            bool solef_EsAnulada = false;
            //tbMovimientoCaja.mocja_FechaApertura = DateTime.Today;
            tbMovimientoCaja.mocja_FechaCrea = Function.DatetimeNow();
            ///////////VAR SESSION//////////
            var list = (List<tbSolicitudEfectivoDetalle>)Session["SolicitudEfectivo"];
            short moneda = (short)Session["SolicitudEfectivoMoneda"];
            string MensajeError = "";
            string MensajeErrorSolicitud = "";
            string MensajeErrorSolicitudDetalle = "";
            //////////LISTAS///////////////
            IEnumerable<object> listMovimientoCaja = null;
            IEnumerable<object> listSolicitudEfectivo = null;
            IEnumerable<object> listSolicitudEfectivoDetalle = null;

            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("MovimientoCaja/CreateApertura"))
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            var fecha = DateTime.Today;
                            if (db.tbMovimientoCaja.Any(a => a.usu_Id == tbMovimientoCaja.usu_Id && a.mocja_FechaApertura == fecha))
                            {
                                ModelState.AddModelError("", "Este usuario ya aperturo una caja el día de hoy.");
                                //return View(tbMovimientoCaja);
                            }
                            else
                            {
                                using (TransactionScope Tran = new TransactionScope())
                                {
                                    listMovimientoCaja = db.UDP_Vent_tbMovimientoCaja_Apertura_Insert(
                                    tbMovimientoCaja.cja_Id,
                                    fecha,
                                    tbMovimientoCaja.mocja_UsuarioApertura,
                                    tbMovimientoCaja.usu_Id,
                                    tbMovimientoCaja.mocja_FechaArqueo,
                                    Function.GetUser(),
                                    tbMovimientoCaja.mocja_FechaAceptacion,
                                    Function.GetUser(),
                                    Function.GetUser(),
                                    Function.DatetimeNow());
                                    foreach (UDP_Vent_tbMovimientoCaja_Apertura_Insert_Result apertura in listMovimientoCaja)

                                        MensajeError = apertura.MensajeError;
                                    if (MensajeError.StartsWith("-1"))
                                    {
                                        Function.InsertBitacoraErrores("MovimientoCaja/CreateApertura", MensajeError, "CreateApertura");
                                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                        return View(tbMovimientoCaja);
                                    }
                                    else
                                    {
                                        listSolicitudEfectivo = db.UDP_Vent_tbSolicitudEfectivo_Apertura_Insert(
                                                Convert.ToInt32(MensajeError),
                                                solef_EsApertura,
                                                fecha,
                                                Function.GetUser(),
                                                moneda,
                                                solef_EsAnulada,
                                                Function.GetUser(),
                                                Function.DatetimeNow());
                                        foreach (UDP_Vent_tbSolicitudEfectivo_Apertura_Insert_Result SolicitudEfectivoMon in listSolicitudEfectivo)
                                            MensajeErrorSolicitud = SolicitudEfectivoMon.MensajeError;
                                        if (MensajeErrorSolicitud.StartsWith("-1"))
                                        {
                                            Function.InsertBitacoraErrores("MovimientoCaja/CreateApertura", MensajeError, "CreateApertura");
                                            ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                            return View(tbMovimientoCaja);
                                        }
                                        else
                                        {
                                            ///////////Solicitud Efectivo Detalle////////////////////
                                            if (MensajeErrorSolicitudDetalle != "-1")
                                            {
                                                if (list != null)
                                                {
                                                    if (list.Count != 0)
                                                    {
                                                        foreach (tbSolicitudEfectivoDetalle efectivodetalle in list)
                                                        {

                                                            var SolicitudDetalle = Convert.ToInt32(MensajeErrorSolicitud);
                                                            efectivodetalle.solef_Id = SolicitudDetalle;
                                                            listSolicitudEfectivoDetalle = db.UDP_Vent_tbSolicitudEfectivoDetalle_Apertura_Insert(
                                                              Convert.ToInt32(MensajeErrorSolicitud),
                                                               efectivodetalle.deno_Id,
                                                               efectivodetalle.soled_CantidadSolicitada,
                                                               efectivodetalle.soled_CantidadEntregada,
                                                               efectivodetalle.soled_MontoEntregado,
                                                               Function.GetUser(),
                                                               Function.DatetimeNow());
                                                            foreach (UDP_Vent_tbSolicitudEfectivoDetalle_Apertura_Insert_Result SolicitudEfectivoDet in listSolicitudEfectivoDetalle)
                                                            {
                                                                MensajeErrorSolicitudDetalle = SolicitudEfectivoDet.MensajeError;
                                                                if (MensajeErrorSolicitudDetalle.StartsWith("-1"))
                                                                {
                                                                    Function.InsertBitacoraErrores("MovimientoCaja/CreateApertura", MensajeError, "CreateApertura");
                                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                                    return View(tbMovimientoCaja);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                                return View(tbMovimientoCaja);
                                            }

                                        }
                                        Tran.Complete();
                                        return RedirectToAction("IndexApertura");
                                    }
                                }
                            }

                        }
                        catch (Exception Ex)
                        {
                            ///
                            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                            var suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                            ViewBag.UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_NombreUsuario).SingleOrDefault();
                            ViewBag.mocja_UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();
                            ///

                            Function.InsertBitacoraErrores("MovimientoCaja/CreateApertura", MensajeError, "CreateApertura");

                            //Usuario
                            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.usu_Id);
                            //Caja
                            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
                            ///Sucursal
                            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
                            ///Moneda
                            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);

                            Ex.Message.ToString();
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador." + Ex.Message);
                            return View(tbMovimientoCaja);
                        }
                    }

                    ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                    var suc_ID = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                    ViewBag.UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_NombreUsuario).SingleOrDefault();
                    ViewBag.mocja_UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();

                    //Usuario
                    ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.usu_Id);
                    ///Sucursal
                    ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
                    //Caja
                    ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
                    ///Moneda
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
                    return View(tbMovimientoCaja);
                }

                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
        ///Variable de Sesion 1
        [HttpPost]
        public JsonResult SaveSolicitudEfectivoDetalle(tbSolicitudEfectivoDetalle SolicitudEfectivoDet)
        {
            List<tbSolicitudEfectivoDetalle> sessionSolicitudEfectivoDetalle = new List<tbSolicitudEfectivoDetalle>();
            var list = (List<tbSolicitudEfectivoDetalle>)Session["SolicitudEfectivo"];
            if (list == null)
            {
                sessionSolicitudEfectivoDetalle.Add(SolicitudEfectivoDet);
                Session["SolicitudEfectivo"] = sessionSolicitudEfectivoDetalle;
            }
            else
            {
                list.Add(SolicitudEfectivoDet);
                Session["SolicitudEfectivo"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        /////Variable de Sesion 2
        [HttpPost]
        public JsonResult SaveSolicitudEfectivoMoneda(tbSolicitudEfectivo SolicitudEfectivoMon)
        {
            Session["SolicitudEfectivoMoneda"] = SolicitudEfectivoMon.mnda_Id;
            var Datos = Session["SolicitudEfectivoMoneda"];
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetDenominacion(int CodMoneda)
        {
            var list = db.spGetDenominacionesMoneda(CodMoneda).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRol(int Sucursal)
        {
            var list = db.UDP_Vent_tbUsuario_Rol_Apertura(Sucursal, Helpers.rol_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //Detalles
        [HttpGet]
        public ActionResult GetDetalle(short CodSolicitud)
        {
            var list = db.UDP_Vent_tbMovimientoCaja_Apertura_Details(CodSolicitud).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportReportApertura(int? id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "SolicitudEfectivo.rpt"));
            var tbSolicitudEfectivo = db.UDP_Vent_tbSolicitudEfectivo_Imprimir(id).ToList();
            var todo = (from r in tbSolicitudEfectivo
                        where r.solef_Id == id
                        select new
                        {
                            solef_Id = r.solef_Id,
                            cja_Descripcion = r.cja_Descripcion,
                            suc_Descripcion = r.suc_Descripcion,
                            mocja_Id = r.mocja_Id,
                            TipoSolicitus = r.TipoSolicitus,
                            UsuarioEntrega = r.UsuarioEntrega,
                            Cajero = r.Cajero,
                            mnda_Nombre = r.mnda_Nombre,
                            solef_FechaEntrega = DateTime.Parse(r.solef_FechaEntrega.ToString()).ToString("dd / MM / yyyy  hh: mm tt"),
                            MontoSolicitado = (decimal)r.MontoSolicitado,
                            deno_valor = (decimal)r.deno_valor,
                            deno_Descripcion = r.deno_Descripcion,
                            soled_CantidadSolicitada = r.soled_CantidadSolicitada,
                            MontoEntregado = (decimal)r.MontoEntregado,
                            soled_CantidadEntregada = r.soled_CantidadEntregada
                        }).ToList();
            // list = db.UDP_Vent_tbSolicitudEfectivo_Details(Solictud).ToList();
            rd.SetDataSource(todo);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf");
            }
            catch
            {
                throw;
            }
        }


        ////////////TERMINO APERTURA////////////
        ///Trae el Usuario Apertura para realizar el arqueo de caja
        [HttpPost]

        public JsonResult UsuarioApertura(int CodUsuario)
        {
            var list = db.UDP_Vent_tbMovimientoCaja_GetUsuarioApertura(CodUsuario).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        // GET: /MovimientoCaja/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbMovimientoCaja tbMovimientoCaja = db.tbMovimientoCaja.Find(id);
            if (tbMovimientoCaja == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbMovimientoCaja);
        }

        ///traer la caja que este arqueada
        [HttpPost]
        public JsonResult GetCaja(int CodUsuario)
        {
            var list = db.spGetCaja(CodUsuario).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        // GET: /MovimientoCaja/Create
        public ActionResult Create()
        {
            //ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");

            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
            ViewBag.DenominacionArqueo = db.tbDenominacionArqueo.ToList();

            int idUser = 0;
            List<tbUsuario> User = Function.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.usu_Id);
            }

            ViewBag.UsuarioArquea = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Nombres + " " + x.usu_Apellidos).SingleOrDefault();
            ViewBag.mocja_UsuarioArquea = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();


            DateTime Date = DateTime.Today;
            ViewBag.FechaApertura = db.tbMovimientoCaja.Where(x => x.usu_Id == idUser && x.mocja_FechaApertura == Date).Select(x => x.mocja_FechaApertura).SingleOrDefault();

            var CajaAperturada = db.tbMovimientoCaja.Where(x => x.usu_Id == idUser && x.mocja_FechaApertura == Date).ToList();
            if (CajaAperturada.Count() > 0)
            {
                ///Cantidad para cada tipo de pago registrado en el dia
                var PagoEfectivo = db.tbPago.Where(x => x.tpa_Id == 1 && x.pago_FechaElaboracion == Date).ToList();
                if (PagoEfectivo.Count() > 0)
                {
                    ViewBag.PagoEfectivo = db.tbPago.Where(x => x.tpa_Id == 1 && x.pago_FechaElaboracion == Date).Select(x => x.pago_TotalPago).Sum();
                }
                else
                {
                    ViewBag.PagoEfectivo = 00.0;
                }
                var PagoTC = db.tbPago.Where(x => x.tpa_Id == 2  && x.pago_FechaElaboracion == Date).ToList();
                if (PagoTC.Count() > 0)
                {
                    ViewBag.PagoTC = db.tbPago.Where(x => x.tpa_Id == 2 && x.pago_FechaElaboracion == Date && x.pago_EstaAnulado == false).Select(x => x.pago_TotalPago).Sum();
                }
                else
                {
                    ViewBag.PagoTC = 00.0;
                }

                var PagoCheque = db.tbPago.Where(x => x.tpa_Id == 3 &&x.pago_FechaElaboracion == Date).ToList();
                if (PagoCheque.Count() > 0)
                {
                    ViewBag.PagoCheque = db.tbPago.Where(x => x.tpa_Id == 3 && x.pago_FechaElaboracion == Date && x.pago_EstaAnulado == false).Select(x => x.pago_TotalPago).Sum();
                }
                else
                {
                    ViewBag.PagoCheque = 00.0;
                }

                var PagoNotaCredito = db.tbPago.Where(x => x.tpa_Id == 4 && x.pago_FechaElaboracion == Date).ToList();
                if (PagoNotaCredito.Count() > 0)
                {
                    ViewBag.PagoNotaCredito = db.tbPago.Where(x => x.tpa_Id == 4 && x.pago_FechaElaboracion == Date && x.pago_EstaAnulado == false).Select(x => x.pago_TotalPago).Sum();
                }
                else
                {
                    ViewBag.PagoNotaCredito = 00.0;
                }

                var PagoCuponDesc = db.tbPago.Where(x => x.tpa_Id == 5 && x.pago_FechaElaboracion == Date).ToList();
                if (PagoCuponDesc.Count() > 0)
                {
                    ViewBag.PagoCuponDesc = db.tbPago.Where(x => x.tpa_Id == 5 && x.pago_FechaElaboracion == Date && x.pago_EstaAnulado == false).Select(x => x.pago_TotalPago).Sum();
                }
                else
                {
                    ViewBag.PagoCuponDesc = 00.0;
                }

                var PagoDeposito = db.tbPago.Where(x => x.tpa_Id == 6 && x.pago_FechaElaboracion == Date).ToList();
                if (PagoDeposito.Count() > 0)
                {
                    ViewBag.PagoDeposito = db.tbPago.Where(x => x.tpa_Id == 6 && x.pago_FechaElaboracion == Date).Select(x => x.pago_TotalPago).Sum();
                }
                else
                {
                    ViewBag.PagoDeposito = 00.0;
                }

                var PagoTranferencia = db.tbPago.Where(x => x.tpa_Id == 7 && x.pago_FechaElaboracion == Date).ToList();
                if (PagoTranferencia.Count() > 0)
                {
                    ViewBag.PagoTranferencia = db.tbPago.Where(x => x.tpa_Id == 7 && x.pago_FechaElaboracion == Date && x.pago_EstaAnulado == false).Select(x => x.pago_TotalPago).Sum();
                }
                else
                {
                    ViewBag.PagoTranferencia = 0.00;
                }

                var TotalPagos = db.tbPago.Where(x => x.pago_FechaElaboracion == Date).ToList();
                if (TotalPagos.Count() > 0 )
                {
                    ViewBag.TotalPagos = db.tbPago.Where(x => x.pago_FechaElaboracion == Date).Select(x => x.pago_TotalPago).Sum();
                }
                else
                {
                    ViewBag.TotalPagos = 0.00;
                }

                //// Aqui termina-----------------------------------------------------------------------------------------------------------

                ////Efectivo Inicial
                var solef_Id = db.tbSolicitudEfectivo.Where(x => x.solef_EsApertura == true && x.solef_FechaEntrega == Date).Select(x => x.solef_Id).SingleOrDefault();
                if (solef_Id > 0)
                {
                    ViewBag.EfectivoInicial = db.tbSolicitudEfectivoDetalle.Where(x => x.solef_Id == solef_Id).Select(x => x.soled_MontoEntregado).Sum();
                }
                else
                {
                    ViewBag.EfectivoInicial = 0.00;
                }

                ////Efectivo Entregado
                var EfectivoEntregado = db.tbPago.ToList();
                if (EfectivoEntregado.Count() > 0)
                {
                    ViewBag.EfectivoEntregado = db.tbPago.Where(x => x.pago_FechaElaboracion == Date).Select(x => x.pago_TotalCambio).Sum();
                }
                else
                {
                    ViewBag.EfectivoEntregado = 0.00;
                }


            }
            ViewBag.Cajero = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Nombres + " " + x.usu_Apellidos).SingleOrDefault();
            ViewBag.usu_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();


            return View();


        }

        // POST: /MovimientoCaja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mocja_Id,cja_Id,mocja_FechaApetura,mocja_UsuarioApertura,usu_Id,mocja_FechaArqueo,mocja_UsuarioArquea,mocja_FechaAceptacion,mocja_UsuarioAceptacion,mocja_UsuarioCrea,mocja_FechaCrea,mocja_UsuarioModifica,mocja_FechaModifica")] tbMovimientoCaja tbMovimientoCaja)
        {
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
            ViewBag.deno_Id = new SelectList(db.tbDenominacionArqueo, "deno_Id", "deno_Descripcion", tbMovimientoCaja.cja_Id);
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = string.Empty;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbMovimientoCaja_Insert(tbMovimientoCaja.cja_Id,
                        tbMovimientoCaja.usu_Id,
                        tbMovimientoCaja.mocja_UsuarioApertura,
                        tbMovimientoCaja.mocja_FechaArqueo,
                        tbMovimientoCaja.mocja_UsuarioArquea,
                        tbMovimientoCaja.mocja_FechaAceptacion,
                        tbMovimientoCaja.mocja_UsuarioAceptacion,
                        Function.GetUser(),
                        Function.DatetimeNow());
                    foreach (UDP_Vent_tbMovimientoCaja_Insert_Result denoarq in list)
                        MensajeError = denoarq.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbMovimientoCaja);
                    }
                    else
                    {

                        return RedirectToAction("Index");
                    }
                }

                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");


                    return View(tbMovimientoCaja);
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            //if (ModelState.IsValid)
            //{
            //    db.tbMovimientoCaja.Add(tbMovimientoCaja);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioCrea);
            ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioModifica);
            ViewBag.deno_Id = new SelectList(db.tbDenominacionArqueo, "deno_Id", "deno_Descripcion", tbMovimientoCaja.cja_Id);
            return View(tbMovimientoCaja);
        }

        // GET: /MovimientoCaja/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbMovimientoCaja tbMovimientoCaja = db.tbMovimientoCaja.Find(id);
            if (tbMovimientoCaja == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioCrea);
            ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
            return View(tbMovimientoCaja);

            //////Efectivo Inicial
            //var solef_Id = db.tbSolicitudEfectivo.Where(x => x.solef_EsApertura == true && x.solef_FechaEntrega == Date && x.solef_EsAnulada == false).Select(x => x.solef_Id).SingleOrDefault();
            //if (solef_Id > 0)
            //{
            //    ViewBag.EfectivoInicial = db.tbSolicitudEfectivoDetalle.Where(x => x.solef_Id == solef_Id).Select(x => x.soled_MontoEntregado).Sum();
            //}
            //else
            //{
            //    ViewBag.EfectivoInicial = 0.00;
            //}

            //////Efectivo Entregado
            //var EfectivoEntregado = db.tbPago.ToList();
            //if (EfectivoEntregado.Count() > 0)
            //{
            //    ViewBag.EfectivoEntregado = db.tbPago.Where(x => x.pago_FechaElaboracion == Date).Select(x => x.pago_TotalCambio).Sum();
            //}
            //else
            //{
            //    ViewBag.EfectivoEntregado = 0.00;
            //}
        }

        // POST: /MovimientoCaja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mocja_Id,cja_Id,mocja_FechaApetura,mocja_UsuarioApertura,mocja_FechaArqueo,mocja_UsuarioArquea,mocja_FechaAceptacion,mocja_UsuarioAceptacion,mocja_UsuarioCrea,mocja_FechaCrea,mocja_UsuarioModifica,mocja_FechaModifica")] tbMovimientoCaja tbMovimientoCaja)
        {

            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
            ViewBag.deno_Id = new SelectList(db.tbDenominacionArqueo, "deno_Id", "deno_Descripcion", tbMovimientoCaja.cja_Id);
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = string.Empty;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbMovimientoCaja_Update(tbMovimientoCaja.mocja_Id, tbMovimientoCaja.mocja_UsuarioCrea, tbMovimientoCaja.mocja_FechaCrea,
                        Function.GetUser(),
                                    Function.DatetimeNow());
                    foreach (UDP_Vent_tbMovimientoCaja_Update_Result denoarq in list)
                        MensajeError = denoarq.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbMovimientoCaja);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");


                    return View(tbMovimientoCaja);
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            //if (ModelState.IsValid)
            //{
            //    db.Entry(tbMovimientoCaja).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioCrea);
            ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioModifica);
            ViewBag.deno_Id = new SelectList(db.tbDenominacionArqueo, "deno_Id", "deno_Descripcion", tbMovimientoCaja.cja_Id);
            return View(tbMovimientoCaja);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Denominaciones()
        {
            var list = db.UDP_Vent_tbDenominacionArqueo_Select().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        //---------------------Imprimición----------------------//

        public ActionResult ExportReport(int? id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ArqueoCaja.rpt"));
            var tbMovimientoCaja = db.UDP_Vent_tbMovimientoCaja_Imprimir(id).ToList();
            var todo = (from r in tbMovimientoCaja
                        where r.mocja_Id == id
                        select new
                        {
                            mocja_Id = r.mocja_Id,
                            cja_Descripcion = r.cja_Descripcion,
                            //suc_Descripcion = r.suc_Descripcion,
                            //Usuario_Apertura = r.Usuario_Apertura,
                            //Usuario_Arquea = r.Usuario_Arquea,
                            //Usuario_Aceptacion = r.Usuario_Aceptacion,

                            solef_FechaEntrega = DateTime.Parse(r.mocja_FechaApertura.ToString()).ToString("dd / MM / yyyy  hh: mm tt"),
                            mocja_FechaArqueo = DateTime.Parse(r.mocja_FechaArqueo.ToString()).ToString("dd / MM / yyyy  hh: mm tt"),
                            mocja_FechaAceptacion = DateTime.Parse(r.mocja_FechaAceptacion.ToString()).ToString("dd / MM / yyyy  hh: mm tt"),



                            deno_Descripcion = r.deno_Descripcion,
                            deno_valor = r.deno_valor,
                            arqde_CantidadDenominacion = r.arqde_CantidadDenominacion,
                            arqde_MontoDenominacion = r.arqde_MontoDenominacion,

                            tpa_Descripcion = r.tpa_Descripcion,
                            arqpg_PagosSistema = r.arqpg_PagosSistema,
                            arqpg_PagosConteo = r.arqpg_PagosConteo



                        }).ToList();

            rd.SetDataSource(todo);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}
