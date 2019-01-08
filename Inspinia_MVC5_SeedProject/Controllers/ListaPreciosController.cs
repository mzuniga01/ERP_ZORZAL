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
    public class ListaPreciosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /ListaPrecios/
        public ActionResult Index()
        {
            var tblistaprecio = db.tbListaPrecio.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbListadoPrecioDetalle);
            return View(tblistaprecio.ToList());
        }

        // GET: /ListaPrecios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbListaPrecio tbListaPrecio = db.tbListaPrecio.Find(id);
            if (tbListaPrecio == null)
            {
                return HttpNotFound();
            }
            return View(tbListaPrecio);
        }

        // GET: /ListaPrecios/Create
        public ActionResult Create()
        {
            ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo");
            ViewBag.Producto = db.tbProducto.ToList();
            return View();
        }

        // POST: /ListaPrecios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include= "listp_Id,listp_Nombre,listp_EsActivo,listp_UsuarioCrea,listp_FechaCrea,listp_UsuarioModifica,listp_FechaModifica,listp_FechaInicioVigencia,listp_FechaFinalVigencia,listp_Prioridad")] tbListaPrecio tbListaPrecio)
        //{
            ////var list = (List<tbListaPrecio>)Session["tbListaPrecio"];
            ////string MensajeError = "";
            ////var MensajeErrorDetalle = "";
            ////IEnumerable<object> listPrecio = null;
            ////IEnumerable<object> listPrecioDetalle = null;
            ////if (ModelState.IsValid)
            ////{
            ////    try
            ////    {
            ////        using (TransactionScope Tran = new TransactionScope())
            ////        {
            ////            //db.tbTipoIdentificacion.Add(tbTipoIdentificacion);
            ////            //db.SaveChanges();
            ////            //return RedirectToAction("Index");

            ////            listPrecio = db.UDP_Vent_tbListaPrecio_Insert(tbListaPrecio.listp_Id,
            ////                                                       tbListaPrecio.listp_Nombre,
            ////                                                       tbListaPrecio.listp_EsActivo,
            ////                                                       tbListaPrecio.listp_Prioridad);
            ////            foreach (UDP_Vent_tbListaPrecio_Insert_Result Precio in listPrecio)
            ////                MensajeError = Precio.MensajeError;
            ////            if (MensajeError == "-1")
            ////            {
            ////                ModelState.AddModelError("", "No se pudo agregar el registro");
            ////                return View(tbListaPrecio);
            ////            }
            ////            else
            ////            {
            ////                if (MensajeError != "-1")
            ////                {
            ////                    if (list != null)
            ////                    {
            ////                        if (list.Count != 0)
            ////                        {
            ////                            foreach (tbListaPrecioDetalle PrecioDetalle in list)
            ////                            {
            ////                                var pedds_Id = Convert.ToInt32(MensajeError);
            ////                                PrecioDetalle.listp_Id = pedds_Id;
            ////                                listPrecioDetalle = db.UDP_Vent_tbListaPrecioDetalle_Insert(
            ////                                    PrecioDetalle.listp_Id,
            ////                                    PrecioDetalle.listp_Nombre,
            ////                                    PrecioDetalle.listp_EsActivo,
            ////                                    PrecioDetalle.listp_Prioridad

            ////                                    );
            ////                                foreach (UDP_Vent_tbListaPrecioDetalle_Insert_Result SPpreciodetalle in listPrecioDetalle)
            ////                                {

            ////                                    MensajeErrorDetalle = SPpreciodetalle.MensajeError;
            ////                                    if (MensajeError == "-1")
            ////                                    {
            ////                                        ModelState.AddModelError("", "No se pudo agregar el registro detalle");
            ////                                        return View(tbListaPrecio);
            ////                                    }
            ////                                }
            ////                            }
            ////                        }
            ////                    }
            ////                }
            ////                else
            ////                {
            ////                    ModelState.AddModelError("", "No se pudo agregar el registro");
            ////                    return View(tbListaPrecio);
            ////                }

            ////            }
            ////            Tran.Complete();
            ////            return RedirectToAction("Index");
            ////        }
            ////    }
            ////    catch (Exception Ex)
            ////    {
            ////        var errors = ModelState.Values.SelectMany(v => v.Errors);
            ////        Ex.Message.ToString();
            ////    }

            ////    ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioCrea);
            ////    ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioModifica);
            ////    ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo", tbListaPrecio.listp_Id);
            ////    return View(tbListaPrecio);
            ////}
            ////else
            ////{
            ////    var errors = ModelState.Values.SelectMany(v => v.Errors);
            ////}
            ////return View(tbListaPrecio);
   
        //}



        //[HttpPost]
        public JsonResult SavePrecioDetalles(tbListadoPrecioDetalle PrecioDetalle)
        {
            List<tbListadoPrecioDetalle> sessionPrecioDetalle = new List<tbListadoPrecioDetalle>();
            var list = (List<tbListadoPrecioDetalle>)Session["tbListadoPrecioDetalle"];
            if (list == null)
            {
                sessionPrecioDetalle.Add(PrecioDetalle);
                Session["tbListadoPrecioDetalle"] = sessionPrecioDetalle;
            }
            else
            {
                list.Add(PrecioDetalle);
                Session["tbListadoPrecioDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }



        // GET: /ListaPrecios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbListaPrecio tbListaPrecio = db.tbListaPrecio.Find(id);
            if (tbListaPrecio == null)
            {
                return HttpNotFound();
            }
            ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioCrea);
            ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioModifica);
            ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo", tbListaPrecio.listp_Id);
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbListaPrecio);
        }

        // POST: /ListaPrecios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "listp_Id,listp_Nombre,listp_EsActivo,listp_UsuarioCrea,listp_FechaCrea,listp_UsuarioModifica,listp_FechaModifica,listp_FechaInicioVigencia,listp_FechaFinalVigencia,listp_Prioridad")] tbListaPrecio tbListaPrecio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbListaPrecio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioCrea);
            ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioModifica);
            ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo", tbListaPrecio.listp_Id);
            return View(tbListaPrecio);
        }

        // GET: /ListaPrecios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbListaPrecio tbListaPrecio = db.tbListaPrecio.Find(id);
            if (tbListaPrecio == null)
            {
                return HttpNotFound();
            }
            return View(tbListaPrecio);
        }

        // POST: /ListaPrecios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbListaPrecio tbListaPrecio = db.tbListaPrecio.Find(id);
            db.tbListaPrecio.Remove(tbListaPrecio);
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
        public JsonResult SaveListaPrecioDetalle(tbListadoPrecioDetalle cPrecioDetalle)
        {
            List<tbListadoPrecioDetalle> sessionCasoExito = new List<tbListadoPrecioDetalle>();
            var list = (List<tbListadoPrecioDetalle>)Session["ListadoPrecioDetalle"];
            if (list == null)
            {
                sessionCasoExito.Add(cPrecioDetalle);
                Session["ListadoPrecioDetalle"] = sessionCasoExito;
            }
            else
            {
                list.Add(cPrecioDetalle);
                Session["ListadoPrecioDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }
    }
}
