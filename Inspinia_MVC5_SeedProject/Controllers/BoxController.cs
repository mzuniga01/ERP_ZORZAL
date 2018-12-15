using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ERP_ZORZAL.Controllers
{
    public class BoxController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Box/
        public ActionResult Index()
        {
            ViewBag.Salida = new tbSalida();
            return View(db.tbBox.ToList());
        }

        // GET: /Box/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBox tbBox = db.tbBox.Find(id);
            if (tbBox == null)
            {
                return HttpNotFound();
            }
            return View(tbBox);
        }

        // GET: /Box/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult _Producto()
        {
            return View();
        }

        // POST: /Box/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "box_Codigo,box_Descripcion")] tbBox tbBox)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Inv_tbBox_Insert(tbBox.box_Codigo,tbBox.box_Descripcion);
                    foreach (UDP_Inv_tbBox_Insert_Result Box in List)
                        MsjError = Box.MensajeError;

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
            }

            return View(tbBox);
        }

        // GET: /Box/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBox tbBox = db.tbBox.Find(id);
            if (tbBox == null)
            {
                return HttpNotFound();
            }
            return View(tbBox);
        }

        // POST: /Box/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id,[Bind(Include = "box_Codigo,box_Descripcion,box_UsuarioCrea,box_FechaCrea")] tbBox tbBox)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        tbBox vBox = db.tbBox.Find(id);
            //        IEnumerable<object> List = null;
            //        var MsjError = "";
            //        List = db.UDP_Inv_tbBox_Update(tbBox.box_Codigo, tbBox.box_Descripcion, vBox.box_UsuarioCrea, vBox.box_FechaCrea);
            //        foreach (UDP_Inv_tbBox_Update_Result Box in List)
            //            MsjError = Box.MensajeError;

            //        if (MsjError == "-1")
            //        {
            //            ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
            //        }
            //        else
            //        {
            //            return RedirectToAction("Index");
            //        }


            //    }
            //    catch (Exception Ex)
            //    {
            //        Ex.Message.ToString();
            //        ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
            //    }
            //}
            return View(tbBox);
        }

        // GET: /Box/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBox tbBox = db.tbBox.Find(id);
            if (tbBox == null)
            {
                return HttpNotFound();
            }
            return View(tbBox);
        }

        // POST: /Box/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbBox tbBox = db.tbBox.Find(id);
            db.tbBox.Remove(tbBox);
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