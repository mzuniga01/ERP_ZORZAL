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

namespace ERP_ZORZAL.Controllers
{
    public class DevolucionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Devolucion/
        public ActionResult Index()
        {
            //db.Configuration.ProxyCreationEnabled = false;

            var tbdevolucion = db.tbDevolucion.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCaja).Include(t => t.tbFactura);
            return View(tbdevolucion.Where(a => a.dev_Estado == false).ToList());


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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            if (tbDevolucion == null)
            {
                return HttpNotFound();
            }


            Session["ID"] = tbDevolucion.dev_Id;
            Session["FECHA"] = tbDevolucion.dev_Fecha;
            Session["RTNCLIENTE"] = tbDevolucion.tbFactura.clte_Identificacion;
            Session["NOMBRE"] = tbDevolucion.tbFactura.clte_Nombres;
            //Session["MONTO"] = tbDevolucionDetalle.devd_Monto;
            return View(tbDevolucion);
        }

        //GET: /Devolucion/Create
        public ActionResult Create()
        {
            //ViewBag.dev_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.dev_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
            //ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "Codigo Factura");

            tbFacturaDetalle FacturaDetalle = new tbFacturaDetalle();


            ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
            ViewBag.Factura = db.tbFactura.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();


            Session["Devolucion"] = null;
            return View();
        }

        public ActionResult _CreateDevolucionDetalle()
        {
            ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
            return View();
        }
        public ActionResult _CreateDevolucionDetalle1()
        {
            ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fact_Id, cja_Id, dev_Fecha, dev_Estado")] tbDevolucion tbDevolucion)

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
                            tbDevolucion.dev_Estado);
                        foreach (UDP_Vent_tbDevolucion_Insert_Result DevolucionL in listDevolucion)
                            MensajeError = DevolucionL.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbDevolucion);
                        }
                        else
                        {
                            if (MensajeError != "-1")
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
                                                Detalle.devd_Monto);
                                            foreach (UDP_Vent_tbDevolucionDetalle_Insert_Result SPDevolucionDetalleDet in listDevolucionDetalle)
                                            {
                                                MensajeErrorDetalle = SPDevolucionDetalleDet.MensajeError;
                                                if (MensajeError == "-1")
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbDevolucion);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
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
                    ModelState.AddModelError("", "No se pudo agregar el registro" + Ex.Message.ToString());
                    return View(tbDevolucion);
                }
            }
            ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
            ViewBag.Factura = db.tbFactura.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();
            return View(tbDevolucion);
        }
        // POST: /Devolucion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="dev_Id,fact_Id,cja_Id,dev_Fecha,dev_UsuarioCrea,dev_FechaCrea,dev_UsuarioModifica,dev_FechaModifica")] tbDevolucion tbDevolucion)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tbDevolucion.Add(tbDevolucion);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.dev_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioCrea);
        //    ViewBag.dev_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioModifica);
        //    ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbDevolucion.cja_Id);
        //    ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbDevolucion.fact_Id);
        //    return View(tbDevolucion);
        //}

        // GET: /Devolucion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

         
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            tbDevolucionDetalle tbDevolucionDetalle = db.tbDevolucionDetalle.Find(id);
            if (tbDevolucion == null)
            {
                return HttpNotFound();
            }
            ViewBag.dev_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioCrea);
            ViewBag.dev_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbDevolucion.cja_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbDevolucion.fact_Id);
            ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
            Session["ID"] = tbDevolucion.dev_Id;
            Session["FECHA"] = tbDevolucion.dev_Fecha;
            Session["RTNCLIENTE"] = tbDevolucion.tbFactura.clte_Identificacion;
            Session["NOMBRE"] = tbDevolucion.tbFactura.clte_Nombres;
            //Session["Devolucion"] = null;
            //Session["MONTO"] = tbDevolucionDetalle.devd_Monto;
            return View(tbDevolucion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dev_Id,fact_Id,cja_Id,dev_Fecha,dev_Estado,dev_UsuarioCrea,dev_FechaCrea,dev_UsuarioModifica,dev_FechaModifica")] tbDevolucion tbDevolucion)
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
                        listDevolucion = db.UDP_Vent_tbDevolucion_Update(
                            tbDevolucion.dev_Id,
                            tbDevolucion.fact_Id,
                            tbDevolucion.cja_Id,
                            tbDevolucion.dev_Fecha,
                            tbDevolucion.dev_Estado,
                            tbDevolucion.dev_UsuarioCrea,
                            tbDevolucion.dev_FechaCrea);
                        foreach (UDP_Vent_tbDevolucion_Update_Result DevolucionL in listDevolucion)
                            MensajeError = DevolucionL.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo Modificar el registro");
                            return View(tbDevolucion);
                        }
                        else
                        {
                            if (MensajeError != "-1")
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
                                                Detalle.devd_Monto);
                                            foreach (UDP_Vent_tbDevolucionDetalle_Insert_Result SPDevolucionDetalleDet in listDevolucionDetalle)
                                            {
                                                MensajeErrorDetalle = SPDevolucionDetalleDet.MensajeError;
                                                if (MensajeError == "-1")
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbDevolucion);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo Modificar el registro");
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
                    ModelState.AddModelError("", "No se pudo Modificar el registro" + Ex.Message.ToString());
                    return View(tbDevolucion);
                }
            }
            ViewBag.FacturaDetalle = db.tbFacturaDetalle.ToList();
            ViewBag.Factura = db.tbFactura.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();
            return View(tbDevolucion);
        }



       [HttpPost]
        public ActionResult UpdateDevolucionDetalle(tbDevolucionDetalle EditDevolucionDetalle)
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
                    EditDevolucionDetalle.devd_FechaCrea);
                foreach (UDP_Vent_tbDevolucionDetalle_Update_Result DevolucionDetalle in list)
                    MensajeError = DevolucionDetalle.MensajeError;
                if (MensajeError == "-1")
                {
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return PartialView("_EditarDetalleDevolucion");
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
                return PartialView("_EditarDetalleDevolucion", EditDevolucionDetalle);
            }
        }



        // GET: /Devolucion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            if (tbDevolucion == null)
            {
                return HttpNotFound();
            }
            return View(tbDevolucion);
        }

        // POST: /Devolucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            db.tbDevolucion.Remove(tbDevolucion);
            db.SaveChanges();
            return RedirectToAction("Index");
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

        public ActionResult EmitirNotaCredito(tbDevolucion Devolucion)
        {
            Session["IDDEVOLUCION"] = Session["ID"];
            Session["FECHADEV"] = Session["FECHA"];
            Session["RTN"] = Session["RTNCLIENTE"];
            Session["NOMBRECLIENTE"] = Session["NOMBRE"];
            Session["MONTODEV"] = Session["MONTO"];
            return RedirectToAction("CreateNotaCredito", "Devolucion");
        }


        public ActionResult CreateNotaCredito()
        {
            if (Session["IDDEVOLUCION"] == null)
            {
                ViewBag.IdDev = 0;
                ViewBag.fechaDev = "";
                ViewBag.Identificacion = "";
                ViewBag.Nombres = "";
                ViewBag.montodev = 0;
                Session["PEDIDO"] = 0;
            }
            else
            {
                int? id = (int)Session["IDDEVOLUCION"];
                ViewBag.IdDev = id;
                DateTime? fechad = (DateTime)Session["FECHADEV"];
                ViewBag.fechaDev = fechad;
                string identificacion = (string)Session["RTN"];
                ViewBag.Identificacion = identificacion;
                string nombres = (string)Session["NOMBRECLIENTE"];
                ViewBag.Nombres = nombres;
                //int montod = (Int32)Session["MONTODEV"];
                //ViewBag.montodev = montod;
            }

            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Id");
            ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();

          
            Session["IDDEVOLUCION"] = null;
            Session["FECHADEV"] = null;
            Session["RTN"] = null;
            Session["NOMBRECLIENTE"] = null;
            Session["MONTODEV"] = null;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNotaCredito([Bind(Include = "nocre_Id,nocre_Codigo,dev_Id,clte_Id,suc_Id,nocre_Anulado,nocre_FechaEmision,nocre_MotivoEmision,nocre_Monto,,nocre_Redimido,nocre_FechaRedimido,nocre_EsImpreso,nocre_UsuarioCrea,nocre_FechaCrea,nocre_UsuarioModifica,nocre_FechaModifica")] tbNotaCredito tbNotaCredito)
        {
                if (ModelState.IsValid)

            {
                try
                {
                    var MensajeError = "";
                    IEnumerable<object> list = null;
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
                                                            tbNotaCredito.nocre_EsImpreso);
                    foreach (UDP_Vent_tbNotaCredito_Insert_Result NotaCredito in list)
                        MensajeError = Convert.ToString(NotaCredito.MensajeError);
                    if (MensajeError != "-1")
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

            //ViewBag.nocre_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioCrea);
            //ViewBag.nocre_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioModifica);
            //ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbNotaCredito.clte_Id);
            //ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Id", tbNotaCredito.dev_Id);
            //ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbNotaCredito.suc_Id);
            return View(tbNotaCredito);
        }
    }
}
