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
    public class DepartamentoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Departamento/
        public ActionResult Index()
        {
            return View(db.tbDepartamento.ToList());
        }
        public ActionResult _IndexMunicipio()
        {
            return View();
        }
        public ActionResult _IndexMunicipio_Botones()
        {
            return View();
        }
        // GET: /Departamento/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamento);
        }

        // GET: /Departamento/Create
        public ActionResult Create()
        {
            ViewBag.dep_Codigo = new SelectList( db.tbDepartamento, "dep_Codigo", "dep_Nombre", "Seleccione");
            return View();
        }

        // POST: /Departamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbDepartamento tbDepartamento)
        {
            IEnumerable<object> list = null;
            IEnumerable<object> lista = null;
            string MensajeError = "0";
            string MsjError = "0";
            var listMunicipios = (List<tbMunicipio>)Session["tbMunicipio"];
            if (ModelState.IsValid)
            {

                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {

                        list = db.UDP_Gral_tbDepartamento_Insert(tbDepartamento.dep_Codigo, tbDepartamento.dep_Nombre);
                        foreach (UDP_Gral_tbDepartamento_Insert_Result departamento in list)
                            MsjError = (departamento.MensajeError);
                        if (MsjError.Substring(0, 1) == "")
                        {
                            ModelState.AddModelError("", "No se Guardo el Registro");
                            return View(tbDepartamento);
                        }
                        else
                        {
                            if (listMunicipios != null )
                            {
                                if (listMunicipios.Count > 0)
                                {
                                    foreach (tbMunicipio mun in listMunicipios)
                                    {
                                        lista = db.UDP_Gral_tbMunicipio_Insert(mun.mun_Codigo, tbDepartamento.dep_Codigo, mun.mun_Nombre);
                                        foreach (UDP_Gral_tbMunicipio_Insert_Result municipios in lista)
                                            MensajeError = (municipios.MensajeError);

                                        if (MensajeError.Substring(0, 1) == "")
                                        {
                                            ModelState.AddModelError("", "No se Guardo el Registro");
                                            return View(tbDepartamento);
                                        }
                                        else
                                        {
                                            _Tran.Complete();
                                            return RedirectToAction("Index");
                                        }
                                    }
                                }
                            }

                            else
                            {
                                _Tran.Complete();
                                return RedirectToAction("Index");
                            }
                            
                        }

                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "No se Guardo el Registro");
                        return View(tbDepartamento);
                    }
                   
                }
            }

            

            return View(tbDepartamento);
        }

        // GET: /Departamento/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamento);
        }

        // POST: /Departamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbDepartamento tbDepartamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbDepartamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbDepartamento);
        }

        // GET: /Departamento/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamento);
        }

        // POST: /Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            db.tbDepartamento.Remove(tbDepartamento);
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
        public JsonResult SaveMunicipio(tbMunicipio Municipio)
        {
            List<tbMunicipio> sessionMunicipio = new List<tbMunicipio>();
            var list = (List<tbMunicipio>)Session["tbMunicipio"];
            if (list == null)
            {
                sessionMunicipio.Add(Municipio);
                Session["tbMunicipio"] = sessionMunicipio;
            }
            else
            {
                list.Add(Municipio);
                Session["tbMunicipio"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult RemoveMunicipios(tbMunicipio Municipios)
        {
            var list = (List<tbMunicipio>)Session["tbMunicipio"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.mun_UsuarioCrea == Municipios.mun_UsuarioCrea);
                list.Remove(itemToRemove);
                Session["tbMunicipio"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
