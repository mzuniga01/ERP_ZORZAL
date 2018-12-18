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
    public class BancoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Banco/
        public ActionResult Index()
        {
            return View(db.tbBanco.ToList());
        }

        // GET: /Banco/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanco tbBanco = db.tbBanco.Find(id);
            if (tbBanco == null)
            {
                return HttpNotFound();
            }
            return View(tbBanco);
        }

        // GET: /Banco/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Banco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ban_Id,ban_Nombre,ban_NombreContacto,ban_TelefonoContacto,ban_UsuarioCrea,ban_FechaCrea,ban_UsuarioModifica,ban_FechaModifica")] tbBanco tbBanco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbBanco_Insert(tbBanco.ban_Nombre, tbBanco.ban_NombreContacto, tbBanco.ban_TelefonoContacto);
                    foreach (UDP_Gral_tbBanco_Insert_Result banco in list)
                     MensajeError = banco.MensajeError;
                if (MensajeError == -1)
                {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbBanco);
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
                    return View(tbBanco);
                }
            }
            return View(tbBanco);
        }

        // GET: /Banco/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanco tbBanco = db.tbBanco.Find(id);
            if (tbBanco == null)
            {
                return HttpNotFound();
            }
            return View(tbBanco);
        }

        // POST: /Banco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ban_Id,ban_Nombre,ban_NombreContacto,ban_TelefonoContacto,ban_UsuarioCrea,ban_FechaCrea,ban_UsuarioModifica,ban_FechaModifica")] tbBanco tbBanco)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    //////////Aqui va la lista//////////////
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbBanco_Update(tbBanco.ban_Id, tbBanco.ban_Nombre, tbBanco.ban_NombreContacto, tbBanco.ban_TelefonoContacto, tbBanco.ban_UsuarioCrea, tbBanco.ban_FechaCrea);
                    foreach (UDP_Gral_tbBanco_Update_Result banco in list)
                     MensajeError = banco.MensajeError;
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
                ModelState.AddModelError("", "Error al agregar el registro" + Ex.Message.ToString());
                return View(tbBanco);
                }
             }
            return View(tbBanco);
        }


    // GET: /Banco/Delete/5
    public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanco tbBanco = db.tbBanco.Find(id);
            if (tbBanco == null)
            {
                return HttpNotFound();
            }
            return View(tbBanco);
        }

        // POST: /Banco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbBanco tbBanco = db.tbBanco.Find(id);
            db.tbBanco.Remove(tbBanco);
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
