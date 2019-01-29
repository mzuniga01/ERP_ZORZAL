using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Transactions;

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
            ViewBag.IdPuntoEmision = id;
            if (tbPuntoEmision == null)
            {
                return HttpNotFound();
            }

            return View(tbPuntoEmision);
        }

        // GET: /PuntoEmision/Create
        public ActionResult Create()
        {
            //PuntoEmision
            tbPuntoEmision PuntoEmision = new tbPuntoEmision();
            ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", PuntoEmision.pemi_Id);

            //PuntoEmisionDetalle
            tbPuntoEmisionDetalle tbPuntoEmisionDetalle = new tbPuntoEmisionDetalle();
            ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", tbPuntoEmisionDetalle.dfisc_Id);

            //Vistas parciales
            ViewBag.PuntoEmisionDetalle = db.tbPuntoEmisionDetalle.ToList();

            Session["PuntoEmision"] = null;
            return View();
        }

        // POST: /PuntoEmision/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pemi_NumeroCAI,tbUsuario,tbUsuario1")] tbPuntoEmision tbPuntoEmision)
        {
            var list = (List<tbPuntoEmisionDetalle>)Session["PuntoEmision"];
            string MensajeError = "";
            var MensajeErrorDetalle = "";
            IEnumerable<object> listPuntoEmision = null;
            IEnumerable<object> listPuntoEmisionDetalle = null;
            tbPuntoEmisionDetalle cPuntoEmisionDetalle = new tbPuntoEmisionDetalle();

            if (db.tbPuntoEmision.Any(a => a.pemi_NumeroCAI == tbPuntoEmision.pemi_NumeroCAI))
            {
                ModelState.AddModelError("", "Ya existe este Número CAI.");
            }
            if (ModelState.IsValid)
            {

                try
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        listPuntoEmision = db.UDP_Vent_tbPuntoEmision_Insert(
                            tbPuntoEmision.pemi_NumeroCAI
                            );
                        foreach (UDP_Vent_tbPuntoEmision_Insert_Result PuntoEmisionL in listPuntoEmision)
                        MensajeError = PuntoEmisionL.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbPuntoEmision);
                        }
                        else
                        {
                            if (MensajeError != "-1")
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbPuntoEmisionDetalle Detalle in list)
                                        {
                                            var PuntoEmisionDetalle = Convert.ToInt32(MensajeError);
                                            Detalle.pemi_Id = PuntoEmisionDetalle;

                                            listPuntoEmisionDetalle = db.UDP_Vent_tbPuntoEmisionDetalle_Insert(
                                                Detalle.pemi_Id,
                                                Detalle.dfisc_Id,
                                                Detalle.pemid_RangoInicio,
                                                Detalle.pemid_RangoFinal,
                                                Detalle.pemid_NumeroActual,
                                                Detalle.pemid_FechaLimite
                                                );
                                            foreach (UDP_Vent_tbPuntoEmisionDetalle_Insert_Result SPpuntoemisiondet in listPuntoEmisionDetalle)
                                            {
                                                MensajeErrorDetalle = SPpuntoemisiondet.MensajeError;
                                                if (MensajeError.StartsWith("-1"))
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbPuntoEmision);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbPuntoEmision);
                            }
                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", cPuntoEmisionDetalle.dfisc_Id);
                    ModelState.AddModelError("", "No se pudo agregar el registro" + Ex.Message.ToString());
                    return View(tbPuntoEmision);
                }
            }
            ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", cPuntoEmisionDetalle.dfisc_Id);
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
            ViewBag.IdPuntoEmisionEdit = id;
            if (tbPuntoEmision == null)
            {
                return HttpNotFound();
            }
            //*****PuntoEmisionDetalle
            string cas = "dfisc_IdList_";
            System.Web.HttpContext.Current.Items[cas] = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion");

            var ValidacionRegistro = db.tbPuntoEmisionDetalle.Where(x => x.pemi_Id == tbPuntoEmision.pemi_Id).ToList();
            if (ValidacionRegistro.Count() > 0)
            {
                ViewBag.Validacion = "1";
            }
            
            return View(tbPuntoEmision);
        }

        // POST: /PuntoEmision/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "pemi_Id,pemi_NumeroCAI,pemi_UsuarioCrea,pemi_FechaCrea,pemi_UsuarioModifica,pemi_FechaModifica,tbUsuario,tbUsuario1")] tbPuntoEmision PuntoEmision)
        {
            if (db.tbPuntoEmision.Any(a => a.pemi_NumeroCAI == PuntoEmision.pemi_NumeroCAI))
            {
                ModelState.AddModelError("", "Ya existe este Número CAI.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                        string MensajeError = "";
                        IEnumerable<object> list = null;
                        list = db.UDP_Vent_tbPuntoEmision_Update(
                            PuntoEmision.pemi_Id,
                            PuntoEmision.pemi_NumeroCAI,
                            PuntoEmision.pemi_UsuarioCrea,
                            PuntoEmision.pemi_FechaCrea);
                        foreach (UDP_Vent_tbPuntoEmision_Update_Result puntoemision in list)
                            MensajeError = puntoemision.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                            return View(PuntoEmision);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    //*****PuntoEmisionDetalle
                    ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return View(PuntoEmision);
                }
            }
            //*****PuntoEmisionDetalle
            ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion");
            return View(PuntoEmision);
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

        [HttpPost]
        public JsonResult SavePuntoEmisionDetalle(tbPuntoEmisionDetalle PuntoEmisionDet)
        {
            List<tbPuntoEmisionDetalle> sessionPuntoEmisionDetalle = new List<tbPuntoEmisionDetalle>();
            var list = (List<tbPuntoEmisionDetalle>)Session["PuntoEmision"];
            if (list == null)
            {
                sessionPuntoEmisionDetalle.Add(PuntoEmisionDet);
                Session["PuntoEmision"] = sessionPuntoEmisionDetalle;
            }
            else
            {
                list.Add(PuntoEmisionDet);
                Session["PuntoEmision"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemovePuntoEmisionDetalle(tbPuntoEmisionDetalle PuntoEmisionDet)
        {
            var list = (List<tbPuntoEmisionDetalle>)Session["PuntoEmision"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.pemid_Id == PuntoEmisionDet.pemid_Id);
                list.Remove(itemToRemove);
                Session["PuntoEmision"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveEditNumeracion (tbPuntoEmisionDetalle EditPuntoEmisionDetalle)
        {
            string MensajeEdit = "";
            
            try
                {
                        string MensajeError = "";
                        IEnumerable<object> list = null;
                        list = db.UDP_Vent_tbPuntoEmisionDetalle_Update(
                                    EditPuntoEmisionDetalle.pemid_Id,
                                    EditPuntoEmisionDetalle.dfisc_Id,
                                    EditPuntoEmisionDetalle.pemid_RangoInicio,
                                    EditPuntoEmisionDetalle.pemid_RangoFinal,
                                    EditPuntoEmisionDetalle.pemid_NumeroActual,
                                    EditPuntoEmisionDetalle.pemid_FechaLimite,
                                    EditPuntoEmisionDetalle.pemid_UsuarioCrea,
                                    EditPuntoEmisionDetalle.pemid_FechaCrea);
                        foreach (UDP_Vent_tbPuntoEmisionDetalle_Update_Result puntoemisiondetalle in list)
                            MensajeError = puntoemisiondetalle.MensajeError;
                            MensajeEdit = "El registro se guardó exitosamente";
                        if (MensajeError == "-1")
                        {
                            MensajeEdit = "No se pudo actualizar el registro, favor contacte al administrador.";
                            ModelState.AddModelError("", MensajeEdit);
                        }
               }
               catch (Exception Ex)
               {
                    MensajeEdit = Ex.Message.ToString();
                    ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", EditPuntoEmisionDetalle.dfisc_Id);
                    ModelState.AddModelError("", MensajeEdit);
                }
            return Json(MensajeEdit, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult SaveCreateNumeracion(tbPuntoEmisionDetalle CreatePuntoEmisionDetalle)
        {
            string Msj = "";

            try
            {
                string MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Vent_tbPuntoEmisionDetalle_Insert(
                            CreatePuntoEmisionDetalle.pemi_Id,
                            CreatePuntoEmisionDetalle.dfisc_Id,
                            CreatePuntoEmisionDetalle.pemid_RangoInicio,
                            CreatePuntoEmisionDetalle.pemid_RangoFinal,
                            CreatePuntoEmisionDetalle.pemid_NumeroActual,
                            CreatePuntoEmisionDetalle.pemid_FechaLimite);
                foreach (UDP_Vent_tbPuntoEmisionDetalle_Insert_Result puntoemisiondetalle in list)
                    MensajeError = puntoemisiondetalle.MensajeError;
                    Msj = "El registro se guardo exitosamente";
                if (MensajeError == "-1")
                {
                    Msj = "No se pudo actualizar el registro, favor contacte al administrador.";
                    ModelState.AddModelError("", Msj);
                }
            }
            catch (Exception Ex)
            {
                Msj = Ex.Message.ToString();
                ViewBag.dfisc_Id = new SelectList(db.tbDocumentoFiscal, "dfisc_Id", "dfisc_Descripcion", CreatePuntoEmisionDetalle.dfisc_Id);
                ModelState.AddModelError("", Msj);
            }
            return Json(Msj, JsonRequestBehavior.AllowGet);
        }

        
    }
}
