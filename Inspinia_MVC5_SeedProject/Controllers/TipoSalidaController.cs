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
    public class TipoSalidaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /TipoSalida/
        public ActionResult Index()
        {
            return View(db.tbTipoSalida.ToList());
        }

        // GET: /TipoSalida/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalida tbTipoSalida = db.tbTipoSalida.Find(id);
            if (tbTipoSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSalida);
        }

        // GET: /TipoSalida/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TipoSalida/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tsal_Descripcion")] tbTipoSalida tbTipoSalida)
        {
            if (ModelState.IsValid)
            {
                //db.tbTipoSalida.Add(tbTipoSalida);
                //db.SaveChanges();
                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Inv_tbTipoSalida_Insert(tbTipoSalida.tsal_Descripcion);
                    foreach (UDP_Inv_tbTipoSalida_Insert_Result TipoSalida in List)
                        MsjError = TipoSalida.MensajeError;

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

            return View(tbTipoSalida);
        }

        // GET: /TipoSalida/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalida tbTipoSalida = db.tbTipoSalida.Find(id);
            if (tbTipoSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSalida);
        }

        // POST: /TipoSalida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(byte? id,[Bind(Include="tsal_Id,tsal_Descripcion,tsal_UsuarioCrea,tsal_FechaCrea")] tbTipoSalida tbTipoSalida)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tbUnidadMedida).State = EntityState.Modified;
                //db.SaveChanges();
                try
                {
                    tbTipoSalida vtbTipoSalida = db.tbTipoSalida.Find(id);
                    /*:ssTZD*/
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    //,uni_UsuarioModifica, uni_FechaModifica
                    //tbUnidadMedida.uni_UsuarioModifica = 1;
                    //var uni_UsuarioCrea = vtbUnidadMedida.uni_UsuarioCrea;
                    //var uni_FechaCrea = Convert.ToDateTime(String.Format("{0:d/M/yyyy HH:mm:ss}", vtbUnidadMedida.uni_FechaCrea));

                    //tbUnidadMedida.uni_FechaModifica = DateTime.Now;tbUnidadMedida.uni_FechaCrea
                    //var FechaCreo = Convert.ToDateTime(uni_FechaCrea);
                    List = db.UDP_Inv_tbTipoSalida_Update(tbTipoSalida.tsal_Id, tbTipoSalida.tsal_Descripcion, vtbTipoSalida.tsal_UsuarioCrea, vtbTipoSalida.tsal_FechaCrea);
                    foreach (UDP_Inv_tbTipoSalida_Update_Result UnidadMedida in List)
                        MsjError = UnidadMedida.MensajeError;

                    if (MsjError == "-1")
                    {

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
            return View(tbTipoSalida);
        }

        // GET: /TipoSalida/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalida tbTipoSalida = db.tbTipoSalida.Find(id);
            if (tbTipoSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSalida);
        }

        // POST: /TipoSalida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbTipoSalida tbTipoSalida = db.tbTipoSalida.Find(id);
            db.tbTipoSalida.Remove(tbTipoSalida);
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
