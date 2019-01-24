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
    public class TipoIdentificacionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /TipoIdentificacion/
        public ActionResult Index()
        {
            return View(db.tbTipoIdentificacion.ToList());
        }

        // GET: /TipoIdentificacion/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoIdentificacion tbTipoIdentificacion = db.tbTipoIdentificacion.Find(id);
            if (tbTipoIdentificacion == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoIdentificacion);
        }

        // GET: /TipoIdentificacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TipoIdentificacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tpi_Id,tpi_Descripcion,tpi_UsuarioCrea,tpi_FechaCrea,tpi_UsuarioModifica,tpi_FechaModifica")] tbTipoIdentificacion tbTipoIdentificacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.tbTipoIdentificacion.Add(tbTipoIdentificacion);
                    //db.SaveChanges();
                    //return RedirectToAction("Index");

                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbTipoIdentificacion_Insert(tbTipoIdentificacion.tpi_Descripcion);
                    foreach (UDP_Gral_tbTipoIdentificacion_Insert_Result TipoIdentificacion in list)
                        MensajeError = TipoIdentificacion.MensajeError;
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

            

            return View(tbTipoIdentificacion);
        }

        // GET: /TipoIdentificacion/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoIdentificacion tbTipoIdentificacion = db.tbTipoIdentificacion.Find(id);
            if (tbTipoIdentificacion == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoIdentificacion);
        }

        // POST: /TipoIdentificacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="tpi_Id,tpi_Descripcion,tpi_UsuarioCrea,tpi_FechaCrea,tpi_UsuarioModifica,tpi_FechaModifica, tbUsuario, tbUsuario1")] tbTipoIdentificacion tbTipoIdentificacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //////////Aqui va la lista//////////////

                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbTipoIdentificacion_Update(tbTipoIdentificacion.tpi_Id, tbTipoIdentificacion.tpi_Descripcion, tbTipoIdentificacion.tpi_UsuarioCrea, tbTipoIdentificacion.tpi_FechaCrea, tbTipoIdentificacion.tpi_UsuarioModifica, tbTipoIdentificacion.tpi_FechaModifica);
                    foreach (UDP_Gral_tbTipoIdentificacion_Update_Result TipoIdentificacion in list)
                        MensajeError = TipoIdentificacion.MensajeError;
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
           
            return View(tbTipoIdentificacion);
        }

        // GET: /TipoIdentificacion/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoIdentificacion tbTipoIdentificacion = db.tbTipoIdentificacion.Find(id);
            if (tbTipoIdentificacion == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoIdentificacion);
        }

        // POST: /TipoIdentificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbTipoIdentificacion tbTipoIdentificacion = db.tbTipoIdentificacion.Find(id);
            db.tbTipoIdentificacion.Remove(tbTipoIdentificacion);
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
