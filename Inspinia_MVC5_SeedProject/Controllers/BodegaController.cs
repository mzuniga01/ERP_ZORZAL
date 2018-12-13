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
    public class BodegaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Bodega/
        public ActionResult Index()
        {
            var tbbodega = db.tbBodega.Include(t => t.tbUsuario).Include(t => t.tbMunicipio);
            return View(tbbodega.ToList());
        }

        // GET: /Bodega/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodega tbBodega = db.tbBodega.Find(id);
            if (tbBodega == null)
            {
                return HttpNotFound();
            }
            return View(tbBodega);
        }

        // GET: /Bodega/Create
        public ActionResult Create()
        {
            //ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo");
            ViewBag.dep_codigo = new SelectList(db.tbMunicipio, "dep_codigo", "dep_Nombre");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.dep_codigo = new SelectList(db.tbDepartamento, "dep_codigo", "dep_Nombre");

            return View();
        }

        // POST: /Bodega/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bod_Id,bod_Nombre,bod_ResponsableBodega,bod_Direccion,bod_Correo,bod_Telefono,usu_Id,mun_Codigo,bod_EsActiva,bod_UsuarioCrea,bod_FechaCrea,bod_UsuarioModifica,bod_FechaModifica")] tbBodega tbBodega)
        {
            //if (ModelState.IsValid)
            //{
            //    db.tbBodega.Add(tbBodega);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBodega.usu_Id);
            //ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbBodega.mun_Codigo);
            //ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
            //return View(tbBodega);
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> list = null;
                    var MsjError = "";
                    list = db.UDP_Inv_tbBodega_Insert(   tbBodega.bod_Nombre, 
                                                         tbBodega.bod_ResponsableBodega
                                                        , tbBodega.bod_Direccion 
                                                        , tbBodega.bod_Correo
                                                        , tbBodega.bod_Telefono 
                                                        , tbBodega.mun_Codigo
                                                        , tbBodega.bod_EsActiva);
                    foreach (UDP_Inv_tbBodega_Insert_Result bodega in list)
                        MsjError = bodega.MensajeError;
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
                    ModelState.AddModelError("", "No se Guardo el registro");
                }
                return RedirectToAction("Index");
            }
            //ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBodega.usu_Id);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbBodega.mun_Codigo);
            //ViewBag.dep_codigo = new SelectList(db.tbMunicipio, "dep_codigo", "dep_Nombre", tbBodega.mun_Codigo);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
            return View(tbBodega);
            
        }

        // GET: /Bodega/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodega tbBodega = db.tbBodega.Find(id);
            if (tbBodega == null)
            {
                return HttpNotFound();
            }
           
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbBodega.mun_Codigo);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
            return View(tbBodega);
        }

        // POST: /Bodega/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include="bod_Id,bod_Nombre,bod_ResponsableBodega,bod_Direccion,bod_Correo,bod_Telefono,usu_Id,mun_Codigo,bod_EsActiva,bod_UsuarioCrea,bod_FechaCrea,bod_UsuarioModifica,bod_FechaModifica")] tbBodega tbBodega)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(tbBodega).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbBodega.mun_Codigo);
            //ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
            //return View(tbBodega);
            if (ModelState.IsValid)
            {
                try
                {
                    tbBodega bod = db.tbBodega.Find(id);
                    IEnumerable<object> list = null;
                    var MsjError = "";
                    list = db.UDP_Inv_tbBodega_Update(tbBodega.bod_Id
                                                        , tbBodega.bod_Nombre
                                                        , tbBodega.bod_ResponsableBodega
                                                        , tbBodega.bod_Direccion
                                                        , tbBodega.bod_Correo
                                                        , tbBodega.bod_Telefono
                                                        , tbBodega.mun_Codigo
                                                        , tbBodega.bod_EsActiva
                                                        , bod.bod_UsuarioCrea
                                                        , bod.bod_FechaCrea);
                    foreach (UDP_Inv_tbBodega_Update_Result bode in list)
                        MsjError = bode.MensajeError;
                    if (MsjError == "-1")
                    {
                        ModelState.AddModelError("", "No se guardo el cambio");
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
                    ModelState.AddModelError("", "No se guardo el cambio");
                }
                return RedirectToAction("Index");
            }
            //ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBodega.usu_Id);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbBodega.mun_Codigo);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
            return View(tbBodega);

        }

        // GET: /Bodega/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBodega tbBodega = db.tbBodega.Find(id);
            if (tbBodega == null)
            {
                return HttpNotFound();
            }
            return View(tbBodega);
        }

        // POST: /Bodega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbBodega tbBodega = db.tbBodega.Find(id);
            db.tbBodega.Remove(tbBodega);
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


        //public List<tbMunicipio> MostrarMunicipioPorDeparatamento(tbMunicipio lstMunicipio)
        //{
        //    tbDepartamento _Departamento = new tbDepartamento();

        //    var ListaMunicipios = from municipio in db.tbMunicipio.ToList()
        //                          where municipio._Departamento.Contains(lstMunicipio.tbDepartamento)
        //                          select municipio;

        //    return ListaMunicipios.ToList();
        //}


    }
}
