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
            var tbmovimientocaja = db.tbMovimientoCaja.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCaja);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion");
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
            ViewBag.DenominacionArqueo = db.tbDenominacionArqueo.ToList();
            return View();
        }

        // POST: /MovimientoCaja/CreateApertura
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateApertura([Bind(Include = "mocja_Id,cja_Id,mocja_FechaApeturamocja_UsuarioCrea,mocja_FechaCrea,mocja_UsuarioModifica,mocja_FechaModifica")] tbMovimientoCaja tbMovimientoCaja)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbMovimientoCaja_Apertura_Insert(tbMovimientoCaja.cja_Id,tbMovimientoCaja.mocja_FechaApertura,tbMovimientoCaja.mocja_UsuarioApertura);
                    foreach (UDP_Vent_tbMovimientoCaja_Apertura_Insert_Result banco in list)
                        MensajeError = banco.MensajeError;
                    if (MensajeError == -1)
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
            return View(tbMovimientoCaja);
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
            MC.cja_Id = 2;
            return View(MC);

        }

        // POST: /MovimientoCaja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="mocja_Id,cja_Id,mocja_FechaApetura,mocja_UsuarioApertura,mocja_FechaArqueo,mocja_UsuarioArquea,mocja_FechaAceptacion,mocja_UsuarioAceptacion,mocja_UsuarioCrea,mocja_FechaCrea,mocja_UsuarioModifica,mocja_FechaModifica")] tbMovimientoCaja tbMovimientoCaja)
        {
            if (ModelState.IsValid)
            {
                db.tbMovimientoCaja.Add(tbMovimientoCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioCrea);
            ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
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
