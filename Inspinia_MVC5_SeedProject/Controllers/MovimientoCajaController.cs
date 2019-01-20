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
    public class MovimientoCajaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /MovimientoCaja/
        public ActionResult Index()
        {
            var tbmovimientocaja = db.tbMovimientoCaja.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCaja);
            return View(tbmovimientocaja.ToList());
        }

        public ActionResult IndexApertura()
        {
            var tbmovimientocaja = db.tbMovimientoCaja.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCaja);
            return View(tbmovimientocaja.ToList());
        }
        ///Create Apertura
        public ActionResult CreateApertura()
        {
            //////Solicitud Efectivo
            tbMovimientoCaja MovimientoCaja = new tbMovimientoCaja();
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", MovimientoCaja.cja_Id);


            //////Solicitud Efectivo
            tbSolicitudEfectivo SolicitudEfectivo = new tbSolicitudEfectivo();
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", SolicitudEfectivo.mnda_Id);


            /////Sucursal
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");

            /////Vistas Parciales
            ViewBag.SolicitudEfectivo = db.tbSolicitudEfectivo.ToList();
            ViewBag.MovimientoCaja = db.tbMovimientoCaja.ToList();
            Session["SolicitudEfectivo"] = null;
            return View();
        }

        // POST: /MovimientoCaja/CreateApertura
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateApertura([Bind(Include = "mocja_Id,cja_Id,mocja_FechaApetura,mocja_UsuarioApertura,mocja_UsuarioCrea,mocja_FechaCrea")] tbMovimientoCaja tbMovimientoCaja)
        {

            tbMovimientoCaja.mocja_UsuarioArquea = 1;
            tbMovimientoCaja.mocja_UsuarioAceptacion = 1;
            ModelState.Remove("mocja_UsuarioApertura");
            ModelState.Remove("mocja_UsuarioArquea");
            ModelState.Remove("mocja_UsuarioAceptacion");

            tbSolicitudEfectivo SolicitudEfectivo = new tbSolicitudEfectivo();
            SolicitudEfectivo.solef_EsApertura = true;
            SolicitudEfectivo.solef_EsAnulada = false;
            var list = (List<tbSolicitudEfectivoDetalle>)Session["SolicitudEfectivo"];
            //tbCaja caja = new tbCaja();
            //ModelState.Remove("mocja_FechaModifica");

            var MensajeError = 0;
            var MensajeErrorSolicitud = 0;
            IEnumerable<object> listMovimientoCaja = null;
            IEnumerable<object> listSolicitudEfectivo = null;
            IEnumerable<object> listSolicitudEfectivoDetalle = null;
            tbMovimientoCaja.mocja_FechaApertura = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        listMovimientoCaja = db.UDP_Vent_tbMovimientoCaja_Apertura_Insert(
                        tbMovimientoCaja.cja_Id,
                        tbMovimientoCaja.mocja_FechaApertura,
                        tbMovimientoCaja.mocja_UsuarioApertura,
                        tbMovimientoCaja.mocja_FechaArqueo,
                        tbMovimientoCaja.mocja_UsuarioArquea,
                        tbMovimientoCaja.mocja_FechaAceptacion,
                        tbMovimientoCaja.mocja_UsuarioAceptacion);
                        foreach (UDP_Vent_tbMovimientoCaja_Apertura_Insert_Result apertura in listMovimientoCaja)

                            MensajeError = apertura.MensajeError;
                        if (MensajeError == -1)
                        {
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View(tbMovimientoCaja);
                        }
                        else
                        {
                            if (MensajeError > 0)
                            {
                                foreach (tbSolicitudEfectivo efectivo in listSolicitudEfectivo)
                                {
                                    listSolicitudEfectivo = db.UDP_Vent_tbSolicitudEfectivo_Apertura_Insert(
                                            efectivo.mocja_Id,
                                            efectivo.solef_EsApertura,
                                            efectivo.mnda_Id,
                                            efectivo.solef_EsAnulada
                                            );
                                    foreach (UDP_Vent_tbSolicitudEfectivo_Apertura_Insert_Result SPpuntoemisiondet in listSolicitudEfectivo)
                                    {
                                        MensajeErrorSolicitud = SPpuntoemisiondet.MensajeError;
                                        if (MensajeError == -1)
                                        {
                                            ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                            return View(tbMovimientoCaja);
                                        }
                                    }
                                }
                            }
                            ///////////Solicitud Efectivo Detalle////////////////////
                            if (MensajeError > 0)
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbSolicitudEfectivoDetalle efectivodetalle in list)
                                        {

                                            efectivodetalle.soled_Id = MensajeError;
                                            listSolicitudEfectivoDetalle = db.UDP_Vent_tbSolicitudEfectivoDetalle_Apertura_Insert(
                                               efectivodetalle.solef_Id,
                                               efectivodetalle.deno_Id,
                                               efectivodetalle.soled_CantidadSolicitada,
                                               efectivodetalle.soled_CantidadEntregada,
                                               efectivodetalle.soled_MontoEntregado
                                                );
                                            foreach (UDP_Vent_tbSolicitudEfectivoDetalle_Apertura_Insert_Result SPpuntoemisiondet in listSolicitudEfectivo)
                                            {
                                                MensajeErrorSolicitud = SPpuntoemisiondet.MensajeError;
                                                if (MensajeError == -1)
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbMovimientoCaja);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbMovimientoCaja);
                            }
                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    //Caja
                    ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
                    ///Sucursal
                    ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
                    ///Moneda
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", SolicitudEfectivo.mnda_Id);

                    //ViewBag.MovimientoCaja = db.tbMovimientoCaja.ToList();
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbMovimientoCaja);
                }
            }
            ///Sucursal
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
            //Caja
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
            ///Moneda
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", SolicitudEfectivo.mnda_Id);
            //ViewBag.MovimientoCaja = db.tbMovimientoCaja.ToList();
            return View(tbMovimientoCaja);
        }


        [HttpPost]
        public JsonResult SaveSolicitudEfectivoDetalle(tbSolicitudEfectivoDetalle SolicitudEfectivoDet)
        {
            List<tbSolicitudEfectivoDetalle> sessionSolicitudEfectivoDetalle = new List<tbSolicitudEfectivoDetalle>();
            var list = (List<tbSolicitudEfectivoDetalle>)Session["SolicitudEfectivo"];
            if (list == null)
            {
                sessionSolicitudEfectivoDetalle.Add(SolicitudEfectivoDet);
                Session["SolicitudEfectivo"] = sessionSolicitudEfectivoDetalle;
            }
            else
            {
                list.Add(SolicitudEfectivoDet);
                Session["SolicitudEfectivo"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }



        // GET: /MovimientoCaja/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMovimientoCaja tbMovimientoCaja = db.tbMovimientoCaja.Find(id);
            if (tbMovimientoCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbMovimientoCaja);
        }

        // GET: /MovimientoCaja/Create
        public ActionResult Create()
        {
            //ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");

            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");           
            ViewBag.DenominacionArqueo = db.tbDenominacionArqueo.ToList();

            


            tbMovimientoCaja MC = new tbMovimientoCaja();
            MC.cja_Id = 4;
            return View(MC);

        }

        // POST: /MovimientoCaja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="mocja_Id,cja_Id,mocja_FechaApetura,mocja_UsuarioApertura,mocja_FechaArqueo,mocja_UsuarioArquea,mocja_FechaAceptacion,mocja_UsuarioAceptacion,mocja_UsuarioCrea,mocja_FechaCrea,mocja_UsuarioModifica,mocja_FechaModifica")] tbMovimientoCaja tbMovimientoCaja)
        {
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
            ViewBag.deno_Id = new SelectList(db.tbDenominacionArqueo, "deno_Id", "deno_Descripcion", tbMovimientoCaja.cja_Id);
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = string.Empty;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbMovimientoCaja_Insert(tbMovimientoCaja.cja_Id, tbMovimientoCaja.mocja_UsuarioApertura, tbMovimientoCaja.mocja_FechaArqueo, tbMovimientoCaja.mocja_UsuarioArquea, tbMovimientoCaja.mocja_FechaAceptacion, tbMovimientoCaja.mocja_UsuarioAceptacion);
                    foreach (UDP_Vent_tbMovimientoCaja_Insert_Result denoarq in list)
                        MensajeError = denoarq.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbMovimientoCaja);
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


                    return View(tbMovimientoCaja);
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            //if (ModelState.IsValid)
            //{
            //    db.tbMovimientoCaja.Add(tbMovimientoCaja);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioCrea);
            ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioModifica);
            ViewBag.deno_Id = new SelectList(db.tbDenominacionArqueo, "deno_Id", "deno_Descripcion", tbMovimientoCaja.cja_Id);
            return View(tbMovimientoCaja);
        }

        // GET: /MovimientoCaja/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMovimientoCaja tbMovimientoCaja = db.tbMovimientoCaja.Find(id);
            if (tbMovimientoCaja == null)
            {
                return HttpNotFound();
            }
            ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioCrea);
            ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
            return View(tbMovimientoCaja);
        }

        // POST: /MovimientoCaja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="mocja_Id,cja_Id,mocja_FechaApetura,mocja_UsuarioApertura,mocja_FechaArqueo,mocja_UsuarioArquea,mocja_FechaAceptacion,mocja_UsuarioAceptacion,mocja_UsuarioCrea,mocja_FechaCrea,mocja_UsuarioModifica,mocja_FechaModifica")] tbMovimientoCaja tbMovimientoCaja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbMovimientoCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioCrea);
            ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
            return View(tbMovimientoCaja);
        }

        // GET: /MovimientoCaja/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMovimientoCaja tbMovimientoCaja = db.tbMovimientoCaja.Find(id);
            if (tbMovimientoCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbMovimientoCaja);
        }

        // POST: /MovimientoCaja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMovimientoCaja tbMovimientoCaja = db.tbMovimientoCaja.Find(id);
            db.tbMovimientoCaja.Remove(tbMovimientoCaja);
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
        public JsonResult GetDenominacion(int CodMoneda)
        {
            var list = db.spGetDenominacionesMoneda(CodMoneda).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
