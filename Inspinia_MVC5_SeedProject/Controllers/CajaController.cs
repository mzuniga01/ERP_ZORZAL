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
    public class CajaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Caja/
        public ActionResult Index()
        {
            var tbcaja = db.tbCaja.Include(t => t.tbSucursal);
            return View(tbcaja.ToList());
        }

        // GET: /Caja/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCaja tbCaja = db.tbCaja.Find(id);
            if (tbCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbCaja);
        }

        // GET: /Caja/Create
        public ActionResult Create()
        {
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
            return View();
        }

        // POST: /Caja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="cja_Id,cja_Descripcion,suc_Id,cja_UsuarioCrea,cja_FechaCrea,cja_UsuarioModifica,cja_FechaModifica")] tbCaja tbCaja)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbCaja_Insert(tbCaja.cja_Id,tbCaja.cja_Descripcion,tbCaja.suc_Id);
                    foreach (UDP_Vent_tbCaja_Insert_Result caja in list)
                        MensajeError = caja.MensajeError;
                    if (MensajeError == -1)
                    {
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                    db.tbCaja.Add(tbCaja);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            //if (ModelState.IsValid)
            //{
            //    db.tbCaja.Add(tbCaja);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbCaja.suc_Id);
            return View(tbCaja);
        }

        // GET: /Caja/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCaja tbCaja = db.tbCaja.Find(id);
            if (tbCaja == null)
            {
                return HttpNotFound();
            }
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbCaja.suc_Id);
            return View(tbCaja);
        }

        // POST: /Caja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="cja_Id,cja_Descripcion,suc_Id,cja_UsuarioCrea,cja_FechaCrea,cja_UsuarioModifica,cja_FechaModifica")] tbCaja tbCaja)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = 0;
                IEnumerable<object> list = null;
                //list = db.UDP_Vent_tbCaja_Update(tbCaja.cja_Id,tbCaja.cja_Descripcion,tbCaja.suc_Id,tbCaja.cja_UsuarioCrea,tbCaja.cja_FechaCrea,tbCaja.cja_UsuarioCrea,tbCaja.cja_FechaModifica);
                foreach (UDP_Vent_tbCaja_Update_Result caja in list)
                    MensajeError = caja.MensajeError;
                if (MensajeError == -1)
                {
                }
                else
                {
                    return RedirectToAction("Index");
                }

                //db.Entry(tbCaja).State = EntityState.Modified
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
            }

            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbCaja.suc_Id);
            
            return View(tbCaja);

            
            
        }

        // GET: /Caja/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCaja tbCaja = db.tbCaja.Find(id);
            if (tbCaja == null)
            {
                return HttpNotFound();
            }
            return View(tbCaja);
        }

        // POST: /Caja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbCaja tbCaja = db.tbCaja.Find(id);
            db.tbCaja.Remove(tbCaja);
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
