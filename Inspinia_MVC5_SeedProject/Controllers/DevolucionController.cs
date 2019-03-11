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
namespace ERP_ZORZAL.Controllers
{
    public class DevolucionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /Devolucion/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Devolucion/Index"))
                    {
                        var tbdevolucion = db.tbDevolucion.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCaja).Include(t => t.tbFactura);
                        return View(tbdevolucion.Where(a => a.dev_Estado == false).ToList());
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
        public JsonResult InsertDevolucion(tbDevolucionDetalle DetalleDevolucioncont)
        {
            List<tbDevolucionDetalle> sessionDevolucionDetalle = new List<tbDevolucionDetalle>();
            var list = (List<tbDevolucionDetalle>)Session["Devolucion"];
            if (list == null)
            {
                sessionDevolucionDetalle.Add(DetalleDevolucioncont);
                Session["Devolucion"] = sessionDevolucionDetalle;
            }
            else
            {
                list.Add(DetalleDevolucioncont);
                Session["Devolucion"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveDevolucionDetalle(tbDevolucionDetalle DetalleDevolucioncont)
        {
            var list = (List<tbDevolucionDetalle>)Session["Devolucion"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.devd_Id == DetalleDevolucioncont.devd_Id);
                list.Remove(itemToRemove);
                Session["Devolucion"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        /// Post
        // GET: /Devolucion/Details/5
        public ActionResult Details(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Devolucion/Details"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbDevolucionDetalle tbDevolucionDetalle = new tbDevolucionDetalle();
                        tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
                        if (tbDevolucion == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        Session["ID"] = tbDevolucion.dev_Id;
                        Session["FECHA"] = tbDevolucion.dev_Fecha;
                        Session["IDCAJA"] = tbDevolucion.cja_Id;
                        Session["CAJA"] = tbDevolucion.tbCaja.cja_Descripcion;
                        Session["IDSUCURSAL"] = tbDevolucion.tbCaja.suc_Id;
                        Session["SUCURSAL"] = tbDevolucion.tbCaja.tbSucursal.suc_Descripcion;

                        Session["RTNCLIENTE"] = tbDevolucion.tbFactura.clte_Identificacion;
                        Session["IDCLIENTE"] = tbDevolucion.tbFactura.clte_Id;
                        Session["NOMBRE"] = tbDevolucion.tbFactura.clte_Nombres;

                        var ExiteNotaCredito = db.tbNotaCredito.Where(x => x.dev_Id == tbDevolucion.dev_Id).ToList();
                        if (ExiteNotaCredito.Count() > 0)
                        {
                            ViewBag.NotaCredito = "1";
                        }
                        return View(tbDevolucion);
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

        //GET: /Devolucion/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Devolucion/Create"))
                    {
                        int idUser = 0;
                        GeneralFunctions Login = new GeneralFunctions();
                        List<tbUsuario> User = Login.getUserInformation();
                        foreach (tbUsuario Usuario in User)
                        {
                            idUser = Convert.ToInt32(Usuario.emp_Id);
                        }
                        ViewBag.usu_Id = idUser;
                        tbFacturaDetalle FacturaDetalle = new tbFacturaDetalle();
                        ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                        ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                        ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
                        ViewBag.Factura = db.tbFactura.ToList();
                        ViewBag.Cliente = db.tbCliente.ToList();
                        Session["Devolucion"] = null;
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

        public ActionResult _CreateDevolucionDetalle()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Devolucion/CreateDevolucionDetalle"))
                    {
                        ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
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
        public ActionResult Create([Bind(Include = "fact_Id, cja_Id, dev_Fecha, dev_Estado")] tbDevolucion tbDevolucion)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Devolucion/Create"))
                    {
                        var list = (List<tbDevolucionDetalle>)Session["Devolucion"];
                        var MensajeError = "";
                        var MensajeErrorDetalle = "";
                        IEnumerable<object> listDevolucion = null;
                        IEnumerable<object> listDevolucionDetalle = null;
                        tbDevolucionDetalle cDevolucionDetalle = new tbDevolucionDetalle();
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                using (TransactionScope Tran = new TransactionScope())
                                {
                                    listDevolucion = db.UDP_Vent_tbDevolucion_Insert(
                                        tbDevolucion.fact_Id,
                                        tbDevolucion.cja_Id,
                                        tbDevolucion.dev_Fecha,
                                        tbDevolucion.dev_Estado,
                                        Function.GetUser(),
                                        Function.DatetimeNow());
                                    foreach (UDP_Vent_tbDevolucion_Insert_Result DevolucionL in listDevolucion)
                                        MensajeError = DevolucionL.MensajeError;
                                    if (MensajeError.StartsWith("-1"))
                                    {
                                        ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
                                        ViewBag.Factura = db.tbFactura.ToList();
                                        ViewBag.Cliente = db.tbCliente.ToList();
                                        Function.InsertBitacoraErrores("Devolucion/Create", MensajeError, "Create");
                                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                        return View(tbDevolucion);
                                    }
                                    else
                                    {
                                        if (!MensajeError.StartsWith("-1"))
                                        {
                                            if (list != null)
                                            {
                                                if (list.Count != 0)
                                                {
                                                    foreach (tbDevolucionDetalle Detalle in list)
                                                    {
                                                        Detalle.dev_Id = Convert.ToInt32(MensajeError);
                                                        listDevolucionDetalle = db.UDP_Vent_tbDevolucionDetalle_Insert(
                                                            Detalle.dev_Id,
                                                            Detalle.prod_Codigo,
                                                            Detalle.devd_CantidadProducto,
                                                            Detalle.devd_Descripcion,
                                                            Detalle.devd_Monto,
                                                            Function.GetUser(),
                                                            Function.DatetimeNow());
                                                        foreach (UDP_Vent_tbDevolucionDetalle_Insert_Result SPDevolucionDetalleDet in listDevolucionDetalle)
                                                        {
                                                            MensajeErrorDetalle = SPDevolucionDetalleDet.MensajeError;
                                                            if (MensajeError.StartsWith("-1"))
                                                            {
                                                                Function.InsertBitacoraErrores("Devolucion/Create", MensajeError, "Create");
                                                                ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                                                return View(tbDevolucion);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
                                            ViewBag.Factura = db.tbFactura.ToList();
                                            ViewBag.Cliente = db.tbCliente.ToList();
                                            Function.InsertBitacoraErrores("Devolucion/Create", MensajeError, "Create");
                                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                            return View(tbDevolucion);
                                        }
                                    }
                                    Tran.Complete();
                                    return RedirectToAction("Create");
                                }
                            }

                            catch (Exception Ex)
                            {
                                ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
                                ViewBag.Factura = db.tbFactura.ToList();
                                ViewBag.Cliente = db.tbCliente.ToList();
                                Function.InsertBitacoraErrores("Devolucion/Create", Ex.Message.ToString(), "Create");
                                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                return View(tbDevolucion);
                            }
                        }
                        ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
                        ViewBag.Factura = db.tbFactura.ToList();
                        ViewBag.Cliente = db.tbCliente.ToList();
                        return View(tbDevolucion);
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

        // GET: /Devolucion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Devolucion/Edit"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }

                        tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
                        tbDevolucionDetalle tbDevolucionDetalle = new tbDevolucionDetalle();
                        if (tbDevolucion == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        ViewBag.dev_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioCrea);
                        ViewBag.dev_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioModifica);
                        ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbDevolucion.cja_Id);
                        ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbDevolucion.fact_Id);
                        ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
                        Session["ID"] = tbDevolucion.dev_Id;
                        Session["FECHA"] = tbDevolucion.dev_Fecha;
                        Session["IDCAJA"] = tbDevolucion.cja_Id;
                        Session["CAJA"] = tbDevolucion.tbCaja.cja_Descripcion;
                        Session["IDSUCURSAL"] = tbDevolucion.tbCaja.suc_Id;
                        Session["SUCURSAL"] = tbDevolucion.tbCaja.tbSucursal.suc_Descripcion;
                        Session["RTNCLIENTE"] = tbDevolucion.tbFactura.clte_Identificacion;
                        Session["IDCLIENTE"] = tbDevolucion.tbFactura.clte_Id;
                        Session["NOMBRE"] = tbDevolucion.tbFactura.clte_Nombres;


                        var ExiteNotaCredito = db.tbNotaCredito.Where(x => x.dev_Id == tbDevolucion.dev_Id).ToList();
                        if (ExiteNotaCredito.Count() > 0)
                        {
                            var NotaCredito = db.tbNotaCredito.Where(l => l.dev_Id == tbDevolucion.dev_Id).Select(l => l.nocre_Codigo);
                            ViewBag.NotaC = NotaCredito;
                            ViewBag.NotaCredito = "1";
                        }
                        return View(tbDevolucion);
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
        public ActionResult Edit([Bind(Include = "dev_Id,fact_Id,cja_Id,dev_Fecha,dev_Estado,dev_UsuarioCrea,dev_FechaCrea,dev_UsuarioModifica,dev_FechaModifica")] tbDevolucion tbDevolucion)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Devolucion/Edit"))
                    {
                        var list = (List<tbDevolucionDetalle>)Session["Devolucion"];
                        var MensajeError = "";
                        var MensajeErrorDetalle = "";
                        IEnumerable<object> listDevolucion = null;
                        IEnumerable<object> listDevolucionDetalle = null;
                        tbDevolucionDetalle cDevolucionDetalle = new tbDevolucionDetalle();
                        var ExiteNotaCredito = db.tbNotaCredito.Where(x => x.dev_Id == tbDevolucion.dev_Id).ToList();
                        if (ExiteNotaCredito.Count() > 0)
                        {
                            ViewBag.Validacion = "1";
                        }
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                using (TransactionScope Tran = new TransactionScope())
                                {
                                    listDevolucion = db.UDP_Vent_tbDevolucion_Update(
                                        tbDevolucion.dev_Id,
                                        tbDevolucion.fact_Id,
                                        tbDevolucion.cja_Id,
                                        tbDevolucion.dev_Fecha,
                                        tbDevolucion.dev_Estado,
                                        tbDevolucion.dev_UsuarioCrea,
                                        tbDevolucion.dev_FechaCrea,
                                        Function.GetUser(),
                                        Function.DatetimeNow());
                                    foreach (UDP_Vent_tbDevolucion_Update_Result DevolucionL in listDevolucion)
                                        MensajeError = DevolucionL.MensajeError;
                                    if (MensajeError.StartsWith("-1"))
                                    {
                                        Function.InsertBitacoraErrores("Devolucion/Edit", MensajeError, "Edit");
                                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                        return View(tbDevolucion);
                                    }
                                    else
                                    {
                                        if (!MensajeError.StartsWith("-1"))
                                        {
                                            if (list != null)
                                            {
                                                if (list.Count != 0)
                                                {
                                                    foreach (tbDevolucionDetalle Detalle in list)
                                                    {
                                                        Detalle.dev_Id = Convert.ToInt32(MensajeError);
                                                        listDevolucionDetalle = db.UDP_Vent_tbDevolucionDetalle_Insert(
                                                            Detalle.dev_Id,
                                                            Detalle.prod_Codigo,
                                                            Detalle.devd_CantidadProducto,
                                                            Detalle.devd_Descripcion,
                                                            Detalle.devd_Monto,
                                                            Function.GetUser(),
                                                            Function.DatetimeNow());
                                                        foreach (UDP_Vent_tbDevolucionDetalle_Insert_Result SPDevolucionDetalleDet in listDevolucionDetalle)
                                                        {
                                                            MensajeErrorDetalle = SPDevolucionDetalleDet.MensajeError;
                                                            if (MensajeError.StartsWith("-1"))
                                                            {
                                                                Function.InsertBitacoraErrores("Devolucion/Edit", MensajeError, "Edit");
                                                                ModelState.AddModelError("", "No se pudo actualizar el registro detalle, favor contacte al administrador.");
                                                                return View(tbDevolucion);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Function.InsertBitacoraErrores("Devolucion/Edit", MensajeError, "Edit");
                                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                            return View(tbDevolucion);
                                        }
                                    }
                                    Tran.Complete();
                                    return RedirectToAction("Index");
                                }
                            }
                            catch (Exception Ex)
                            {
                                ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
                                ViewBag.Factura = db.tbFactura.ToList();
                                ViewBag.Cliente = db.tbCliente.ToList();
                                Function.InsertBitacoraErrores("Devolucion/Edit", Ex.Message.ToString(), "Edit");
                                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                return View(tbDevolucion);
                            }
                        }
                        ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
                        ViewBag.Factura = db.tbFactura.ToList();
                        ViewBag.Cliente = db.tbCliente.ToList();
                        return View(tbDevolucion);
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
        public ActionResult UpdateDevolucionDetalle(tbDevolucionDetalle EditDevolucionDetalle)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Devolucion/UpdateDevolucionDetalle"))
                    {
                        try
                        {
                            var MensajeError = "";
                            IEnumerable<object> list = null;
                            list = db.UDP_Vent_tbDevolucionDetalle_Update(EditDevolucionDetalle.devd_Id,
                                                                            EditDevolucionDetalle.dev_Id,
                                                                            EditDevolucionDetalle.prod_Codigo,
                                                                            EditDevolucionDetalle.devd_CantidadProducto,
                                                                            EditDevolucionDetalle.devd_Descripcion,
                                                                            EditDevolucionDetalle.devd_Monto,
                                                                            EditDevolucionDetalle.devd_UsuarioCrea,
                                                                            EditDevolucionDetalle.devd_FechaCrea,
                                                                            Function.GetUser(),
                                                                            Function.DatetimeNow());
                            foreach (UDP_Vent_tbDevolucionDetalle_Update_Result DevDetalle in list)
                                MensajeError = DevDetalle.MensajeError;
                            if (MensajeError.StartsWith("-1"))
                            {
                                Function.InsertBitacoraErrores("Devolucion/UpdateDevolucionDetalle", MensajeError, "UpdateDevolucionDetalle");
                                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                return RedirectToAction("Edit", "Devolucion");
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        catch (Exception Ex)
                        {
                            Function.InsertBitacoraErrores("Devolucion/UpdateDevolucionDetalle", Ex.Message.ToString(), "UpdateDevolucionDetalle");
                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                            return RedirectToAction("Edit", "Devolucion");
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
        public JsonResult AnularDevolucion(int CodDevolucion, bool Estado)
        {
            var list = db.UDP_Vent_tbDevolucion_Estado(CodDevolucion, Estado).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BuscarCodigoProducto(int CodFactura, string CodProducto)
        {
            var list = db.UDP_Vent_tbDevolucion_Filtrado_CodProducto(CodFactura, CodProducto).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDevolucionDetalle(long devolucionId)
        {
            var list = db.UDP_Vent_tbDevolucion_GetDetalle(devolucionId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDevolucionDetalleEditar(long DetalleDevID)
        {
            var list = db.UDP_Vent_tbDevolucion_GetDetalle_Editar(DetalleDevID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult FiltrarModal(string CodCliente)
        {

            var list = ViewBag.Factura = db.tbFactura.Where(a => a.clte_Identificacion == CodCliente)
           .Select(a => new
           {
               FactId = a.fact_Id,
               FactCodigo = a.fact_Codigo,
               FactFecha = a.fact_Fecha,
               CtleRTN = a.clte_Identificacion,
               Nombre = a.clte_Nombres


           });

            return Json(list, JsonRequestBehavior.AllowGet);

            //return Json(list);
        }

        [HttpPost]
        public JsonResult FiltrarModalProducto(int FacturaID)
        {

            var list = ViewBag.Factura = db.tbFacturaDetalle.Where(a => a.fact_Id == FacturaID)
           .Select(a => new
           {
               CodigoProducto = a.prod_Codigo,
               Descripcion = a.tbProducto.prod_Descripcion,
               CantidadFacturada = a.factd_Cantidad,
               PorcentajeDesc = a.factd_PorcentajeDescuento,
               PorcentajeImpu = a.factd_Impuesto,
               PrecioUnitario = a.factd_PrecioUnitario
           });

            return Json(list, JsonRequestBehavior.AllowGet);

            //return Json(list);
        }

        public ActionResult MontoDevolucion(int MontoDev)
        {
            Session["MONTO"] = MontoDev;
            return RedirectToAction("EmitirNotaCredito", "Devolucion");
        }
        public ActionResult EmitirNotaCredito(tbDevolucion Devolucion)
        {
            Session["IDDEVOLUCION"] = Session["ID"];
            Session["FECHADEV"] = Session["FECHA"];
            Session["CODIGOCAJA"] = Session["IDCAJA"];
            Session["CAJAA"] = Session["CAJA"];
            Session["CODIGOSUCURSAL"] = Session["IDSUCURSAL"];
            Session["SUC"] = Session["SUCURSAL"];
            Session["IDCTL"] = Session["IDCLIENTE"];
            Session["RTN"] = Session["RTNCLIENTE"];
            Session["NOMBRECLIENTE"] = Session["NOMBRE"];
            Session["MONTODEV"] = Session["MONTO"];
            return RedirectToAction("CreateNotaCredito", "Devolucion");
        }


        public ActionResult CreateNotaCredito()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Devolucion/CreateNotaCredito"))
                    {
                        if (Session["IDDEVOLUCION"] == null)
                        {
                            ViewBag.IdDev = 0;
                            ViewBag.fechaDev = "";
                            ViewBag.IDCaja = 0;
                            ViewBag.Caja = "";
                            ViewBag.IDSucursal = 0;
                            ViewBag.Sucursal = "";
                            ViewBag.Identificacion = "";
                            ViewBag.Nombres = "";
                            ViewBag.montodev = 0;
                            //Session["DEVOLUCION"] = 0;
                        }
                        else
                        {
                            int? id = (int)Session["IDDEVOLUCION"];
                            ViewBag.IdDev = id;
                            short IdCaj = (short)Session["CODIGOCAJA"];
                            ViewBag.IDCaja = IdCaj;
                            string Cajaa = (string)Session["CAJAA"];
                            ViewBag.Caja = Cajaa;
                            int IdSuc = (int)Session["CODIGOSUCURSAL"];
                            ViewBag.IDSucursal = IdSuc;
                            string Sucurs = (string)Session["SUC"];
                            ViewBag.Sucursal = Sucurs;
                            DateTime? fechad = (DateTime)Session["FECHADEV"];
                            ViewBag.fechaDev = fechad;
                            int IdCtl = (int)Session["IDCTL"];
                            ViewBag.IdCtll = IdCtl;
                            string identificacion = (string)Session["RTN"];
                            ViewBag.Identificacion = identificacion;
                            string nombres = (string)Session["NOMBRECLIENTE"];
                            ViewBag.Nombres = nombres;
                            int montod = (Int32)Session["MONTODEV"];
                            ViewBag.montodev = montod;

                        }

                        ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Id");
                        ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
                        ViewBag.Cliente = db.tbCliente.ToList();


                        Session["IDDEVOLUCION"] = null;
                        Session["IDCJ"] = null;
                        Session["CAJAA"] = null;
                        Session["IDSUC"] = null;
                        Session["SUC"] = null;
                        Session["FECHADEV"] = null;
                        Session["RTN"] = null;
                        Session["NOMBRECLIENTE"] = null;
                        Session["MONTODEV"] = null;
                        Session["IDCTL"] = null;

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
        public JsonResult GetCodigoNotaCredito(int CodSucursal, short CodCaja)
        {
            var list = db.UDP_Vent_tbNotaCredito_CodigoNotaCredito(CodSucursal, CodCaja).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // POST: /NotaCredito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNotaCredito([Bind(Include = "nocre_Id,nocre_Codigo,dev_Id,clte_Id,suc_Id,cja_Id,nocre_Anulado,nocre_FechaEmision,nocre_MotivoEmision,nocre_Monto,nocre_Redimido,nocre_FechaRedimido,nocre_EsImpreso,nocre_UsuarioCrea,nocre_FechaCrea,nocre_UsuarioModifica,nocre_FechaModifica")] tbNotaCredito tbNotaCredito)

        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Devolucion/CreateNotaCredito"))
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
                                Function.InsertBitacoraErrores("Devolucion/CreateNotaCredito", Ex.Message.ToString(), "CreateNotaCredito");
                                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                return View(tbNotaCredito);
                            }
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

    }
}

