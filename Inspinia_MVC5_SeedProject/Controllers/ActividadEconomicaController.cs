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
    public class ActividadEconomicaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /ActividadEconomica/
        public ActionResult Index()
        {
            return View(db.tbActividadEconomica.ToList());
        }

        // GET: /ActividadEconomica/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            if (tbActividadEconomica == null)
            {
                return HttpNotFound();
            }
            return View(tbActividadEconomica);
        }

        // GET: /ActividadEconomica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ActividadEconomica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="acte_Id,acte_Descripcion,acte_UsuarioCrea,acte_FechaCrea,acte_UsuarioModifica,acte_FechaModifica")] tbActividadEconomica tbActividadEconomica)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //} db.tbActividadEconomica.Add(tbActividadEconomica);
                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbActividadEconomica_Insert(tbActividadEconomica.acte_Descripcion);

                    foreach (UDP_Gral_tbActividadEconomica_Insert_Result ActividadEconomica in list)
                        MensajeError = ActividadEconomica.MensajeError;

                    if (MensajeError == "-1")
                    {
                    }

                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            catch(Exception Ex)
            {
                Ex.Message.ToString();
            }
                return View(tbActividadEconomica);
        }

        // GET: /ActividadEconomica/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            if (tbActividadEconomica == null)
            {
                return HttpNotFound();
            }
            return View(tbActividadEconomica);
        }

        // POST: /ActividadEconomica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "acte_Id,acte_Descripcion,acte_UsuarioCrea,acte_FechaCrea,acte_UsuarioModifica,acte_FechaModifica, tbUsuario, tbUsuario1")] tbActividadEconomica tbActividadEconomica)
        {
            try
            {


                if (ModelState.IsValid)
                {
                  

                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbActividadEconomica_Update(tbActividadEconomica.acte_Id, tbActividadEconomica.acte_Descripcion, tbActividadEconomica.acte_UsuarioCrea, tbActividadEconomica.acte_FechaCrea);
                    foreach (UDP_Gral_tbActividadEconomica_Update_Result ActividadEconomica in list)
                        MensajeError = ActividadEconomica.MensajeError;
                    if (MensajeError == "-1")
                    {
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }


            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return View(tbActividadEconomica);
        }

        // GET: /ActividadEconomica/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            if (tbActividadEconomica == null)
            {
                return HttpNotFound();
            }
            return View(tbActividadEconomica);
        }

        // POST: /ActividadEconomica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            db.tbActividadEconomica.Remove(tbActividadEconomica);
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
