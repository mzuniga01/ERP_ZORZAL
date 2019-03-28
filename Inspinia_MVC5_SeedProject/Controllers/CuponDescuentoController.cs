using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Transactions;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Reflection;
using CrystalDecisions.Shared;
using ERP_GMEDINA.Dataset;
using ERP_GMEDINA.Reports;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class CuponDescuentoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /CuponDescuento/
        [SessionManager("CuponDescuento/Index")]
        public ActionResult Index()
        {
            var tbcupondescuento = db.tbCuponDescuento.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbSucursal);
            return View(tbcupondescuento.ToList());
        }

        // GET: /CuponDescuento/Details/5
        [SessionManager("CuponDescuento/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbCuponDescuento tbCuponDescuento = db.tbCuponDescuento.Find(id);
            if (tbCuponDescuento == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbCuponDescuento);
        }

        // GET: /CuponDescuento/Create
        [SessionManager("CuponDescuento/Create")]
        public ActionResult Create()
        {
            int idUser = 0;
            List<tbUsuario> User = Function.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }
            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
            ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("CuponDescuento/Create")]
        public ActionResult Create([Bind(Include = "cdto_ID,suc_Id,cdto_FechaEmision,cdto_FechaVencimiento,cdto_PorcentajeDescuento,cdto_MontoDescuento,cdto_MaximoMontoDescuento,cdto_CantidadCompraMinima,cdto_Redimido,cdto_FechaRedencion,cdto_Anulado,cdto_RazonAnulado,cdto_EsImpreso,cdto_UsuarioCrea,cdto_FechaCrea,cdto_UsuarioModifica,cdto_FechaModifica")] tbCuponDescuento tbCuponDescuento)
        {
            int idUser = 0;
            List<tbUsuario> User = Function.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbCuponDescuento_Insert(tbCuponDescuento.suc_Id,
                            tbCuponDescuento.cdto_FechaEmision,
                            tbCuponDescuento.cdto_FechaVencimiento,
                            tbCuponDescuento.cdto_PorcentajeDescuento,
                            tbCuponDescuento.cdto_MontoDescuento,
                            tbCuponDescuento.cdto_MaximoMontoDescuento,
                            tbCuponDescuento.cdto_CantidadCompraMinima,
                            tbCuponDescuento.cdto_Redimido,
                            tbCuponDescuento.cdto_FechaRedencion,
                            tbCuponDescuento.cdto_Anulado,
                            tbCuponDescuento.cdto_RazonAnulado,
                            tbCuponDescuento.cdto_EsImpreso,
                            Function.GetUser(),
                            Function.DatetimeNow());
                    foreach (UDP_Vent_tbCuponDescuento_Insert_Result CuponDescuento in list)
                        MensajeError = CuponDescuento.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                        ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                        Function.InsertBitacoraErrores("CuponDescuento/Create", MensajeError, "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbCuponDescuento);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                    ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                    Function.InsertBitacoraErrores("CuponDescuento/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbCuponDescuento);
                }
            }
            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
            ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            return View(tbCuponDescuento);
        }

        // GET: /CuponDescuento/Edit/5
        [SessionManager("CuponDescuento/Edit")]
        public ActionResult Edit(int? id)
        {
            int idUser = 0;
            GeneralFunctions Login = new GeneralFunctions();
            List<tbUsuario> User = Login.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }
            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
            ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbCuponDescuento tbCuponDescuento = db.tbCuponDescuento.Find(id);
            if (tbCuponDescuento == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbCuponDescuento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("CuponDescuento/Edit")]
        public ActionResult Edit([Bind(Include = "cdto_ID,suc_Id,cdto_FechaEmision,cdto_FechaVencimiento,cdto_PorcentajeDescuento,cdto_MontoDescuento,cdto_MaximoMontoDescuento,cdto_CantidadCompraMinima,cdto_Redimido,cdto_FechaRedencion, cdto_Anulado,cdto_EsImpreso,cdto_UsuarioCrea,cdto_FechaCrea,cdto_UsuarioModifica,cdto_FechaModifica, tbUsuario, tbUsuario1")] tbCuponDescuento tbCuponDescuento)
        {
                        int idUser = 0;
                        GeneralFunctions Login = new GeneralFunctions();
                        List<tbUsuario> User = Login.getUserInformation();
                        foreach (tbUsuario Usuario in User)
                        {
                            idUser = Convert.ToInt32(Usuario.emp_Id);
                        }
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                var MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbCuponDescuento_Update(tbCuponDescuento.cdto_ID,
                                    tbCuponDescuento.suc_Id,
                                    tbCuponDescuento.cdto_FechaEmision,
                                    tbCuponDescuento.cdto_FechaVencimiento,
                                    tbCuponDescuento.cdto_PorcentajeDescuento,
                                    tbCuponDescuento.cdto_MontoDescuento,
                                    tbCuponDescuento.cdto_MaximoMontoDescuento,
                                    tbCuponDescuento.cdto_CantidadCompraMinima,
                                    tbCuponDescuento.cdto_Redimido,
                                    tbCuponDescuento.cdto_FechaRedencion,
                                    tbCuponDescuento.cdto_Anulado,
                                    tbCuponDescuento.cdto_EsImpreso,
                                    tbCuponDescuento.cdto_UsuarioCrea,
                                    tbCuponDescuento.cdto_FechaCrea,
                                    Function.GetUser(),
                                    Function.DatetimeNow());
                                foreach (UDP_Vent_tbCuponDescuento_Update_Result CuponDescuento in list)
                                    MensajeError = CuponDescuento.MensajeError;
                                if (MensajeError.StartsWith("-1"))
                                {
                                    ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                                    ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                                    Function.InsertBitacoraErrores("CuponDescuento/Create", MensajeError, "Create");
                                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                    return View(tbCuponDescuento);
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                            catch (Exception Ex)
                            {
                                ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                                ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                                Function.InsertBitacoraErrores("CuponDescuento/Create", Ex.Message.ToString(), "Create");
                                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                return View(tbCuponDescuento);
                            }

                        }
                        ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                        ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                        return View(tbCuponDescuento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //--------------------------Anular Actualiza DB---------------------------------
        [HttpPost]
        public JsonResult AnularCuponDescuento(int cdtoId, bool Anulada, string RazonAnular)
        {
            var list = db.UDP_Vent_tbCuponDescuento_Anulado(cdtoId, Anulada, RazonAnular).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        //--------------------------------Imprimir Actualiza DB-------------------------
        [HttpPost]
        public JsonResult CuponEsImpreso(int cdtoId, bool EsImpreso)
        {
            var list = db.UDP_Vent_tbCuponDescuento_EsImpreso(cdtoId, EsImpreso).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        ///----------------Reporte de Cupon de Descuento------------------------------

        [HttpPost]
        public ActionResult Reporte(tbObjeto Objeto)
        {
            int iTipoReporte = Objeto.obj_Id;
            ReportDocument rd = new ReportDocument();
            Stream stream = null;
            CuponDescuento CuponRV = new CuponDescuento();
            Reportes CuponDescuentoDST = new Reportes();

            var CuponTableAdapter = new UDV_Vent_CuponDescuentoPorFechaTableAdapter();
             
            try
            {
                CuponTableAdapter.FillFiltros(CuponDescuentoDST.UDV_Vent_CuponDescuentoPorFecha, "2019-01-18", "2019-01-18");

                CuponRV.SetDataSource(CuponDescuentoDST);
                stream = CuponRV.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                CuponRV.Close();
                CuponRV.Dispose();

                string fileName = "cupon_descuento.pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(stream, "application/pdf");
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                throw;
            }
        }

        //----------------------Report de Imprimir PDF-----------------------
        public ActionResult ExportReport(int? id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ImprimirCuponDescuento.rpt"));
            var tbcupondescuento = db.UDP_Vent_tbCuponDescuento_Imprimir(id).ToList();
            var todo = (from CD in tbcupondescuento
                        where CD.cdto_ID == id
                        select new
                        {
                            cdto_ID = CD.cdto_ID,
                            cdto_FechaEmision = CD.cdto_FechaEmision,
                            suc_Telefono = CD.cdto_CantidadCompraMinima,
                            cdto_FechaVencimiento = CD.cdto_FechaVencimiento,
                            cdto_PorcentajeDescuento = (Decimal)CD.cdto_PorcentajeDescuento,
                            cdto_MaximoMontoDescuento = (Decimal)CD.cdto_MaximoMontoDescuento,
                            cdto_MontoDescuento = (Decimal)CD.cdto_MontoDescuento,
                            cdto_CantidadCompraMinima = (Decimal)CD.cdto_CantidadCompraMinima,
                            suc_Correo = CD.suc_Correo,
                            suc_Descripcion = CD.suc_Descripcion
                            }).ToList();
            rd.SetDataSource(todo);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                var list = db.UDP_Vent_tbCuponDescuento_EsImpreso(id, Helpers.EsImpreso).ToList();
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