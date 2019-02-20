﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Net.Mime;

namespace ERP_GMEDINA.Controllers
{
    public class InventarioFisicoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: /InventarioFisico/
        public ActionResult Index()
        {
            if (Function.Sesiones("InventarioFisico/Index"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }
            var tbinventariofisico = db.tbInventarioFisico.Include(t => t.tbEstadoInventarioFisico).Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            this.listas();
            return View(tbinventariofisico.ToList());
        }

        // GET: /InventarioFisico/Details/5
        public ActionResult Details(int? id)
        {
            if (Function.Sesiones("InventarioFisico/Details"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            if(tbInventarioFisico == null)
            {
                return HttpNotFound();
            }
            ViewBag.bodega_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            this.listas();
            return View(tbInventarioFisico);
        }

        [HttpPost]
        public JsonResult GetResponsableBodega(int invf_responsable)
        {
            var list = db.SPGetResponsableBodega(invf_responsable).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        private void listas()
        {
          
            ViewBag.estif_Id = new SelectList(db.tbEstadoInventarioFisico, "estif_Id", "estif_Descripcion");
            ViewBag.invf_Id = new SelectList(db.tbInventarioFisico, "invf_Id", "invf_Descripcion");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Codigo");
            ViewBag.prod_Descripcion = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.tbProducto.ToList();
        }

        [HttpPost]
        public JsonResult CantidadExistencias(tbBodegaDetalle CantidadExistencias)
        {
            var list = db.UDP_Inv_CantidadExistente(CantidadExistencias.bod_Id,CantidadExistencias.prod_Codigo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProductosBodega(tbBodegaDetalle productos)
        {
            var list = db.UDP_Inv_tbInventarioFisico_ListaProductos(productos.bod_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProductosEnter(string cod_Barras)
        {
            var list = db.SP_tbInventariofisico_ProductosRepetidos(cod_Barras).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult ProductosRepetidos(string data_producto)
        //{
        //    var datos = "";
        //    if (Session["tbInventarioFisicoDetalle"] == null)
        //    {

        //    }
        //    else
        //    {
        //        var menu = Session["tbInventarioFisicoDetalle"] as List<tbInventarioFisicoDetalle>;

        //        foreach (var t in menu)
        //        {
        //            if (t.prod_Codigo == data_producto)
        //                datos = data_producto;
        //        }


        //    }

        //    return Json(datos);
        //}

        public FileResult ExportReport(int? id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ImprimirConciliacion.rpt"));
            var tbInventarioFisico = db.tbInventarioFisico.ToList();
            var tbEmpleado = db.tbEmpleado.ToList();
            var tbestadoinventariofisico = db.tbEstadoInventarioFisico.ToList();
            var tbInventarioFisicoDetalle = db.tbInventarioFisicoDetalle.ToList();
            var tbProducto = db.tbProducto.ToList();
            var tbBodega = db.tbBodega.ToList();
            var tbUnidadMedida = db.tbUnidadMedida.ToList();
            var todo = (from inv in tbInventarioFisico
                        join  e in tbestadoinventariofisico on inv.estif_Id equals e.estif_Id
                        join b in tbBodega on inv.bod_Id equals b.bod_Id
                        join emp in tbEmpleado on b.bod_ResponsableBodega equals emp.emp_Id
                        join invd in tbInventarioFisicoDetalle on inv.invf_Id equals invd.invf_Id
                        join p in tbProducto on invd.prod_Codigo equals p.prod_Codigo
                        join u in tbUnidadMedida on p.uni_Id equals u.uni_Id
                        where inv.invf_Id == id
                        select new
                        {
                            invf_Descripcion = inv.invf_Descripcion,
                            invf_FechaInventario = inv.invf_FechaInventario,
                            estif_Descripcion = e.estif_Descripcion,
                            bod_Nombre = b.bod_Nombre,
                            emp_Nombres = emp.emp_Nombres,
                            emp_Apellidos = emp.emp_Apellidos,
                            prod_CodigoBarras = p.prod_CodigoBarras,
                            prod_Descripcion = p.prod_Descripcion,
                            prod_Marca = p.prod_Marca,
                            prod_Modelo = p.prod_Modelo,
                            uni_Descripcion = u.uni_Descripcion,
                            invfd_CantidadSistema = invd.invfd_CantidadSistema,
                            invfd_Cantidad = invd.invfd_Cantidad
                        }).ToList();

            rd.SetDataSource(todo);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                var fileName = "Inventario Fisico.pdf";
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf",fileName);
            }
            catch
            {
                throw;
            }
        }

        //public class PdfResult : FileResult
        //{
        //    private const String DefaultFileName = "ImprimirConciliacion.pdf";
        //    private readonly Byte[] _byteArray;

        //    public PdfResult(Byte[] byteArray, String fileName = DefaultFileName)
        //        : base(MediaTypeNames.Application.Pdf)
        //    {
        //        _byteArray = byteArray;
        //        FileDownloadName = fileName;
        //    }
        //    protected override void WriteFile(HttpResponseBase response) { response.BinaryWrite(_byteArray); }
        //}

        // GET: /InventarioFisico/Create
        public ActionResult Create()
        {
            if (Function.Sesiones("InventarioFisico/Create"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }
            try
            {
                ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            this.listas();
            Session["tbInventarioFisicoDetalle"] = null;
            return View();
        }

        // POST: /InventarioFisico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invf_Id,invf_Descripcion,invf_ResponsableBodega,bod_Id,estif_Id,invf_FechaInventario")] tbInventarioFisico tbInventarioFisico)
        {
            IEnumerable<object> INVENTARIOFISICO = null;
            IEnumerable<object> INVFISICODETALLE = null;
            var id = 0;
            var MensajeError = "";
            var MsjError = "";
            var detalle = (List<tbInventarioFisicoDetalle>)Session["tbInventarioFisicoDetalle"];
            if (ModelState.IsValid)
            {
                if (detalle == null)
                {
                    TempData["smserror"] = " No Puede Ingresar Una Entrada Sin Detalle.";
                    ViewBag.smserror = TempData["smserror"];
                    ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbInventarioFisico.bod_Id);
                    this.listas();
                    return View();
                }
                else
                {
                    using (TransactionScope _Tran = new TransactionScope())
                    {
                        try
                        {
                            INVENTARIOFISICO = db.UDP_Inv_tbInventarioFisico_Insert(tbInventarioFisico.invf_Descripcion
                                                                                    , tbInventarioFisico.invf_ResponsableBodega
                                                                                    , tbInventarioFisico.bod_Id
                                                                                    , tbInventarioFisico.estif_Id
                                                                                    , tbInventarioFisico.invf_FechaInventario);
                            foreach (UDP_Inv_tbInventarioFisico_Insert_Result InventarioFisico in INVENTARIOFISICO)
                                id = Convert.ToInt32(InventarioFisico.MensajeError);
                            if (MsjError == "-")
                            {
                                ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                                return View(tbInventarioFisico);
                            }
                            else
                            {
                                if (detalle != null)
                                {
                                    if (detalle.Count > 0)
                                    {
                                        foreach (tbInventarioFisicoDetalle invfd in detalle)
                                        {
                                            INVFISICODETALLE = db.UDP_Inv_tbInventarioFisicoDetalle_Insert(id
                                                                                                            , invfd.prod_Codigo
                                                                                                            , invfd.invfd_Cantidad
                                                                                                            , invfd.invfd_CantidadSistema
                                                                                                            , invfd.uni_Id);
                                            foreach (UDP_Inv_tbInventarioFisicoDetalle_Insert_Result invfdetalle in INVFISICODETALLE)
                                                MsjError = invfdetalle.MensajeError;
                                            {
                                                ModelState.AddModelError("", "No se Guardo el Registro");
                                            }
                                        }
                                    }
                                }
                                {
                                    _Tran.Complete();
                                }
                            }


                        }
                        catch (Exception Ex)
                        {
                            //Ex.Message.ToString();
                            //ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                            MsjError = "-1";
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            this.listas();
            return View(tbInventarioFisico);
        }


        [HttpPost]
        public JsonResult NuevoDetallemodal(tbInventarioFisicoDetalle guardar_detalle)
        {
            string Msj = "";
            try
            {
                IEnumerable<object> list = null;
                list = db.UDP_Inv_tbInventarioFisicoDetalle_Insert(guardar_detalle.invf_Id
                                                           , guardar_detalle.prod_Codigo
                                                         , guardar_detalle.invfd_Cantidad
                                                         , guardar_detalle.invfd_CantidadSistema
                                                         , guardar_detalle.uni_Id
                                                                            );
                foreach (UDP_Inv_tbInventarioFisicoDetalle_Insert_Result invfd in list)
                    Msj = invfd.MensajeError;

                if (Msj.Substring(0, 2) == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");


                }
                else
                {
                    //return View("Edit/" + bod_Id);
                    return Json("Index");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
            }
            return Json("Index");
        }

        //Inventario Fisico Detalle
        [HttpPost]
        public JsonResult GuardarInventarioDetalle(tbInventarioFisicoDetalle invfd,string data_producto)
        {
            var datos = "";
            decimal cantvieja = 0;
            decimal cantnueva = 0;
            data_producto = invfd.prod_Codigo;
            decimal data_cantidad = invfd.invfd_Cantidad;
            List<tbInventarioFisicoDetalle> sessionInventarioFisicoDetalle = new List<tbInventarioFisicoDetalle>();
            var list = (List<tbInventarioFisicoDetalle>)Session["tbInventarioFisicoDetalle"];
            if (list == null)
            {
                sessionInventarioFisicoDetalle.Add(invfd);
                Session["tbInventarioFisicoDetalle"] = sessionInventarioFisicoDetalle;
            }
            else
            {
                foreach (var t in list)
                    if (t.prod_Codigo == data_producto)
                        {
                            datos = data_producto;
                        foreach (var viejo in list)
                            if (viejo.prod_Codigo == invfd.prod_Codigo)
                                cantvieja = viejo.invfd_Cantidad;
                        cantnueva = cantvieja + data_cantidad;
                        t.invfd_Cantidad = cantnueva;
                        return Json(datos, JsonRequestBehavior.AllowGet);
                    }
                            list.Add(invfd);
                            Session["tbInventarioFisicoDetalle"] = list;
                        return Json(datos, JsonRequestBehavior.AllowGet);
                    }
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        // GET: /InventarioFisico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Function.Sesiones("InventarioFisico/Edit"))
            {

            }
            else
            {
                return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbInventarioFisico.invf_UsuarioCrea).usu_NombreUsuario;
            if (tbInventarioFisico == null)
            {
                return HttpNotFound();
            }
            ViewBag.bodegas = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbInventarioFisico.bod_Id);
            this.listas();
            Session["tbInventarioFisicoDetalle"] = null;
            return View(tbInventarioFisico);
        }

        // POST: /InventarioFisico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id,[Bind(Include="invf_Id,invf_Descripcion,invf_ResponsableBodega,bod_Id,estif_Id,invf_FechaInventario,invf_UsuarioCrea,invf_FechaCrea")] tbInventarioFisico tbInventarioFisico)
        {
            IEnumerable<object> Inv = null;
            IEnumerable<object> Detalle = null;
            var idMaster = 0;
            var MsjError = "";
            var listaDetalle = (List<tbInventarioFisicoDetalle>)Session["tbInventarioFisicoDetalle"];
            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {
                        Inv = db.UDP_Inv_tbInventarioFisico_Update(tbInventarioFisico.invf_Id,
                            tbInventarioFisico.invf_Descripcion, 
                            tbInventarioFisico.invf_ResponsableBodega, 
                            tbInventarioFisico.bod_Id, 
                            tbInventarioFisico.estif_Id, 
                            tbInventarioFisico.invf_FechaInventario, 
                            tbInventarioFisico.invf_UsuarioCrea,
                            tbInventarioFisico.invf_FechaCrea);
                        foreach (UDP_Inv_tbInventarioFisico_Update_Result InventarioFisico in Inv)
                            idMaster = Convert.ToInt32(InventarioFisico.MensajeError);

                        if (MsjError == "-")
                        {
                            ModelState.AddModelError("", "No se Actualizó el Registro");
                            return View(tbInventarioFisico);
                        }
                        else
                        {
                            if (listaDetalle != null)
                            {
                                if (listaDetalle.Count > 0)
                                {
                                    foreach (tbInventarioFisicoDetalle invd in listaDetalle)
                                    {
                                        Detalle = db.UDP_Inv_tbInventarioFisicoDetalle_Insert(idMaster,
                                                                                               invd.prod_Codigo,
                                                                                               invd.invfd_Cantidad,
                                                                                               invd.invfd_CantidadSistema,
                                                                                               invd.uni_Id);
                                        foreach (UDP_Inv_tbInventarioFisicoDetalle_Insert_Result inv_detalle in Detalle)
                                        //    MsjError = inv_detalle.MensajeError;

                                        //if (MsjError == "-1")
                                        {
                                            ModelState.AddModelError("", "No se Actualizó el Registro");
                                        //    ViewBag.bodegas = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbInventarioFisico.bod_Id);
                                        //    this.listas();
                                        //    return View(tbInventarioFisico);
                                        //}
                                        //else
                                        //{
                                        //    _Tran.Complete();
                                        //    return RedirectToAction("Index");
                                        }
                                    }
                                }
                            }
                            //else
                            {
                                _Tran.Complete();
                                return RedirectToAction("Index");
                            }

                        }


                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                        ViewBag.bodegas = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbInventarioFisico.bod_Id);
                        this.listas();
                        return View(tbInventarioFisico);

                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.bodegas = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbInventarioFisico.bod_Id);
            this.listas();
            return View(tbInventarioFisico);
        }
        //public ActionResult ExportToExcel()
        //{
        //    List<tbInventarioFisico> Inventario = db.tbInventarioFisico.ToList();
        //    if (Inventario == null || Inventario.Count() == 0)
        //    {
        //        return RedirectToAction("Index", "InventarioFisico");
        //    }
        //    string[] columns = { "CodIntructor", "Nombre", "Apellido", "Identidad",
        //                        "Departamento", "CodMunicipio", "NombreMadre",
        //                        "NombrePadre", "CodGradoPolicial", "Sexo", "Correo",
        //                        "Celular", "Telefono", "CodBanco", "CtaBanco", "CodUnidadDepartamental",
        //                        "CodUnidadMetropolitana", "CodAldea", "CodEstado",
        //                        "CodTipoCuenta", "DistritoUnidad", "UsuarioCreacion",
        //                        "FechaCreacion", "UsuarioModifica", "FechaModifica" };

        //    byte[] filecontent = ExcelExportHelper.ExportExcel(Inventario, "Inventario", false, columns);
        //    return File(filecontent, ExcelExportHelper.ExcelContentType, "Conciliacion de Inventario Fisico.xlsx");
        //}

        [HttpPost]
        public JsonResult GetInventarioDetalle(int invfd_Id)
        {
            var list = db.SDP_tbInventarioFisicoDetalle_Select(invfd_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateInvFisicoDetalle(tbInventarioFisicoDetalle actualizardetalle)
        {
            string Msj = "";
            try
            {
                IEnumerable<object> list = null;
                list = db.UDP_Inv_tbInventarioFisicoDetalle_Update(actualizardetalle.invfd_Id
                                                        , actualizardetalle.invf_Id
                                                        , actualizardetalle.prod_Codigo
                                                        , actualizardetalle.invfd_Cantidad
                                                        , actualizardetalle.invfd_CantidadSistema
                                                        , actualizardetalle.uni_Id
                                                                            );
                foreach (UDP_Inv_tbInventarioFisicoDetalle_Update_Result invfd in list)
                    Msj = invfd.MensajeError;

                if (Msj.Substring(0, 2) == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    this.listas();

                }
                else
                {
                    //return View("Edit/" + bod_Id);
                    return Json("Index");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
            }
            return Json("Index");
        }

        // GET: /InventarioFisico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            if (tbInventarioFisico == null)
            {
                return HttpNotFound();
            }
            return View(tbInventarioFisico);
        }

        [HttpPost]
        public JsonResult removeInvFisicoDetalle(tbInventarioFisicoDetalle detalle)
        {
            var list = (List<tbInventarioFisicoDetalle>)Session["tbInventarioFisicoDetalle"];
            if (list != null)
            {
                var itemToRemove = list.Single(r => r.prod_Codigo == detalle.prod_Codigo);
                list.Remove(itemToRemove);
                Session["tbInventarioFisicoDetalle"] = list;
               if(list.Count == 0)
                {
                    Session["tbInventarioFisicoDetalle"] = null;
                }
            }
            //return Json(list, JsonRequestBehavior.AllowGet);
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        // POST: /InventarioFisico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            db.tbInventarioFisico.Remove(tbInventarioFisico);
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

        public ActionResult Conciliar(int? id)
        {

            try
            {
                tbInventarioFisico obj = db.tbInventarioFisico.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbInventarioFisico_Update_Estado(id, Helpers.InvFisicoConciliado );
                foreach (UDP_Inv_tbInventarioFisico_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Index");
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

