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
using ERP_GMEDINA.Dataset;
using System.IO;
using ERP_GMEDINA.Reports;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;

namespace ERP_GMEDINA.Controllers
{
    public class PedidoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        public ActionResult Index()
        {
            var tbpedido = db.tbPedido.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCliente).Include(t => t.tbEstadoPedido).Include(t => t.tbSucursal);

            return View(tbpedido.ToList());
        }

        public ActionResult IndexPedido()
        {

            return View(db.tbPedido.ToList());
        }


        public int Usuario()
        {
            int idUser = 0;
            try
            {
                List<tbUsuario> User = Function.getUserInformation();
                foreach (tbUsuario Usuario in User)
                {
                    idUser = Convert.ToInt32(Usuario.suc_Id);
                }
                return idUser;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return 0;
            }
        }

        // GET: /Pedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbPedido tbPedido = db.tbPedido.Find(id);
            if (tbPedido == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbPedido);
        }


        public ActionResult _IndexCliente()
        {
            return PartialView();
        }

        public ActionResult _IndexProducto()
        {
            return PartialView();
        }


        public ActionResult Facturar(tbPedido Pedido)
        {
            Session["IDCLIENTE"] = Session["ID"];
            Session["IDENTIFICACION"] = Session["IDENT"];
            Session["NOMBRES"] = Session["NOM"];
            Session["PEDIDO"] = Session["PEDID"];
            Session["DETALLE"] = Session["DETALLES"];
            //Session["PRODUCTO"] = Session["PRO"];
            //Session["CANTIDAD"] = Session["CANT"];
            //Session["DESCRIPCION"] = Session["DESC"];
            //Session["PRECIOUNI"] = Session["PRECIO"];
            //Session["IMPUESTO"] = Session["IMP"];
            return RedirectToAction("Create", "Factura");
        }


        // GET: /Pedido/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Pedido/Create"))
                    {
                        //ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                        //ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                        //ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte");
                        //ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo");
                        //ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");


                        ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion");
     
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo");
            ViewBag.Cliente = db.tbCliente.ToList();
            Session["tbPedidoDetalle"] = null;
            ViewBag.Producto = db.tbProducto.ToList();
            tbPedido Pedido = new tbPedido();
            Pedido.esped_Id = Helpers.Pendiente;
            Pedido.suc_Id = Usuario();
                        tbSucursal tbSucursal = new tbSucursal();
                        int suc = Usuario();
                        ViewBag.Sucursal = db.tbSucursal.Find(suc).suc_Descripcion;

            return View(Pedido);
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");

        }

        // POST: /Pedido/Create
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "esped_Id,ped_FechaElaboracion,ped_FechaEntrega,clte_Id,suc_Id,fact_Id,ped_EsAnulado,ped_RazonAnulado,tbUsuario,tbUsuario1")] tbPedido tbPedido)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Pedido/Create"))
                    {

                        ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
            ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion");
            ViewBag.Producto = db.tbProducto.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();
            var list = (List<tbPedidoDetalle>)Session["tbPedidoDetalle"];
            string MensajeError = "";
            var MensajeErrorDetalle = "";
            IEnumerable<object> listPedido = null;
            IEnumerable<object> listPedidoDetalles = null;
            if (ModelState.IsValid)
            {
                try
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        listPedido = db.UDP_Vent_tbPedido_Insert(
                                                tbPedido.esped_Id,
                                                tbPedido.ped_FechaElaboracion,
                                                tbPedido.ped_FechaEntrega,
                                                tbPedido.clte_Id,
                                                tbPedido.suc_Id,
                                                tbPedido.fact_Id,
                                                Function.GetUser(),
                                                Function.DatetimeNow(),
                                                tbPedido.ped_EsAnulado,
                                                tbPedido.ped_RazonAnulado);
                        foreach (UDP_Vent_tbPedido_Insert_Result Pedido in listPedido)
                            MensajeError = Pedido.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbPedido);
                        }
                        else
                        {
                            if (MensajeError != "-1")
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbPedidoDetalle PedDetalle in list)
                                        {
                                            var pedds_Id = Convert.ToInt32(MensajeError);
                                            PedDetalle.ped_Id = pedds_Id;

                                            PedDetalle.ped_Id = pedds_Id;
                                            listPedidoDetalles = db.UDP_Vent_tbPedidoDetalle_Insert(PedDetalle.ped_Id,
                                                PedDetalle.prod_Codigo,
                                                PedDetalle.pedd_Cantidad,
                                                PedDetalle.pedd_CantidadFacturada, 
                                                Function.GetUser(),
                                                Function.DatetimeNow());
                                            foreach (UDP_Vent_tbPedidoDetalle_Insert_Result SPpedidodetalle in listPedidoDetalles)
                                            {
                                                MensajeErrorDetalle = SPpedidodetalle.MensajeError;
                                                if (MensajeError == "-1")
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbPedido);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbPedido);
                            }

                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
                    ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioModifica);
                    ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbPedido.clte_Id);
                    ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPedido.fact_Id);
                    ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbPedido.suc_Id);
                    ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion");
                    ViewBag.Producto = db.tbProducto.ToList();
                    ViewBag.Cliente = db.tbCliente.ToList();
                    ViewBag.ListaPrecio = db.tbListaPrecio.ToList();
                }

            }
            ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
    
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbPedido.clte_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPedido.fact_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbPedido.suc_Id);
            ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion");
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.Producto = db.tbProducto.ToList();
            ViewBag.ListaPrecio = db.tbListaPrecio.ToList();
                        return View(tbPedido);
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");

        }























        [HttpPost]
        public JsonResult SavePedidoDetalles(tbPedidoDetalle PedidoDetalle, string data_producto)
        {
            List<tbPedidoDetalle> sessionPedidoDetalle = new List<tbPedidoDetalle>();
            var list = (List<tbPedidoDetalle>)Session["tbPedidoDetalle"];
            var datos = "";
            decimal cantvieja = 0;
            decimal cantnueva = 0;
            decimal data_cantidad = PedidoDetalle.pedd_Cantidad;


            if (list == null)
            {
                sessionPedidoDetalle.Add(PedidoDetalle);
                Session["tbPedidoDetalle"] = sessionPedidoDetalle;
            }
            else
            {
                foreach (var t in list)
                    if (t.prod_Codigo == data_producto)
                    {

                        datos = data_producto;
                        foreach (var viejo in list)
                            if (viejo.prod_Codigo == PedidoDetalle.prod_Codigo)
                                cantvieja = viejo.pedd_Cantidad;
                        cantnueva = cantvieja + data_cantidad;
                        t.pedd_Cantidad = cantnueva;
                        return Json(datos, JsonRequestBehavior.AllowGet);
                    }
                list.Add(PedidoDetalle);
                Session["tbPedidoDetalle"] = list;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }

            return Json("datos", JsonRequestBehavior.AllowGet);
        }
         
        [HttpPost]
        public JsonResult QuitarPedidoDetalle(tbPedidoDetalle PedidoDetalle)
        {
            var list = (List<tbPedidoDetalle>)Session["tbPedidoDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.prod_Codigo == PedidoDetalle.prod_Codigo);
                list.Remove(itemToRemove);
                Session["tbPedidoDetalle"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }


        
        [HttpPost]
        public JsonResult getPedidoDetalle  (int pedd_Id)
        {
            IEnumerable<object> list = null;
            try
            {
                list = db.SDP_tbPedidoDetalle_Select(pedd_Id).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: /Pedido/Edit/5
        public ActionResult EditPedido(int? id)

        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbPedido tbPedido = db.tbPedido.Find(id);
            if (tbPedido == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
            ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioModifica);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbPedido.clte_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPedido.fact_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbPedido.suc_Id);
            ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion", tbPedido.esped_Id);
            ViewBag.Producto = db.tbProducto.ToList();
            tbPedido Pedido = new tbPedido();
            ViewBag.Cliente = db.tbCliente.ToList();
            tbPedido.esped_Id = Helpers.Pendiente;

            Session["ID"] = Convert.ToString(tbPedido.tbCliente.clte_Id) ;
            Session["PEDID"] = tbPedido.ped_Id;
            Session["IDENT"] = tbPedido.tbCliente.clte_Identificacion;

            if (tbPedido.tbCliente.clte_EsPersonaNatural)
            {
                Session["NOM"] = tbPedido.tbCliente.clte_Nombres + " " + tbPedido.tbCliente.clte_Apellidos;
            }
            else
            {
                Session["NOM"] = tbPedido.tbCliente.clte_NombreComercial;
            }
            Session["DETALLES"]=tbPedido.ped_Id;

            return View(tbPedido);

        }
        public ActionResult Edit(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Pedido/Edit"))
                    {
                        if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbPedido tbPedido = db.tbPedido.Find(id);
            if (tbPedido == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            Session["tbPedidoDetalle"] = null;
            ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
            ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioModifica);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbPedido.clte_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPedido.fact_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbPedido.suc_Id);
            ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion", tbPedido.esped_Id);
            ViewBag.ped_idd = id;
            ViewBag.Producto = db.tbProducto.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();

                        return View(tbPedido);
                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");

        }

        // POST: /Pedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "ped_Id,esped_Id,ped_FechaElaboracion,ped_FechaEntrega,clte_Id,suc_Id,fact_Id,ped_EsAnulado,ped_RazonAnulado,ped_UsuarioCrea,ped_FechaCrea")] tbPedido tbPedido)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("Pedido/Edit"))
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {

                                ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
                                ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioModifica);
                                ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbPedido.clte_Id);
                                ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPedido.fact_Id);
                                ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbPedido.suc_Id);
                                ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion", tbPedido.esped_Id);

                                ViewBag.Producto = db.tbProducto.ToList();
                                ViewBag.Cliente = db.tbCliente.ToList();
                                var MensajeErrorDetalle = "";
                                var MensajeError = "";
                                IEnumerable<object> list = null;
                                IEnumerable<object> listEdit = null;
                                var listPedidoDetalle = (List<tbPedidoDetalle>)Session["tbPedidoDetalle"];
                                list = db.UDP_Vent_tbPedido_Update(tbPedido.ped_Id,
                                                                    tbPedido.esped_Id = 1,
                                                                    tbPedido.ped_FechaElaboracion,
                                                                    tbPedido.ped_FechaEntrega,
                                                                    tbPedido.clte_Id,
                                                                    tbPedido.suc_Id,
                                                                    tbPedido.fact_Id,
                                                                    tbPedido.ped_EsAnulado,
                                                                    tbPedido.ped_RazonAnulado,
                                                                    tbPedido.ped_UsuarioCrea,
                                                                    tbPedido.ped_FechaCrea,
                                                                    Function.GetUser(),
                                                                    Function.DatetimeNow());

                                foreach (UDP_Vent_tbPedido_Update_Result PedidoDetalle1 in list)
                                    MensajeError = PedidoDetalle1.MensajeError;
                                if (MensajeError.StartsWith("-1"))
                                {

                                }
                                else
                                {
                                    if (MensajeError != "-1")
                                    {
                                        if (listPedidoDetalle != null)
                                        {
                                            if (listPedidoDetalle.Count != 0)
                                            {
                                                foreach (tbPedidoDetalle Detalle in listPedidoDetalle)
                                                {
                                                    var Exits = db.tbPedidoDetalle.Where(
                                                        x => x.prod_Codigo == Detalle.prod_Codigo
                                                        &&
                                                        x.ped_Id == tbPedido.ped_Id).FirstOrDefault();

                                                    if (Exits != null)
                                                    {
                                                        var Cantidad = db.tbPedidoDetalle.Where(
                                                        x => x.prod_Codigo == Detalle.prod_Codigo
                                                        &&
                                                        x.ped_Id == tbPedido.ped_Id).Select(c => c.pedd_Cantidad).FirstOrDefault();

                                                        tbPedidoDetalle vPedidoDetalle = db.tbPedidoDetalle.Find(tbPedido.ped_Id);

                                                        decimal CantidadNew = Convert.ToDecimal(Exits.pedd_Cantidad) + Convert.ToDecimal(Detalle.pedd_Cantidad);
                                                        listEdit = db.UDP_Vent_tbPedidoDetalle_Update(
                                                                            Exits.pedd_Id,
                                                                            Exits.prod_Codigo,
                                                                            CantidadNew,
                                                                            Detalle.pedd_CantidadFacturada,
                                                                            Exits.pedd_UsuarioCrea,
                                                                            Exits.pedd_FechaCrea,
                                                                            Function.GetUser(),
                                                                            Function.DatetimeNow());
                                                        foreach (UDP_Vent_tbPedidoDetalle_Update_Result RSSalidaDetalle in listEdit)
                                                            MensajeErrorDetalle = RSSalidaDetalle.MensajeError;
                                                        if (MensajeErrorDetalle.StartsWith("-1"))
                                                        {
                                                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                                            return Json(ModelState, JsonRequestBehavior.AllowGet);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        var DetallesD = Convert.ToInt32(MensajeError);
                                                        Detalle.ped_Id = DetallesD;
                                                        listEdit = db.UDP_Vent_tbPedidoDetalle_Insert(
                                                        tbPedido.ped_Id,
                                                        Detalle.prod_Codigo,
                                                        Detalle.pedd_Cantidad,
                                                        0, Function.GetUser(),
                                                        Function.DatetimeNow());
                                                        foreach (UDP_Vent_tbPedidoDetalle_Insert_Result PedidoDetalleD in listEdit)
                                                            MensajeErrorDetalle = PedidoDetalleD.MensajeError;
                                                        if (MensajeErrorDetalle.StartsWith("-1"))
                                                        {
                                                            MensajeErrorDetalle = "No se pudo actualizar el registro, favor contacte al administrador.";
                                                            ModelState.AddModelError("", MensajeErrorDetalle);
                                                        }
                                                    }

                                                }
                                            }
                                        }

                                    }
                                    return RedirectToAction("Index");
                                }
                            }
                            catch (Exception Ex)
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                                ViewBag.listp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
                                ViewBag.listp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioModifica);
                                ViewBag.listp_Id = new SelectList(db.tbListadoPrecioDetalle, "listp_Id", "prod_Codigo", tbPedido.ped_Id);
                                ViewBag.Producto = db.tbProducto.ToList();
                            }

                            return RedirectToAction("Index");
                        }
                        ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
                        ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioModifica);
                        ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbPedido.clte_Id);
                        ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPedido.fact_Id);
                        ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbPedido.suc_Id);
                        ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion", tbPedido.esped_Id);
                        ViewBag.Cliente = db.tbCliente.ToList();
                        ViewBag.Producto = db.tbProducto.ToList();
                        return View(tbPedido);


                    }
                    else
                    {
                        return RedirectToAction("SinAcceso", "Login");
                    }
                }
                else
                    return RedirectToAction("SinRol", "Login");
            }
            else
                return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPedido(int? id, [Bind(Include = "ped_Id,esped_Id,ped_FechaElaboracion,ped_FechaEntrega,clte_Id,suc_Id,fact_Id,ped_EsAnulado,ped_RazonAnulado,ped_UsuarioCrea,ped_FechaCrea")] tbPedido tbPedido)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbPedido vPedido = db.tbPedido.Find(id);
                
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbPedido_Update(tbPedido.ped_Id,
                                                       tbPedido.esped_Id,
                                                       tbPedido.ped_FechaElaboracion,
                                                       tbPedido.ped_FechaEntrega,
                                                       tbPedido.clte_Id,
                                                       tbPedido.suc_Id,
                                                       tbPedido.fact_Id,
                                                       tbPedido.ped_EsAnulado,
                                                       tbPedido.ped_RazonAnulado,
                                                       vPedido.ped_UsuarioCrea,
                                                       vPedido.ped_FechaCrea,
                                                       Function.GetUser(),
                                                        Function.DatetimeNow());
                    foreach (UDP_Vent_tbPedido_Update_Result Pedido in list)
                        MensajeError = Pedido.MensajeError;
                    if (MensajeError == "-1")
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    Ex.Message.ToString();
                }

            }
            ViewBag.ped_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioCrea);
            ViewBag.ped_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPedido.ped_UsuarioModifica);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_RTN_Identidad_Pasaporte", tbPedido.clte_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbPedido.fact_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbPedido.suc_Id);
            ViewBag.esped_Id = new SelectList(db.tbEstadoPedido, "esped_Id", "esped_Descripcion", tbPedido.esped_Id);
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbPedido);
        }
        [HttpPost]
        public JsonResult GuardarPedidoDetalle(tbPedidoDetalle PedidoDetalles)
        {
            string Msj = "";

            try
            {
                string MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Vent_tbPedidoDetalle_Insert(
                           PedidoDetalles.ped_Id,
                           PedidoDetalles.prod_Codigo,
                           PedidoDetalles.pedd_Cantidad,
                           0, Function.GetUser(),
                          Function.DatetimeNow());
                foreach (UDP_Vent_tbPedidoDetalle_Insert_Result PedidoDetalleD in list)
                    MensajeError = PedidoDetalleD.MensajeError;
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
        public ActionResult UpdatePedidoDetalle(tbPedidoDetalle EditPedidoDetalle)
        {
            try
            {
                //var MensajeError = 0;

                string MensajeError = "";
                IEnumerable<object> list = null;
                tbPedidoDetalle PedidoDetails = db.tbPedidoDetalle.Find(EditPedidoDetalle.pedd_Id);
                list = db.UDP_Vent_tbPedidoDetalle_Update(
                            EditPedidoDetalle.pedd_Id,
                            EditPedidoDetalle.prod_Codigo,
                            EditPedidoDetalle.pedd_Cantidad,
                            EditPedidoDetalle.pedd_CantidadFacturada,
                            EditPedidoDetalle.pedd_UsuarioCrea,
                            PedidoDetails.pedd_FechaCrea,
                            EditPedidoDetalle.pedd_UsuarioModifica,
                            EditPedidoDetalle.pedd_FechaModifica);

                foreach (UDP_Vent_tbPedidoDetalle_Update_Result PedidoDetalle in list)
                    MensajeError = PedidoDetalle.MensajeError;
                if (MensajeError == "-1")
                {
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return RedirectToAction("Edit", "Pedido");
                }
                else
                {
                    return RedirectToAction("Edit" , "Pedido");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al programador.");
                return RedirectToAction("Edit", "Pedido");
            }
        }

        [HttpPost]
        public JsonResult GetDetallePedido(int pedido)
        {
            var list = db.sp_GetDetallePedido(pedido).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AnularPedido(int CodPedido, bool NoAnulado, string RazonAnulado)
        {
            var list = db.UDP_Vent_tbPedido_Estado(CodPedido, NoAnulado, RazonAnulado).ToList();
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
        

        public int GetUsuarioMetodo()
        {
            int idUser = 0;
            try
            {
                List<tbUsuario> User = Function.getUserInformation();
                foreach (tbUsuario Usuario in User)
                {
                    idUser = Convert.ToInt32(Usuario.usu_Id);
                }
                return idUser;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return 0;
            }
        }

        [HttpPost]
        public ActionResult Reporte(tbObjeto Objeto/*, int clte_Id, bool clte_EsPersonaNatural*/)
        {

            int clte_Id = 4;
            bool clte_EsPersonaNatural = true;
            int iTipoReporte = Objeto.obj_Id;
            var list = db.SDP_Acce_GetReportes().ToList();
            var GetUsuario = GetUsuarioMetodo();
            var UsuarioName = db.tbUsuario.Where(x => x.usu_Id == GetUsuario).Select(i => new { i.usu_Nombres, i.usu_Apellidos }).FirstOrDefault();
            ReportDocument rd = new ReportDocument();
            Stream stream = null;
            rptPedidoEntreFechas rptPedido = new rptPedidoEntreFechas();
            Reportes ReportePedido = new Reportes();

            var ReportePedidoTableAdapter = new UDV_Vent_Pedido_EntreFechasTableAdapter();

            try
            {
                ReportePedidoTableAdapter.FillFiltros(ReportePedido.UDV_Vent_Pedido_EntreFechas, clte_Id, clte_EsPersonaNatural);

                rptPedido.SetDataSource(ReportePedido);
                rptPedido.SetParameterValue("usuario", UsuarioName.usu_Nombres + " " + UsuarioName.usu_Apellidos);

                stream = rptPedido.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                rptPedido.Close();
                rptPedido.Dispose();


                string fileName = "Reporte_PedidoEntreFechas.pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(stream, "application/pdf");
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                throw;
            }
        }












    }

}
