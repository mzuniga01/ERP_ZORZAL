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

namespace ERP_GMEDINA.Controllers
{
    public class CuponDescuentoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /CuponDescuento/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("CuponDescuento/Index"))
                    {
                        var tbcupondescuento = db.tbCuponDescuento.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbSucursal);
                        return View(tbcupondescuento.ToList());
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

        // GET: /CuponDescuento/Details/5
        public ActionResult Details(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("CuponDescuento/Details"))
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

        // GET: /CuponDescuento/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("CuponDescuento/Create"))
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "cdto_ID,suc_Id,cdto_FechaEmision,cdto_FechaVencimiento,cdto_PorcentajeDescuento,cdto_MontoDescuento,cdto_MaximoMontoDescuento,cdto_CantidadCompraMinima,cdto_Redimido,cdto_FechaRedencion,cdto_Anulado,cdto_EsImpreso,cdto_UsuarioCrea,cdto_FechaCrea,cdto_UsuarioModifica,cdto_FechaModifica")] tbCuponDescuento tbCuponDescuento)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("CuponDescuento/Create"))
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
                                tbCuponDescuento.cdto_PorcentajeDescuento = 0;
                                tbCuponDescuento.cdto_MaximoMontoDescuento = 0;
                                tbCuponDescuento.cdto_MontoDescuento = 10;
                                tbCuponDescuento.cdto_CantidadCompraMinima = 100;
                                tbCuponDescuento.cdto_Redimido = false;
                                tbCuponDescuento.cdto_Anulado = false;
                                tbCuponDescuento.cdto_EsImpreso = false;
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbCuponDescuento_Insert(tbCuponDescuento.suc_Id, tbCuponDescuento.cdto_FechaEmision,
                                                                 tbCuponDescuento.cdto_FechaVencimiento, tbCuponDescuento.cdto_PorcentajeDescuento,
                                                                 tbCuponDescuento.cdto_MontoDescuento, tbCuponDescuento.cdto_MaximoMontoDescuento,
                                                                 tbCuponDescuento.cdto_CantidadCompraMinima,
                                                                 tbCuponDescuento.cdto_Redimido,
                                                                 tbCuponDescuento.cdto_FechaRedencion,
                                                                 tbCuponDescuento.cdto_Anulado,
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

        // GET: /CuponDescuento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("CuponDescuento/Edit"))
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "cdto_ID,suc_Id,cdto_FechaEmision,cdto_FechaVencimiento,cdto_PorcentajeDescuento,cdto_MontoDescuento,cdto_MaximoMontoDescuento,cdto_CantidadCompraMinima,cdto_Redimido,cdto_FechaRedencion, cdto_Anulado,cdto_EsImpreso,cdto_UsuarioCrea,cdto_FechaCrea,cdto_UsuarioModifica,cdto_FechaModifica, tbUsuario, tbUsuario1")] tbCuponDescuento tbCuponDescuento)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("CuponDescuento/Edit"))
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
        //--------------------------Anular Actualiza DB---------------------------------
        [HttpPost]
        public JsonResult AnularCuponDescuento(int cdtoId, bool Anulada)
        {
            var list = db.UDP_Vent_tbCuponDescuento_Anulado(cdtoId, Anulada).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //--------------------------------Imprimir Actualiza DB-------------------------
        [HttpPost]
        public JsonResult CuponEsImpreso(int cdtoId, bool EsImpreso)
        {
            var list = db.UDP_Vent_tbCuponDescuento_EsImpreso(cdtoId, EsImpreso).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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
