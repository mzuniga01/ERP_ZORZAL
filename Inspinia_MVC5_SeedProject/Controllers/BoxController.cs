using ERP_ZORZAL.Models;
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
        public ActionResult Create([Bind(Include = "box_Codigo,box_Descripcion,box_UsuarioCrea,box_FechaCrea,box_UsuarioModifica,box_FechaModifica")] tbBox tbBox)
        {
            if (ModelState.IsValid)
            {
                db.tbBox.Add(tbBox);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        public ActionResult Edit([Bind(Include = "box_Codigo,box_Descripcion,box_UsuarioCrea,box_FechaCrea,box_UsuarioModifica,box_FechaModifica")] tbBox tbBox)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbBox).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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