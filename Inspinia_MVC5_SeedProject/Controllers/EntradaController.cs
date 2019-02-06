using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Transactions;

namespace ERP_GMEDINA.Controllers
{
    public class EntradaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Entrada/
        public ActionResult Index()
        {
            
            var tbentrada = db.tbEntrada.Include(t => t.tbBodega).Include(t => t.tbEstadoMovimiento).Include(t => t.tbProveedor).Include(t => t.tbTipoEntrada);
            return View(tbentrada.ToList());
        }

        // GET: /Entrada/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = await db.tbEntrada.FindAsync(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbEntrada.ent_UsuarioCrea).usu_NombreUsuario;
            var UsuarioModfica = tbEntrada.ent_UsuarioModifica;
            if (UsuarioModfica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModfica).usu_NombreUsuario;
            };
            if (tbEntrada == null)
            {
                return HttpNotFound();
            }
            //vista parcial de entrada detalle
            ViewBag.ent_Id = new SelectList(db.tbEntrada, "ent_Id", "ent_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            return View(tbEntrada);
        }

        // GET: /Entrada/Create
        [HttpPost]
        public JsonResult GetRTNProveedor(int codigoProveedor)
        {
            try { ViewBag.smserror = TempData["smserror"].ToString(); } catch { }
            var list = db.SPGetRTNproveedor(codigoProveedor).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            try { ViewBag.smserror = TempData["smserror"].ToString(); } catch { }
            string UserName = "";
            int idUser = 0;
            GeneralFunctions Login = new GeneralFunctions();
            List<tbUsuario> User = Login.getUserInformation();
            tbBodega tbBod = new tbBodega();
            foreach (tbUsuario Usuario in User)
            {
                UserName = Usuario.usu_Nombres + " " + Usuario.usu_Apellidos;
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }

            ViewBag.bod_Id = new SelectList(db.tbBodega.Where(x => x.bod_ResponsableBodega == idUser).ToList(), "bod_Id", "bod_Nombre");

            //ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion");
            ViewBag.ent_RazonDevolucion = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion");

            //vista parcial de entrada detalle
            ViewBag.ent_Id = new SelectList(db.tbEntrada, "ent_Id", "ent_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.bod_Idd = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();
            Session["CrearDetalleEntrada"] =null;
            return View();
        }
        
        // GET: /Entrada/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            
            if (tbEntrada == null)
            {
                return HttpNotFound();
            }
            //*****PuntoEmisionDetalle
            string cas = "uni_IdList";
            System.Web.HttpContext.Current.Items[cas] = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            

            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.bod_Id);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbEntrada.tbEstadoMovimiento.estm_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.tbProveedor.prov_Id);
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tbTipoEntrada.tent_Id);
            ViewBag.tdev_Id = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion", tbEntrada.ent_RazonDevolucion);
            ViewBag.bbod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.ent_BodegaDestino);

            //vista parcial de entrada detalle
            ViewBag.ent_Id = new SelectList(db.tbEntrada, "ent_Id", "ent_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.bod_Idd = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");

            ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();
            Session["CrearDetalleEntrada"] =null;
            return View(tbEntrada);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //para añadir datos temporales a la tabla
        [HttpPost]
        public JsonResult Guardardetalleentrada(tbEntradaDetalle EntradaDetalle)
        {
            List<tbEntradaDetalle> sessionentradadetalle = new List<tbEntradaDetalle>();
            var list = (List<tbEntradaDetalle>)Session["CrearDetalleEntrada"];
            if (list == null)
            {
                sessionentradadetalle.Add(EntradaDetalle);
                Session["CrearDetalleEntrada"] = sessionentradadetalle;
            }
            else
            {
                list.Add(EntradaDetalle);
                Session["CrearDetalleEntrada"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

            //para actualizar detalle de la entrada del modal
        [HttpPost]
        public ActionResult GetDetalleEntrada(int? entd_Id)
        {
            var list = db.SDP_tbentradadetalle_Select(entd_Id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //para q actualize la tabla
        [HttpPost]
        public JsonResult UpdateEntradaDetalle(tbEntradaDetalle Editardetalle)
        {
            string Msj = "";
            var maestro = Editardetalle.ent_Id;
            try
            {
                IEnumerable<object> list = null;

                list = db.UDP_Inv_tbEntradaDetalle_Update(Editardetalle.entd_Id
                                                            , Editardetalle.ent_Id
                                                           , Editardetalle.prod_Codigo
                                                           , Editardetalle.entd_Cantidad
                                    );
                 foreach (UDP_Inv_tbEntradaDetalle_Update_Result detalle in list)
                    Msj = detalle.MensajeError;

                if (Msj.StartsWith("-1"))
                {
                    Msj = "-1";
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Guardo el registro");
                Msj = "-1";
            }
            //return Json(Msj, JsonRequestBehavior.AllowGet);
            return Json("Edit/" + maestro);

        }
        //para borrar registros en la tabla temporal
        [HttpPost]
        public JsonResult Eliminardetalleentrada(tbEntradaDetalle eliminardetalle)
        {
            var list = (List<tbEntradaDetalle>)Session["_CrearDetalleEntrada"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.ent_Id == eliminardetalle.ent_Id);
                list.Remove(itemToRemove);
                Session["_CrearDetalleEntrada"] = list;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        // POST: /Entrada/Create
        //Para inserte en la master y la detalle

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ent_FechaElaboracion,bod_Id,estm_Id,prov_Id,ent_FacturaCompra,ent_FechaCompra,fact_Id,ent_RazonDevolucion,ent_BodegaDestino,tent_Id")] tbEntrada tbEntrada)
        {
            //var tipoEntrada = tbEntrada.tent_Id;
            //if (tipoEntrada == 1)
            //{
            //    Campos de Traslado
            //    if (tbEntrada.ent_BodegaDestino != null)
            //    {
            //        tbEntrada.ent_BodegaDestino = 9999;
            //    }

            //    campos de devolucion
            //    if (tbEntrada.ent_RazonDevolucion == null)
            //    {
            //        tbEntrada.ent_RazonDevolucion = "##";
            //    }
            //    if (tbEntrada.fact_Id == null)
            //    {
            //        tbEntrada.fact_Id = 9999;
            //    }
            //}
            //if (tipoEntrada == 2)
            //{
            //    campos de Compra
            //    if (tbEntrada.fact_Id == null)
            //    {
            //        tbEntrada.fact_Id = 9999;
            //    }
            //    if (tbEntrada.ent_FacturaCompra == null)
            //    {
            //        tbEntrada.ent_FacturaCompra = "99";
            //    }
            //    if (tbEntrada.ent_FechaCompra == null)
            //    {
            //        tbEntrada.ent_FechaCompra = Convert.ToDateTime("####-##-##");
            //    }
            //    campos de Traslado
            //    if (tbEntrada.ent_BodegaDestino != null)
            //    {
            //        tbEntrada.ent_BodegaDestino = 9999;
            //    }
            //}
            //if (tipoEntrada == 3)
            //{
            //    campos de Compra
            //    if (tbEntrada.fact_Id == null)
            //    {
            //        tbEntrada.fact_Id = 9999;
            //    }
            //    if (tbEntrada.ent_FacturaCompra == null)
            //    {
            //        tbEntrada.ent_FacturaCompra = "####";
            //    }
            //    if (tbEntrada.ent_FechaCompra == null)
            //    {
            //        tbEntrada.ent_FechaCompra = Convert.ToDateTime("####-##-##");
            //    }
            //    campos de devolucion
            //    if (tbEntrada.ent_RazonDevolucion == null)
            //    {
            //        tbEntrada.ent_RazonDevolucion = "##";
            //    }
            //    if (tbEntrada.fact_Id == null)
            //    {
            //        tbEntrada.fact_Id = 9999;
            //    }
            //}
            IEnumerable<object> ENTRADA = null;
            IEnumerable<object> DETALLE = null;
            tbEntrada.estm_Id = 2;
            var idMaster = 0;
            var MensajeError = "";
            var MsjError = "";
            var listaDetalle = (List<tbEntradaDetalle>)Session["CrearDetalleEntrada"];

            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.bod_Id);
            ViewBag.tdev_Id = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion", tbEntrada.ent_RazonDevolucion);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.prov_Id);
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            ViewBag.ent_BodegaDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.ent_BodegaDestino);
            ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();


            if (ModelState.IsValid)
            {
                if (listaDetalle == null)
                {
                    TempData["smserror"] = " No Puede Ingresar Una Entrada Sin Detalle.";
                    ViewBag.smserror = TempData["smserror"];
                    return RedirectToAction("Create");
                }
                else
                {
                    using (TransactionScope _Tran = new TransactionScope())
                    {
                        try
                        {

                            ENTRADA = db.UDP_Inv_tbEntrada_Insert(
                                                                tbEntrada.ent_FechaElaboracion,
                                                                tbEntrada.bod_Id,
                                                                tbEntrada.estm_Id,
                                                                tbEntrada.prov_Id,
                                                                tbEntrada.ent_FacturaCompra,
                                                                tbEntrada.ent_FechaCompra,
                                                                tbEntrada.fact_Id,
                                                                tbEntrada.ent_RazonDevolucion,
                                                                tbEntrada.ent_BodegaDestino,
                                                                tbEntrada.tent_Id);
                            foreach (UDP_Inv_tbEntrada_Insert_Result Entrada in ENTRADA)
                                idMaster = Convert.ToInt32(Entrada.MensajeError);

                            if (MsjError == "-")
                            {
                                ModelState.AddModelError("", "No se guardo el registro");
                                return View(tbEntrada);
                            }
                            else
                            {
                                if (listaDetalle != null)
                                {
                                    if (listaDetalle.Count > 0)
                                    {
                                        foreach (tbEntradaDetalle entd in listaDetalle)
                                        {
                                            DETALLE = db.UDP_Inv_tbEntradaDetalle_Insert(idMaster
                                                                                        , entd.prod_Codigo
                                                                                        , entd.entd_Cantidad);
                                            foreach (UDP_Inv_tbEntradaDetalle_Insert_Result B_detalle in DETALLE)

                                            //if (MensajeError == "-1")
                                            {
                                                ModelState.AddModelError("", "No se Guardo el Registro");
                                                //return View(tbEntrada);
                                                //}
                                                //else
                                                //{
                                                //    _Tran.Complete();
                                                //    return RedirectToAction("Index");
                                            }
                                        }
                                    }
                                }
                                {
                                    _Tran.Complete();
                                    //return RedirectToAction("Index");
                                }

                            }

                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                            //ModelState.AddModelError("", "No se Guardo el Registro");
                            //return View(tbBodega);
                            MsjError = "-1";
                        }
                    }
                }
                
                return RedirectToAction("Index");
            }
            
            return View(tbEntrada);
        }

        // POST: /Entrada/Edit/5
        //Para q edite la master y el detalle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "ent_Id,ent_NumeroFormato,ent_FechaElaboracion,bod_Id,prov_Id,ent_FacturaCompra,ent_FechaCompra,fact_Id,ent_RazonDevolucion,ent_BodegaDestino,tent_Id,ent_usuarioCrea,ent_FechaCrea,ent_UsuarioModifica,ent_FechaModifica")] tbEntrada tbEntrada)
        {
            
            IEnumerable<object> ENTRADA = null;
            IEnumerable<object> DETALLE = null;
            var idMaster = 0;
            var MensajeError = "";
            var MsjError = "";
            var listaDetalle = (List<tbEntradaDetalle>)Session["CrearDetalleEntrada"];

            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.bod_Id);
            ViewBag.ent_RazonDevolucion = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion", tbEntrada.ent_RazonDevolucion);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.prov_Id);
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            ViewBag.ent_BodegaDestino = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.ent_BodegaDestino);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();

            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {

                        ENTRADA = db.UDP_Inv_tbEntrada_Update(tbEntrada.ent_Id,
                                                                        tbEntrada.ent_NumeroFormato,
                                                                        tbEntrada.ent_FechaElaboracion,
                                                                        tbEntrada.bod_Id,
                                                                        tbEntrada.prov_Id,
                                                                        tbEntrada.ent_FacturaCompra,
                                                                        tbEntrada.ent_FechaCompra,
                                                                        tbEntrada.fact_Id, 
                                                                        tbEntrada.ent_RazonDevolucion,
                                                                        tbEntrada.ent_BodegaDestino,
                                                                        tbEntrada.tent_Id,
                                                                        tbEntrada.ent_UsuarioCrea,
                                                                        tbEntrada.ent_FechaCrea);
                        foreach (UDP_Inv_tbEntrada_Update_Result Entrada in ENTRADA)
                            idMaster = Convert.ToInt32(Entrada.MensajeError);

                        if (MsjError == "-")
                        {
                            ModelState.AddModelError("", "No se guardo el registro");
                            return View(tbEntrada);
                        }
                        else
                        {
                            if (listaDetalle != null)
                            {
                                if (listaDetalle.Count > 0)
                                {
                                    foreach (tbEntradaDetalle entd in listaDetalle)
                                    {
                                        entd.entd_UsuarioCrea = 1;
                                        entd.entd_FechaCrea = DateTime.Now;

                                        DETALLE = db.UDP_Inv_tbEntradaDetalle_Insert(idMaster
                                                                                    ,entd.prod_Codigo
                                                                                    ,entd.entd_Cantidad);
                                        foreach (UDP_Inv_tbEntradaDetalle_Insert_Result B_detalle in DETALLE)

                                        //if (MensajeError == "-1")
                                        {
                                            ModelState.AddModelError("", "No se Guardo el Registro");
                                            //return View(tbEntrada);
                                            //}
                                            //else
                                            //{
                                            //    _Tran.Complete();
                                            //    return RedirectToAction("Index");
                                        }
                                    }
                                }
                            }
                            {
                                _Tran.Complete();
                                //return RedirectToAction("Index");
                            }

                        }

                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        //ModelState.AddModelError("", "No se Guardo el Registro");
                        //return View(tbBodega);
                        MsjError = "-1";
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbEntrada.bod_Id);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbEntrada.estm_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.prov_Id);
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            ViewBag.ent_BodegaDestino = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbEntrada.ent_BodegaDestino);
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.ent_RazonDevolucion = new SelectList(db.tbTipoDevolucion, "tdev_Id", "tdev_Descripcion", tbEntrada.ent_RazonDevolucion);
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.SDP_Inv_tbProducto_Select().ToList();
            return View(tbEntrada);
        }






        public ActionResult EstadoInactivar(int? id)
        {

            try
            {
                tbEntrada obj = db.tbEntrada.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbEntrada_Update_Estado(id, Helpers.EntradaInactivada);
                foreach (UDP_Inv_tbEntrada_Update_Estado_Result obje in list)
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
                tbEntrada obj = db.tbEntrada.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbEntrada_Update_Estado(id, Helpers.EntradaEmitida);
                foreach (UDP_Inv_tbEntrada_Update_Estado_Result obje in list)
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





        //para que Anular una entrada
        public ActionResult EstadoAnular(tbEntrada cambiaAnular)
        {
            //var maestro = cambiaAnular.ent_Id;
            tbEntrada obj = db.tbEntrada.Find(cambiaAnular.ent_Id);
            try
            {

                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbEntrada_Update_Anular(cambiaAnular.ent_Id, Helpers.EntradaAnulada, cambiaAnular.entd_RazonAnulada);
                foreach (UDP_Inv_tbEntrada_Update_Anular_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    //return RedirectToAction("Edit/" + cambiaAnular.ent_Id);
                }
                else
                {
                    return RedirectToAction("Edit/" + obj);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + obj);
            }
            return RedirectToAction("Edit/" + obj);
            //return RedirectToAction("Index");
        }
        //para Aplicar una entrada
        public ActionResult EstadoAplicar(int? id)
        {

            try
            {
                tbEntrada obj = db.tbEntrada.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbEntrada_Update_Estado(id, Helpers.EntradaAplicada);
                foreach (UDP_Inv_tbEntrada_Update_Estado_Result obje in list)
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

        }



    }
}
