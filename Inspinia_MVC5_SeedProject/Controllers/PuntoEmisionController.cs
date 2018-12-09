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
    public class PuntoEmisionController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /PuntoEmision/
        public ActionResult Index()
        {
            var tbpuntoemision = db.tbPuntoEmision.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbpuntoemision.ToList());
        }

        // GET: /PuntoEmision/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPuntoEmision tbPuntoEmision = db.tbPuntoEmision.Find(id);
            if (tbPuntoEmision == null)
            {
                return HttpNotFound();
            }
            //PuntoEmisionDetalle
            tbPuntoEmisionDetalle tbPuntoEmisionDetalle = db.tbPuntoEmisionDetalle.Find(id);
            ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", tbPuntoEmisionDetalle.dfisc_Id);
            ViewBag.pemid_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmisionDetalle.pemid_UsuarioCrea);
            ViewBag.pemid_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmisionDetalle.pemid_UsuarioModifica);

            return View(tbPuntoEmision);
        }

        // GET: /PuntoEmision/Create
        public ActionResult Create()
        {
            //PuntoEmisionDetalle
            tbPuntoEmisionDetalle tbPuntoEmisionDetalle = new tbPuntoEmisionDetalle();
            ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", tbPuntoEmisionDetalle.dfisc_Id);
            ViewBag.pemid_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmisionDetalle.pemid_UsuarioCrea);
            ViewBag.pemid_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmisionDetalle.pemid_UsuarioModifica);

            //Vistas parciales
            ViewBag.PuntoEmisionDetalle = db.tbPuntoEmisionDetalle.ToList();
            ViewBag.Sucursal = db.tbSucursal.ToList();
            return View();
        }

        // POST: /PuntoEmision/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include="pemi_Id,pemi_NumeroCAI,pemi_UsuarioCrea,pemi_FechaCrea,pemi_UsuarioModifica,pemi_FechaModifica")] tbPuntoEmision tbPuntoEmision)
        {
            if (ModelState.IsValid)
            {
                db.tbPuntoEmision.Add(tbPuntoEmision);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            tbPuntoEmisionDetalle tbPuntoEmisionDetalle = new tbPuntoEmisionDetalle();
            ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", tbPuntoEmisionDetalle.dfisc_Id);
            ViewBag.pemid_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmisionDetalle.pemid_UsuarioCrea);
            ViewBag.pemid_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmisionDetalle.pemid_UsuarioModifica);


            return View(tbPuntoEmision);
        }

        // GET: /PuntoEmision/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPuntoEmision tbPuntoEmision = db.tbPuntoEmision.Find(id);
           
            if (tbPuntoEmision == null)
            {
                return HttpNotFound();
            }

            //PuntoEmisionDetalle
            tbPuntoEmisionDetalle tbPuntoEmisionDetalle = db.tbPuntoEmisionDetalle.Find(id);
            ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", tbPuntoEmisionDetalle.dfisc_Id);
            ViewBag.pemid_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmisionDetalle.pemid_UsuarioCrea);
            ViewBag.pemid_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmisionDetalle.pemid_UsuarioModifica);
            
            return View(tbPuntoEmision);
        }

        // POST: /PuntoEmision/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="pemi_Id,pemi_NumeroCAI,pemi_UsuarioCrea,pemi_FechaCrea,pemi_UsuarioModifica,pemi_FechaModifica")] tbPuntoEmision tbPuntoEmision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPuntoEmision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            //PuntoEmisionDetalle
            tbPuntoEmisionDetalle tbPuntoEmisionDetalle = new tbPuntoEmisionDetalle();
            ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", tbPuntoEmisionDetalle.dfisc_Id);
            ViewBag.pemid_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmisionDetalle.pemid_UsuarioCrea);
            ViewBag.pemid_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPuntoEmisionDetalle.pemid_UsuarioModifica);

            return View(tbPuntoEmision);
        }

        // GET: /PuntoEmision/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPuntoEmision tbPuntoEmision = db.tbPuntoEmision.Find(id);
            if (tbPuntoEmision == null)
            {
                return HttpNotFound();
            }
            return View(tbPuntoEmision);
        }





        //Modal
        //EditarPuntoEmisionDetalle
        public ActionResult EditNumeracion(tbPuntoEmisionDetalle PuntoEmisionDetalle)
        {
            try
            {
                db.Entry(PuntoEmisionDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_EditNumeracion");
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se guardaron los cambios");
                return PartialView("_EditNumeracion", PuntoEmisionDetalle);
            }
        }
        //CreatePuntoEmisionDetalle
        public ActionResult CreateNumeracion(tbPuntoEmisionDetalle PuntoEmisionDetalle)
        {
            try
            {

                db.Entry(PuntoEmisionDetalle).State = EntityState.Modified;
                db.SaveChanges();
               
                return PartialView("_CreateNumeracion");
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se guardaron los cambios");
                return PartialView("_CreateNumeracion", PuntoEmisionDetalle);
            }
        }
        //Modal




        // POST: /PuntoEmision/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPuntoEmision tbPuntoEmision = db.tbPuntoEmision.Find(id);
            db.tbPuntoEmision.Remove(tbPuntoEmision);
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
