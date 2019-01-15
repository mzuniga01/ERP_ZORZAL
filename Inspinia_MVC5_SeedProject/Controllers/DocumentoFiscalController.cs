using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class DocumentoFiscalController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /DocumentoFiscal/
        public ActionResult Index()
        {
            var tbdocumentofiscal = db.tbDocumentoFiscal.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbdocumentofiscal.ToList());
        }

        // GET: /DocumentoFiscal/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDocumentoFiscal tbDocumentoFiscal = db.tbDocumentoFiscal.Find(id);
            if (tbDocumentoFiscal == null)
            {
                return HttpNotFound();
            }
            return View(tbDocumentoFiscal);
        }

        // GET: /DocumentoFiscal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DocumentoFiscal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "dfisc_Id,dfisc_Descripcion,dfisc_UsuarioCrea,dfisc_FechaCrea,dfisc_UsuarioModifica,dfisc_FechaModifica")] tbDocumentoFiscal tbDocumentoFiscal)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbDocumentoFiscal_Insert(tbDocumentoFiscal.dfisc_Id, tbDocumentoFiscal.dfisc_Descripcion);
                    foreach (UDP_Vent_tbDocumentoFiscal_Insert_Result DocumentoFiscal in list)
                        MensajeError = Convert.ToInt32(DocumentoFiscal.MensajeError);
                    if (MensajeError == -1)
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, Codigo Duplicado.");
                        return View(tbDocumentoFiscal);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo insertar el registro, Codigo Duplicado.");
                    return View(tbDocumentoFiscal);
                }
            }
            return View(tbDocumentoFiscal);
        }

        // GET: /DocumentoFiscal/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDocumentoFiscal tbDocumentoFiscal = db.tbDocumentoFiscal.Find(id);
            if (tbDocumentoFiscal == null)
            {
                return HttpNotFound();
            }
            ViewBag.dfisc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDocumentoFiscal.dfisc_UsuarioCrea);
            ViewBag.dfisc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDocumentoFiscal.dfisc_UsuarioModifica);
            return View(tbDocumentoFiscal);
        }

        // POST: /DocumentoFiscal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dfisc_Id,dfisc_Descripcion,dfisc_UsuarioCrea,dfisc_FechaCrea,dfisc_UsuarioModifica,dfisc_FechaModifica")] tbDocumentoFiscal tbDocumentoFiscal)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbDocumentoFiscal_Update(tbDocumentoFiscal.dfisc_Id, tbDocumentoFiscal.dfisc_Descripcion, tbDocumentoFiscal.dfisc_UsuarioCrea, tbDocumentoFiscal.dfisc_FechaCrea);
                    foreach (UDP_Vent_tbDocumentoFiscal_Update_Result DocumentoFiscal in list)
                        MensajeError = Convert.ToInt32(DocumentoFiscal.MensajeError);
                    if (MensajeError == -1)
                    {
                        ModelState.AddModelError("", "No se pudo actualizar el registro, Codigo Duplicado.");
                        return View(tbDocumentoFiscal);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo actualizar el registro, Codigo Duplicado.");
                    return View(tbDocumentoFiscal);
                }
            }
            return View(tbDocumentoFiscal);
        }

        // GET: /DocumentoFiscal/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDocumentoFiscal tbDocumentoFiscal = db.tbDocumentoFiscal.Find(id);
            if (tbDocumentoFiscal == null)
            {
                return HttpNotFound();
            }
            return View(tbDocumentoFiscal);
        }

        // POST: /DocumentoFiscal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbDocumentoFiscal tbDocumentoFiscal = db.tbDocumentoFiscal.Find(id);
            db.tbDocumentoFiscal.Remove(tbDocumentoFiscal);
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
