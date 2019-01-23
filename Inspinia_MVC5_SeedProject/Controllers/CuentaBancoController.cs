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
            tbCuentasBanco.TipoCuentaList = cUtilities.TipoCuentaList();
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
                    var MensajeError = "";
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
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbCuentasBanco);
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
            tbCuentasBanco CuentasBanco = db.tbCuentasBanco.Find(id);
            if (CuentasBanco == null)
            {
                return HttpNotFound();
            }
            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", CuentasBanco.ban_Id);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", CuentasBanco.mnda_Id);
            var Lista = cUtilities.TipoCuentaList();
            ViewBag.TipoCuentaList = new SelectList(Lista, "ID_TIPOCUENTA", "DESCRIPCION", CuentasBanco.bcta_TipoCuenta);
            return View(CuentasBanco);
        }

        // POST: /CuentaBanco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "bcta_Id,ban_Id,mnda_Id,bcta_TipoCuenta,bcta_TotalCredito,bcta_TotalDebito,bcta_FechaApertura,bcta_Numero,bcta_UsuarioCrea,bcta_FechaCrea,bcta_UsuarioModifica,bcta_FechaModifica, tbUsuario, tbUsuario1") ] tbCuentasBanco CuentasBanco)
        {
              if (ModelState.IsValid)
            {
            try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbCuentasBanco_Update(
                        CuentasBanco.bcta_Id,
                        CuentasBanco.ban_Id,
                        CuentasBanco.mnda_Id,
                        CuentasBanco.bcta_TipoCuenta,
                        CuentasBanco.bcta_TotalCredito,
                        CuentasBanco.bcta_TotalDebito,
                        CuentasBanco.bcta_FechaApertura,
                        CuentasBanco.bcta_Numero,
                        CuentasBanco.bcta_UsuarioCrea,
                        CuentasBanco.bcta_FechaCrea);
                    foreach (UDP_Gral_tbCuentasBanco_Update_Result cuentasbanco in list)
                    MensajeError = cuentasbanco.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(CuentasBanco);
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
                    ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", CuentasBanco.ban_Id);
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", CuentasBanco.mnda_Id);
                    var Lista = cUtilities.TipoCuentaList();
                    ViewBag.TipoCuentaList = new SelectList(Lista, "ID_TIPOCUENTA", "DESCRIPCION", CuentasBanco.bcta_TipoCuenta);
                    return View(CuentasBanco);
                }
            
            }

            ViewBag.ban_Id = new SelectList(db.tbBanco, "ban_Id", "ban_Nombre", CuentasBanco.ban_Id);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", CuentasBanco.mnda_Id);
            
            return View(CuentasBanco);
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
