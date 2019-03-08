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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;
using ERP_GMEDINA.Dataset;
using ERP_GMEDINA.Reports;

namespace ERP_ZORZAL.Controllers
{
    public class PuntoEmisionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /PuntoEmision/
        public ActionResult Index()
        {
            var tbpuntoemision = db.tbPuntoEmision.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            
            //Reporte
            tbSucursal Sucursal = new tbSucursal();
            ViewBag.ReporteSucursal = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion", Sucursal.suc_Id);

            return View(tbpuntoemision.ToList());
        }

        // GET: /PuntoEmision/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbPuntoEmision tbPuntoEmision = db.tbPuntoEmision.Find(id);
            ViewBag.IdPuntoEmision = id;
            if (tbPuntoEmision == null)
            {
                return RedirectToAction("NotFound", "Login");
            }

            return View(tbPuntoEmision);
        }

        // GET: /PuntoEmision/Create
        public ActionResult Create()
        {
            //PuntoEmision
            tbPuntoEmision PuntoEmision = new tbPuntoEmision();
            ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", PuntoEmision.pemi_Id);

            //PuntoEmisionDetalle
            var _documentofiscal = db.tbDocumentoFiscal.Select(s => new
            {
                CodDocumentoFiscal = s.dfisc_Id,
                DescDocumentoFiscal = string.Concat(s.dfisc_Id + " - " + s.dfisc_Descripcion)
            }).ToList();
            ViewBag.DocumentoFiscal = new SelectList(_documentofiscal, "CodDocumentoFiscal", "DescDocumentoFiscal");

            //Vistas parciales
            ViewBag.PuntoEmisionDetalle = db.tbPuntoEmisionDetalle.ToList();
            
            Session["PuntoEmision"] = null;
            return View();
        }

        // POST: /PuntoEmision/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pemi_NumeroCAI,tbUsuario,tbUsuario1")] tbPuntoEmision tbPuntoEmision)
        {
            var list = (List<tbPuntoEmisionDetalle>)Session["PuntoEmision"];
            string MensajeError = "";
            var MensajeErrorDetalle = "";
            IEnumerable<object> listPuntoEmision = null;
            IEnumerable<object> listPuntoEmisionDetalle = null;
            var _documentofiscal = db.tbDocumentoFiscal.Select(s => new
            {
                CodDocumentoFiscal = s.dfisc_Id,
                DescDocumentoFiscal = string.Concat(s.dfisc_Id + " - " + s.dfisc_Descripcion)
            }).ToList();

            if (ModelState.IsValid)
            {

                try
                {
                    if (db.tbPuntoEmision.Any(a => a.pemi_NumeroCAI == tbPuntoEmision.pemi_NumeroCAI))
                    {
                        ViewBag.DocumentoFiscal = new SelectList(_documentofiscal, "CodDocumentoFiscal", "DescDocumentoFiscal");
                        ModelState.AddModelError("pemi_NumeroCAI", "Ya existe este Número CAI.");
                        return View(tbPuntoEmision);
                    }
                    else
                    {
                        using (TransactionScope Tran = new TransactionScope())
                        {
                            listPuntoEmision = db.UDP_Vent_tbPuntoEmision_Insert(
                                tbPuntoEmision.pemi_NumeroCAI,
                                Function.GetUser(),
                                Function.DatetimeNow()
                                );
                            foreach (UDP_Vent_tbPuntoEmision_Insert_Result PuntoEmisionL in listPuntoEmision)
                                MensajeError = PuntoEmisionL.MensajeError;
                            if (MensajeError.StartsWith("-1"))
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbPuntoEmision);
                            }
                            else
                            {
                                if (!MensajeError.StartsWith("-1"))
                                {
                                    if (list != null)
                                    {
                                        if (list.Count != 0)
                                        {
                                            foreach (tbPuntoEmisionDetalle Detalle in list)
                                            {
                                                var PuntoEmisionDetalle = Convert.ToInt32(MensajeError);
                                                Detalle.pemi_Id = PuntoEmisionDetalle;

                                                listPuntoEmisionDetalle = db.UDP_Vent_tbPuntoEmisionDetalle_Insert(
                                                    Detalle.pemi_Id,
                                                    Detalle.dfisc_Id,
                                                    Detalle.pemid_RangoInicio,
                                                    Detalle.pemid_RangoFinal,
                                                    Detalle.pemid_NumeroActual,
                                                    Detalle.pemid_FechaLimite,
                                                    Function.GetUser(),
                                                    Function.DatetimeNow()
                                                    );
                                                foreach (UDP_Vent_tbPuntoEmisionDetalle_Insert_Result SPpuntoemisiondet in listPuntoEmisionDetalle)
                                                {
                                                    MensajeErrorDetalle = SPpuntoemisiondet.MensajeError;
                                                    if (MensajeErrorDetalle.StartsWith("-1"))
                                                    {
                                                        ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                        return View(tbPuntoEmision);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "No se pudo agregar el registro");
                                    return View(tbPuntoEmision);
                                }
                            }
                            Tran.Complete();
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ViewBag.DocumentoFiscal = new SelectList(_documentofiscal, "CodDocumentoFiscal", "DescDocumentoFiscal");
                    ModelState.AddModelError("", "No se pudo agregar el registro");
                    return View(tbPuntoEmision);
                }
            }
            ViewBag.DocumentoFiscal = new SelectList(_documentofiscal, "CodDocumentoFiscal", "DescDocumentoFiscal");
            return View(tbPuntoEmision);
        }

        // GET: /PuntoEmision/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbPuntoEmision tbPuntoEmision = db.tbPuntoEmision.Find(id);
            ViewBag.IdPuntoEmisionEdit = id;
            if (tbPuntoEmision == null)
            {
                return RedirectToAction("NotFound", "Login");
            }

            //*****PuntoEmisionDetalle
            string cas = "dfisc_IdList_";
            var DocumentoFiscal = db.tbDocumentoFiscal.Select(s => new {
                dfisc_Id = s.dfisc_Id,
                dfisc_Descripcion = string.Concat(s.dfisc_Id + " - " + s.dfisc_Descripcion)
            }).ToList();
            System.Web.HttpContext.Current.Items[cas] = new SelectList(DocumentoFiscal, "dfisc_Id", "dfisc_Descripcion");

            var ValidacionRegistro = db.tbPuntoEmisionDetalle.Where(x => x.pemi_Id == tbPuntoEmision.pemi_Id).ToList();
            if (ValidacionRegistro.Count() > 0)
            {
                ViewBag.Validacion = "1";
            }
            return View(tbPuntoEmision);
        }

        // POST: /PuntoEmision/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "pemi_Id,pemi_NumeroCAI,pemi_UsuarioCrea,pemi_FechaCrea,pemi_UsuarioModifica,pemi_FechaModifica,tbUsuario,tbUsuario1")] tbPuntoEmision PuntoEmision)
        {
            string cas = "dfisc_IdList_";
            var DocumentoFiscal = db.tbDocumentoFiscal.Select(s => new {
                dfisc_Id = s.dfisc_Id,
                dfisc_Descripcion = string.Concat(s.dfisc_Id + " - " + s.dfisc_Descripcion)
            }).ToList();
          
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.tbPuntoEmision.Any(a => a.pemi_NumeroCAI == PuntoEmision.pemi_NumeroCAI))
                    {
                        System.Web.HttpContext.Current.Items[cas] = new SelectList(DocumentoFiscal, "dfisc_Id", "dfisc_Descripcion");
                        ViewBag.IdPuntoEmisionEdit = PuntoEmision.pemi_Id;
                        ModelState.AddModelError("", "Ya existe este Número CAI.");
                        return View(PuntoEmision);
                    }
                    else {
                        string MensajeError = "";
                        IEnumerable<object> list = null;
                        list = db.UDP_Vent_tbPuntoEmision_Update(
                            PuntoEmision.pemi_Id,
                            PuntoEmision.pemi_NumeroCAI,
                            PuntoEmision.pemi_UsuarioCrea,
                            PuntoEmision.pemi_FechaCrea,
                            Function.GetUser(),
                            Function.DatetimeNow());
                        foreach (UDP_Vent_tbPuntoEmision_Update_Result puntoemision in list)
                            MensajeError = puntoemision.MensajeError;
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                            return View(PuntoEmision);
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Items[cas] = new SelectList(DocumentoFiscal, "dfisc_Id", "dfisc_Descripcion");       
                            return RedirectToAction("Edit/" + PuntoEmision.pemi_Id);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    System.Web.HttpContext.Current.Items[cas] = new SelectList(DocumentoFiscal, "dfisc_Id", "dfisc_Descripcion");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return View(PuntoEmision);
                }
            }
            System.Web.HttpContext.Current.Items[cas] = new SelectList(DocumentoFiscal, "dfisc_Id", "dfisc_Descripcion");
            return View(PuntoEmision);
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
        public JsonResult SavePuntoEmisionDetalle(tbPuntoEmisionDetalle PuntoEmisionDet)
        {
            List<tbPuntoEmisionDetalle> sessionPuntoEmisionDetalle = new List<tbPuntoEmisionDetalle>();
            var list = (List<tbPuntoEmisionDetalle>)Session["PuntoEmision"];
            if (list == null)
            {
                sessionPuntoEmisionDetalle.Add(PuntoEmisionDet);
                Session["PuntoEmision"] = sessionPuntoEmisionDetalle;
            }
            else
            {
                list.Add(PuntoEmisionDet);
                Session["PuntoEmision"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemovePuntoEmisionDetalle(tbPuntoEmisionDetalle PuntoEmisionDet)
        {
            var list = (List<tbPuntoEmisionDetalle>)Session["PuntoEmision"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.pemid_Id == PuntoEmisionDet.pemid_Id);
                list.Remove(itemToRemove);
                Session["PuntoEmision"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveEditNumeracion (tbPuntoEmisionDetalle EditPuntoEmisionDetalle)
        {
            string MensajeEdit = "";
            
            try
                {
                        var MensajeError = "";
                        IEnumerable<object> list = null;
                        list = db.UDP_Vent_tbPuntoEmisionDetalle_Update(
                                    EditPuntoEmisionDetalle.pemid_Id,
                                    EditPuntoEmisionDetalle.dfisc_Id,
                                    EditPuntoEmisionDetalle.pemid_RangoInicio,
                                    EditPuntoEmisionDetalle.pemid_RangoFinal,
                                    EditPuntoEmisionDetalle.pemid_NumeroActual,
                                    EditPuntoEmisionDetalle.pemid_FechaLimite,
                                    EditPuntoEmisionDetalle.pemid_UsuarioCrea,
                                    EditPuntoEmisionDetalle.pemid_FechaCrea,
                                    Function.GetUser(),
                                    Function.DatetimeNow());
                        foreach (UDP_Vent_tbPuntoEmisionDetalle_Update_Result puntoemisiondetalle in list)
                            MensajeError = puntoemisiondetalle.MensajeError;
                if (MensajeError.StartsWith("-1"))
                {
                    MensajeEdit = "No se pudo actualizar el registro, favor contacte al administrador.";
                    ModelState.AddModelError("", MensajeEdit);
                }
                else
                {
                    MensajeEdit = "El registro se guardó exitosamente";
                }
               }
               catch (Exception Ex)
               {
                    MensajeEdit = Ex.Message.ToString();
                    ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", EditPuntoEmisionDetalle.dfisc_Id);
                    ModelState.AddModelError("", MensajeEdit);
                }
            return Json(MensajeEdit, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult SaveCreateNumeracion(tbPuntoEmisionDetalle CreatePuntoEmisionDetalle)
        {
            string Msj = "";

            try
            {
                string MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Vent_tbPuntoEmisionDetalle_Insert(
                            CreatePuntoEmisionDetalle.pemi_Id,
                            CreatePuntoEmisionDetalle.dfisc_Id,
                            CreatePuntoEmisionDetalle.pemid_RangoInicio,
                            CreatePuntoEmisionDetalle.pemid_RangoFinal,
                            CreatePuntoEmisionDetalle.pemid_NumeroActual,
                            CreatePuntoEmisionDetalle.pemid_FechaLimite,
                            Function.GetUser(),
                            Function.DatetimeNow());
                foreach (UDP_Vent_tbPuntoEmisionDetalle_Insert_Result puntoemisiondetalle in list)
                    MensajeError = puntoemisiondetalle.MensajeError;
                if (MensajeError.StartsWith("-1"))
                {
                    Msj = "No se pudo guardar el registro, favor contacte al administrador.";
                    ModelState.AddModelError("", Msj);
                }
                else
                {
                    Msj = "El registro se guardó exitosamente";
                }
            }
            catch (Exception Ex)
            {
                Msj = Ex.Message.ToString();
                ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", CreatePuntoEmisionDetalle.dfisc_Id);
                ModelState.AddModelError("", Msj);
            }
            return Json(Msj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Reporte(tbObjeto Objeto)
        {

            int iTipoReporte = Objeto.obj_Id;
            //var list = db.SDP_Acce_GetReportes().ToList();
            ReportDocument rd = new ReportDocument();
            Stream stream = null;
            rptInventarioNumeraciones SalidaRV = new rptInventarioNumeraciones();
            Reportes SalidaDST = new Reportes();

            var SalidaTableAdapter = new UDV_Vent_InventarioNumeracionesTableAdapter();

            try
            {
                SalidaTableAdapter.FillFiltros(SalidaDST.UDV_Vent_InventarioNumeraciones, 1);

                SalidaRV.SetDataSource(SalidaDST);
                stream = SalidaRV.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                SalidaRV.Close();
                SalidaRV.Dispose();

                string fileName = "Salida_List.pdf";
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
