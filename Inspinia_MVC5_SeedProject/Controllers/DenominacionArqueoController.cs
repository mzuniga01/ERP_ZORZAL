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
    public class DenominacionArqueoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /DenominacionArqueo/
        public ActionResult Index()
        {
            var tbdenominacionarqueo = db.tbDenominacionArqueo.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbDenominacion)/*.Include(t => t.tbMovimientoCaja)*/;
            return View(tbdenominacionarqueo.ToList());
        }

        // GET: /DenominacionArqueo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDenominacionArqueo tbDenominacionArqueo = db.tbDenominacionArqueo.Find(id);
            if (tbDenominacionArqueo == null)
            {
                return HttpNotFound();
            }
            return View(tbDenominacionArqueo);
        }

        // GET: /DenominacionArqueo/Create
        public ActionResult Create()
        {
            ViewBag.arqde_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.arqde_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion");
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id");
            return View();
        }

        // POST: /DenominacionArqueo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="mocja_Id,deno_Id,arqde_CantidadDenominacion,arqde_MontoDenominacion")] tbDenominacionArqueo tbDenominacionArqueo)
        {
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbDenominacionArqueo.mocja_Id);
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = string.Empty;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbDenominacionArqueo_Insert(tbDenominacionArqueo.mocja_Id, tbDenominacionArqueo.deno_Id, tbDenominacionArqueo.arqde_CantidadDenominacion, tbDenominacionArqueo.arqde_MontoDenominacion);
                    foreach (UDP_Vent_tbDenominacionArqueo_Insert_Result denoarq in list)
                        MensajeError = denoarq.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbDenominacionArqueo);
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
                   

                    return View(tbDenominacionArqueo);
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }

            ViewBag.arqde_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacionArqueo.arqde_UsuarioCrea);
            ViewBag.arqde_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacionArqueo.arqde_UsuarioModifica);
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);
           
            return View(tbDenominacionArqueo);
        }

        // GET: /DenominacionArqueo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDenominacionArqueo tbDenominacionArqueo = db.tbDenominacionArqueo.Find(id);
            if (tbDenominacionArqueo == null)
            {
                return HttpNotFound();
            }
            ViewBag.arqde_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacionArqueo.arqde_UsuarioCrea);
            ViewBag.arqde_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacionArqueo.arqde_UsuarioModifica);
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbDenominacionArqueo.mocja_Id);
            return View(tbDenominacionArqueo);
        }

        // POST: /DenominacionArqueo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "arqde_Id,mocja_Id,deno_Id,arqde_CantidadDenominacion,arqde_MontoDenominacion,arqde_UsuarioCrea,arqde_FechaCrea,arqde_UsuarioModifica,arqde_FechaModifica,tbUsuario,tbUsuario1")] tbDenominacionArqueo tbDenominacionArqueo)
        {
            
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbDenominacionArqueo.mocja_Id);
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);
            if (ModelState.IsValid)
            {
                try
                {
                    //tbDenominacionArqueo vDenominacionArqueo = db.tbDenominacionArqueo.Find(id);
                    //////////Aqui va la lista//////////////
                    var MensajeError = string.Empty;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbDenominacionArqueo_Update(tbDenominacionArqueo.arqde_Id, tbDenominacionArqueo.mocja_Id, tbDenominacionArqueo.deno_Id, tbDenominacionArqueo.arqde_CantidadDenominacion, tbDenominacionArqueo.arqde_MontoDenominacion, tbDenominacionArqueo.arqde_UsuarioCrea, tbDenominacionArqueo.arqde_FechaCrea);
                    foreach (UDP_Vent_tbDenominacionArqueo_Update_Result denoarq in list)
                        MensajeError = denoarq.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbDenominacionArqueo);
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


                    return View(tbDenominacionArqueo);
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }

            ViewBag.arqde_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacionArqueo.arqde_UsuarioCrea);
            ViewBag.arqde_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbDenominacionArqueo.arqde_UsuarioModifica);
            ViewBag.deno_Id = new SelectList(db.tbDenominacion, "deno_Id", "deno_Descripcion", tbDenominacionArqueo.deno_Id);

            return View(tbDenominacionArqueo);
        }

        // GET: /DenominacionArqueo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDenominacionArqueo tbDenominacionArqueo = db.tbDenominacionArqueo.Find(id);
            if (tbDenominacionArqueo == null)
            {
                return HttpNotFound();
            }
            return View(tbDenominacionArqueo);
        }

        // POST: /DenominacionArqueo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDenominacionArqueo tbDenominacionArqueo = db.tbDenominacionArqueo.Find(id);
            db.tbDenominacionArqueo.Remove(tbDenominacionArqueo);
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
