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
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class BodegaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();      
        GeneralFunctions Function = new GeneralFunctions();

        [SessionManager("Bodega/Index")]
        // GET: /Bodega/
        public ActionResult Index()
        {
            var tbbodega = db.tbBodega.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbMunicipio);
            return View(tbbodega.ToList());
        }

        // GET: /Bodega/Details/5
        [SessionManager("Bodega/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbBodega tbBodega = db.tbBodega.Find(id);
            
            if (tbBodega == null)
            {
                return RedirectToAction("NotFound", "Login");
            }

            this.AllLists();
            return View(tbBodega);
        }

        // GET: /Bodega/Create
        [SessionManager("Bodega/Create")]
        public ActionResult Create()
        {
            this.AllLists();
            Session["tbBodegaDetalle"] = null;
            return View();
        }

        [SessionManager("Bodega/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bod_Id,bod_Nombre,bod_ResponsableBodega,bod_Direccion,bod_Correo,bod_Telefono,usu_Id,mun_Codigo,bod_EsActiva")] tbBodega tbBodega)
        {
            IEnumerable<object> BODEGA = null;
            IEnumerable<object> DETALLE = null;
            var idMaster = 0;
            string MsjError = "";
            string MensajeError = "";
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
                                                        , tbBodega.mun_Codigo
                                                        , Function.GetUser()
                                                        ,Function.DatetimeNow());
                        foreach (UDP_Inv_tbBodega_Insert_Result bodega in BODEGA)
                            idMaster = Convert.ToInt32(bodega.MensajeError);
                        if (MsjError.StartsWith("-1"))
                        {
                            this.AllLists();
                            Function.InsertBitacoraErrores("Bodega/Create", MsjError, "Create");
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
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
                                                                                    , idMaster
                                                                                    , bodd.bodd_CantidadMinima
                                                                                    , bodd.bodd_CantidadMaxima
                                                                                    , bodd.bodd_PuntoReorden
                                                                                    , bodd.bodd_Costo
                                                                                    , bodd.bodd_CostoPromedio);
                                        foreach (UDP_Inv_tbBodegaDetalle_Insert_Result B_detalle in DETALLE)
                                            MensajeError = B_detalle.MensajeError;
                                        if (MensajeError.StartsWith("-1"))
                                        {
                                            this.AllLists();
                                            Function.InsertBitacoraErrores("Bodega/Create", MsjError, "Create");
                                            ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                            return View(tbBodega);
                                        }
                                    }
                                }
                            }
                            _Tran.Complete();
                        }
                    }
                    catch (Exception Ex)
                    {
                        this.AllLists();
                        Function.InsertBitacoraErrores("Bodega/Create", Ex.Message.ToString(), "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbBodega);
                    }
                }
                return RedirectToAction("Index");
            }
            this.AllLists();
            return View(tbBodega);
        }

        // GET: /Bodega/Edit/5
        [SessionManager("Bodega/Edit")]
        public ActionResult Edit(int? id)
        {
            try
            {
                ViewBag.smserror = TempData["smserror"].ToString();
            }
            catch { }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbBodega tbBodega = db.tbBodega.Find(id);
            ViewBag.IdBodegaDetalleEdit = id;
            if (tbBodega == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            this.AllLists();
            ResponsableBodega(tbBodega.bod_ResponsableBodega);
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbBodega.dep_Codigo);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
            Session["tbBodegaDetalle"] = null;
            return View(tbBodega);
        }

        [SessionManager("Bodega/Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "bod_Id,bod_Nombre,bod_ResponsableBodega,bod_Direccion,bod_Correo,bod_Telefono,usu_Id,mun_Codigo,bod_EsActiva,bod_UsuarioCrea,bod_FechaCrea,bod_UsuarioModifica,bod_FechaModifica,dep_Codigo")] tbBodega tbBodega)
        {
            IEnumerable<object> BODEGA = null;
            IEnumerable<object> DETALLE = null;
            var idMaster = 0;
            string MsjError = "";
            string MensajeError = "";
            var listaDetalle = (List<tbBodegaDetalle>)Session["tbBodegaDetalle"];
            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {
                        BODEGA = db.UDP_Inv_tbBodega_Update(tbBodega.bod_Id
                                                            , tbBodega.bod_Nombre
                                                            , tbBodega.bod_ResponsableBodega
                                                            , tbBodega.bod_Direccion
                                                            , tbBodega.bod_Correo
                                                            , tbBodega.bod_Telefono
                                                            , tbBodega.mun_Codigo
                                                            , tbBodega.bod_UsuarioCrea
                                                            , tbBodega.bod_FechaCrea,
                                                            Function.GetUser()
                                                        , DateTime.Now);
                        foreach (UDP_Inv_tbBodega_Update_Result bodega in BODEGA)
                            idMaster = Convert.ToInt32(bodega.MensajeError);
                        if (MsjError.StartsWith("-1"))
                        {
                            this.AllLists();
                            ResponsableBodega(tbBodega.bod_ResponsableBodega);
                            Function.InsertBitacoraErrores("Bodega/Edit", MsjError, "Edit");
                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                            return RedirectToAction("Edit/" + idMaster);
                        }
                        else
                        {
                            if (listaDetalle != null)
                            {
                                if (listaDetalle.Count > 0)
                                {
                                    foreach (tbBodegaDetalle bodd in listaDetalle)
                                    {
                                        DETALLE = db.UDP_Inv_tbBodegaDetalle_Insert(
                                                                                      bodd.prod_Codigo
                                                                                    , idMaster
                                                                                    , bodd.bodd_CantidadMinima
                                                                                    , bodd.bodd_CantidadMaxima
                                                                                    , bodd.bodd_PuntoReorden
                                                                                    , bodd.bodd_Costo
                                                                                    , bodd.bodd_CostoPromedio);
                                        foreach (UDP_Inv_tbBodegaDetalle_Insert_Result B_detalle in DETALLE)
                                            MensajeError = B_detalle.MensajeError;
                                        if (MensajeError.StartsWith("-1"))
                                        {
                                            this.AllLists();
                                            ResponsableBodega(tbBodega.bod_ResponsableBodega);
                                            ViewBag.deparatamento_Edit = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbBodega.dep_Codigo);
                                            ViewBag.municipio_Edit = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
                                            Function.InsertBitacoraErrores("Bodega/Edit", MsjError, "Edit");
                                            ModelState.AddModelError("", "No se pudo actualizar el registro detalle, favor contacte al administrador.");
                                            return RedirectToAction("Edit/" + idMaster);
                                        }
                                    }
                                }
                            }
                            _Tran.Complete();
                            return RedirectToAction("Edit/" + idMaster);
                        }
                    }
                    catch (Exception Ex)
                    {
                        this.AllLists();
                        ResponsableBodega(tbBodega.bod_ResponsableBodega);
                        ViewBag.deparatamento_Edit = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbBodega.dep_Codigo);
                        ViewBag.municipio_Edit = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
                        Function.InsertBitacoraErrores("Bodega/Create", Ex.Message.ToString(), "Create");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return RedirectToAction("Edit/" + idMaster);
                    }
                }
            }
            this.AllLists();
            ResponsableBodega(tbBodega.bod_ResponsableBodega);
            ViewBag.deparatamento_Edit = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbBodega.dep_Codigo);
            ViewBag.municipio_Edit = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbBodega.mun_Codigo);
            ViewBag.bod_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBodega.bod_UsuarioCrea);
            ViewBag.bod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbBodega.bod_UsuarioModifica);
            return View(tbBodega);
        }

        //para Mostrar la cantidad en Existencia de la bodega


        //para que cambie estado a activar
        [SessionManager("Bodega/InactivarEstado")]
        public ActionResult EstadoInactivar(int? id)
        {

            try
            {
                tbBodega obj = db.tbBodega.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbBodega_Update_Estado_Validacion(id, EstadoBodega.Inactivo);
                foreach (UDP_Inv_tbBodega_Update_Estado_Validacion_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {
                    TempData["smserror"] = "No se puede Inactivar Bodegas Con Detalles";
                    ViewBag.smserror_Estado = TempData["smserror"];

                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    ViewBag.smserror_Estado = "";
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
        [SessionManager("Bodega/ActivarEstado")]
        public ActionResult Estadoactivar(int? id)
        {

            try
            {
                tbBodega obj = db.tbBodega.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbBodega_Update_Estado_Validacion(id, EstadoBodega.Activo);
                foreach (UDP_Inv_tbBodega_Update_Estado_Validacion_Result obje in list)
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

        }
        [SessionManager("Bodega/DeleteDetalle")]
        public ActionResult DeleteDetalle(int? id)
        {

            try
            {

                tbBodegaDetalle obj = db.tbBodegaDetalle.Find(id);
                var idbodega = obj.bod_Id;
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbBodegaDetalle_Delete(id);
                foreach (UDP_Inv_tbBodegaDetalle_Delete_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {
                    TempData["smserror"] = "Nose puede eliminar el detalle tiene Cantidad Existente en bodega.";
                    ViewBag.smserror = TempData["smserror"];

                    ModelState.AddModelError("", "No se puede Borrar el registro");
                    //return RedirectToAction("Index");
                    return RedirectToAction("Edit/" + idbodega);
                }
                else
                {
                    ViewBag.smserror = "";
                    //return RedirectToAction("Index");
                    return RedirectToAction("Edit/" + idbodega);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se puede Borrar el registro");
                return RedirectToAction("Index");
            }


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

        [HttpPost]
        public JsonResult UpdateBodegaDetalle(tbBodegaDetalle Editardetalle)
        {
            
            string Msj = "";
            try
            {
                tbBodegaDetalle obj = db.tbBodegaDetalle.Find(Editardetalle.bodd_Id);
                var MensajeError = "";
                var idbodega = Editardetalle.bod_Id;
                IEnumerable<object> list = null;
                list = db.UDP_Inv_tbBodegaDetalle_Update(Editardetalle.bodd_Id
                                                        , Editardetalle.prod_Codigo
                                                        , Editardetalle.bod_Id
                                                        , Editardetalle.bodd_CantidadMinima
                                                        , Editardetalle.bodd_CantidadMaxima
                                                        , Editardetalle.bodd_PuntoReorden
                                                        , Editardetalle.bodd_UsuarioCrea
                                                        , Editardetalle.bodd_FechaCrea
                                                        , Editardetalle.bodd_Costo
                                                        , Editardetalle.bodd_CostoPromedio
                                                                            );
                foreach (UDP_Inv_tbBodegaDetalle_Update_Result bodega in list)
                    MensajeError = bodega.MensajeError;

                if (MensajeError.StartsWith("-1"))
                {
                    //ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    //return PartialView("_EditBodegaDetalleModal");
                    Msj = "No se pudo agregar el registro, favor contacte al administrador.";
                    ModelState.AddModelError("", Msj);

                }
                else
                {
                    //return View("Edit/" + idbodega);
                    //return RedirectToAction("Index");
                    //return RedirectToAction("Edit/" + idbodega);
                    Msj = "Exito.";
                }
            }
            catch (Exception Ex)
            {
                //Ex.Message.ToString();
                //ModelState.AddModelError("", "No se Actualizo el registro");
                //return PartialView("_EditBodegaDetalleModal", Editardetalle);
                Msj = Ex.Message.ToString();
                ModelState.AddModelError("", Msj);
            }
            return Json(Msj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BuscarProductos(string GET_Barras_Nuevo)
        {
            
            var list = db.spGetProducto_BodegaDetalle(GET_Barras_Nuevo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult BuscarCodigoBarras(int GET_Bod, string GET_Barras /*tbProducto GET_Barras , tbBodegaDetalle GET_Bod*/)
        {
            var list = db.spGetProducto(GET_Bod, GET_Barras).ToList();
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

        //Validar que no hayan campos repetidos en la variable de session:
        [HttpPost]
        public JsonResult ProductosRepetidos(string data_producto)
        {
            var Datos = "";
            if (Session["tbBodegaDetalle"] == null)
            {

            }
            else
            {
                var menu = Session["tbBodegaDetalle"] as List<tbBodegaDetalle>;

                foreach (var t in menu)
                {
                    if (t.prod_Codigo == data_producto)
                        Datos = data_producto;
                }


            }

            return Json(Datos);
        }

        [HttpPost]
        public JsonResult GetMunicipios(string CodDepartamento)
        {
            var list = db.spGetMunicipios(CodDepartamento).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDepartamento(string CodMunicipio)
        {
            var list = db.spGetDepartamento(CodMunicipio).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private void AllLists()
        {
            ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();
            ViewBag.Depto = db.tbDepartamento.ToList();
            ViewBag.Muni = db.tbMunicipio.ToList();
            var _departamentos = db.tbDepartamento.Select(s => new
            {
                dep_Codigo = s.dep_Codigo,
                dep_Nombre = string.Concat(s.dep_Codigo + " - " + s.dep_Nombre)
            }).ToList();

            var _Municipios = db.tbMunicipio.Select(s => new
            {
                mun_Codigo = s.mun_Codigo,
                mun_Nombre = string.Concat(s.mun_Codigo + " - " + s.mun_Nombre)
            }).ToList();

            var _EncargadoBodega = db.tbEmpleado.Select(s => new
            {
                emp_Id = s.emp_Id,
                emp_Nombres = s.emp_Nombres,
                emp_Apellidos = string.Concat(s.emp_Nombres + " " + s.emp_Apellidos)
            }).ToList();

            var _Empleado = db.tbBodega.Select(s=> new {
                bod_ResponsableBodega = s.bod_ResponsableBodega}).ToList();

            var NotInRecord = _EncargadoBodega.Where(p => !_Empleado.Any(p2 => p2.bod_ResponsableBodega == p.emp_Id)).ToList();

            ViewBag.DepartamentoList = new SelectList(_departamentos, "dep_Codigo", "dep_Nombre", "Seleccione");
            ViewBag.MunicipioList = new SelectList(_Municipios, "mun_Codigo", "mun_Nombre", "Seleccione");
            ViewBag.ResponsableBodegaList = new SelectList(NotInRecord, "emp_Id", "emp_Apellidos");
        }

        private void ResponsableBodega(int ID)
        {
            var _EncargadoBodega = db.tbEmpleado.Select(s => new
            {
                emp_Id = s.emp_Id,
                emp_Nombres = s.emp_Nombres,
                emp_Apellidos = string.Concat(s.emp_Nombres + " " + s.emp_Apellidos)
            }).ToList();

            var _Empleado = db.tbBodega.Select(s => new {
                bod_ResponsableBodega = s.bod_ResponsableBodega
            }).ToList();

            var NotInRecord = _EncargadoBodega.Where(p => !_Empleado.Any(p2 => p2.bod_ResponsableBodega == p.emp_Id)).ToList();

            var Actual = db.tbEmpleado.Select(s => new
            {
                emp_Id = s.emp_Id,
                emp_Nombres = s.emp_Nombres,
                emp_Apellidos = string.Concat(s.emp_Nombres + " " + s.emp_Apellidos)
            }).Where(s => s.emp_Id == ID).First();

            NotInRecord.Add(Actual);

            ViewBag.ResponsableBodegaList = new SelectList(NotInRecord, "emp_Id", "emp_Apellidos", ID);
        }
        [HttpPost]
        public JsonResult removeBodegaDetalle(tbBodegaDetalle BorrarItem)
        {
            var list = (List<tbBodegaDetalle>)Session["tbBodegaDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.bodd_Id == BorrarItem.bodd_Id);
                list.Remove(itemToRemove);
                Session["tbBodegaDetalle"] = list;
            }
            //return Json(list, JsonRequestBehavior.AllowGet);
            return Json(list, JsonRequestBehavior.AllowGet);

        }
    }
}
