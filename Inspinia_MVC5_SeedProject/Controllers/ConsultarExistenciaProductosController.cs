using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;


namespace ERP_GMEDINA.Controllers
{
    public class ConsultarExistenciaProductosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        // GET: ConsultarExistenciaProductos
        public ActionResult Index()
        {

            return View(/*db.UDV_Inv_Consultar_Existencias_Productos*/);
        }
        //[HttpPost]
        //public ActionResult InsertPedido1(int IDBodega, string IDProducto, decimal CantidadSolicitada)
        //{
        //    var MensajeError = "0";
        //    var MensajeErrorDetalle = "0";
        //    IEnumerable<object> listSalida = null;
        //    IEnumerable<object> listSalidaDetalle = null;
        //    //if (ModelState.IsValid)
        //    //{
        //    try
        //    {
        //        using (TransactionScope Tran = new TransactionScope())
        //        {
        //            listSalida = db.UDP_Inv_ValidacionCantidadExistente(CantidadSolicitada, IDBodega, IDProducto);
        //            foreach (UDP_Inv_ValidacionCantidadExistente_Result Salida in listSalida)
        //                MensajeError = Salida.MensajeError;
        //            if (MensajeError == "-1")
        //            {
        //                ModelState.AddModelError("", "No se pudo agregar el registro");
        //                return View(db.UDV_Inv_Consultar_Existencias_Productos);
        //            }
        //            else
        //            {
        //                var IDSalida = Convert.ToInt32(MensajeError);
        //                db.UDP_Inv_tbSalidaDetalle_Insert(IDSalida, IDProducto, CantidadSolicitada, "*");

        //                foreach (UDP_Inv_tbSalida_Insert_Result spDetalle in listSalidaDetalle)
        //                {
        //                    MensajeErrorDetalle = spDetalle.MensajeError;
        //                    if (MensajeError == "-1")
        //                    {
        //                        ModelState.AddModelError("", "No se pudo agregar el registro detalle");
        //                        return View(db.UDV_Inv_Consultar_Existencias_Productos);
        //                    }

        //                    else
        //                    {
        //                        ModelState.AddModelError("", "No se pudo agregar el registro");
        //                        return View(db.UDV_Inv_Consultar_Existencias_Productos);
        //                    }
        //                }

        //            }
        //            Tran.Complete();
        //        }
        //            return RedirectToAction("Index");
        //        }
        //    catch (Exception Ex)
        //    {
        //        ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
        //    }
        //    return View(db.UDV_Inv_Consultar_Existencias_Productos);
        //}

        [HttpPost]
        public JsonResult InsertPedido(int IDBodega, int bodd_Id, decimal CantidadSolicitada)
        {
            var MensajeError = "0";
            var MensajeErrorDetalle = "0";
            IEnumerable<object> listSalida = null;
            IEnumerable<object> listSalidaDetalle = null;
            //if (ModelState.IsValid)
            //{
            try
            {
                using (TransactionScope Tran = new TransactionScope())
                {
                    var FechaPedido = DateTime.Now;
                    var IDProducto = db.tbBodegaDetalle.Find(bodd_Id).prod_Codigo;
                    listSalida = db.UDP_Inv_ValidacionCantidadExistente(CantidadSolicitada, IDBodega, IDProducto, FechaPedido);
                    foreach (UDP_Inv_ValidacionCantidadExistente_Result Salida in listSalida)
                        MensajeError = Salida.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo realizar el Pedido, Por favor contacte con el Administrador");
                    }
                    else
                    {
                        var IDSalida = Convert.ToInt32(MensajeError);
                        var IDObjeto = "0";
                        listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Insert(IDSalida, bodd_Id, CantidadSolicitada, IDObjeto);

                        foreach (UDP_Inv_tbSalidaDetalle_Insert_Result spDetalle in listSalidaDetalle)
                        {
                            MensajeErrorDetalle = spDetalle.MensajeError;
                            if (MensajeError == "-1")
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                            }
                        }

                    }
                    Tran.Complete();
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("hola", "No puede pedir esa cantidad" + Ex.Message.ToString());
            }
            return Json(MensajeError, JsonRequestBehavior.AllowGet);
        }
    }

}