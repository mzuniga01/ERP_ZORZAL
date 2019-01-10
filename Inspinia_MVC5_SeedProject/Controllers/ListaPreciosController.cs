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
            Session["ListadoPrecio"] = null;
            return View();

        }

        // POST: /ListaPrecios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "listp_Id,listp_Nombre,listp_EsActivo,listp_UsuarioCrea,listp_FechaCrea,listp_UsuarioModifica,listp_FechaModifica,listp_FechaInicioVigencia,listp_FechaFinalVigencia,listp_Prioridad")] tbListaPrecio tbListaPrecio)
        {
            var list = (List<tbListadoPrecioDetalle>)Session["ListadoPrecio"];
            string MensajeError ="";
            var MensajeErrorDetalle = "";
            IEnumerable<object> listPrecio = null;
            IEnumerable<object> listPrecioDetalle = null;
            if (ModelState.IsValid)
            {
                try
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        listPrecio = db.UDP_Vent_tbListaPrecio_Insert(
                                                                   tbListaPrecio.listp_Nombre,
                                                                   tbListaPrecio.listp_EsActivo,
                                                                   tbListaPrecio.listp_FechaInicioVigencia,
                                                                   tbListaPrecio.listp_FechaFinalVigencia,
                                                                   tbListaPrecio.listp_Prioridad);
                        foreach (UDP_Vent_tbListaPrecio_Insert_Result Precio in listPrecio)
                            MensajeError = Precio.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbListaPrecio);
                        }
                        else
                        {
                            if (MensajeError != "-1")
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbListadoPrecioDetalle PrecioDetalle in list)
                                        {
                                            var pedds_Id = Convert.ToInt32(MensajeError);
                                            var descca = Convert.ToString(MensajeError);
                                            PrecioDetalle.listp_Id = pedds_Id;
                                            listPrecioDetalle = db.UDP_Vent_tbListadoPrecioDetalle_Insert(
                                                PrecioDetalle.listp_Id,
                                                PrecioDetalle.prod_Codigo,
                                                 PrecioDetalle.lispd_PrecioMayorista,
                                                 PrecioDetalle.lispd_PrecioMinorista,
                                                PrecioDetalle.lispd_DescCaja,
                                                PrecioDetalle.lispd_DescGerente
                                               
                                                );
                                            foreach (UDP_Vent_tbListadoPrecioDetalle_Insert_Result SPpreciodetalle in listPrecioDetalle)
                                            {

                                                MensajeErrorDetalle = SPpreciodetalle.MensajeError;
                                                if (MensajeError == "-1")
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbListaPrecio);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbListaPrecio);
                            }

                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    Ex.Message.ToString();
                    ViewBag.Producto = db.tbProducto.ToList();
                    ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioCrea);
                    ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioModifica);
                    ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo", tbListaPrecio.listp_Id);

                }
                ViewBag.Producto = db.tbProducto.ToList();
                ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioCrea);
                ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioModifica);
                ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo", tbListaPrecio.listp_Id);
                return View(tbListaPrecio);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(tbListaPrecio);

        }


        [HttpPost]
        public JsonResult SavePrecioDetalles(tbListadoPrecioDetalle PrecioDetalle)
        {
            List<tbListadoPrecioDetalle> sessionPrecioDetalle = new List<tbListadoPrecioDetalle>();
            var list = (List<tbListadoPrecioDetalle>)Session["ListadoPrecio"];
            if (list == null)
            {
                sessionPrecioDetalle.Add(PrecioDetalle);
                Session["ListadoPrecio"] = sessionPrecioDetalle;
            }
            else
            {
                list.Add(PrecioDetalle);
                Session["ListadoPrecio"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult QuitarPedidoDetalle(tbListadoPrecioDetalle ListadoPrecioDetalle)
        {
            var list = (List<tbListadoPrecioDetalle>)Session["ListadoPrecio"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.lispd_UsuarioCrea == ListadoPrecioDetalle.lispd_UsuarioCrea);
                list.Remove(itemToRemove);
                Session["ListadoPrecio"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
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
       
    }
}
