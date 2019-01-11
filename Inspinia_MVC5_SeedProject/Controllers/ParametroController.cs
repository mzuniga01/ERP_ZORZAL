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
    public class ParametroController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Parametro/
        public ActionResult Index()
        {
            var conteo = db.ConteoParametro(1).ToList();
            int? par = 0;
            var tbparametro = db.tbParametro.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbMoneda);
            if(tbparametro == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (ConteoParametro_Result contarparametro in conteo)
                    par = contarparametro.Conteo;
                if(par > 0)
                {
                    return RedirectToAction("Edit/" + 1 , "Parametro");
                }
                else
                {
                    return RedirectToAction("Create" , "Parametro");
                }
            }
            return View(tbparametro.ToList());
        }

        // GET: /Parametro/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbParametro tbParametro = db.tbParametro.Find(id);
            if (tbParametro == null)
            {
                return HttpNotFound();
            }
            return View(tbParametro);
        }

        // GET: /Parametro/Create
        public ActionResult Create()
        {
            ViewBag.par_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.par_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Abreviatura");
            return View();
        }

        // POST: /Parametro/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="par_Id,par_NombreEmpresa,par_TelefonoEmpresa,par_CorreoEmpresa,par_PathLogo,mnda_Id,par_RolGerenteTienda,par_RolCreditoCobranza,par_RolSupervisorCaja,par_RolCajero,par_RolAuditor,par_SucursalPrincipal,par_UsuarioCrea,par_FechaCrea,par_UsuarioModifica,par_FechaModifica,par_PorcentajeDescuentoTE,par_IdConsumidorFinal")] tbParametro tbParametro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Gral_tbParametro_Insert(tbParametro.par_Id,tbParametro.par_NombreEmpresa, tbParametro.par_TelefonoEmpresa, tbParametro.par_CorreoEmpresa, tbParametro.par_PathLogo, tbParametro.mnda_Id, tbParametro.par_RolGerenteTienda, tbParametro.par_RolCreditoCobranza, tbParametro.par_RolSupervisorCaja, tbParametro.par_RolCajero, tbParametro.par_RolAuditor, tbParametro.par_SucursalPrincipal, tbParametro.par_PorcentajeDescuentoTE, tbParametro.par_IdConsumidorFinal);
                    foreach (UDP_Gral_tbParametro_Insert_Result parametro in List)
                        MsjError = parametro.MensajeError;

                    if (MsjError == "-1")
                    {
                        ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            
            ViewBag.par_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbParametro.par_UsuarioModifica);
            ViewBag.par_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbParametro.par_UsuarioCrea);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Abreviatura", tbParametro.mnda_Id);
            return View(tbParametro);
        }

        // GET: /Parametro/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbParametro tbParametro = db.tbParametro.Find(id);
            if (tbParametro == null)
            {
                return HttpNotFound();
            }
            ViewBag.par_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbParametro.par_UsuarioModifica);
            ViewBag.par_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbParametro.par_UsuarioCrea);
            ViewBag.id_mnda = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Abreviatura", tbParametro.mnda_Id);
            return View(tbParametro);
        }

        // POST: /Parametro/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(byte? id,[Bind(Include="par_Id,par_NombreEmpresa,par_TelefonoEmpresa,par_CorreoEmpresa,par_PathLogo,mnda_Id,par_RolGerenteTienda,par_RolCreditoCobranza,par_RolSupervisorCaja,par_RolCajero,par_RolAuditor,par_SucursalPrincipal,par_UsuarioCrea,par_FechaCrea,par_UsuarioModifica,par_FechaModifica,par_PorcentajeDescuentoTE,par_IdConsumidorFinal")] tbParametro tbParametro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbParametro vtbparametro = db.tbParametro.Find(id);

                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Gral_tbParametro_Update(tbParametro.par_Id,tbParametro.par_NombreEmpresa,tbParametro.par_TelefonoEmpresa, tbParametro.par_CorreoEmpresa, tbParametro.par_PathLogo, tbParametro.mnda_Id, tbParametro.par_RolGerenteTienda, tbParametro.par_RolCreditoCobranza, tbParametro.par_RolSupervisorCaja, tbParametro.par_RolCajero, tbParametro.par_RolAuditor, tbParametro.par_SucursalPrincipal, tbParametro.par_UsuarioCrea, tbParametro.par_FechaCrea, tbParametro.par_PorcentajeDescuentoTE, tbParametro.par_IdConsumidorFinal);
                    foreach (UDP_Gral_tbParametro_Update_Result parametro in List)
                        MsjError = parametro.MensajeError;
                    if (MsjError == "-1")
                    {
                        ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                }
                return RedirectToAction("Index");
            }
            ViewBag.par_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbParametro.par_UsuarioModifica);
            ViewBag.par_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbParametro.par_UsuarioCrea);
            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Abreviatura", tbParametro.mnda_Id);
            return View(tbParametro);
        }

        // GET: /Parametro/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbParametro tbParametro = db.tbParametro.Find(id);
            if (tbParametro == null)
            {
                return HttpNotFound();
            }
            return View(tbParametro);
        }

        // POST: /Parametro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbParametro tbParametro = db.tbParametro.Find(id);
            db.tbParametro.Remove(tbParametro);
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
