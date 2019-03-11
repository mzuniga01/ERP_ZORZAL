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
using ERP_GMEDINA.Dataset; using ERP_GMEDINA.Reports; using ERP_GMEDINA.Dataset.ReportesTableAdapters;
namespace ERP_GMEDINA.Controllers
{
    public class NotaCreditoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /NotaCredito/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("NotaCredito/Index"))
                    {
                        var tbnotacredito = db.tbNotaCredito.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbDevolucion).Include(t => t.tbSucursal);
                        return View(tbnotacredito.ToList());
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public ActionResult _IndexDevolucion(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            if (tbDevolucion == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbDevolucion);
        }


        // GET: /NotaCredito/Details/5
        public ActionResult Details(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("NotaCredito/Details"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
                        if (tbNotaCredito == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        return View(tbNotaCredito);
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /NotaCredito/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("NotaCredito/Create"))
                    {
                        int idUser = 0;
                        GeneralFunctions Login = new GeneralFunctions();
                        List<tbUsuario> User = Login.getUserInformation();
                        foreach (tbUsuario Usuario in User)
                        {
                            idUser = Convert.ToInt32(Usuario.emp_Id);
                        }
                        ViewBag.usu_Id = idUser;
                        ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                        ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                        ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
                        ViewBag.Cliente = db.tbCliente.ToList();
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // POST: /NotaCredito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nocre_Id,nocre_Codigo,dev_Id,clte_Id,suc_Id,cja_Id,nocre_Anulado,nocre_FechaEmision,nocre_MotivoEmision,nocre_Monto,nocre_Redimido,nocre_FechaRedimido,nocre_EsImpreso,nocre_UsuarioCrea,nocre_FechaCrea,nocre_UsuarioModifica,nocre_FechaModifica")] tbNotaCredito tbNotaCredito)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("CuponDescuento/Create"))
                    {
                        int idUser = 0;
                        GeneralFunctions Login = new GeneralFunctions();
                        List<tbUsuario> User = Login.getUserInformation();
                        foreach (tbUsuario Usuario in User)
                        {
                            idUser = Convert.ToInt32(Usuario.emp_Id);
                        }
                        var MensajeError = "";
                        IEnumerable<object> list = null;
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                list = db.UDP_Vent_tbNotaCredito_Insert(tbNotaCredito.nocre_Codigo,
                                        tbNotaCredito.dev_Id,
                                        tbNotaCredito.clte_Id,
                                        tbNotaCredito.suc_Id,
                                        tbNotaCredito.cja_Id,
                                        tbNotaCredito.nocre_Anulado,
                                        tbNotaCredito.nocre_FechaEmision,
                                        tbNotaCredito.nocre_MotivoEmision,
                                        tbNotaCredito.nocre_Monto,
                                        tbNotaCredito.nocre_Redimido,
                                        tbNotaCredito.nocre_FechaRedimido,
                                        tbNotaCredito.nocre_EsImpreso,
                                        Function.GetUser(),
                                        Function.DatetimeNow());
                                foreach (UDP_Vent_tbNotaCredito_Insert_Result NotaCredito in list)
                                    MensajeError = NotaCredito.MensajeError;
                                if (MensajeError == "-1")
                                {
                                    ModelState.AddModelError("", "No se pudo Insertar el registro, favor contacte al administrador.");
                                    return View(tbNotaCredito);
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
                                return View(tbNotaCredito);
                            }
                        }
                        ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
                        ViewBag.Cliente = db.tbCliente.ToList();
                        ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                        ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                        return View(tbNotaCredito);
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /NotaCredito/Edit/5
        public ActionResult Edit(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("NotaCredito/Edit"))
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
                        tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
                        if (tbNotaCredito == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        ViewBag.clte_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbNotaCredito.cja_Id);
                        ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbNotaCredito.clte_Id);
                        ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Id", tbNotaCredito.dev_Id);
                        ViewBag.Cliente = db.tbCliente.ToList();
                        ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
                        return View(tbNotaCredito);
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // POST: /NotaCredito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nocre_Id,nocre_Codigo,dev_Id,clte_Id,suc_Id,cja_Id,nocre_Anulado,nocre_FechaEmision,nocre_MotivoEmision,nocre_Monto,nocre_Redimido,nocre_FechaRedimido,nocre_EsImpreso,nocre_UsuarioCrea,nocre_FechaCrea,nocre_UsuarioModifica,nocre_FechaModifica, tbUsuario, tbUsuario1")] tbNotaCredito tbNotaCredito)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("CuponDescuento/Edit"))
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                var MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbNotaCredito_Update(tbNotaCredito.nocre_Id, tbNotaCredito.nocre_Codigo,
                                    tbNotaCredito.dev_Id, tbNotaCredito.clte_Id, tbNotaCredito.suc_Id, tbNotaCredito.cja_Id, tbNotaCredito.nocre_Anulado,
                                    tbNotaCredito.nocre_FechaEmision, tbNotaCredito.nocre_MotivoEmision, tbNotaCredito.nocre_Monto,
                                    tbNotaCredito.nocre_Redimido, tbNotaCredito.nocre_FechaRedimido, tbNotaCredito.nocre_EsImpreso,
                                    tbNotaCredito.nocre_UsuarioCrea, tbNotaCredito.nocre_FechaCrea, Function.GetUser(),
                                                Function.DatetimeNow());
                                foreach (UDP_Vent_tbNotaCredito_Update_Result NotaCredito in list)
                                    MensajeError = Convert.ToString(NotaCredito.MensajeError);
                                if (MensajeError == "-1")
                                {
                                    ModelState.AddModelError("", "No se pudo Editar el registro, favor contacte al administrador.");
                                    return View(tbNotaCredito);
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                            catch (Exception Ex)
                            {
                                Ex.Message.ToString();
                                ModelState.AddModelError("", "No se pudo Editar el registro, favor contacte al administrador.");
                                ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
                                ViewBag.Cliente = db.tbCliente.ToList();
                                return View(tbNotaCredito);
                            }
                        }
                        var motivo = tbNotaCredito.nocre_MotivoEmision;
                        if (motivo == null)
                        {
                            ModelState.AddModelError("", "No se pudo Editar el registro, Campos Requeridos.");
                            return RedirectToAction("Edit");
                        }
                        else
                        {
                            ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
                            ViewBag.Cliente = db.tbCliente.ToList();
                            return View(tbNotaCredito);
                        }
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
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
        public JsonResult GetCaja(int CodUsuario)
        {
            var list = db.spGetCaja(CodUsuario).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AnularNotaCredito(Int16 nocreId, bool Anulado)
        {
            var list = db.UDP_Vent_tbNotaCredito_Anulado(nocreId, Anulado).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BuscarDevolucion()
        {
            var list = db.UDP_Vent_tbNotaCredito_Select().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetCodigoNotaCredito(int CodSucursal, short CodCaja)
        {
            var list = db.UDP_Vent_tbNotaCredito_CodigoNotaCredito(CodSucursal, CodCaja).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //---------------------Imprimir Cliente Natural por Devolucion-----------------------------
        public ActionResult ExportReportclteNaturalxDEV(short? id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ImprimirNotaCreditoclteNaturalxDEV.rpt"));
            var tbnotacredito = db.UDP_Vent_tbNotaCredito_Imprimir(id).ToList();
            var todo = (from NC in tbnotacredito
                        where NC.nocre_Id == id
                        select new
                        {
                            nocre_Id = NC.nocre_Id,
                            nocre_Codigo = NC.nocre_Codigo,
                            dev_Id = NC.dev_Id,
                            clte_Identificacion = NC.clte_Identificacion,
                            clte_Nombres = NC.clte_Nombres,
                            clte_Apellidos = NC.clte_Apellidos,
                            suc_Descripcion = NC.suc_Descripcion,
                            suc_Correo = NC.suc_Correo,
                            suc_Direccion = NC.suc_Direccion,
                            suc_Telefono = NC.suc_Telefono,
                            nocre_FechaEmision = NC.nocre_FechaEmision,
                            nocre_MotivoEmision = NC.nocre_MotivoEmision,
                            devd_Monto = (Decimal)NC.devd_Monto
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

        //---------------------Imprimir Cliente Natural por Otros-----------------------------
        public ActionResult ExportReportclteNaturalxOtros(short? id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ImprimirNotaCreditoclteNaturalxOtros.rpt"));
            var tbnotacredito = db.UDP_Vent_tbNotaCredito_Imprimir(id).ToList();
            var todo = (from NC in tbnotacredito
                        where NC.nocre_Id == id
                        select new
                        {
                            nocre_Id = NC.nocre_Id,
                            nocre_Codigo = NC.nocre_Codigo,
                            clte_Identificacion = NC.clte_Identificacion,
                            clte_Nombres = NC.clte_Nombres,
                            clte_Apellidos = NC.clte_Apellidos,
                            suc_Descripcion = NC.suc_Descripcion,
                            suc_Correo = NC.suc_Correo,
                            suc_Direccion = NC.suc_Direccion,
                            suc_Telefono = NC.suc_Telefono,
                            nocre_FechaEmision = NC.nocre_FechaEmision,
                            nocre_MotivoEmision = NC.nocre_MotivoEmision,
                            nocre_Monto = (Decimal)NC.nocre_Monto
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
        
        //---------------------Imprimir Cliente Juridico por Devolucion-----------------------------
        public ActionResult ExportReportclteJuridicoxDEV(short? id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ImprimirNotaCreditoclteJuridicoxDEV.rpt"));
            var tbnotacredito = db.UDP_Vent_tbNotaCredito_Imprimir(id).ToList();
            var todo = (from NC in tbnotacredito
                        where NC.nocre_Id == id
                        select new
                        {
                            nocre_Id = NC.nocre_Id,
                            nocre_Codigo = NC.nocre_Codigo,
                            dev_Id = NC.dev_Id,
                            clte_Identificacion = NC.clte_Identificacion,
                            clte_NombreComercial = NC.clte_NombreComercial,
                            suc_Descripcion = NC.suc_Descripcion,
                            suc_Correo = NC.suc_Correo,
                            suc_Direccion = NC.suc_Direccion,
                            suc_Telefono = NC.suc_Telefono,
                            nocre_FechaEmision = NC.nocre_FechaEmision,
                            nocre_MotivoEmision = NC.nocre_MotivoEmision,
                            devd_Monto = (Decimal)NC.devd_Monto
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

        //---------------------Imprimir Cliente Juridico por Otros-----------------------------
        public ActionResult ExportReportclteJuridicoxOtros(short? id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ImprimirNotaCreditoclteNaturalxOtros.rpt"));
            var tbnotacredito = db.UDP_Vent_tbNotaCredito_Imprimir(id).ToList();
            var todo = (from NC in tbnotacredito
                        where NC.nocre_Id == id
                        select new
                        {
                            nocre_Id = NC.nocre_Id,
                            nocre_Codigo = NC.nocre_Codigo,
                            clte_Identificacion = NC.clte_Identificacion,
                            clte_NombreComercial = NC.clte_NombreComercial,
                            suc_Descripcion = NC.suc_Descripcion,
                            suc_Correo = NC.suc_Correo,
                            suc_Direccion = NC.suc_Direccion,
                            suc_Telefono = NC.suc_Telefono,
                            nocre_FechaEmision = NC.nocre_FechaEmision,
                            nocre_MotivoEmision = NC.nocre_MotivoEmision,
                            nocre_Monto = (Decimal)NC.nocre_Monto
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

        //Autorizar Nota Credito por Otros
        public JsonResult AprobarDescuento(string User, string Password)
        {
            var list = db.UDP_Vent_tbSolicitudCredito_RolGerenteCreditosCobranza(User, Password).SingleOrDefault();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        ///----------------Reporte de Cupon de Descuento------------------------------

        [HttpPost]
        public ActionResult Reporte(tbObjeto Objeto)
        {
            int iTipoReporte = Objeto.obj_Id;
            ReportDocument rd = new ReportDocument();
            Stream stream = null;
            NotaCredito NTCreditoRV = new NotaCredito();
            Reportes NotaCreditoDST = new Reportes();

            var CreditoTableAdapter = new UDV_Vent_NotaCreditoPorFechaTableAdapter();

            try
            {
                CreditoTableAdapter.FillFiltros(NotaCreditoDST.UDV_Vent_NotaCreditoPorFecha, "EDU", Convert.ToDateTime("2019-03-10"), Convert.ToDateTime("2019-03-10"));

                NTCreditoRV.SetDataSource(NotaCreditoDST);
                stream = NTCreditoRV.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                NTCreditoRV.Close();
                NTCreditoRV.Dispose();

                string fileName = "Nota_Credito.pdf";
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