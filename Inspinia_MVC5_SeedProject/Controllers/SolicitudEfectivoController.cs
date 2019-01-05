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
    public class SolicitudEfectivoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /SolicitudEfectivo/
        public ActionResult Index()
        {

           
            var tbsolicitudefectivo = db.tbSolicitudEfectivo.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbUsuario2).Include(t => t.tbMoneda).Include(t => t.tbMovimientoCaja);
            return View(tbsolicitudefectivo.ToList());

        }


        [HttpGet]
        public JsonResult BuscarDenoId(int denoid)
        {
            try
            {
                var lider = (from p in db.tbDenominacion
                             where p.deno_Id == denoid
                             select p.deno_valor).ToList();

                return Json(lider, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        // GET: /SolicitudEfectivo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            if (tbSolicitudEfectivo == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudEfectivo);
        }

        // GET: /SolicitudEfectivo/Create
        

        public JsonResult GetDenominacionList(int mnda_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<tbDenominacion> DenominacionList = db.tbDenominacion.Where(x => x.mnda_Id == mnda_Id).ToList();            
            return Json(DenominacionList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDenominacionValor(int deno_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<tbDenominacion> DenominacionValor = db.tbDenominacion.Where(x => x.deno_Id == deno_Id).ToList();                    
            return Json(DenominacionValor, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult valor(int id = 0)
        //{
        //    tbDenominacion tbdeno = new tbDenominacion();
        //    using (ERP_ZORZALEntities db = new ERP_ZORZALEntities())
        //    {
        //        if (id != 0)
        //            tbdeno = db.tbDenominacion.Where(x => x.deno_Id == id).FirstOrDefault();
        //            tbdeno.DenominacionCollection = db.tbDenominacion.ToList<tbDenominacion>();

        //    }
        //    return View(tbdeno);
        //}
        public ActionResult Create()
        {
            ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
            //ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id");            

            ViewBag.Denominacion = db.tbDenominacion.ToList();

            List<tbMoneda> MonedaList = db.tbMoneda.ToList();
            ViewBag.MonedaList = new SelectList(MonedaList, "mnda_Id", "mnda_Nombre");

            ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();

            Session["Solicitud"] = null;


            return View();
        }

        // POST: /SolicitudEfectivo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "solef_Id,mocja_Id,solef_EsApertura,solef_FechaEntrega,solef_UsuarioEntrega,mnda_Id,solef_EsAnulada,solef_UsuarioCrea,solef_FechaCrea,solef_UsuarioModifica,solef_FechaModifica")] tbSolicitudEfectivo tbSolicitudEfectivo)
        {

            var list = (List<tbSolicitudEfectivoDetalle>)Session["Solicitud"];
            var MensajeError = 0;
            var MensajeErrorDetalle = 0;
            IEnumerable<object> listSolicitudEfectivo = null;
            IEnumerable<object> listSolicitudEfectivoDetalle = null;
            if (ModelState.IsValid)
            {
                try
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        listSolicitudEfectivo = db.UDP_Vent_tbSolicitudEfectivo_Insert(
                                                tbSolicitudEfectivo.mocja_Id,                                                
                                                tbSolicitudEfectivo.mnda_Id                                              

                                                );
                        foreach (UDP_Vent_tbSolicitudEfectivo_Insert_Result SolicitudE in listSolicitudEfectivo)
                            MensajeError = SolicitudE.MensajeError;
                        if (MensajeError == -1)
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbSolicitudEfectivo);
                        }
                        else
                        {
                            if (MensajeError > 0)
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbSolicitudEfectivoDetalle Detalle in list)
                                        {

                                            Detalle.solef_Id = MensajeError;
                                            listSolicitudEfectivoDetalle = db.UDP_Vent_tbSolicitudEfectivoDetalle_Insert(
                                                Detalle.solef_Id,
                                                Detalle.deno_Id,
                                                Detalle.soled_CantidadSolicitada,
                                                Detalle.soled_CantidadEntregada,
                                                Detalle.soled_MontoEntregado
                                                );
                                            foreach (UDP_Vent_tbSolicitudEfectivoDetalle_Insert_Result spDetalle in listSolicitudEfectivoDetalle)
                                            {
                                                MensajeErrorDetalle = spDetalle.MensajeError;
                                                if (MensajeError == -1)
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbSolicitudEfectivo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbSolicitudEfectivo);
                            }

                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");

                    ViewBag.Denominacion = db.tbDenominacion.ToList();
                    List<tbMoneda> MonedaList = db.tbMoneda.ToList();
                    ViewBag.MonedaList = new SelectList(MonedaList, "mnda_Id", "mnda_Nombre");

                    ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();
                }

            }

            ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
            ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
            ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);

            return View(tbSolicitudEfectivo);
        
        }

        // GET: /SolicitudEfectivo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            if (tbSolicitudEfectivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
            ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
            ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);
            return View(tbSolicitudEfectivo);
        }

        // POST: /SolicitudEfectivo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="solef_Id,mocja_Id,solef_EsApertura,solef_FechaEntrega,solef_UsuarioEntrega,mnda_Id,solef_EsAnulada,solef_UsuarioCrea,solef_FechaCrea,solef_UsuarioModifica,solef_FechaModifica")] tbSolicitudEfectivo tbSolicitudEfectivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSolicitudEfectivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
            ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
            ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);
            return View(tbSolicitudEfectivo);
        }

        // GET: /SolicitudEfectivo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            if (tbSolicitudEfectivo == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudEfectivo);
        }

        // POST: /SolicitudEfectivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            db.tbSolicitudEfectivo.Remove(tbSolicitudEfectivo);
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
        public JsonResult SaveSolicitudEfectivoDetalle(tbSolicitudEfectivoDetalle SolicitudEfeDetalleC)
        {
            List<tbSolicitudEfectivoDetalle> sessionSolicitudDetalle = new List<tbSolicitudEfectivoDetalle>();
            var list = (List<tbSolicitudEfectivoDetalle>)Session["Solicitud"];
            if (list == null)
            {
                sessionSolicitudDetalle.Add(SolicitudEfeDetalleC);
                Session["Solicitud"] = sessionSolicitudDetalle;
            }
            else
            {
                list.Add(SolicitudEfeDetalleC);
                Session["Solicitud"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveSolicitudEfectivo(tbSolicitudEfectivoDetalle SolicitudEfeDetalleC)
        {
            var list = (List<tbSolicitudEfectivoDetalle>)Session["Solicitud"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.soled_Id == SolicitudEfeDetalleC.soled_Id);
                list.Remove(itemToRemove);
                Session["Solicitud"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }



    }
}
