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
    public class ClienteController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Cliente/
        public ActionResult Index()
        {
            var tbcliente = db.tbCliente.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbMunicipio).Include(t => t.tbTipoIdentificacion);
            return View(tbcliente.ToList());
        }

        // GET: /Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            return View(tbCliente);
        }

        // GET: /Cliente/Create
        public ActionResult Create()
        {
            tbCliente Cliente = new tbCliente();
            Cliente.GeneroList = cUtilities.GeneroList();
            Cliente.NacionalidadList = cUtilities.NacionalidadList();

            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo");
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion");


            return View(Cliente);
        }

        // POST: /Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="clte_Id,tpi_Id,clte_RTN_Identidad_Pasaporte,clte_EsPersonaNatural,clte_Nombres,clte_Apellidos,clte_FechaNacimiento,clte_Nacionalidad,clte_Sexo,clte_Telefono,clte_NombreComercial,clte_RazonSocial,clte_ContactoNombre,clte_ContactoEmail,clte_ContactoTelefono,clte_FechaConstitucion,mun_Codigo,clte_Direccion,clte_CorreoElectronico,clte_EsActivo,clte_RazonInactivo,clte_ConCredito,clte_EsMinorista,clte_Observaciones_,clte_UsuarioCrea,clte_FechaCrea,clte_UsuarioModifica,clte_FechaModifica")] tbCliente tbCliente)
        {
            if (ModelState.IsValid)
            {
                db.tbCliente.Add(tbCliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbCliente.tbMunicipio.dep_Codigo);
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioCrea);
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioModifica);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbCliente.mun_Codigo);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);

            tbCliente Cliente = new tbCliente();
            Cliente.GeneroList = cUtilities.GeneroList();
            Cliente.NacionalidadList = cUtilities.NacionalidadList();
            return View(tbCliente);
        }

        // GET: /Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioCrea);
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioModifica);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbCliente.mun_Codigo);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);

            tbCliente Cliente = db.tbCliente.Find(id);
            Cliente.GeneroList = cUtilities.GeneroList();
            Cliente.NacionalidadList = cUtilities.NacionalidadList();
            return View(tbCliente);
        }

        // POST: /Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clte_Id,tpi_Id,clte_RTN_Identidad_Pasaporte,clte_EsPersonaNatural,clte_Nombres,clte_Apellidos,clte_FechaNacimiento,clte_Nacionalidad,clte_Sexo,clte_Telefono,clte_NombreComercial,clte_RazonSocial,clte_ContactoNombre,clte_ContactoEmail,clte_ContactoTelefono,clte_FechaConstitucion,mun_Codigo,clte_Direccion,clte_CorreoElectronico,clte_EsActivo,clte_RazonInactivo,clte_ConCredito,clte_EsMinorista,clte_Observaciones_,clte_UsuarioCrea,clte_FechaCrea,clte_UsuarioModifica,clte_FechaModifica")] tbCliente tbCliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbCliente.tbMunicipio.dep_Codigo);
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioCrea);
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioModifica);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbCliente.mun_Codigo);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);


            tbCliente.GeneroList = cUtilities.GeneroList();
            tbCliente.NacionalidadList = cUtilities.NacionalidadList();
            return View(tbCliente);
        }

        // GET: /Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            return View(tbCliente);
        }

        // POST: /Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCliente tbCliente = db.tbCliente.Find(id);
            db.tbCliente.Remove(tbCliente);
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
        public JsonResult GetMunicipios(string CodDepartamento)
        {
            var list = db.tbMunicipio.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }



    }
}
