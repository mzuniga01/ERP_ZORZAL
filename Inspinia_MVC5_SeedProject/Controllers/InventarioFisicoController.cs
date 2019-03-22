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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Net.Mime;
using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Reports;
using ERP_GMEDINA.Dataset;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;

namespace ERP_GMEDINA.Controllers
{
    public class InventarioFisicoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: /InventarioFisico/
        [SessionManager("InventarioFisico/Index")]
        public ActionResult Index()
        {
            var tbinventariofisico = db.tbInventarioFisico.Include(t => t.tbEstadoInventarioFisico).Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            this.listas();
            return View(tbinventariofisico.ToList());
        }

        // GET: /InventarioFisico/Details/5
        [SessionManager("InventarioFisico/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            if(tbInventarioFisico == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.bodega_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            this.listas();
            return View(tbInventarioFisico);
        }

        [HttpPost]
        public JsonResult GetResponsableBodega(int invf_responsable)
        {
            IEnumerable<object> list = null;
            try
            {
                list = db.SPGetResponsableBodega(invf_responsable).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
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
            IEnumerable<object> list = null;
            try
            {
                 list = db.UDP_Inv_CantidadExistente(CantidadExistencias.bod_Id, CantidadExistencias.prod_Codigo).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProductosEnter(string cod_Barras)
        {
            IEnumerable<object> list = null;
            try
            {
                 list = db.SP_tbInventariofisico_ProductosRepetidos(cod_Barras).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportReport(int? id)
        {
            var idInvf = Convert.ToInt32(id);
            ReportDocument rd = new ReportDocument();
            Stream stream = null;
            ImprimirConciliacion FaltantesRP = new ImprimirConciliacion();
            Reportes faltantes = new Reportes();

            var InventarioTableAdapter = new UDV_TBInventarioFisico_ImprimirConciliacionTableAdapter();

            try
            {
                InventarioTableAdapter.Fill(faltantes.UDV_TBInventarioFisico_ImprimirConciliacion, idInvf);

                FaltantesRP.SetDataSource(faltantes);
                stream = FaltantesRP.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                FaltantesRP.Close();
                FaltantesRP.Dispose();

                string fileName = "ImprimirConciliacion.pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(stream, "application/pdf");
            }

            catch (Exception Ex)
            {
                Ex.Message.ToString();
                throw;
            }
        }

        // GET: /InventarioFisico/Create
        [SessionManager("InventarioFisico/Create")]
        public ActionResult Create()
        {
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
        [SessionManager("InventarioFisico/Create")]
        public ActionResult Create([Bind(Include = "invf_Id,invf_Descripcion,invf_ResponsableBodega,bod_Id,estif_Id,invf_FechaInventario")] tbInventarioFisico tbInventarioFisico)
        {
            IEnumerable<object> INVENTARIOFISICO = null;
            IEnumerable<object> INVFISICODETALLE = null;
            string MensajeError = "";
            string MsjError = "";
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
                                                                                    , tbInventarioFisico.invf_FechaInventario,
                                                                                    Function.GetUser(), Function.DatetimeNow());
                            foreach (UDP_Inv_tbInventarioFisico_Insert_Result InventarioFisico in INVENTARIOFISICO)
                                MsjError = InventarioFisico.MensajeError;
                            if (MsjError.StartsWith("-1"))
                            {
                                this.listas();
                                Function.InsertBitacoraErrores("InventarioFisico/Create", MsjError, "Create");
                                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
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
                                            INVFISICODETALLE = db.UDP_Inv_tbInventarioFisicoDetalle_Insert(Convert.ToInt16(MsjError)
                                                                                                            , invfd.prod_Codigo
                                                                                                            , invfd.invfd_Cantidad
                                                                                                            , invfd.invfd_CantidadSistema
                                                                                                            , invfd.uni_Id, Function.GetUser(), Function.DatetimeNow());
                                            foreach (UDP_Inv_tbInventarioFisicoDetalle_Insert_Result invfdetalle in INVFISICODETALLE)
                                                MensajeError = invfdetalle.MensajeError;
                                            if (MensajeError.StartsWith("-1"))
                                            {
                                                ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
                                                this.listas();
                                                Function.InsertBitacoraErrores("InventarioFisico/Create", MsjError, "Create");
                                                ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                                return View(tbInventarioFisico);
                                            }
                                        }
                                    }
                                }
                                    _Tran.Complete();
                            }
                        }
                        catch (Exception Ex)
                        {
                            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
                            this.listas();
                            Function.InsertBitacoraErrores("InventarioFisico/Create", Ex.Message.ToString(), "Create");
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View(tbInventarioFisico);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            this.listas();
            return View(tbInventarioFisico);
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
        [SessionManager("InventarioFisico/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbInventarioFisico.invf_UsuarioCrea).usu_NombreUsuario;
            if (tbInventarioFisico == null)
            {
                return RedirectToAction("NotFound", "Login");
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
        [SessionManager("InventarioFisico/Edit")]
        public ActionResult Edit(int? id,[Bind(Include="invf_Id,invf_Descripcion,invf_ResponsableBodega,bod_Id,estif_Id,invf_FechaInventario,invf_UsuarioCrea,invf_FechaCrea")] tbInventarioFisico tbInventarioFisico)
        {
            IEnumerable<object> Inv = null;
            IEnumerable<object> Detalle = null;
            string MsjError = "";
            string MensajeError = "";
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
                            tbInventarioFisico.invf_FechaCrea,
                            Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Inv_tbInventarioFisico_Update_Result InventarioFisico in Inv)
                            MsjError = InventarioFisico.MensajeError;

                        if (MsjError.StartsWith("-1"))
                        {
                            ViewBag.bodegas = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbInventarioFisico.bod_Id);
                            this.listas();
                            Function.InsertBitacoraErrores("InventarioFisico/Edit", MsjError, "Edit");
                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
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
                                        Detalle = db.UDP_Inv_tbInventarioFisicoDetalle_Insert(Convert.ToInt16(MsjError),
                                                                                               invd.prod_Codigo,
                                                                                               invd.invfd_Cantidad,
                                                                                               invd.invfd_CantidadSistema,
                                                                                               invd.uni_Id, 
                                                                                               Function.GetUser(), 
                                                                                               Function.DatetimeNow());
                                        foreach (UDP_Inv_tbInventarioFisicoDetalle_Insert_Result inv_detalle in Detalle)
                                            MensajeError = inv_detalle.MensajeError;
                                        if (MensajeError.StartsWith("-1"))
                                        {
                                            Function.InsertBitacoraErrores("InventarioFisico/Edit", MsjError, "Edit");
                                            ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                            ViewBag.bodegas = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbInventarioFisico.bod_Id);
                                            this.listas();
                                            return RedirectToAction("Edit/" + MsjError);
                                        }
                                    }
                                }
                            }
                                _Tran.Complete();
                        }
                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                        ViewBag.bodegas = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbInventarioFisico.bod_Id);
                        this.listas();
                        Function.InsertBitacoraErrores("InventarioFisico/Create", Ex.Message.ToString(), "Create");
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return RedirectToAction("Edit/" + MsjError);
                    }
                }
                return RedirectToAction("Edit");
            }
            ViewBag.bodegas = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbInventarioFisico.bod_Id);
            this.listas();
            return View(tbInventarioFisico);
        }

        [HttpPost]
        public JsonResult GetInventarioDetalle(int invfd_Id)
        {
            IEnumerable<object> list = null;
            try
            {
                 list = db.SDP_tbInventarioFisicoDetalle_Select(invfd_Id).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult cambiobodega(int bod_Id)
        {
            var list = (List<tbInventarioFisicoDetalle>)Session["tbInventarioFisicoDetalle"];
            try
            {
                if (list != null)
                {
                    Session["tbInventarioFisicoDetalle"] = null;
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(bod_Id);
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
                                                                            , Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbInventarioFisicoDetalle_Update_Result invfd in list)
                    Msj = invfd.MensajeError;

                if (Msj.Substring(0, 2) == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    this.listas();

                }
                else
                {
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
            return Json(list, JsonRequestBehavior.AllowGet);
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
                list = db.UDP_Inv_tbInventarioFisico_Update_Estado(id, Helpers.InvFisicoConciliado, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbInventarioFisico_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
            }
        }

        public JsonResult Reconteo(int id,string User_NombreUsuario,string User_Password)
        {
            try
            {
                var incorrecto = "";
                var rol = 0;
                var usuid = 0;
                var idparametro = 0;
                var parametro = db.tbParametro.ToList();
                foreach (tbParametro idpara in parametro)
                    idparametro = idpara.par_RolAuditor;
                var credenciales = db.UDP_Acce_Login(User_NombreUsuario, User_Password).ToList();
                foreach (UDP_Acce_Login_Result usuario in credenciales)
                    usuid = usuario.usu_Id;
                if (credenciales.Count > 0)
                {
                    var lista = db.SDP_Acce_GetRolesAsignados(usuid).ToList();
                    foreach (SDP_Acce_GetRolesAsignados_Result roles in lista)
                        rol = roles.rol_Id;
                    if (rol == idparametro)
                    {

                    }
                    else
                    {
                        incorrecto = "incorrecto";
                        return Json(incorrecto);
                    }
                }
                else
                {
                    return Json(incorrecto);
                }
               
                    tbInventarioFisico obj = db.tbInventarioFisico.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbInventarioFisico_Update_Estado(id, Helpers.InvFisicoReconteo, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbInventarioFisico_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return Json("Edit/" + id);
                }
                else
                {
                    return Json("Edit/"+ id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return Json("Edit/" + id);
            }
        }

    }
}

