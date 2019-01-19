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

namespace ERP_GMEDINA.Controllers
{
    public class SalidaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Salida/
        public ActionResult Index()
        {
            var tbsalida = db.tbSalida.Include(t => t.tbUsuario).Include(t => t.tbBodega).Include(t => t.tbEstadoMovimiento).Include(t => t.tbTipoSalida);
            return View(tbsalida.ToList());
        }

        // GET: /Salida/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            if (tbSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbSalida);
        }

        // GET: /Salida/Create
        public ActionResult Create()
        {
            ViewBag.sal_BodDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion");
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.box_Codigo = new SelectList(db.tbSalidaDetalle, "sald_Id", "box_Codigo");

            ViewBag.sal_Id = new SelectList(db.tbProductoSubcategoria, "sal_Id", "sal_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.tbBodegaDetalle.ToList();

            return View();
        }

        //public JsonResult GetProdList()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    List<tbProducto> tbProducto = db.tbProducto.ToList();
        //    return Json(tbProducto, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetProdList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<tbBodegaDetalle> tbBodegaDetalle = db.tbBodegaDetalle.ToList();
            return Json(tbBodegaDetalle, JsonRequestBehavior.AllowGet);
        }
        // POST: /Salida/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bod_Id,fact_Id,sal_FechaElaboracion,estm_Id,tsal_Id,sal_RazonDevolucion, sal_BodDestino, sal_EsAnulada, sal_RazonAnulada")] tbSalida tbSalida)
        {
            ViewBag.sal_BodDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.box_Codigo = new SelectList(db.tbBox, "box_Codigo", "box_Descripcion");
            ViewBag.estm_Descripcion = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo");
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion");
            ViewBag.Producto = db.tbBodegaDetalle.ToList();

            var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];
            var MensajeError = "0";
            var MensajeErrorDetalle = "0";
            IEnumerable<object> listSalida = null;
            IEnumerable<object> listSalidaDetalle = null;
            if (ModelState.IsValid)
            {
                try
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        listSalida = db.UDP_Inv_tbSalida_Insert(tbSalida.bod_Id, tbSalida.fact_Id, tbSalida.sal_FechaElaboracion, tbSalida.estm_Id, tbSalida.tsal_Id, tbSalida.sal_BodDestino, tbSalida.sal_EsAnulada, tbSalida.sal_RazonAnulada, tbSalida.sal_RazonDevolucion);
                        foreach (UDP_Inv_tbSalida_Insert_Result Salida in listSalida)
                            MensajeError = Salida.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbSalida);
                        }
                        else
                        {
                            var MsjError = Convert.ToInt32(MensajeError);
                            if (MsjError > 0)
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbSalidaDetalle Detalle in list)
                                        {
                                            var box_Codigo = "0";
                                            Detalle.box_Codigo = MensajeError;
                                            listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Insert(
                                                MsjError,
                                                Detalle.prod_Codigo,
                                                Detalle.sal_Cantidad,
                                                box_Codigo
                                                );
                                            foreach (UDP_Inv_tbSalidaDetalle_Insert_Result spDetalle in listSalidaDetalle)
                                            {
                                                MensajeErrorDetalle = spDetalle.MensajeError;
                                                if (MensajeError == "-1")
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbSalida);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbSalida);
                            }

                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());

                    ViewBag.Producto = db.tbBodegaDetalle.ToList();
                }

            }
            else
            {
                ViewBag.Producto = db.tbBodegaDetalle.ToList();
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(tbSalida);
        }





        // GET: /Salida/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            //ViewBag.UsuarioCrea = db.tbUsuario.Find(tbSalida.sal_UsuarioCrea).usu_NombreUsuario;
            var UsuarioModifica = tbSalida.sal_UsuarioModifica;
            if (UsuarioModifica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModifica).usu_NombreUsuario;
            };
            if (tbSalida == null)
            {
                return HttpNotFound();
            }

            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.bod_Id);
            ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSalida.bod_Id);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbSalida.estm_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbSalida.fact_Id);
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion", tbSalida.tsal_Id);

            return View(tbSalida);
        }



        // POST: /Salida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult EditSalidaDetalle(tbSalidaDetalle SalidaDetalle)
        {
            try
            {

                tbSalidaDetalle pSalidaDetalle = db.tbSalidaDetalle.Find(SalidaDetalle.sald_Id);
                var MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Inv_tbSalidaDetalle_Update(SalidaDetalle.sald_Id,
                                                    pSalidaDetalle.sal_Id,
                                                    SalidaDetalle.prod_Codigo,
                                                    SalidaDetalle.sal_Cantidad,
                                                    SalidaDetalle.box_Codigo);

                foreach (UDP_Inv_tbSalidaDetalle_Update_Result RSSalidaDetalle in list)
                    MensajeError = RSSalidaDetalle.MensajeError;
                if (MensajeError == "-1")
                {
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return PartialView("_EditSalidaDetalle");
                }
                else
                {
                    return RedirectToAction("Edit", "Salida", new { @id = pSalidaDetalle.sal_Id });
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return PartialView("_EditSalidaDetalle", SalidaDetalle);
            }
        }

        // POST: /Salida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "sal_Id, bod_Id,fact_Id,sal_FechaElaboracion,estm_Id,tsal_Id,  sal_RazonDevolucion, sal_UsuarioCrea, sal_FechaCrea")] tbSalida tbSalida)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
                    ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.bod_Id);
                    ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
                    ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbSalida.estm_Id);
                    ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbSalida.fact_Id);
                    ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion", tbSalida.tsal_Id);
                    ViewBag.Producto = db.tbProducto.ToList();
                    ViewBag.Cliente = db.tbCliente.ToList();

                    tbSalida pSalida = db.tbSalida.Find(id);

                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Inv_tbSalida_Update(tbSalida.sal_Id,
                                                    tbSalida.bod_Id, 
                                                    tbSalida.fact_Id, 
                                                    tbSalida.sal_FechaElaboracion, 
                                                    tbSalida.estm_Id, 
                                                    tbSalida.tsal_Id, 
                                                    tbSalida.sal_BodDestino, 
                                                    tbSalida.sal_EsAnulada, 
                                                    tbSalida.sal_RazonAnulada, 
                                                    tbSalida.sal_RazonDevolucion,
                                                    pSalida.sal_UsuarioCrea,
                                                    pSalida.sal_FechaCrea);

                    foreach (UDP_Inv_tbSalida_Update_Result ListaPrecio in list)
                        MensajeError = ListaPrecio.MensajeError;
                    if (MensajeError == "-1")
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
                    ViewBag.sal_FechaCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalida.sal_FechaCrea);
                    ViewBag.sal_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSalida.sal_UsuarioModifica);
                    ViewBag.Producto = db.tbProducto.ToList();
                }

                return RedirectToAction("Index");
            }
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.bod_Id);
            ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbSalida.estm_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbSalida.fact_Id);
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion", tbSalida.tsal_Id);
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbSalida);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Aplicar(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = "";
                    byte Estado = 2;
                    IEnumerable<object> list = null;
                    tbSalida tbSalida = db.tbSalida.Find(id);
                    list = db.UDP_Inv_tbSalida_Update(tbSalida.sal_Id,
                                                    tbSalida.bod_Id,
                                                    tbSalida.fact_Id,
                                                    tbSalida.sal_FechaElaboracion,
                                                    Estado,
                                                    tbSalida.tsal_Id,
                                                    tbSalida.sal_BodDestino,
                                                    tbSalida.sal_EsAnulada,
                                                    tbSalida.sal_RazonAnulada,
                                                    tbSalida.sal_RazonDevolucion,
                                                    tbSalida.sal_UsuarioCrea,
                                                    tbSalida.sal_FechaCrea);

                    foreach (UDP_Inv_tbSalida_Update_Result Salida in list)
                        MensajeError = Salida.MensajeError;
                    if (MensajeError == "-1")
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
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Imprimir(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = "";
                    byte Estado = 3;
                    IEnumerable<object> list = null;
                    tbSalida tbSalida = db.tbSalida.Find(id);
                    list = db.UDP_Inv_tbSalida_Update(tbSalida.sal_Id,
                                                    tbSalida.bod_Id,
                                                    tbSalida.fact_Id,
                                                    tbSalida.sal_FechaElaboracion,
                                                    Estado,
                                                    tbSalida.tsal_Id,
                                                    tbSalida.sal_BodDestino,
                                                    tbSalida.sal_EsAnulada,
                                                    tbSalida.sal_RazonAnulada,
                                                    tbSalida.sal_RazonDevolucion,
                                                    tbSalida.sal_UsuarioCrea,
                                                    tbSalida.sal_FechaCrea);

                    foreach (UDP_Inv_tbSalida_Update_Result Salida in list)
                        MensajeError = Salida.MensajeError;
                    if (MensajeError == "-1")
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
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Anular(int? id,[Bind(Include = "sal_Id, bod_Id,fact_Id,sal_FechaElaboracion,estm_Id,tsal_Id,sal_RazonDevolucion, sal_UsuarioCrea, sal_FechaCrea")] tbSalida tbSalida)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = "";
                    bool EsAnulada = true;
                    IEnumerable<object> list = null;
                    tbSalida vSalida = db.tbSalida.Find(id);
                    list = db.UDP_Inv_tbSalida_Update(vSalida.sal_Id,
                                                    vSalida.bod_Id,
                                                    vSalida.fact_Id,
                                                    vSalida.sal_FechaElaboracion,
                                                    vSalida.estm_Id,
                                                    vSalida.tsal_Id,
                                                    vSalida.sal_BodDestino,
                                                    EsAnulada,
                                                    tbSalida.sal_RazonAnulada,
                                                    vSalida.sal_RazonDevolucion,
                                                    vSalida.sal_UsuarioCrea,
                                                    vSalida.sal_FechaCrea);

                    foreach (UDP_Inv_tbSalida_Update_Result Salida in list)
                        MensajeError = Salida.MensajeError;
                    if (MensajeError == "-1")
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
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public JsonResult SaveSalidaDetalle(tbSalidaDetalle SalidaDetalle)
        {
            List<tbSalidaDetalle> sessionSalidaDetalle = new List<tbSalidaDetalle>();
            var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];
            if (list == null)
            {
                sessionSalidaDetalle.Add(SalidaDetalle);
                Session["SalidaDetalle"] = sessionSalidaDetalle;
            }
            else
            {
                list.Add(SalidaDetalle);
                Session["SalidaDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveSalidaDetalle(tbSalidaDetalle SalidaDetalle)
        {
            var list = (List<tbSalidaDetalle>)Session["tbSalidaDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.sald_UsuarioCrea == SalidaDetalle.sald_UsuarioCrea);
                list.Remove(itemToRemove);
                Session["tbSalidaDetalle"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }



        // GET: /Salida/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            if (tbSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbSalida);
        }

        // POST: /Salida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSalida tbSalida = db.tbSalida.Find(id);
            db.tbSalida.Remove(tbSalida);
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
    }
}
