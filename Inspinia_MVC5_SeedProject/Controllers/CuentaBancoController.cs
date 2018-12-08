using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_ZORZAL.Models;

namespace ERP_ZORZAL.Controllers
{
    public class CuentaBancoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /CuentaBanco/
        public ActionResult Index()
        {
            var tbcuentasbanco = db.tbCuentasBanco.Include(t => t.tbBanco);
            return View(tbcuentasbanco.ToList());
        }

        // GET: /CuentaBanco/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuentasBanco tbCuentasBanco = db.tbCuentasBanco.Find(id);
            if (tbCuentasBanco == null)
            {
                return HttpNotFound();
            }
            return View(tbCuentasBanco);
        }

        // GET: /CuentaBanco/Create
        public ActionResult Create()
        {
            tbCuentasBanco CuentasBanco = new tbCuentasBanco();
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre");
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
            /////////Aqui lleno la lista/////////
            CuentasBanco.TipoCuentaList = cUtilities.TipoCuentaList();

            return View(CuentasBanco);
        }

        // POST: /CuentaBanco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bcta_Id,ban_Id,mnda_Id,bcta_TipoCuenta,bcta_TotalCredito,bcta_TotalDebito,bcta_FechaApertura,bcta_Numero,bcta_UsuarioCrea,bcta_FechaCrea,bcta_UsuarioModifica,bcta_FechaModifica")] tbCuentasBanco tbCuentasBanco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbCuentasBanco_Insert(
                        tbCuentasBanco.ban_Id,
                        tbCuentasBanco.mnda_Id,
                        tbCuentasBanco.bcta_TipoCuenta, 
                        tbCuentasBanco.bcta_TotalCredito,
                        tbCuentasBanco.bcta_TotalDebito, 
                        tbCuentasBanco.bcta_FechaApertura,
                        tbCuentasBanco.bcta_Numero);
                    foreach (UDP_Gral_tbCuentasBanco_Insert_Result cuentasbanco in list)
                    MensajeError = cuentasbanco.MensajeError;
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
                      return View(tbCuentasBanco);
                    }   
            }

            

            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", tbCuentasBanco.ban_Id);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbCuentasBanco.mnda_Id);
            tbCuentasBanco CuentasBanco = new tbCuentasBanco();
            CuentasBanco.TipoCuentaList = cUtilities.TipoCuentaList();
            return View(tbCuentasBanco);
        }

        // GET: /CuentaBanco/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuentasBanco tbCuentasBanco = db.tbCuentasBanco.Find(id);
            if (tbCuentasBanco == null)
            {
                return HttpNotFound();
            }
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", tbCuentasBanco.ban_Id);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbCuentasBanco.mnda_Id);
            tbCuentasBanco.TipoCuentaList = cUtilities.TipoCuentaList();
            return View(tbCuentasBanco);
        }

        // POST: /CuentaBanco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="bcta_Id,ban_Id,mnda_Id,bcta_TipoCuenta,bcta_TotalCredito,bcta_TotalDebito,bcta_FechaApertura,bcta_Numero,bcta_UsuarioCrea,bcta_FechaCrea,bcta_UsuarioModifica,bcta_FechaModifica") ] tbCuentasBanco tbCuentasBanco)
        {
              if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbCuentasBanco_Update(
                        tbCuentasBanco.bcta_Id,
                        tbCuentasBanco.ban_Id,
                        tbCuentasBanco.mnda_Id,
                        tbCuentasBanco.bcta_TipoCuenta,
                        tbCuentasBanco.bcta_TotalCredito,
                        tbCuentasBanco.bcta_TotalDebito,
                        tbCuentasBanco.bcta_FechaApertura,
                        tbCuentasBanco.bcta_Numero,
                        tbCuentasBanco.bcta_UsuarioCrea,
                        tbCuentasBanco.bcta_FechaCrea);
                    foreach (UDP_Gral_tbCuentasBanco_Update_Result cuentasbanco in list)
                    MensajeError = cuentasbanco.MensajeError;
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
                return View(tbCuentasBanco);
            }
        }

            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", tbCuentasBanco.ban_Id);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbCuentasBanco.mnda_Id);
            tbCuentasBanco.TipoCuentaList = cUtilities.TipoCuentaList();
            return View(tbCuentasBanco);
        }

        // GET: /CuentaBanco/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCuentasBanco tbCuentasBanco = db.tbCuentasBanco.Find(id);
            if (tbCuentasBanco == null)
            {
                return HttpNotFound();
            }
            return View(tbCuentasBanco);
        }

        // POST: /CuentaBanco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbCuentasBanco tbCuentasBanco = db.tbCuentasBanco.Find(id);
            db.tbCuentasBanco.Remove(tbCuentasBanco);
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
