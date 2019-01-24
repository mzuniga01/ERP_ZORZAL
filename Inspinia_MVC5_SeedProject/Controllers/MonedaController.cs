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
    public class MonedaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Moneda/
        public ActionResult Index()
        {
            var tbMoneda = db.tbMoneda.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbMoneda.ToList());
        }

        // GET: /Moneda/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbMoneda);
        }

        // GET: /Moneda/Create
        public ActionResult Create()
        {
            //ViewBag.mnda_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.mnda_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: /Moneda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="mnda_Id,mnda_Abreviatura,mnda_Nombre,mnda_UsuarioCrea,mnda_FechaCrea,mnda_UsuarioModifica,mnda_FechaModifica")] tbMoneda tbMoneda)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //////////Aqui va la lista//////////////

                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbMoneda_Insert(tbMoneda.mnda_Abreviatura, tbMoneda.mnda_Nombre);
                    foreach (UDP_Gral_tbMoneda_Insert_Result Moneda in list)
                        MensajeError = Moneda.MensajeError;
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

            return View(tbMoneda);


        }

        // GET: /Moneda/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return HttpNotFound();
            }
            //ViewBag.mnda_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMoneda.mnda_UsuarioCrea);
            //ViewBag.mnda_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMoneda.mnda_UsuarioModifica);
            return View(tbMoneda);
        }

        // POST: /Moneda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "mnda_Id,mnda_Abreviatura,mnda_Nombre,mnda_UsuarioCrea,mnda_FechaCrea,mnda_UsuarioModifica,mnda_FechaModifica, tbUsuario, tbUsuario1")] tbMoneda tbMoneda)
        {

            try
            {

                if (ModelState.IsValid)
                {  
                    //    db.Entry(tbMoneda).State = EntityState.Modified;
                   //    db.SaveChanges();
                   //    return RedirectToAction("Index");


                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbMoneda_Update(tbMoneda.mnda_Id, tbMoneda.mnda_Abreviatura, tbMoneda.mnda_Nombre, tbMoneda.mnda_UsuarioCrea,tbMoneda.mnda_FechaCrea, tbMoneda.mnda_UsuarioModifica, tbMoneda.mnda_FechaModifica);
                    foreach (UDP_Gral_tbMoneda_Update_Result Moneda in list)
                        MensajeError = Moneda.MensajeError;
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

            return View(tbMoneda);
        }

        // GET: /Moneda/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbMoneda);
        }

        // POST: /Moneda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            db.tbMoneda.Remove(tbMoneda);
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
