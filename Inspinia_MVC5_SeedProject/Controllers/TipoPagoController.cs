using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_ZORZAL.Controllers
{
    public class TipoPagoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /TipoPago/
        public ActionResult Index()
        {
            var tbtipopago = db.tbTipoPago.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbtipopago.ToList());
        }

        // GET: /TipoPago/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            if (tbTipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoPago);
        }

        // GET: /TipoPago/Create
        public ActionResult Create()
        {
            ViewBag.tpa_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tpa_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: /TipoPago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tpa_Id,tpa_Descripcion,tpa_Emisor,tpa_Cuenta,tpa_FechaVencimiento,tpa_Titular,tpa_UsuarioCrea,tpa_FechaCrea,tpa_UsuarioModifica,tpa_FechaModifica")] tbTipoPago tbTipoPago)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbTipoPago_Insert(tbTipoPago.tpa_Descripcion,  tbTipoPago.tpa_Emisor, tbTipoPago.tpa_Cuenta, tbTipoPago.tpa_FechaVencimiento, tbTipoPago.tpa_Titular);
                    foreach (UDP_Vent_tbTipoPago_Insert_Result tipopago in list)
                        MensajeError = tipopago.MensajeError;
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
                    ModelState.AddModelError("", "No se ha podido ingresar el registro, favor contacte al administrador " + Ex.Message.ToString());
                    return View(tbTipoPago);
                }
                //db.tbTipoPago.Add(tbTipoPago);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
        
         
            ViewBag.tpa_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPago.tpa_UsuarioCrea);
            ViewBag.tpa_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPago.tpa_UsuarioModifica);
            return View(tbTipoPago);
        }

        // GET: /TipoPago/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            if (tbTipoPago == null)
            {
                return HttpNotFound();
            }
            ViewBag.tpa_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPago.tpa_UsuarioCrea);
            ViewBag.tpa_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPago.tpa_UsuarioModifica);
            return View(tbTipoPago);
        }

        // POST: /TipoPago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "tpa_Id,tpa_Descripcion,tpa_Emisor,tpa_Cuenta,tpa_FechaVencimiento,tpa_Titular,tpa_UsuarioCrea,tpa_FechaCrea,tpa_UsuarioModifica,tpa_FechaModifica, tbUsuario, tbUsuario1")] tbTipoPago tbTipoPago)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbTipoPago_Update(tbTipoPago.tpa_Id, tbTipoPago.tpa_Descripcion, tbTipoPago.tpa_Emisor, tbTipoPago.tpa_Cuenta, tbTipoPago.tpa_FechaVencimiento, tbTipoPago.tpa_Titular, tbTipoPago.tpa_UsuarioCrea, tbTipoPago.tpa_FechaCrea);
                    foreach (UDP_Vent_tbTipoPago_Update_Result tipopago in list)
                        MensajeError = tipopago.MensajeError;
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
                    ModelState.AddModelError("", "No se ha podido actualizar el registro, favor contacte al administrador" + Ex.Message.ToString());
                    return View(tbTipoPago);
                }

                //db.Entry(tbTipoPago).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.tpa_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPago.tpa_UsuarioCrea);
            ViewBag.tpa_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoPago.tpa_UsuarioModifica);
            return View(tbTipoPago);
        }

        // GET: /TipoPago/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            if (tbTipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoPago);
        }

        // POST: /TipoPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            db.tbTipoPago.Remove(tbTipoPago);
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
