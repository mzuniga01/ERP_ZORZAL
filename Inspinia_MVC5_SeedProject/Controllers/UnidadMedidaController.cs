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
    public class UnidadMedidaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /UnidadMedida/
        public ActionResult Index()
        {
            return View(db.tbUnidadMedida.ToList());
        }

        // GET: /UnidadMedida/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            if (tbUnidadMedida == null)
            {
                return HttpNotFound();
            }
            return View(tbUnidadMedida);
        }

        // GET: /UnidadMedida/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /UnidadMedida/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="uni_Descripcion,uni_Abreviatura")] tbUnidadMedida tbUnidadMedida)
        {
            if (ModelState.IsValid)
            {
                //db.tbUnidadMedida.Add(tbUnidadMedida);
                //db.SaveChanges();
                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Gral_tbUnidadMedida_Insert(tbUnidadMedida.uni_Descripcion, tbUnidadMedida.uni_Abreviatura);
                    foreach (UDP_Gral_tbUnidadMedida_Insert_Result UnidadMedida in List)
                        MsjError = UnidadMedida.MensajeError;

                    if (MsjError == "-1")
                    {
                        ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }


                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro, Contacte al Administrador");
                }
                return RedirectToAction("Index");
            }

            return View(tbUnidadMedida);
        }

        // GET: /UnidadMedida/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            if (tbUnidadMedida == null)
            {
                return HttpNotFound();
            }
            return View(tbUnidadMedida);
        }

        // POST: /UnidadMedida/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id,[Bind(Include= "uni_Id,uni_Descripcion,uni_Abreviatura,uni_UsuarioCrea, uni_FechaCrea")] tbUnidadMedida tbUnidadMedida)
        {
            tbUnidadMedida vtbUnidadMedida = db.tbUnidadMedida.Find(id);
            if (ModelState.IsValid)
            {
                //db.Entry(tbUnidadMedida).State = EntityState.Modified;
                //db.SaveChanges();
                try
                {
                    /*:ssTZD*/
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    //,uni_UsuarioModifica, uni_FechaModifica
                    //tbUnidadMedida.uni_UsuarioModifica = 1;
                    //var uni_UsuarioCrea = vtbUnidadMedida.uni_UsuarioCrea;
                    //var uni_FechaCrea = Convert.ToDateTime(String.Format("{0:d/M/yyyy HH:mm:ss}", vtbUnidadMedida.uni_FechaCrea));

                    //tbUnidadMedida.uni_FechaModifica = DateTime.Now;tbUnidadMedida.uni_FechaCrea
                    //var FechaCreo = Convert.ToDateTime(uni_FechaCrea);
                    List = db.UDP_Gral_tbUnidadMedida_Update(tbUnidadMedida.uni_Id, tbUnidadMedida.uni_Descripcion, tbUnidadMedida.uni_Abreviatura, vtbUnidadMedida.uni_UsuarioCrea, vtbUnidadMedida.uni_FechaCrea);
                    foreach (UDP_Gral_tbUnidadMedida_Update_Result UnidadMedida in List)
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
            return View(tbUnidadMedida);
        }

        // GET: /UnidadMedida/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            if (tbUnidadMedida == null)
            {
                return HttpNotFound();
            }
            return View(tbUnidadMedida);
        }

        public ActionResult Usuario(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuario tbUsuario = db.tbUsuario.Find(id);
            if (tbUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuario);
        }

        // POST: /UnidadMedida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            db.tbUnidadMedida.Remove(tbUnidadMedida);
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
