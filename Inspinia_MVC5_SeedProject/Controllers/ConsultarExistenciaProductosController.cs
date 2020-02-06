﻿using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class ConsultarExistenciaProductosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        // GET: ConsultarExistenciaProductos
        [SessionManager("ConsultarExistenciaProductos/Index")]
        public ActionResult Index()
        {
            return View(db.UDV_Inv_Consultar_Existencias_Productos);
        }

        [HttpPost]
        public JsonResult InsertPedido(int IDBodega, string IDProducto, decimal CantidadSolicitada, int BodegaDestino, decimal CantidadDisponible)
        {
            string MensajeError = "0";
            string MensajeErrorDetalle = "0";
            IEnumerable<object> listSalida = null;
            IEnumerable<object> listSalidaDetalle = null;

            try
            {
                using (TransactionScope Tran = new TransactionScope())
                {
                    var FechaPedido = Function.DatetimeNow();
                    listSalida = db.UDP_Inv_ValidacionCantidadExistente(CantidadSolicitada, IDBodega, IDProducto, FechaPedido, BodegaDestino, CantidadDisponible, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Inv_ValidacionCantidadExistente_Result Salida in listSalida)
                        MensajeError = Salida.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo realizar el Pedido, Por favor contacte con el Administrador");
                    }
                    if (MensajeError == "-2")
                    {
                        ModelState.AddModelError("", "No se pudo realizar el Pedido, Por favor contacte con el Administrador");
                    }
                    else
                    {
                        var IDSalida = Convert.ToInt32(MensajeError);
                        listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Insert(IDSalida, IDProducto, CantidadSolicitada, null, Function.GetUser(), Function.DatetimeNow());

                        foreach (UDP_Inv_tbSalidaDetalle_Insert_Result spDetalle in listSalidaDetalle)
                        {
                            MensajeErrorDetalle = spDetalle.MensajeError;
                            if (MensajeError == "-1")
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                            }
                            else if (MensajeError != "-1")
                            {
                                TempData["smserror"] = "El Pedido ha sido enviado correctamente. Codigo de referencia de la salidad Generada es: " + IDSalida;
                                ViewBag.smserror = TempData["smserror"];
                                //return RedirectToAction("Index");
                            }
                        }

                    }
                    Tran.Complete();
                }
            }
            catch (Exception Ex)
            {
                MensajeError = "-1";
                ModelState.AddModelError("hola", "No puede pedir esa cantidad" + Ex.Message.ToString());
            }
            return Json(MensajeError, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ValidacionPorBodega(int IDBodega, string IDProducto)
        {
            var MensajeError = "";
            IEnumerable<object> ListSolicitarProducto = null;
            //if (ModelState.IsValid)
            //{
            try
            {
                    var FechaPedido = Function.DatetimeNow();
                ListSolicitarProducto = db.UDP_Inv_SolicitarProducto_ValidacionPorBodega(IDBodega, IDProducto);
                    foreach (UDP_Inv_SolicitarProducto_ValidacionPorBodega_Result SolicitarProducto in ListSolicitarProducto)
                        MensajeError = SolicitarProducto.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo realizar el Pedido, Por favor contacte con el Administrador");
                    }
            }
            catch (Exception Ex)
            {
                MensajeError = "-1";
                ModelState.AddModelError("", "No puede pedir esa cantidad" + Ex.Message.ToString());
            }
            return Json(MensajeError, JsonRequestBehavior.AllowGet);
        }
    }

}