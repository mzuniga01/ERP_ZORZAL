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
using System.Data.SqlClient;
using System.Data.Common;



namespace ERP_GMEDINA.Controllers
{
    public class ListaPreciosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /ListaPrecios/
        public ActionResult Index()
        {
            var tblistaprecio = db.tbListaPrecio.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbListadoPrecioDetalle);
            return View(tblistaprecio.ToList());
        }

        // GET: /ListaPrecios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbListaPrecio tbListaPrecio = db.tbListaPrecio.Find(id);
            if (tbListaPrecio == null)
            {
                return HttpNotFound();
            }
            return View(tbListaPrecio);
        }

        // GET: /ListaPrecios/Create
        public ActionResult Create()
        {
            ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo");
            ViewBag.Producto = db.tbProducto.ToList();
            Session["ListadoPrecio"] = null;
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = " listp_Nombre,listp_EsActivo,listp_FechaInicioVigencia,listp_FechaFinalVigencia,listp_Prioridad")] tbListaPrecio tbListaPrecio)
        {

            ViewBag.Producto = db.tbProducto.ToList();
        
            var list = (List<tbListadoPrecioDetalle>)Session["ListadoPrecio"];
            string MensajeError = "";
            var MensajeErrorDetalle = "";
            IEnumerable<object> listPrecio = null;
            IEnumerable<object> listPrecioDetalle = null;

            if (db.tbListaPrecio.Any(a => a.listp_Nombre == tbListaPrecio.listp_Nombre))
            {
                ModelState.AddModelError("", "Ya existe una Lista Precio con este mismo nombre.");
            }
            if (ModelState.IsValid)
            {                
                try
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        listPrecio = db.UDP_Vent_tbListaPrecio_Insert(
                                                 tbListaPrecio.listp_Nombre,
                                                                   tbListaPrecio.listp_EsActivo = true,
                                                                 tbListaPrecio.listp_FechaInicioVigencia,
                                                                 tbListaPrecio.listp_FechaFinalVigencia,
                                                                tbListaPrecio.listp_Prioridad,
                                                                Function.GetUser(),
                                                                Function.DatetimeNow());
                   
                        foreach (UDP_Vent_tbListaPrecio_Insert_Result Precio in listPrecio)
                            MensajeError = Precio.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                           
                            //TempData["message"] = "No se pudo agregar el registro,ya existe nombre de lista";
                            return View(tbListaPrecio);
                        }
                        else
                        {
                            if (MensajeError != "-1")
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbListadoPrecioDetalle PrecioDetalle in list)
                                        {
                                            var liistp_Id = Convert.ToInt32(MensajeError);
                                            PrecioDetalle.listp_Id = liistp_Id;
                                            PrecioDetalle.listp_Id = liistp_Id;
                                            listPrecioDetalle = db.UDP_Vent_tbListadoPrecioDetalle_Insert(
                                              PrecioDetalle.listp_Id,
                                              PrecioDetalle.prod_Codigo,
                                              PrecioDetalle.lispd_PrecioMayorista,
                                              PrecioDetalle.lispd_PrecioMinorista,
                                              PrecioDetalle.lispd_DescCaja,
                                              PrecioDetalle.lispd_DescGerente,
                                              Function.GetUser(),
                                    Function.DatetimeNow());
                                            foreach (UDP_Vent_tbListadoPrecioDetalle_Insert_Result SPpreciodetalle in listPrecioDetalle)
                                            {
                                                MensajeErrorDetalle = SPpreciodetalle.MensajeError;
                                                if (MensajeError == "-1")
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbListaPrecio);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbListaPrecio);
                            }

                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.Producto = db.tbProducto.ToList();
                    ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioCrea);
                    ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioModifica);
                    ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo", tbListaPrecio.listp_Id);

                }

            }
         
                

            ViewBag.Producto = db.tbProducto.ToList();
            ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioCrea);
            ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioModifica);
            ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo", tbListaPrecio.listp_Id);
            return View(tbListaPrecio);
        }









        [HttpPost]
        public JsonResult SaveListaPrecioDetalle(tbListadoPrecioDetalle ListaDetalle)
        {
            List<tbListadoPrecioDetalle> sessionPrecioDetalle = new List<tbListadoPrecioDetalle>();
            var list = (List<tbListadoPrecioDetalle>)Session["ListadoPrecio"];
            if (list == null)
            {
                sessionPrecioDetalle.Add(ListaDetalle);
                Session["ListadoPrecio"] = sessionPrecioDetalle;
            }
            else
            {
                list.Add(ListaDetalle);
                Session["ListadoPrecio"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult QuitarPrecioDetalle(tbListadoPrecioDetalle ListadoPrecioDetalle)
        {
            var list = (List<tbListadoPrecioDetalle>)Session["ListadoPrecio"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.lispd_UsuarioCrea == ListadoPrecioDetalle.lispd_UsuarioCrea);
                list.Remove(itemToRemove);
                Session["ListadoPrecio"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }



        // GET: /ListaPrecios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbListaPrecio tbListaPrecio = db.tbListaPrecio.Find(id);
            if (tbListaPrecio == null)
            {
                return HttpNotFound();
            }
            ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioCrea);
            ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioModifica);
            ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo", tbListaPrecio.listp_Id);
            ViewBag.Producto = db.tbProducto.ToList();
            Session["listaEdit"] = null;
            return View(tbListaPrecio);
        }

        // POST: /ListaPrecios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "listp_Id,listp_Nombre,listp_EsActivo,listp_UsuarioCrea,listp_FechaCrea,listp_UsuarioModifica,listp_FechaModifica,listp_FechaInicioVigencia,listp_FechaFinalVigencia,listp_Prioridad")] tbListaPrecio tbListaPrecio)
        {
            var listEdit = (List<tbListadoPrecioDetalle>)Session["listaEdit"];
            string MensajeError = "";
            var MensajeErrorDetalle = "";
            IEnumerable<object> list = null;
            IEnumerable<object> listDetalle = null;
            if (ModelState.IsValid)
            {




                try
                {                    using (TransactionScope Tran = new TransactionScope())
                    {
                        list = db.UDP_Vent_tbListaPrecio_Update(tbListaPrecio.listp_Id,
                                                                   tbListaPrecio.listp_Nombre,
                                                                   tbListaPrecio.listp_EsActivo,
                                                                       tbListaPrecio.listp_UsuarioCrea,
                                                                   tbListaPrecio.listp_FechaCrea,
                                                                   Function.GetUser(),
                                                                    Function.DatetimeNow(),
                                                                   tbListaPrecio.listp_FechaInicioVigencia,
                                                                   tbListaPrecio.listp_FechaFinalVigencia,
                                                               Convert.ToInt16(tbListaPrecio.listp_Prioridad)
                                                               );
                                                                  
                    foreach (UDP_Vent_tbListaPrecio_Update_Result ListaPrecio in list)
                        MensajeError = ListaPrecio.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbListaPrecio);
                        }
                        else
                        {

                        }
                        {
                            if (MensajeError != "-1")
                            {
                                if (listEdit != null)
                                {
                                    if (listEdit.Count != 0)
                                    {
                                        foreach (tbListadoPrecioDetalle PrecioDetalle in listEdit)
                                        {
                                            var liistp_Id = Convert.ToInt32(MensajeError);
                                            PrecioDetalle.listp_Id = liistp_Id;
                                            PrecioDetalle.listp_Id = liistp_Id;
                                            listDetalle = db.UDP_Vent_tbListadoPrecioDetalle_Insert(
                                              PrecioDetalle.listp_Id,
                                              PrecioDetalle.prod_Codigo,
                                              PrecioDetalle.lispd_PrecioMayorista,
                                              PrecioDetalle.lispd_PrecioMinorista,
                                              PrecioDetalle.lispd_DescCaja,
                                              PrecioDetalle.lispd_DescGerente,
                                              Function.GetUser(),
                                    Function.DatetimeNow());
                                            foreach (UDP_Vent_tbListadoPrecioDetalle_Insert_Result SPpreciodetalle in listDetalle)
                                            {
                                                MensajeErrorDetalle = SPpreciodetalle.MensajeError;
                                                if (MensajeError == "-1")
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbListaPrecio);
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbListaPrecio);
                            }

                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioCrea);
                    ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioModifica);
                    ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo", tbListaPrecio.listp_Id);
                    ViewBag.Producto = db.tbProducto.ToList();

                    ModelState.AddModelError("", "No se pudo agregar el registro");
                    ModelState.AddModelError("", "Ya existe un producto con este mismo nombre.");
                    //TempData["message"] = "No se pudo agregar el registro,ya existe producto";

                }

                return RedirectToAction("Index");
            }
            ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioCrea);
            ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbListaPrecio.listp_UsuarioModifica);
            ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo", tbListaPrecio.listp_Id);
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbListaPrecio);
        }


        [HttpPost]
        public JsonResult SaveDetalleEdit(tbListadoPrecioDetalle FacturaDetalleEdit)
        {
            List<tbListadoPrecioDetalle> sessionPrecioDetalleEdit = new List<tbListadoPrecioDetalle>();
            var listEdit = (List<tbListadoPrecioDetalle>)Session["listaEdit"];
            if (listEdit == null)
            {
                sessionPrecioDetalleEdit.Add(FacturaDetalleEdit);
                Session["listaEdit"] = sessionPrecioDetalleEdit;
            }
            else
            {
                listEdit.Add(FacturaDetalleEdit);
                Session["listaEdit"] = listEdit;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult InactivarListaPrecio(int CodLp, bool Activo)
        {
            var list = db.UDP_Vent_tbListaPrecio_Estado(CodLp, Helpers.ListaPrecioInactivo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActivarListaPrecio(int CodLp, bool Activo)
        {
            var list = db.UDP_Vent_tbListaPrecio_Estado(CodLp, Helpers.ListaPrecioActivo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaPrecioFechaFinal(Int16 Prioridad)
        {

            var list = db.UDP_Vent_tbListaPrecio_UltimaFechaVigente(Prioridad).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
            
        } 

        [HttpPost]
        public ActionResult GetListadoDetalleEdit(int listp_Id)
        {
            var list = db.UDP_Vent_tbListadoPrecioDetalle_Select(listp_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        // GET: /ListaPrecios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbListaPrecio tbListaPrecio = db.tbListaPrecio.Find(id);
            if (tbListaPrecio == null)
            {
                return HttpNotFound();
            }
            return View(tbListaPrecio);
        }



        [HttpPost]
        public JsonResult SaveFacturaDetalleEdit(tbFacturaDetalle FacturaDetalleEdit)
        {
            List<tbFacturaDetalle> sessionFacturaDetalle = new List<tbFacturaDetalle>();
            var listEdit = (List<tbFacturaDetalle>)Session["FacturaEdit"];
            if (listEdit == null)
            {
                sessionFacturaDetalle.Add(FacturaDetalleEdit);
                Session["FacturaEdit"] = sessionFacturaDetalle;
            }
            else
            {
                listEdit.Add(FacturaDetalleEdit);
                Session["FacturaEdit"] = listEdit;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }




        // POST: /ListaPrecios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbListaPrecio tbListaPrecio = db.tbListaPrecio.Find(id);
            db.tbListaPrecio.Remove(tbListaPrecio);
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
        public JsonResult GuardarListadoPrecioDetalle(tbListadoPrecioDetalle ListadoPrecioDetalles)
        {
            string Msj = "";

            try
            {
                string MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Vent_tbListadoPrecioDetalle_Insert(
                           ListadoPrecioDetalles.listp_Id,
                           ListadoPrecioDetalles.prod_Codigo,
                           ListadoPrecioDetalles.lispd_PrecioMayorista,
                           ListadoPrecioDetalles.lispd_PrecioMinorista,
                           ListadoPrecioDetalles.lispd_DescCaja,
                            ListadoPrecioDetalles.lispd_DescGerente,
                            Function.GetUser(),
                                    Function.DatetimeNow());
                foreach (UDP_Vent_tbListadoPrecioDetalle_Insert_Result ListadoDetalleD in list)
                    MensajeError = ListadoDetalleD.MensajeError;
                Msj = "El registro se guardo exitosamente";
                if (MensajeError == "-1")
                {
                    Msj = "No se pudo actualizar el registro, favor contacte al administrador.";
                    ModelState.AddModelError("", Msj);
                }
            }
            catch (Exception Ex)
            {
                Msj = Ex.Message.ToString();
                ModelState.AddModelError("", Msj);
            }

            return Json(Msj, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult UpdateListadoPrecioDetalle(tbListadoPrecioDetalle EditListadoPrecioDetalle)
        {
            try
            {
                //var MensajeError = 0;
                tbListadoPrecioDetalle VListaPrecio = db.tbListadoPrecioDetalle.Find(EditListadoPrecioDetalle.lispd_Id);
                string MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Vent_tbListadoPrecioDetalle_Update(
                            EditListadoPrecioDetalle.lispd_Id,
                            EditListadoPrecioDetalle.prod_Codigo,
                            EditListadoPrecioDetalle.lispd_PrecioMayorista,
                            EditListadoPrecioDetalle.lispd_PrecioMinorista,
                            EditListadoPrecioDetalle.lispd_DescCaja,
                            EditListadoPrecioDetalle.lispd_DescGerente,
                            VListaPrecio.lispd_UsuarioCrea,
                            VListaPrecio.lispd_FechaCrea,
                            Function.GetUser(),
                                    Function.DatetimeNow());

                foreach (UDP_Vent_tbListadoPrecioDetalle_Update_Result ListaDetalle in list)
                    MensajeError = ListaDetalle.MensajeError;
                if (MensajeError == "-1")
                {
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return PartialView("_EditListaPrecioDetalle");
                }
                else
                {
                    return RedirectToAction("Edit", "ListaPrecio", EditListadoPrecioDetalle.lispd_Id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return PartialView("_EditListaPrecioDetalle", EditListadoPrecioDetalle);
            }
        }

        [HttpPost]
        public JsonResult getListadoPrecioDetalle(int lispd_Id)
        {
            IEnumerable<object> list = null;
            try
            {
                list = db.UDP_Vent_tbListadoPrecioDetalle_Select(lispd_Id).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }










    }
}
