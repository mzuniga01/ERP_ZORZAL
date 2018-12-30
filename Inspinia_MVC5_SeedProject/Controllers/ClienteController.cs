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
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion");
            return View();
        }

        // POST: /Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "clte_Id,tpi_Id,clte_Identificacion,clte_EsPersonaNatural,clte_Nombres,clte_Apellidos,clte_FechaNacimiento,clte_Nacionalidad,clte_Sexo,clte_Telefono,clte_NombreComercial,clte_RazonSocial,clte_ContactoNombre,clte_ContactoEmail,clte_ContactoTelefono,clte_FechaConstitucion,mun_Codigo,clte_Direccion,clte_CorreoElectronico,clte_EsActivo,clte_RazonInactivo,clte_ConCredito,clte_EsMinorista,clte_Observaciones,clte_UsuarioCrea,clte_FechaCrea,clte_UsuarioModifica,clte_FechaModifica,clte_ConsumidorFinal")] tbCliente tbCliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbCliente_Insert(tbCliente.tpi_Id, tbCliente.clte_Identificacion,
                                                        tbCliente.clte_EsPersonaNatural,
                                                        tbCliente.clte_Nombres,
                                                        tbCliente.clte_Apellidos,
                                                        tbCliente.clte_FechaNacimiento,
                                                        tbCliente.clte_Nacionalidad,
                                                        tbCliente.clte_Sexo,
                                                        tbCliente.clte_Telefono,
                                                        tbCliente.clte_NombreComercial,
                                                        tbCliente.clte_RazonSocial,
                                                        tbCliente.clte_ContactoNombre,
                                                        tbCliente.clte_ContactoEmail,
                                                        tbCliente.clte_ContactoTelefono,
                                                        tbCliente.clte_FechaConstitucion,
                                                        tbCliente.mun_Codigo,
                                                        tbCliente.clte_Direccion,
                                                        tbCliente.clte_CorreoElectronico,
                                                        tbCliente.clte_EsActivo,
                                                        tbCliente.clte_RazonInactivo,
                                                        tbCliente.clte_ConCredito,
                                                        tbCliente.clte_EsMinorista,
                                                        tbCliente.clte_Observaciones,
                                                        tbCliente.clte_ConsumidorFinal);
                    foreach (UDP_Vent_tbCliente_Insert_Result cliente in list)
                        MensajeError = cliente.MensajeError;
                    if (MensajeError == -1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "Error al agregar el registro" + Ex.Message.ToString());
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                    //ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioCrea);
                    //ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioModifica);
                    ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
                    ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
                    return View(tbCliente);
                }
                return RedirectToAction("Index");
            }
            tbCliente Cliente = new tbCliente();
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            //ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioCrea);
            //ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioModifica);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);

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
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre",tbCliente.tbMunicipio.tbDepartamento.dep_Codigo);
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioCrea);
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioModifica);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
            var Lista = cUtilities.GeneroList();
            ViewBag.GeneroList = new SelectList(Lista, "ID_GENERO", "DESCRIPCION", tbCliente.clte_Sexo);
            return View(tbCliente);
        }

        // POST: /Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "clte_Id,tpi_Id,clte_Identificacion,clte_EsPersonaNatural,clte_Nombres,clte_Apellidos,clte_FechaNacimiento,clte_Nacionalidad,clte_Sexo,clte_Telefono,clte_NombreComercial,clte_RazonSocial,clte_ContactoNombre,clte_ContactoEmail,clte_ContactoTelefono,clte_FechaConstitucion,mun_Codigo,clte_Direccion,clte_CorreoElectronico,clte_EsActivo,clte_RazonInactivo,clte_ConCredito,clte_EsMinorista,clte_Observaciones,clte_UsuarioCrea,clte_FechaCrea,clte_UsuarioModifica,clte_FechaModifica,clte_ConsumidorFinal")] tbCliente tbCliente)
        {
            var Lista = cUtilities.GeneroList();
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = 0;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbCliente_Update(tbCliente.clte_Id, tbCliente.tpi_Id, 
                                                        tbCliente.clte_Identificacion,
                                                        tbCliente.clte_EsPersonaNatural,
                                                        tbCliente.clte_Nombres,
                                                        tbCliente.clte_Apellidos,
                                                        tbCliente.clte_FechaNacimiento,
                                                        tbCliente.clte_Nacionalidad,
                                                        tbCliente.clte_Sexo,
                                                        tbCliente.clte_Telefono,
                                                        tbCliente.clte_NombreComercial,
                                                        tbCliente.clte_RazonSocial,
                                                        tbCliente.clte_ContactoNombre,
                                                        tbCliente.clte_ContactoEmail,
                                                        tbCliente.clte_ContactoTelefono,
                                                        tbCliente.clte_FechaConstitucion,
                                                        tbCliente.mun_Codigo,
                                                        tbCliente.clte_Direccion,
                                                        tbCliente.clte_CorreoElectronico,
                                                        tbCliente.clte_EsActivo,
                                                        tbCliente.clte_RazonInactivo,
                                                        tbCliente.clte_ConCredito,
                                                        tbCliente.clte_EsMinorista,
                                                        tbCliente.clte_Observaciones,
                                                        tbCliente.clte_UsuarioCrea,
                                                        tbCliente.clte_FechaCrea,
                                                        tbCliente.clte_ConsumidorFinal);
                    foreach (UDP_Vent_tbCliente_Update_Result cliente in list)
                        MensajeError = cliente.MensajeError;
                    if (MensajeError == -1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "Error al agregar el registro" + Ex.Message.ToString());
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                    //ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioCrea);
                    //ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioModifica);
                    ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
                    ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
                    ViewBag.GeneroList = new SelectList(Lista, "ID_GENERO", "DESCRIPCION", tbCliente.clte_Sexo);
                    Lista = cUtilities.GeneroList();
                    return View(tbCliente);
                }
                return RedirectToAction("Index");
            }
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            //ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioCrea);
            //ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioModifica);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
            ViewBag.GeneroList = new SelectList(Lista, "ID_GENERO", "DESCRIPCION", tbCliente.clte_Sexo);
            Lista = cUtilities.GeneroList();
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
            var list = db.spGetMunicipios1(CodDepartamento).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetIdentificacion(bool CodIdentificacion)
        {
            var list = db.spGetTipoIdentificacion(CodIdentificacion).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDepartamento(string CodMunicipio)
        {
            var list = db.spGetDepartamento(CodMunicipio).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InactivarCliente(int CodCliente, bool Activo, string RazonInactivo)
        {
            var list = db.UDP_Vent_tbCliente_Estado(CodCliente, Activo, RazonInactivo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActivarCliente(int CodCliente, bool Activo, string RazonInactivo)
        {
            var list = db.UDP_Vent_tbCliente_Estado(CodCliente, Activo, RazonInactivo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
