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
    public class NotaCreditoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /NotaCredito/
        public ActionResult Index()
        {
            var tbnotacredito = db.tbNotaCredito.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbDevolucion).Include(t => t.tbSucursal);
            return View(tbnotacredito.ToList());
        }

        public ActionResult _IndexDevolucion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            if (tbDevolucion == null)
            {
                return HttpNotFound();
            }
            return View(tbDevolucion);
        }


        // GET: /NotaCredito/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            if (tbNotaCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbNotaCredito);
        }

        // GET: /NotaCredito/Create
        public ActionResult Create()
        {
            ViewBag.nocre_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.nocre_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion");
            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Id");
            ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();

            return View();
        }

        // POST: /NotaCredito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "nocre_Id,nocre_Codigo,dev_Id,clte_Id,suc_Id,cja_Id,nocre_Anulado,nocre_FechaEmision,nocre_MotivoEmision,nocre_Monto,nocre_Redimido,nocre_FechaRedimido,nocre_EsImpreso,nocre_UsuarioCrea,nocre_FechaCrea,nocre_UsuarioModifica,nocre_FechaModifica")] tbNotaCredito tbNotaCredito)
        {

            var MensajeError = "";
            IEnumerable<object> list = null;
            if (ModelState.IsValid)
            {
                try
                {
                    list = db.UDP_Vent_tbNotaCredito_Insert(tbNotaCredito.nocre_Codigo, 
                                                            tbNotaCredito.dev_Id, 
                                                            tbNotaCredito.clte_Id,
                                                            tbNotaCredito.suc_Id,
                                                            tbNotaCredito.cja_Id,
                                                            tbNotaCredito.nocre_Anulado, 
                                                            tbNotaCredito.nocre_FechaEmision, 
                                                            tbNotaCredito.nocre_MotivoEmision,
                                                            tbNotaCredito.nocre_Monto, 
                                                            tbNotaCredito.nocre_Redimido,
                                                            tbNotaCredito.nocre_FechaRedimido,
                                                            tbNotaCredito.nocre_EsImpreso);
                    foreach (UDP_Vent_tbNotaCredito_Insert_Result NotaCredito in list)
                        MensajeError = NotaCredito.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo Insertar el registro, favor contacte al administrador.");
                        ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
                        ViewBag.Cliente = db.tbCliente.ToList();
                        return View(tbNotaCredito);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch(Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
                    ViewBag.Cliente = db.tbCliente.ToList();
                    return View(tbNotaCredito);

                }
            }

            ViewBag.nocre_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioCrea);
            ViewBag.nocre_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioModifica);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbNotaCredito.clte_Id);
            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Id", tbNotaCredito.dev_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbNotaCredito.suc_Id);
            ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();
            return View(tbNotaCredito);
        }

        // GET: /NotaCredito/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            if (tbNotaCredito == null)
            {
                return HttpNotFound();
            }
            //ViewBag.nocre_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioCrea);
            //ViewBag.nocre_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNotaCredito.nocre_UsuarioModifica);
            ViewBag.clte_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbNotaCredito.cja_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbNotaCredito.clte_Id);
            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Id", tbNotaCredito.dev_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
            return View(tbNotaCredito);
        }

        // POST: /NotaCredito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "nocre_Id,nocre_Codigo,dev_Id,clte_Id,suc_Id,cja_Id,nocre_Anulado,nocre_FechaEmision,nocre_MotivoEmision,nocre_Monto,nocre_Redimido,nocre_FechaRedimido,nocre_EsImpreso,nocre_UsuarioCrea,nocre_FechaCrea,nocre_UsuarioModifica,nocre_FechaModifica, tbUsuario, tbUsuario1")] tbNotaCredito tbNotaCredito)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbNotaCredito_Update(tbNotaCredito.nocre_Id, tbNotaCredito.nocre_Codigo, 
                        tbNotaCredito.dev_Id, tbNotaCredito.clte_Id, tbNotaCredito.suc_Id, tbNotaCredito.cja_Id, tbNotaCredito.nocre_Anulado,
                        tbNotaCredito.nocre_FechaEmision, tbNotaCredito.nocre_MotivoEmision, tbNotaCredito.nocre_Monto,
                        tbNotaCredito.nocre_Redimido, tbNotaCredito.nocre_FechaRedimido, tbNotaCredito.nocre_EsImpreso,
                        tbNotaCredito.nocre_UsuarioCrea, tbNotaCredito.nocre_FechaCrea);
                    foreach (UDP_Vent_tbNotaCredito_Update_Result NotaCredito in list)
                        MensajeError = Convert.ToString(NotaCredito.MensajeError);
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo Editar el registro, favor contacte al administrador.");
                        return View(tbNotaCredito);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
            }
            catch(Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo Editar el registro, favor contacte al administrador.");
                    ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
                    ViewBag.Cliente = db.tbCliente.ToList();
                    return View(tbNotaCredito);
            }
            }
            ViewBag.Devolucion = db.tbDevolucionDetalle.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();
            return View(tbNotaCredito);
        }

        // GET: /NotaCredito/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            if (tbNotaCredito == null)
            {
                return HttpNotFound();
            }
            return View(tbNotaCredito);
        }

        // POST: /NotaCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbNotaCredito tbNotaCredito = db.tbNotaCredito.Find(id);
            db.tbNotaCredito.Remove(tbNotaCredito);
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
        public JsonResult AnularNotaCredito(Int16 nocreId, bool Anulado)
        {
            var list = db.UDP_Vent_tbNotaCredito_Anulado(nocreId, Anulado).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCodigoNotaCredito(int CodSucursal, short CodCaja)
        {
            var list = db.UDP_Vent_tbNotaCredito_CodigoNotaCredito(CodSucursal, CodCaja).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
