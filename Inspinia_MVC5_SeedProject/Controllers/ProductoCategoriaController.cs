using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_ZORZAL.Models;

namespace ERP_ZORZAL.Controllers
{
    public class ProductoCategoriaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /ProductoCategoria/
        public async Task<ActionResult> Index()
        {
            return View(await db.tbProductoCategoria.ToListAsync());
        }

        // GET: /ProductoCategoria/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = await db.tbProductoCategoria.FindAsync(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoCategoria);
        }

        // GET: /ProductoCategoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ProductoCategoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica")] tbProductoCategoria tbProductoCategoria)
        {
            if (ModelState.IsValid)
            {
                db.tbProductoCategoria.Add(tbProductoCategoria);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tbProductoCategoria);
        }

        // GET: /ProductoCategoria/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = await db.tbProductoCategoria.FindAsync(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoCategoria);
        }

        // POST: /ProductoCategoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="pcat_Id,pcat_Nombre,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica")] tbProductoCategoria tbProductoCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbProductoCategoria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tbProductoCategoria);
        }

        // GET: /ProductoCategoria/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = await db.tbProductoCategoria.FindAsync(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoCategoria);
        }

        // POST: /ProductoCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tbProductoCategoria tbProductoCategoria = await db.tbProductoCategoria.FindAsync(id);
            db.tbProductoCategoria.Remove(tbProductoCategoria);
            await db.SaveChangesAsync();
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
