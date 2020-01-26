using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using CrystalDecisions.CrystalReports.Engine;
using ERP_GMEDINA.Reports;
using ERP_GMEDINA.Dataset;
using System.IO;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class ExoneracionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        public ActionResult ClientesnoExonerado()
        {
            return View(db.UDP_Vent_listExoneracion_Select);
        }

        // GET: /Exoneracion/
        [SessionManager("Exoneracion/Index")]
        public ActionResult Index()
        {
            var tbexoneracion = db.tbExoneracion.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente);
            return View(tbexoneracion.ToList());
        }

        // GET: /Exoneracion/Details/5
        [SessionManager("Exoneracion/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
            if (tbExoneracion == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbExoneracion);
        }

        // GET: /Exoneracion/Create
        [SessionManager("Exoneracion/Create")]
        public ActionResult Create()
        {
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
            return View();
        }

        // POST: /Exoneracion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Exoneracion/Create")]
        public ActionResult Create([Bind(Include = "exo_Id,exo_Documento,exo_ExoneracionActiva,exo_FechaInicialVigencia,exo_FechaIFinalVigencia,clte_Id,exo_UsuarioCrea,exo_FechaCrea,exo_UsuarioModifa,exo_FechaModifica")] tbExoneracion tbExoneracion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbExoneracion_Insert(tbExoneracion.exo_Documento,
                                                            Helpers.ExoneracionActiva,
                                                            tbExoneracion.exo_FechaInicialVigencia,
                                                            tbExoneracion.exo_FechaIFinalVigencia,
                                                            tbExoneracion.clte_Id,
                                                            Function.GetUser(),
                                                            Function.DatetimeNow());
                    foreach (UDP_Vent_tbExoneracion_Insert_Result Exoneracion in list)
                        MensajeError = Exoneracion.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                        ViewBag.Cliente = db.tbCliente.ToList();
                        ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                        Function.InsertBitacoraErrores("Exoneracion/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbExoneracion);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                ViewBag.Cliente = db.tbCliente.ToList();
                ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                return View(tbExoneracion);
            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("Exoneracion/Create", Ex.Message.ToString(), "Create");
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                ViewBag.Cliente = db.tbCliente.ToList();
                ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                return View(tbExoneracion);
            }
        }

        // GET: /Exoneracion/Edit/5
        [SessionManager("Exoneracion/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbExoneracion tbExoneracion = db.tbExoneracion.Find(id);
            if (tbExoneracion == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.exo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioCrea);
            ViewBag.exo_UsuarioModifa = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbExoneracion.exo_UsuarioModifa);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
            return View(tbExoneracion);
        }

        // POST: /Exoneracion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Exoneracion/Edit")]
        public ActionResult Edit(int? id, [Bind(Include = "exo_Id,exo_Documento,exo_ExoneracionActiva,exo_FechaInicialVigencia,exo_FechaIFinalVigencia,clte_Id,exo_UsuarioCrea,exo_FechaCrea")] tbExoneracion tbExoneracion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tbExoneracion pExoneracion = db.tbExoneracion.Find(id);
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbExoneracion_Update(tbExoneracion.exo_Id,
                                                            tbExoneracion.exo_Documento,
                                                            pExoneracion.exo_ExoneracionActiva,
                                                            tbExoneracion.exo_FechaInicialVigencia,
                                                            tbExoneracion.exo_FechaIFinalVigencia,
                                                            tbExoneracion.clte_Id,
                                                            pExoneracion.exo_UsuarioCrea,
                                                            pExoneracion.exo_FechaCrea, 
                                                            Function.GetUser(),
                                                            Function.DatetimeNow());
                    foreach (UDP_Vent_tbExoneracion_Update_Result Exoneracion in list)
                        MensajeError = Exoneracion.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                        ViewBag.Cliente = db.tbCliente.ToList();
                        ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                        Function.InsertBitacoraErrores("Exoneracion/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbExoneracion);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                ViewBag.Cliente = db.tbCliente.ToList();
                ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                return View(tbExoneracion);
            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("Exoneracion/Create", Ex.Message.ToString(), "Create");
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbExoneracion.clte_Id);
                ViewBag.Cliente = db.tbCliente.ToList();
                ViewBag.noExonerado = db.UDP_Vent_listExoneracion_Select.ToList();
                return View(tbExoneracion);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
        [HttpPost]
        public JsonResult InactivarCliente(int CodExoneracion, bool Activo)
        {
            var list = db.UDP_Vent_tbExoneracion_Estado(CodExoneracion, Helpers.ExoneracionInactiva, Function.GetUser(), Function.DatetimeNow()).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActivarCliente(int CodExoneracion, bool Activo)
        {
            var list = db.UDP_Vent_tbExoneracion_Estado(CodExoneracion, Helpers.ExoneracionActiva, Function.GetUser(), Function.DatetimeNow()).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public int GetUsuarioMetodo()
        {
            int idUser = 0;
            try
            {
                List<tbUsuario> User = Function.getUserInformation();
                foreach (tbUsuario Usuario in User)
                {
                    idUser = Convert.ToInt32(Usuario.usu_Id);
                }
                return idUser;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return 0;
            }
        }

        [HttpPost]
        public ActionResult Reporte(tbObjeto Objeto/*, int clte_Id, bool clte_EsPersonaNatural*/)
        {

            string Fecha_factura = "2019-03-15";
            string clte_Identificacion = "0401199800256";
            string fact_Codigo = "2";
            int iTipoReporte = Objeto.obj_Id;
            var list = db.SDP_Acce_GetReportes().ToList();
            var GetUsuario = GetUsuarioMetodo();
            var UsuarioName = db.tbUsuario.Where(x => x.usu_Id == GetUsuario).Select(i => new { i.usu_Nombres, i.usu_Apellidos }).FirstOrDefault();
            ReportDocument rd = new ReportDocument();
            Stream stream = null;
            rptVentasExoneradas rptVentasE = new rptVentasExoneradas();
            Reportes ReporteVentas = new Reportes();

            var ReporteVentasExoneradasTableAdapter = new UDV_Vent_VentasExoneradasTableAdapter();

            try
            {
                ReporteVentasExoneradasTableAdapter.FillFiltros(ReporteVentas.UDV_Vent_VentasExoneradas,Fecha_factura,clte_Identificacion,fact_Codigo);

                rptVentasE.SetDataSource(ReporteVentas);
                rptVentasE.SetParameterValue("usuario", UsuarioName.usu_Nombres + " " + UsuarioName.usu_Apellidos);

                stream = rptVentasE.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                rptVentasE.Close();
                rptVentasE.Dispose();


                string fileName = "Reporte_PedidoEntreFechas.pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(stream, "application/pdf");
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                throw;
            }
        }
    }
}
