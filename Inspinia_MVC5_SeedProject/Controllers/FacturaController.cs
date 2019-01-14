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

namespace ERP_GMEDINA.Controllers
{
    public class FacturaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Factura/
        public ActionResult Index()
        {
            var tbFactura = db.UDV_Vent_Busqueda_Factura;
            return View(tbFactura.ToList());
        }

        [HttpPost]
        public ActionResult Index(string cliente, string fecha, string caja)
        {
            try
            {
                //var resultado = 0;
                List<UDV_Vent_Busqueda_Factura> list = new List<UDV_Vent_Busqueda_Factura>();
                using (var db = new ERP_ZORZALEntities())
                {
                    using (var oCmd = db.Database.Connection.CreateCommand())
                    {
                        db.Database.Connection.Open();
                        oCmd.CommandText = "Vent.GetBusquedaFactura";
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Add(new SqlParameter("@cliente", cliente));
                        oCmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        oCmd.Parameters.Add(new SqlParameter("@caja", caja));

                        DbDataReader reader = oCmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UDV_Vent_Busqueda_Factura tbFactura = new UDV_Vent_Busqueda_Factura();
                            if (!reader.IsDBNull(reader.GetOrdinal("fact_Codigo")))
                                tbFactura.fact_Codigo = Convert.ToString(reader["fact_Codigo"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_Fecha")))
                                tbFactura.fact_Fecha = Convert.ToDateTime(reader["fact_Fecha"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("esfac_Id")))
                                tbFactura.esfac_Id = Convert.ToByte(reader["esfac_Id"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("esfac_Descripcion")))
                                tbFactura.esfac_Descripcion = Convert.ToString(reader["esfac_Descripcion"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("cja_Id")))
                                tbFactura.cja_Id = Convert.ToByte(reader["cja_Id"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("cja_Descripcion")))
                                tbFactura.cja_Descripcion = Convert.ToString(reader["cja_Descripcion"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("suc_Id")))
                                tbFactura.suc_Id = Convert.ToByte(reader["suc_Id"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Id")))
                                tbFactura.clte_Id = Convert.ToByte(reader["clte_Id"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("pemi_NumeroCAI")))
                                tbFactura.pemi_NumeroCAI = Convert.ToString(reader["pemi_NumeroCAI"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_AlCredito")))
                                tbFactura.fact_AlCredito = Convert.ToBoolean(reader["fact_AlCredito"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_DiasCredito")))
                                tbFactura.fact_DiasCredito = Convert.ToByte(reader["fact_DiasCredito"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_PorcentajeDescuento")))
                                tbFactura.fact_PorcentajeDescuento = Convert.ToByte(reader["fact_PorcentajeDescuento"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_Vendedor")))
                                tbFactura.fact_Vendedor = Convert.ToString(reader["fact_Vendedor"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Identificacion")))
                                tbFactura.clte_Identificacion = Convert.ToString(reader["clte_Identificacion"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Nombres")))
                                tbFactura.clte_Nombres = Convert.ToString(reader["clte_Nombres"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_IdentidadTE")))
                                tbFactura.fact_IdentidadTE = Convert.ToString(reader["fact_IdentidadTE"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_NombresTE")))
                                tbFactura.fact_NombresTE = Convert.ToString(reader["fact_NombresTE"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_FechaNacimientoTE")))
                                tbFactura.fact_FechaNacimientoTE = Convert.ToDateTime(reader["fact_FechaNacimientoTE"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_UsuarioAutoriza")))
                                tbFactura.fact_UsuarioAutoriza = Convert.ToInt32(reader["fact_UsuarioAutoriza"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_FechaAutoriza")))
                                tbFactura.fact_FechaAutoriza = Convert.ToDateTime(reader["fact_FechaAutoriza"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_UsuarioCrea")))
                                tbFactura.fact_UsuarioCrea = Convert.ToInt32(reader["fact_UsuarioCrea"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_FechaCrea")))
                                tbFactura.fact_FechaCrea = Convert.ToDateTime(reader["fact_FechaCrea"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_UsuarioModifica")))
                                tbFactura.fact_UsuarioModifica = Convert.ToInt32(reader["fact_UsuarioModifica"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_FechaModifica")))
                                tbFactura.fact_FechaModifica = Convert.ToDateTime(reader["fact_FechaModifica"]);

                            list.Add(tbFactura);
                        }
                    }
                }

                return View(list);
            }
            catch (Exception ex)
            {

                var tbFactura = db.UDV_Vent_Busqueda_Factura;
                Console.Write(ex.Message);
                return View(tbFactura.ToList());
            }

        }

        // GET: /Factura/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }
        public ActionResult _IndexCliente()
        {
            return PartialView();
        }

        public ActionResult _IndexProducto()
        {
            return PartialView();
        }

        public ActionResult _IndexListaPrecio()
        {
            return PartialView();
        }

        // GET: /Factura/Create
        public ActionResult Create()
        {
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion");
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion");
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
            ViewBag.Producto = db.tbProducto.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();

            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion");

            Session["Factura"] = null;
            Session["TerceraEdad"] = null;
            return View();
        }

        // POST: /Factura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fact_Id,fact_Codigo,fact_Fecha,esfac_Id,cja_Id,suc_Id,clte_Id,pemi_NumeroCAI,fact_AlCredito,fact_DiasCredito,fact_PorcentajeDescuento,fact_Vendedor,clte_Identificacion,clte_Nombres,fact_UsuarioCrea,fact_FechaCrea,fact_UsuarioModifica,fact_FechaModifica,tbUsuario,tbUsuario1")] tbFactura tbFactura)
        {
            var list = (List<tbFacturaDetalle>)Session["Factura"];
            var listTercera = (List<tbFactura>)Session["TerceraEdad"];
            long MensajeError = 0;
            var MensajeErrorDetalle = 0;
            IEnumerable<object> listFactura = null;
            IEnumerable<object> listFacturaDetalle = null;
            if (ModelState.IsValid)
            {
                try
                {
                         if (listTercera != null)
                            {
                                if (listTercera.Count != 0)
                                {
                                    foreach (tbFactura Tercera in listTercera)
                                    {
                                        tbFactura.fact_IdentidadTE = Tercera.fact_IdentidadTE;
                                        tbFactura.fact_NombresTE = Tercera.fact_NombresTE;
                                        tbFactura.fact_FechaNacimientoTE = Tercera.fact_FechaNacimientoTE;
                                    }
                                }
                            }


                    using (TransactionScope Tran = new TransactionScope())
                    {
                        
                        listFactura = db.UDP_Vent_tbFactura_Insert(
                                                tbFactura.fact_Codigo,
                                                tbFactura.fact_Fecha,
                                                tbFactura.esfac_Id= 1,
                                                tbFactura.cja_Id,
                                                tbFactura.suc_Id,
                                                tbFactura.clte_Id,
                                                tbFactura.pemi_NumeroCAI,
                                                tbFactura.fact_AlCredito,
                                                tbFactura.fact_DiasCredito,
                                                tbFactura.fact_PorcentajeDescuento,
                                                tbFactura.fact_Vendedor,
                                                tbFactura.clte_Identificacion,
                                                tbFactura.clte_Nombres,
                                                tbFactura.fact_IdentidadTE,
                                                tbFactura.fact_NombresTE,
                                                tbFactura.fact_FechaNacimientoTE,
                                                tbFactura.fact_EsAnulada);
                        foreach (UDP_Vent_tbFactura_Insert_Result Factura in listFactura)
                            MensajeError = Factura.MensajeError;
                        if (MensajeError == -1)
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbFactura);
                        }
                        else
                        {
                            if (MensajeError > 0)
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbFacturaDetalle Detalle in list)
                                        {

                                            Detalle.fact_Id = MensajeError;
                                            listFacturaDetalle = db.UDP_Vent_tbFacturaDetalle_Insert(
                                                Detalle.fact_Id,
                                                Detalle.prod_Codigo,
                                                Detalle.factd_Cantidad,
                                                Detalle.factd_MontoDescuento,
                                                Detalle.factd_PorcentajeDescuento,
                                                Detalle.factd_Impuesto,
                                                Detalle.factd_PrecioUnitario
                                                );
                                            foreach (UDP_Vent_tbFacturaDetalle_Insert_Result SPfacturadet in listFacturaDetalle)
                                            {
                                                MensajeErrorDetalle = SPfacturadet.MensajeError;
                                                if (MensajeError == -1)
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbFactura);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbFactura);
                            }

                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
                    ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion");
                    ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");

                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                    ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
                    ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion");

                    ViewBag.Producto = db.tbProducto.ToList();
                    ViewBag.Cliente = db.tbCliente.ToList();
                }

            }
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbFactura.suc_Id);

            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion");

            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.Producto = db.tbProducto.ToList();

            Session["Factura"] = null;
            Session["TerceraEdad"] = null;

            return View(tbFactura);
        }

        // GET: /Factura/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbFactura.suc_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbFactura);
        }

        // POST: /Factura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fact_Id,fact_Codigo,fact_Fecha,esfac_Id,cja_Id,suc_Id,clte_Id,pemi_NumeroCAI,fact_AlCredito,fact_DiasCredito,fact_PorcentajeDescuento,fact_Vendedor,clte_Identificacion,clte_Nombres,fact_UsuarioCrea,fact_FechaCrea,fact_UsuarioModifica,fact_FechaModifica,tbUsuario,tbUsuario1")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    long MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbFactura_Update(
                        tbFactura.fact_Id,
                        tbFactura.fact_Codigo,
                        tbFactura.fact_Fecha,
                        tbFactura.esfac_Id= 1,
                        tbFactura.clte_Id,
                        tbFactura.pemi_NumeroCAI,
                        tbFactura.fact_AlCredito,
                        tbFactura.fact_DiasCredito,
                        tbFactura.fact_PorcentajeDescuento,
                        tbFactura.fact_Vendedor,
                        tbFactura.clte_Identificacion,
                        tbFactura.clte_Nombres,
                        tbFactura.fact_IdentidadTE,
                        tbFactura.fact_NombresTE,
                        tbFactura.fact_FechaNacimientoTE,
                        tbFactura.fact_UsuarioAutoriza,
                        tbFactura.fact_FechaAutoriza,
                        tbFactura.fact_EsAnulada,
                        tbFactura.fact_UsuarioCrea,
                        tbFactura.fact_FechaCrea);
                    foreach (UDP_Vent_tbFactura_Update_Result Factura in list)
                        MensajeError = Factura.MensajeError;
                    if (MensajeError == -1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
                    ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion");
                    ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
                    ViewBag.Producto = db.tbProducto.ToList();
                    ViewBag.Cliente = db.tbCliente.ToList();
                    ViewBag.Producto = db.tbProducto.ToList();
                }

                return RedirectToAction("Index");
            }
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbFactura.suc_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbFactura);
        }

        // GET: /Factura/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }

        // POST: /Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tbFactura tbFactura = db.tbFactura.Find(id);
            db.tbFactura.Remove(tbFactura);
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
        public JsonResult SaveFacturaDetalle(tbFacturaDetalle FacturaDetalleC)
        {
            List<tbFacturaDetalle> sessionFacturaDetalle = new List<tbFacturaDetalle>();
            var list = (List<tbFacturaDetalle>)Session["Factura"];
            if (list == null)
            {
                sessionFacturaDetalle.Add(FacturaDetalleC);
                Session["Factura"] = sessionFacturaDetalle;
            }
            else
            {
                list.Add(FacturaDetalleC);
                Session["Factura"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveTerceraEdad(tbFactura TerceraEdadC)
        {
            List<tbFactura> sessionTercera = new List<tbFactura>();
            var listTercera = (List<tbFactura>)Session["TerceraEdad"];
            if (listTercera == null)
            {
                sessionTercera.Add(TerceraEdadC);
                Session["TerceraEdad"] = sessionTercera;
            }
            else
            {
                listTercera.Add(TerceraEdadC);
                Session["TerceraEdad"] = listTercera;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveFacturaDetalle(tbFacturaDetalle FacturaDetalleC)
        {
            var list = (List<tbFacturaDetalle>)Session["Factura"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.factd_Id == FacturaDetalleC.factd_Id);
                list.Remove(itemToRemove);
                Session["Factura"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateFacturaDetalle(tbFacturaDetalle EditFacturaDetalle)
        {
            try
            {
                var MensajeError = 0;
                IEnumerable<object> list = null;
                list = db.UDP_Vent_tbFacturaDetalle_Update(
                            EditFacturaDetalle.factd_Id,
                            EditFacturaDetalle.factd_Cantidad,
                            EditFacturaDetalle.factd_MontoDescuento,
                            EditFacturaDetalle.factd_PorcentajeDescuento,
                            EditFacturaDetalle.factd_Impuesto,
                            EditFacturaDetalle.factd_PrecioUnitario,
                            EditFacturaDetalle.factd_UsuarioAutoriza,
                            EditFacturaDetalle.factd_FechaAutoriza,
                            EditFacturaDetalle.factd_UsuarioCrea,
                            EditFacturaDetalle.factd_FechaCrea);
                foreach (UDP_Vent_tbFacturaDetalle_Update_Result FacturaDetalle in list)
                    MensajeError = FacturaDetalle.MensajeError;
                if (MensajeError == -1)
                {
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return PartialView("_EditFacturaDetalle");
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
                return PartialView("_EditFacturaDetalle", EditFacturaDetalle);
            }
        }

        public JsonResult GetEmpleados(string term)
        {
            var results = db.UDV_Inv_Nombre_Empleado.Where(s => term == null || s.Empleados.ToLower().Contains(term.ToLower())).Select(x => new { id = x.emp_Id, value = x.Empleados }).Take(5).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AnularFactura(int CodFactura, int Estado)
        {
            var list = db.UDP_Vent_tbFactura_Estado(CodFactura, Estado).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPrecio(int Cliente, string idItem)
        {
            var list = db.UDP_Vent_tbFactura_BuscarListaPrecio(Cliente, idItem).ToArray();
           return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCantidad(short CodSucursal, string CodProducto)
        {
            var list = db.UDP_Vent_tbFactura_ConsultaBodega(CodSucursal, CodProducto).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }      

    }
}
