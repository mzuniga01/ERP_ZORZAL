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
    public class PagosArqueoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /PagosArqueo/
        public ActionResult Index()
        {
            var tbpagosarqueo = db.tbPagosArqueo.Include(t => t.tbUsuario).Include(t => t.tbUsuario1)/*.Include(t => t.tbMovimientoCaja)*/.Include(t => t.tbTipoPago);
            return View(tbpagosarqueo.ToList());
        }

        // GET: /PagosArqueo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPagosArqueo tbPagosArqueo = db.tbPagosArqueo.Find(id);
            if (tbPagosArqueo == null)
            {
                return HttpNotFound();
            }
            return View(tbPagosArqueo);
        }

        // GET: /PagosArqueo/Create
        public ActionResult Create()
        {
            ViewBag.arqpg_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.arqpg_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id");
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion");
            return View();
        }

        // POST: /PagosArqueo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="arqpg_Id,mocja_Id,tpa_Id,arqpg_PagosSistema,arqpg_PagosConteo,arqpg_UsuarioCrea,arqpg_FechaCrea,arqpg_UsuarioModifica,arqpg_FechaModifica")] tbPagosArqueo tbPagosArqueo)
        {
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbPagosArqueo.mocja_Id);
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPagosArqueo.tpa_Id);
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = string.Empty;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbPagosArqueo_Insert(tbPagosArqueo.mocja_Id, tbPagosArqueo.tpa_Id, tbPagosArqueo.arqpg_PagosSistema, tbPagosArqueo.arqpg_PagosConteo);
                    foreach (UDP_Vent_tbPagosArqueo_Insert_Result denoarq in list)
                        MensajeError = denoarq.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbPagosArqueo);
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


                    return View(tbPagosArqueo);
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }

            ViewBag.arqde_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPagosArqueo.arqpg_UsuarioCrea);
            ViewBag.arqde_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPagosArqueo.arqpg_UsuarioModifica);
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPagosArqueo.tpa_Id);

            return View(tbPagosArqueo);
            
        }

        // GET: /PagosArqueo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPagosArqueo tbPagosArqueo = db.tbPagosArqueo.Find(id);
            if (tbPagosArqueo == null)
            {
                return HttpNotFound();
            }
            ViewBag.arqpg_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPagosArqueo.arqpg_UsuarioCrea);
            ViewBag.arqpg_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPagosArqueo.arqpg_UsuarioModifica);
            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbPagosArqueo.mocja_Id);
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPagosArqueo.tpa_Id);
            return View(tbPagosArqueo);
        }

        // POST: /PagosArqueo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "arqpg_Id,mocja_Id,tpa_Id,arqpg_PagosSistema,arqpg_PagosConteo,arqpg_UsuarioCrea,arqpg_FechaCrea,arqpg_UsuarioModifica,arqpg_FechaModifica,tbUsuario,tbUsuario1")] tbPagosArqueo tbPagosArqueo)
        {
             ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbPagosArqueo.mocja_Id);
             ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPagosArqueo.tpa_Id);
            if (ModelState.IsValid)
            {
                try
                {
                    //tbDenominacionArqueo vDenominacionArqueo = db.tbDenominacionArqueo.Find(id);
                    //////////Aqui va la lista//////////////
                    var MensajeError = string.Empty;
                    IEnumerable<object> list = null;
                    //list = db.UDP_Vent_tbPagosArqueo_Update(tbPagosArqueo.arqpg_Id, tbPagosArqueo.mocja_Id, tbPagosArqueo.tpa_Id, tbPagosArqueo.arqpg_PagosSistema, tbPagosArqueo.arqpg_PagosConteo, tbPagosArqueo.arqpg_UsuarioCrea, tbPagosArqueo.arqpg_FechaCrea);
                    //foreach (UDP_Vent_tbPagosArqueo_Update_Result paar in list)
                        //MensajeError = paar.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbPagosArqueo);
                    }
                    else
                    {

                        return RedirectToAction("Index");
                    }
                }

                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");


                    return View(tbPagosArqueo);
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }

            ViewBag.arqpg_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPagosArqueo.arqpg_UsuarioCrea);
            ViewBag.arqpg_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPagosArqueo.arqpg_UsuarioModifica);
            ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPagosArqueo.tpa_Id);

            return View(tbPagosArqueo);






            //if (ModelState.IsValid)
            //{
            //    db.Entry(tbPagosArqueo).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.arqpg_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPagosArqueo.arqpg_UsuarioCrea);
            //ViewBag.arqpg_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPagosArqueo.arqpg_UsuarioModifica);
            //ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbPagosArqueo.mocja_Id);
            //ViewBag.tpa_Id = new SelectList(db.tbTipoPago, "tpa_Id", "tpa_Descripcion", tbPagosArqueo.tpa_Id);
            //return View(tbPagosArqueo);
        }

        // GET: /PagosArqueo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPagosArqueo tbPagosArqueo = db.tbPagosArqueo.Find(id);
            if (tbPagosArqueo == null)
            {
                return HttpNotFound();
            }
            return View(tbPagosArqueo);
        }

        // POST: /PagosArqueo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPagosArqueo tbPagosArqueo = db.tbPagosArqueo.Find(id);
            db.tbPagosArqueo.Remove(tbPagosArqueo);
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
