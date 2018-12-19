using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
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
            this.AllLists();
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

            this.AllLists();
            return View(tbBodega);
        }



        public ActionResult _DetallesProductos(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return HttpNotFound();
            }
            return View(tbProducto);
        }



        private void AllLists()
        {
            var _municipios = db.tbMunicipio.Select(s => new
            {
                mun_Codigo = s.mun_Codigo,
                mun_Nombre = string.Concat(s.mun_Codigo + " - " + s.mun_Nombre)
            }).ToList();

            var _departamentos = db.tbDepartamento.Select(s => new
            {
                dep_Codigo = s.dep_Codigo, dep_Nombre = string.Concat(s.dep_Codigo + " - " + s.dep_Nombre)
            }).ToList();


            ViewBag.Producto = db.tbProducto.ToList();
            //ViewBag.UnidadList = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", "Seleccione");
            //ViewBag.ProductoList = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion", "Seleccione");
            //ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "pscat_Id");
            //ViewBag.CategoriaList = new SelectList(db.tbProductoCategoria, "pcat_Id","pcat_Nombre");
            //ViewBag.SubcategoriaList = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.dep_Codigo = new SelectList(_departamentos, "dep_Codigo", "dep_Nombre");
            //ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            //ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            //ViewBag.EstadoList = new SelectList(Estados.EstadoList(), "estif_Id", "estif_Descripcion", "Seleccione");

        }

        [HttpPost]
        public JsonResult GetMunicipios_Create(string dep_Codigo)
        {
            var list = db.spGetMunicipios(dep_Codigo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult GetDepartamento(string mun_Codigo)
        //{
        //    var list = db.spGetMunicipios(mun_Codigo).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult removeBodegaDetalle(tbBodegaDetalle bodedaDetalle)
        {
            var list = (List<tbBodegaDetalle>)Session["tbBodegaDetalles"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.bodd_Id == bodedaDetalle.bodd_Id);
                list.Remove(itemToRemove);
                Session["tbBodegaDetalless"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);

        }


        // GET: /Bodega/Create
        public ActionResult Create()
        {
            this.AllLists();
            return View();
        }

        // POST: /Bodega/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bod_Id,bod_Nombre,bod_ResponsableBodega,bod_Direccion,bod_Correo,bod_Telefono,usu_Id,mun_Codigo,bod_EsActiva,bod_UsuarioCrea,bod_FechaCrea,bod_UsuarioModifica,bod_FechaModifica")] tbBodega tbBodega)
        {
            IEnumerable<object> BODEGA = null;
            IEnumerable<object> DETALLE = null;
            var MensajeError = "";
            var MsjError = "";
            var listaDetalle = (List<tbBodegaDetalle>)Session["tbBodegaDetalle"];
            if (ModelState.IsValid)
            {

                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {

                        BODEGA = db.UDP_Inv_tbBodega_Insert(tbBodega.bod_Nombre,
                                                         tbBodega.bod_ResponsableBodega
                                                        , tbBodega.bod_Direccion
                                                        , tbBodega.bod_Correo
                                                        , tbBodega.bod_Telefono
                                                        , tbBodega.mun_Codigo);
                        foreach (UDP_Inv_tbBodega_Insert_Result bodega in BODEGA)
                            MsjError = bodega.MensajeError;
                        if (MsjError == "-1")
                        {
                            ModelState.AddModelError("", "No se Guardo el Registro");
                            return View(tbBodega);
                        }
                        else
                        {
                            if (listaDetalle != null)
                            {
                                if (listaDetalle.Count > 0)
                                {
                                    foreach (tbBodegaDetalle bodd in listaDetalle)
                                    {
                                        DETALLE = db.UDP_Inv_tbBodegaDetalle_Insert(bodd.prod_Codigo
                                                                                    , tbBodega.bod_Id
                                                                                    , bodd.bodd_CantidadMinima
                                                                                    , bodd.bodd_CantidadMaxima
                                                                                    , bodd.bodd_PuntoReorden
                                                                                    , bodd.bodd_Costo
                                                                                    , bodd.bodd_CostoPromedio);
                                        foreach (UDP_Inv_tbBodegaDetalle_Insert_Result B_detalle in DETALLE)
                                            MensajeError = (B_detalle.MensajeError);

                                        if (MensajeError == "-1")
                                        {
                                            ModelState.AddModelError("", "No se Guardo el Registro");
                                            return View(tbBodega);
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
                        return View(tbBodega);
                    }
                }
            }
            this.AllLists();
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
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbBodega.bod_UsuarioCrea).usu_NombreUsuario;
            var UsuarioModfica = tbBodega.bod_UsuarioModifica;
            if (UsuarioModfica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(tbBodega.bod_UsuarioModifica).usu_NombreUsuario;
            };
            if (tbBodega == null)
            {
                return HttpNotFound();
            }
            this.AllLists();
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            return View(tbBodega);
        }

        [HttpPost]
        public JsonResult Getbodegadetalle()
        {
            var list = (List<tbBodegaDetalle>)Session["tbBodegaDetalle"];
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveBodegaDetalle(tbBodegaDetalle BODEGADETALLE)
        {
            List<tbBodegaDetalle> sessionbodegadetalle = new List<tbBodegaDetalle>();
            var list = (List<tbBodegaDetalle>)Session["tbBodegaDetalle"];
            if (list == null)
            {
                sessionbodegadetalle.Add(BODEGADETALLE);
                Session["tbBodegaDetalle"] = sessionbodegadetalle;
            }
            else
            {
                list.Add(BODEGADETALLE);
                Session["tbBodegaDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }


        // POST: /Bodega/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include="bod_Id,bod_Nombre,bod_ResponsableBodega,bod_Direccion,bod_Correo,bod_Telefono,usu_Id,mun_Codigo,bod_EsActiva,bod_UsuarioCrea,bod_FechaCrea,bod_UsuarioModifica,bod_FechaModifica, tbUsuario ,tbUsuario1")] tbBodega tbBodega)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbBodega bod = db.tbBodega.Find(id);
                    IEnumerable<object> list = null;
                    string MsjError = "";
                    list = db.UDP_Inv_tbBodega_Update(tbBodega.bod_Id
                                                        , tbBodega.bod_Nombre
                                                        , tbBodega.bod_ResponsableBodega
                                                        , tbBodega.bod_Direccion
                                                        , tbBodega.bod_Correo
                                                        , tbBodega.bod_Telefono
                                                        , tbBodega.mun_Codigo
                                                        , tbBodega.bod_EsActiva
                                                        , tbBodega.bod_UsuarioCrea
                                                        , tbBodega.bod_FechaCrea);
                    foreach (UDP_Inv_tbBodega_Update_Result bode in list)
                        MsjError = bode.MensajeError;
                    if (MsjError.Substring(0, 2) == "-1")
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
                    ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                }
                return RedirectToAction("Index");
            }
            this.AllLists();
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
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

        //para que cambie estado a activar
        public ActionResult EstadoInactivar(int? id)
        {

            try
            {
                tbBodega obj = db.tbBodega.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbBodega_Update_Estado(id, EstadoBodega.Inactivo);
                foreach (UDP_Inv_tbBodega_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
            }


            //return RedirectToAction("Index");
        }
        //para que cambie estado a inactivar
        public ActionResult Estadoactivar(int? id)
        {

            try
            {
                tbBodega obj = db.tbBodega.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbBodega_Update_Estado(id, EstadoBodega.Activo);
                foreach (UDP_Inv_tbBodega_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
            }


            //return RedirectToAction("Index");
        }

    }
}
