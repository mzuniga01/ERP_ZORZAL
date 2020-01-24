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
using System.Data.Entity.Core.Objects;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class FacturaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        
        // GET: /Factura/
        [SessionManager("Factura/Index")]
        public ActionResult Index()
        {
            int idUser = Function.GetUser();
            var tbFacturaUser = db.UDV_Vent_Busqueda_Factura;
            if (idUser!=0)
            {
                var Suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.suc_Id).SingleOrDefault();
                return View(tbFacturaUser.Where(a => a.suc_Id == Suc_Id).ToList());
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [SessionManager("Factura/Index")]
        public ActionResult Index(string cliente, string fecha, string caja)
        {
            try
            {
                //var resultado = 0;
                List<UDV_Vent_Busqueda_Factura> list = new List<UDV_Vent_Busqueda_Factura>();
                using (var db = new ERP_ZORZALEntities())
                {
                    using (var oCmd = db.Database.Connection.CreateCommand())
                    {
                        db.Database.Connection.Open();
                        oCmd.CommandText = "Vent.GetBusquedaFactura";
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Add(new SqlParameter("@cliente", cliente));
                        oCmd.Parameters.Add(new SqlParameter("@fecha", fecha));
                        oCmd.Parameters.Add(new SqlParameter("@caja", caja));
                        DbDataReader reader = oCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            UDV_Vent_Busqueda_Factura tbFactura = new UDV_Vent_Busqueda_Factura();
                            if (!reader.IsDBNull(reader.GetOrdinal("fact_Id")))
                                tbFactura.fact_Id = Convert.ToInt16(reader["fact_Id"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_Codigo")))
                                tbFactura.fact_Codigo = Convert.ToString(reader["fact_Codigo"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_Fecha")))
                                tbFactura.fact_Fecha = Convert.ToDateTime(reader["fact_Fecha"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("esfac_Id")))
                                tbFactura.esfac_Id = Convert.ToByte(reader["esfac_Id"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("esfac_Descripcion")))
                                tbFactura.esfac_Descripcion = Convert.ToString(reader["esfac_Descripcion"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("cja_Id")))
                                tbFactura.cja_Id = Convert.ToByte(reader["cja_Id"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("cja_Descripcion")))
                                tbFactura.cja_Descripcion = Convert.ToString(reader["cja_Descripcion"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("suc_Id")))
                                tbFactura.suc_Id = Convert.ToByte(reader["suc_Id"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Id")))
                                tbFactura.clte_Id = Convert.ToByte(reader["clte_Id"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("pemi_NumeroCAI")))
                                tbFactura.pemi_NumeroCAI = Convert.ToString(reader["pemi_NumeroCAI"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_AlCredito")))
                                tbFactura.fact_AlCredito = Convert.ToBoolean(reader["fact_AlCredito"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_DiasCredito")))
                                tbFactura.fact_DiasCredito = Convert.ToByte(reader["fact_DiasCredito"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_PorcentajeDescuento")))
                                tbFactura.fact_PorcentajeDescuento = Convert.ToByte(reader["fact_PorcentajeDescuento"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_Vendedor")))
                                tbFactura.fact_Vendedor = Convert.ToString(reader["fact_Vendedor"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Identificacion")))
                                tbFactura.clte_Identificacion = Convert.ToString(reader["clte_Identificacion"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("clte_Nombres")))
                                tbFactura.clte_Nombres = Convert.ToString(reader["clte_Nombres"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_IdentidadTE")))
                                tbFactura.fact_IdentidadTE = Convert.ToString(reader["fact_IdentidadTE"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_NombresTE")))
                                tbFactura.fact_NombresTE = Convert.ToString(reader["fact_NombresTE"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_FechaNacimientoTE")))
                                tbFactura.fact_FechaNacimientoTE = Convert.ToDateTime(reader["fact_FechaNacimientoTE"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_UsuarioAutoriza")))
                                tbFactura.fact_UsuarioAutoriza = Convert.ToInt32(reader["fact_UsuarioAutoriza"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_FechaAutoriza")))
                                tbFactura.fact_FechaAutoriza = Convert.ToDateTime(reader["fact_FechaAutoriza"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_UsuarioCrea")))
                                tbFactura.fact_UsuarioCrea = Convert.ToInt32(reader["fact_UsuarioCrea"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_FechaCrea")))
                                tbFactura.fact_FechaCrea = Convert.ToDateTime(reader["fact_FechaCrea"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_UsuarioModifica")))
                                tbFactura.fact_UsuarioModifica = Convert.ToInt32(reader["fact_UsuarioModifica"]);

                            if (!reader.IsDBNull(reader.GetOrdinal("fact_FechaModifica")))
                                tbFactura.fact_FechaModifica = Convert.ToDateTime(reader["fact_FechaModifica"]);

                            list.Add(tbFactura);
                        }
                    }
                }
                ViewBag.cliente = cliente;
                ViewBag.fecha = fecha;
                ViewBag.caja = caja;

                int idUser = Function.GetUser();
                var tbFacturaUser = db.UDV_Vent_Busqueda_Factura;
                if (idUser != 0)
                {
                    var Suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.suc_Id).SingleOrDefault();
                    return View(tbFacturaUser.Where(a => a.suc_Id == Suc_Id).ToList());
                }
                else
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Function.InsertBitacoraErrores("Factura/Create", ex.Message, "Create");
                int idUser = Function.GetUser();
                var tbFacturaUser = db.UDV_Vent_Busqueda_Factura;
                if (idUser != 0)
                {
                    var Suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.suc_Id).SingleOrDefault();
                    return View(tbFacturaUser.Where(a => a.suc_Id == Suc_Id).ToList());
                }
                else
                    return RedirectToAction("Index");
            }

        }

        public ActionResult ExportReport(long? id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Factura.rpt"));
            var Factura = db.UDP_Vent_tbFactura_Imprimir(id).ToList();
            var todo = (from r in Factura
                        where r.fact_Id == id
                        select new
                        {
                            fact_Codigo = r.fact_Codigo,
                            fact_Fecha = r.fact_Fecha,
                            suc_Direccion = r.suc_Direccion,
                            mun_Nombre = r.mun_Nombre,
                            dep_Nombre = r.dep_Nombre,
                            suc_Correo = r.suc_Correo,
                            pemi_NumeroCAI = r.pemi_NumeroCAI,
                            clte_Identificacion = r.clte_Identificacion,
                            clte_Nombres = r.clte_Nombres,
                            RangoInicial = r.RangoInicial,
                            RangoFinal = r.RangoFinal,
                            FechaLimite = r.FechaLimite,
                            FormaPago = r.FormaPago,
                            prod_Descripcion = r.prod_Descripcion,
                            factd_Cantidad = r.factd_Cantidad,
                            factd_PrecioUnitario = r.factd_PrecioUnitario,
                            MontoImpuesto = (decimal)r.MontoImpuesto,
                            MontoDescuento = (decimal)r.MontoDescuento,
                            MontoEfectivo = (decimal)r.MontoEfectivo,
                            TotalCambio = (decimal)r.TotalCambio,
                            TotalEfectivo = (decimal)r.TotalEfectivo
                        }).ToList();

            rd.SetDataSource(todo);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                var list = db.UDP_Vent_tbFactura_EstadoImpreso(id, Helpers.EstadoImpreso).ToList();
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf");
            }
            catch(Exception Ex)
            {
                Ex.Message.ToString();
                throw;
            }
        }
        // GET: /Factura/Details/5
        [SessionManager("Factura/Details")]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbFactura);
        }
        // GET: /Factura/Create
        [SessionManager("Factura/Create")]
        public ActionResult Create()
        {
            tbFactura Factura = new tbFactura();
            if (Session["IDCLIENTE"] == null)
            {
                ViewBag.Iden = 1;
                ViewBag.Identificacion = db.tbCliente.Where(x => x.clte_Id == 1).Select(x => x.clte_Identificacion).SingleOrDefault();
                ViewBag.Nombres = db.tbCliente.Where(x => x.clte_Id == 1).Select(x => x.clte_Nombres + " " + x.clte_Apellidos).SingleOrDefault();
                ViewBag.Pedid = 0;
                Session["PEDIDO"] = 0;
            }
            else
            {
                string id = (string)Session["IDCLIENTE"];
                ViewBag.Iden = id;
                int? idped = (int)Session["PEDIDO"];
                ViewBag.Pedid = idped;
                string identificacion = (string)Session["IDENTIFICACION"];
                ViewBag.Identificacion = identificacion;
                string nombres = (string)Session["NOMBRES"];
                ViewBag.Nombres = nombres;
            }

            int idUser = Function.GetUser();
            int IDSucursal = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            short IDCaja = 0;
            ViewBag.usu_Id = idUser;
            var Fact_Id = db.tbFactura.OrderBy(x => x.fact_Id).Select(x => x.fact_Id).ToList().LastOrDefault();
            ViewBag.fact_Id = Fact_Id + 1;
            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
            ViewBag.suc_Id = IDSucursal;
            var ListCaja = db.spGetCaja(idUser).ToList();
            foreach (spGetCaja_Result Caja in ListCaja)
            {
                ViewBag.cja_Descripcion = Caja.cja_Descripcion;
                ViewBag.cja_Id = Caja.cja_Id;
                IDCaja = Caja.cja_Id;
            }
            var ListCai = db.UDP_Vent_tbFactura_ObtenerCai_CodigoFactura(IDSucursal, IDCaja).ToList();
            foreach (UDP_Vent_tbFactura_ObtenerCai_CodigoFactura_Result Resultado in ListCai)
            {
                ViewBag.pemi_NumeroCAI = Resultado.CAI;
            }
            var prod = db.tbProducto.ToList();
            ViewBag.Producto = db.tbProducto.ToList();
            ViewBag.Cliente = db.tbCliente.ToList();
            Session["Factura"] = null;
            Session["Consumidor"] = null;
            Session["TerceraEdad"] = null;
            Session["IDCLIENTE"] = null;
            Session["IDENTIFICACION"] = null;
            Session["NOMBRES"] = null;
            return View();
        }
        [HttpPost]
        public JsonResult Validar()
        {
            int? idped = (int)Session["PEDIDO"];
            ViewBag.Pedid = idped;
            var PedidoId = ViewBag.Pedid;
            return Json(PedidoId);
        }

        // POST: /Factura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Factura/Create")]
        public ActionResult Create([Bind(Include = "fact_Id,fact_Codigo,fact_Fecha,esfac_Id,cja_Id,suc_Id,clte_Id,pemi_NumeroCAI,fact_AlCredito,fact_DiasCredito,fact_PorcentajeDescuento,fact_Vendedor,clte_Identificacion,clte_Nombres,fact_UsuarioCrea,fact_FechaCrea,fact_UsuarioModifica,fact_FechaModifica,tbUsuario,tbUsuario1")] tbFactura tbFactura)
        {
            if (tbFactura.fact_Vendedor == null)
            {
                tbFactura.fact_Vendedor = "Ninguno";
            }
            tbFactura.fact_Fecha = Function.DatetimeNow();
            ModelState.Remove("fact_Codigo");
            ModelState.Remove("fact_PorcentajeDescuento");
            ModelState.Remove("fact_DiasCredito");
            ModelState.Remove("fact_Fecha");
            int idUser = Function.GetUser();
            var list = (List<tbFacturaDetalle>)Session["Factura"];
            var listTercera = (List<tbFactura>)Session["TerceraEdad"];
            var listConsumidor = (List<DatosConsumidorFinal>)Session["Consumidor"];
            string MensajeError = "";
            var MensajeErrorDetalle = "";
            string MensajeErrorCodigo = "";
            IEnumerable<object> listFactura = null;
            IEnumerable<object> listFacturaDetalle = null;
            string CodigoFactura = "";
            if (ModelState.IsValid)
            {
                try
                {
                    if (listTercera != null && listTercera.Count != 0)
                    {
                        foreach (tbFactura Tercera in listTercera)
                        {
                            tbFactura.fact_IdentidadTE = Tercera.fact_IdentidadTE;
                            tbFactura.fact_NombresTE = Tercera.fact_NombresTE;
                            tbFactura.fact_FechaNacimientoTE = Tercera.fact_FechaNacimientoTE;
                        }
                    }
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        var lista = db.UDP_Vent_tbFactura_ObtenerCai_CodigoFactura(tbFactura.suc_Id, tbFactura.cja_Id).ToList();
                        foreach (UDP_Vent_tbFactura_ObtenerCai_CodigoFactura_Result Resultado in lista)
                        {
                            MensajeErrorCodigo = Resultado.ERRORMSJ;
                            CodigoFactura = Resultado.CODFACTURA;
                        }
                        if(MensajeErrorCodigo.StartsWith("-1") || MensajeErrorCodigo.StartsWith("-2"))
                        {
                            ModelState.AddModelError("", "Rango de Punto Emisión no disponible");
                            return View(tbFactura);
                        }
                        else
                        {
                            listFactura = db.UDP_Vent_tbFactura_Insert(
                                                CodigoFactura,
                                                tbFactura.fact_Fecha,
                                                tbFactura.esfac_Id = 1,
                                                tbFactura.cja_Id,
                                                tbFactura.suc_Id,
                                                tbFactura.clte_Id,
                                                tbFactura.pemi_NumeroCAI,
                                                tbFactura.fact_AlCredito,
                                                tbFactura.fact_DiasCredito,
                                                tbFactura.fact_PorcentajeDescuento,
                                                tbFactura.fact_Vendedor,
                                                tbFactura.clte_Identificacion,
                                                tbFactura.clte_Nombres,
                                                tbFactura.fact_IdentidadTE,
                                                tbFactura.fact_NombresTE,
                                                tbFactura.fact_FechaNacimientoTE,
                                                tbFactura.fact_EsAnulada,
                                                tbFactura.fact_RazonAnulado,
                                                Function.GetUser(),
                                                Function.DatetimeNow());
                            foreach (UDP_Vent_tbFactura_Insert_Result Factura in listFactura)
                                MensajeError = Factura.MensajeError;
                            if (MensajeError.StartsWith("-1"))
                            {
                                Function.InsertBitacoraErrores("Factura/Create", MensajeError, "Create");
                                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                return View(tbFactura);
                            }
                            else
                            {
                                if (list != null && list.Count != 0)
                                {
                                    foreach (tbFacturaDetalle Detalle in list)
                                    {
                                        var FacturaD_Id = Convert.ToInt64(MensajeError);
                                        Detalle.fact_Id = FacturaD_Id;
                                        listFacturaDetalle = db.UDP_Vent_tbFacturaDetalle_Insert(
                                            Detalle.fact_Id,
                                            Detalle.prod_Codigo,
                                            Detalle.factd_Cantidad,
                                            Detalle.factd_MontoDescuento,
                                            Detalle.factd_PorcentajeDescuento,
                                            Detalle.factd_Impuesto,
                                            Detalle.factd_PrecioUnitario,
                                            Function.GetUser(),
                                            Function.DatetimeNow());
                                        foreach (UDP_Vent_tbFacturaDetalle_Insert_Result SPfacturadet in listFacturaDetalle)
                                        {
                                            MensajeErrorDetalle = SPfacturadet.MensajeError;
                                            if (MensajeErrorDetalle.StartsWith("-1"))
                                            {
                                                Function.InsertBitacoraErrores("Factura/Create", MensajeError, "Create");
                                                ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                                return View(tbFactura);
                                            }
                                        }
                                    }
                                }
                            }
                            Tran.Complete();
                            Session["IDCLIENTE"] = null;
                            Session["IDENTIFICACION"] = null;
                            Session["NOMBRES"] = null;
                            return RedirectToAction("Index");
                        }
                    }

                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
                    ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
                    ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
                    ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbFactura.suc_Id);
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                    ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
                    ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion");
                    ViewBag.usu_Id = idUser;
                    ViewBag.Cliente = db.tbCliente.ToList();
                    ViewBag.Producto = db.tbProducto.ToList();
                }

            }
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbFactura.suc_Id);
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion");
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.Producto = db.tbProducto.ToList();
            ViewBag.usu_Id = idUser;
            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
            ViewBag.suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            Session["IDCLIENTE"] = null;
            Session["IDENTIFICACION"] = null;
            Session["NOMBRES"] = null;

            return View(tbFactura);
        }

        // GET: /Factura/Edit/5
        [SessionManager("Factura/Edit")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            int idUser = Function.GetUser();
            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
            ViewBag.suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.Producto = db.tbProducto.ToList();
            Session["FacturaEdit"] = null;
            return View(tbFactura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Factura/Edit")]
        public ActionResult Edit([Bind(Include = "fact_Id,fact_Codigo,fact_Fecha,esfac_Id,cja_Id,suc_Id,clte_Id,pemi_NumeroCAI,fact_AlCredito,fact_DiasCredito,fact_PorcentajeDescuento,fact_Vendedor,clte_Identificacion,clte_Nombres,fact_UsuarioCrea,fact_FechaCrea,fact_UsuarioModifica,fact_FechaModifica,tbUsuario,tbUsuario1")] tbFactura tbFactura)
        {
            var listEdit = (List<tbFacturaDetalle>)Session["FacturaEdit"];
            string MensajeError = "";
            var MensajeErrorDetalle = "";
            IEnumerable<object> listFactura = null;
            IEnumerable<object> listFacturaDetalle = null;

            if (ModelState.IsValid)
            {
                try
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {

                        listFactura = db.UDP_Vent_tbFactura_Update(
                                                tbFactura.fact_Id,
                                                tbFactura.fact_Codigo,
                                                tbFactura.fact_Fecha,
                                                tbFactura.esfac_Id = 1,
                                                tbFactura.clte_Id,
                                                tbFactura.pemi_NumeroCAI,
                                                tbFactura.fact_AlCredito,
                                                tbFactura.fact_DiasCredito,
                                                tbFactura.fact_PorcentajeDescuento,
                                                tbFactura.fact_Vendedor,
                                                tbFactura.clte_Identificacion,
                                                tbFactura.clte_Nombres,
                                                tbFactura.fact_IdentidadTE,
                                                tbFactura.fact_NombresTE,
                                                tbFactura.fact_FechaNacimientoTE,
                                                Function.GetUser(),
                                                tbFactura.fact_FechaAutoriza,
                                                tbFactura.fact_EsAnulada,
                                                tbFactura.fact_RazonAnulado,
                                                tbFactura.fact_UsuarioCrea,
                                                tbFactura.fact_FechaCrea,
                                                Function.GetUser(),
                                                Function.DatetimeNow());
                        foreach (UDP_Vent_tbFactura_Update_Result Factura in listFactura)
                            MensajeError = Factura.MensajeError;
                        if (MensajeError.StartsWith("-1"))
                        {
                            Function.InsertBitacoraErrores("Factura/Edit", MensajeError, "Edit");
                            ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                            return View(tbFactura);
                        }
                        else
                        {
                            if (listEdit != null && listEdit.Count != 0)
                            {
                                foreach (tbFacturaDetalle Detalle in listEdit)
                                {
                                    var Exits = db.tbFacturaDetalle.Where(
                                        x => x.prod_Codigo == Detalle.prod_Codigo
                                        &&
                                        x.fact_Id == Detalle.fact_Id).FirstOrDefault();

                                    if (Exits != null)
                                    {
                                        var Cantidad = db.tbFacturaDetalle.Where(
                                        x => x.prod_Codigo == Detalle.prod_Codigo
                                        &&
                                        x.fact_Id == Detalle.fact_Id).Select(c => c.factd_Cantidad).FirstOrDefault();

                                        var pFacturaDetalle = db.tbFacturaDetalle.Where(x => x.fact_Id == Detalle.fact_Id).Select(c => c.fact_Id).FirstOrDefault();
                                        tbFacturaDetalle vSalidaDetalle = db.tbFacturaDetalle.Find(pFacturaDetalle);

                                        decimal CantidadNew = Convert.ToDecimal(Exits.factd_Cantidad) + Convert.ToDecimal(Detalle.factd_Cantidad);
                                        listFacturaDetalle = db.UDP_Vent_tbFacturaDetalle_Update(
                                                            Exits.factd_Id,

                                                            Detalle.prod_Codigo,
                                                            Detalle.factd_Cantidad,
                                                            Detalle.factd_MontoDescuento,
                                                            Detalle.factd_PorcentajeDescuento,
                                                            Detalle.factd_Impuesto,
                                                            Detalle.factd_PrecioUnitario,
                                                            Detalle.factd_UsuarioAutoriza,
                                                            Detalle.factd_FechaAutoriza,
                                                            Detalle.factd_UsuarioCrea,
                                                            Detalle.factd_FechaCrea,
                                                            Function.GetUser(),
                                                            Function.DatetimeNow());
                                        foreach (UDP_Vent_tbFacturaDetalle_Update_Result RSSalidaDetalle in listFacturaDetalle)
                                            MensajeErrorDetalle = RSSalidaDetalle.MensajeError;
                                        if (MensajeErrorDetalle.StartsWith("-1"))
                                        {
                                            Function.InsertBitacoraErrores("Factura/EditDetalle", MensajeError, "Edit");
                                            ModelState.AddModelError("", "No se pudo actualizar el registro detalle, favor contacte al administrador.");
                                            return View(tbFactura);
                                        }
                                    }
                                    else
                                    {
                                        var FacturaD_Id = Convert.ToInt64(MensajeError);
                                        Detalle.fact_Id = FacturaD_Id;
                                        listFacturaDetalle = db.UDP_Vent_tbFacturaDetalle_Insert(
                                        Detalle.fact_Id,
                                        Detalle.prod_Codigo,
                                        Detalle.factd_Cantidad,
                                        Detalle.factd_MontoDescuento,
                                        Detalle.factd_PorcentajeDescuento,
                                        Detalle.factd_Impuesto,
                                        Detalle.factd_PrecioUnitario,
                                        Function.GetUser(),
                                        Function.DatetimeNow());
                                        foreach (UDP_Vent_tbFacturaDetalle_Insert_Result SPfacturadet in listFacturaDetalle)
                                        {
                                            MensajeErrorDetalle = SPfacturadet.MensajeError;
                                            if (MensajeError.StartsWith("-1"))
                                            {
                                                Function.InsertBitacoraErrores("Factura/Edit", MensajeError, "Edit");
                                                ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                return View(tbFactura);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Factura/Edit", MensajeError, "Edit");
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
                    ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
                    ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
                    ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
                    ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
                    ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbFactura.suc_Id);
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                    ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
                    ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion");
                    ViewBag.Cliente = db.tbCliente.ToList();
                    ViewBag.Producto = db.tbProducto.ToList();
                }
            }
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbFactura.suc_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.Producto = db.tbProducto.ToList();
            int idUser = Function.GetUser();
            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
            ViewBag.suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            return View(tbFactura);
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
        public JsonResult SaveFacturaDetalleEdit(tbFacturaDetalle FacturaDetalleEdit, string data_producto)
        {
            var datos = "";
            decimal cantvieja = 0;
            decimal cantnueva = 0;
            data_producto = FacturaDetalleEdit.prod_Codigo;
            decimal data_cantidad = FacturaDetalleEdit.factd_Cantidad;
            List<tbFacturaDetalle> sessionFacturaDetalle = new List<tbFacturaDetalle>();
            var listEdit = (List<tbFacturaDetalle>)Session["FacturaEdit"];
            if (listEdit == null)
            {
                sessionFacturaDetalle.Add(FacturaDetalleEdit);
                Session["FacturaEdit"] = sessionFacturaDetalle;
            }
            else
            {
                foreach (var t in listEdit)
                    if (t.prod_Codigo == data_producto)
                    {
                        datos = data_producto;
                        foreach (var viejo in listEdit)
                            if (viejo.prod_Codigo == FacturaDetalleEdit.prod_Codigo)
                                cantvieja = viejo.factd_Cantidad;
                        cantnueva = cantvieja + data_cantidad;
                        t.factd_Cantidad = cantnueva;
                        return Json(datos, JsonRequestBehavior.AllowGet);
                    }
                listEdit.Add(FacturaDetalleEdit);
                Session["FacturaEdit"] = listEdit;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveFacturaDetalle(tbFacturaDetalle FacturaDetalleC, string data_producto)

        {
            var datos = "";
            decimal cantvieja = 0;
            decimal cantnueva = 0;
            data_producto = FacturaDetalleC.prod_Codigo;
            decimal data_cantidad = FacturaDetalleC.factd_Cantidad;
            List<tbFacturaDetalle> sessionFacturaDetalle = new List<tbFacturaDetalle>();
            var list = (List<tbFacturaDetalle>)Session["Factura"];
            if (list == null)
            {
                sessionFacturaDetalle.Add(FacturaDetalleC);
                Session["Factura"] = sessionFacturaDetalle;
            }
            else
            {
                foreach (var t in list)
                    if (t.prod_Codigo == data_producto)
                    {
                        datos = data_producto;
                        foreach (var viejo in list)
                            if (viejo.prod_Codigo == FacturaDetalleC.prod_Codigo)
                                cantvieja = viejo.factd_Cantidad;
                        cantnueva = cantvieja + data_cantidad;
                        t.factd_Cantidad = cantnueva;
                        return Json(datos, JsonRequestBehavior.AllowGet);
                    }
                list.Add(FacturaDetalleC);
                Session["Factura"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveTerceraEdad(tbFactura TerceraEdadC)
        {
            List<tbFactura> sessionTercera = new List<tbFactura>();
            var listTercera = (List<tbFactura>)Session["TerceraEdad"];
            if (listTercera == null)
            {
                sessionTercera.Add(TerceraEdadC);
                Session["TerceraEdad"] = sessionTercera;
            }
            else
            {
                listTercera.Add(TerceraEdadC);
                Session["TerceraEdad"] = listTercera;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConsumidorFinal(DatosConsumidorFinal ConsumidorFinal)
        {
            List<DatosConsumidorFinal> sessionConsumidor = new List<DatosConsumidorFinal>();
            var listConsumidor = (List<DatosConsumidorFinal>)Session["Consumidor"];
            if (listConsumidor == null)
            {
                sessionConsumidor.Add(ConsumidorFinal);
                Session["Consumidor"] = sessionConsumidor;
            }
            else
            {
                listConsumidor.Add(ConsumidorFinal);
                Session["Consumidor"] = listConsumidor;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveFacturaDetalle(tbFacturaDetalle FacturaDetalleC)
        {
            var list = (List<tbFacturaDetalle>)Session["Factura"];
            string Msj = "";
            if (list != null)
            {
                var itemToRemove = list.Single(r => r.prod_Codigo == FacturaDetalleC.prod_Codigo);
                list.Remove(itemToRemove);
                Session["Factura"] = list;
                Msj = "Exito";
            }
            return Json(Msj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveFacturaDetalleEdit(tbFacturaDetalle FacturaDetalleC)
        {
            var list = (List<tbFacturaDetalle>)Session["FacturaEdit"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.factd_Id == FacturaDetalleC.factd_Id);
                list.Remove(itemToRemove);
                Session["FacturaEdit"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateFacturaDetalle(tbFacturaDetalle EditFacturaDetalle)
        {
            try
            {
                tbFacturaDetalle tbfactDetalle = db.tbFacturaDetalle.Find(EditFacturaDetalle.factd_Id);
                var MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Vent_tbFacturaDetalle_Update(
                            EditFacturaDetalle.factd_Id,
                            EditFacturaDetalle.prod_Codigo,
                            EditFacturaDetalle.factd_Cantidad,
                            EditFacturaDetalle.factd_MontoDescuento,
                            EditFacturaDetalle.factd_PorcentajeDescuento,
                            EditFacturaDetalle.factd_Impuesto,
                            EditFacturaDetalle.factd_PrecioUnitario,
                            EditFacturaDetalle.factd_UsuarioAutoriza,
                            EditFacturaDetalle.factd_FechaAutoriza,
                            EditFacturaDetalle.factd_UsuarioCrea,
                            tbfactDetalle.factd_FechaCrea,
                            Function.GetUser(),
                            Function.DatetimeNow());
                foreach (UDP_Vent_tbFacturaDetalle_Update_Result FacturaDetalle in list)
                    MensajeError = FacturaDetalle.MensajeError;
                if (MensajeError == "-1")
                {
                    Function.InsertBitacoraErrores("Factura/Edit", MensajeError, "Edit");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return RedirectToAction("Edit", "Factura");
                }
                else
                {
                    return RedirectToAction("Edit", "Factura");
                }
            }
            catch (Exception Ex)
            {

                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return RedirectToAction("Edit", "Factura");
            }
        }

        public JsonResult GetEmpleados(string term)
        {
            var results = db.UDV_Inv_Nombre_Empleado.Where(s => term == null || s.Empleados.ToLower().Contains(term.ToLower())).Select(x => new { id = x.emp_Id, value = x.Empleados }).Take(5).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AnularFactura(int CodFactura, bool FacturaAnulado, string RazonAnulado)
        {
            var list = db.UDP_Vent_tbFactura_Estado(CodFactura, Helpers.AnuladoFactura, RazonAnulado).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPrecio(int Cliente, string idItem)
        {
            var list = db.UDP_Vent_tbFactura_BuscarListaPrecio(Cliente, idItem).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCaja(int CodUsuario)
        {
            var list = db.spGetCaja(CodUsuario).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCantidad(short CodSucursal, string CodProducto)
        {
            var list = db.UDP_Vent_tbFactura_ConsultaBodega(CodSucursal, CodProducto).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetNumeroFact(int CodSucursal, short CodCaja)
        {
            var list = db.UDP_Vent_tbFactura_ObtenerCai_CodigoFactura(CodSucursal, CodCaja).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetParametro()
        {
            var list = db.spGetParametro().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BuscarCodigoBarras(int IDSucursal, string CodBarra, int IDCliente)
        {
            var list = db.UDP_Vent_tbFactura_Filtrado_CodBarra_Sucursal_Cliente(IDSucursal, CodBarra, IDCliente).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FacturaPedido(int CodPedido, int CodFactura)
        {
            var list = db.UDP_Vent_tbPedido_Factura(CodPedido, CodFactura).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetFacturaDetalle(long factID)
        {
            var list = db.UDP_Vent_tbFactura_GetDetalle(factID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetFacturaDetalleD(long factID)
        {
            var list = db.UDP_Vent_tbFactura_GetDetalle(factID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetalleEdit(int StudentId)
        {
            var list = db.UDP_Vent_tbFactura_GetDetalle_Edit(StudentId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutorizarDescuento(string User, string Password)
        {
            var list = db.UDP_Vent_tbFactura_RolSupervisorCaja(User, Password).SingleOrDefault();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutorizarDescuentoDetalle(string User, string Password)
        {
            var list = db.UDP_Vent_tbFactura_RolSupervisorCaja(User, Password).SingleOrDefault();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDetallePedido(int CodPedido)
        {
            var list = db.sp_GetDetallePedido(CodPedido).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: /Cliente/Create
        public ActionResult _CreateCliente()
        {
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion");
            return View();
        }

        // POST: /Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CreateCliente([Bind(Include = "clte_Id,tpi_Id,clte_Identificacion,clte_EsPersonaNatural,clte_Nombres,clte_Apellidos,clte_FechaNacimiento,clte_Nacionalidad,clte_Sexo,clte_Telefono,clte_NombreComercial,clte_RazonSocial,clte_ContactoNombre,clte_ContactoEmail,clte_ContactoTelefono,clte_FechaConstitucion,mun_Codigo,clte_Direccion,clte_CorreoElectronico,clte_EsActivo,clte_RazonInactivo,clte_ConCredito,clte_EsMinorista,clte_Observaciones,clte_UsuarioCrea,clte_FechaCrea,clte_UsuarioModifica,clte_FechaModifica,clte_MontoCredito,clte_DiasCredito")] tbCliente tbCliente, string dep_Codigo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbCliente_Insert(tbCliente.tpi_Id,
                                                        tbCliente.clte_Identificacion,
                                                        tbCliente.clte_EsPersonaNatural,
                                                        tbCliente.clte_Nombres,
                                                        tbCliente.clte_Apellidos,
                                                        tbCliente.clte_FechaNacimiento,
                                                        tbCliente.clte_Nacionalidad,
                                                        tbCliente.clte_Sexo,
                                                        tbCliente.clte_Telefono,
                                                        tbCliente.clte_NombreComercial,
                                                        tbCliente.clte_RazonSocial,
                                                        tbCliente.clte_ContactoNombre,
                                                        tbCliente.clte_ContactoEmail,
                                                        tbCliente.clte_ContactoTelefono,
                                                        tbCliente.clte_FechaConstitucion,
                                                        tbCliente.mun_Codigo,
                                                        tbCliente.clte_Direccion,
                                                        tbCliente.clte_CorreoElectronico,
                                                        Helpers.ClienteActivo,
                                                        tbCliente.clte_RazonInactivo,
                                                        Helpers.ClienteCredito,
                                                        tbCliente.clte_EsMinorista,
                                                        tbCliente.clte_Observaciones,
                                                        tbCliente.clte_MontoCredito,
                                                        tbCliente.clte_DiasCredito,
                                                        tbCliente.clte_Exonerado, Function.GetUser(),
                                    Function.DatetimeNow());
                    foreach (UDP_Vent_tbCliente_Insert_Result cliente in list)
                        MensajeError = cliente.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        Function.InsertBitacoraErrores("Factura/_CreateCliente", MensajeError, "_CreateCliente");
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbCliente);
                    }
                    else
                    {
                        Session["IDCLIENTE"] = MensajeError;
                        Session["IDENTIFICACION"] = tbCliente.clte_Identificacion;
                        if (tbCliente.clte_EsPersonaNatural)
                            Session["NOMBRES"] = tbCliente.clte_Nombres + " " + tbCliente.clte_Apellidos;
                        else
                            Session["NOMBRES"] = tbCliente.clte_NombreComercial;

                        return RedirectToAction("Create", "Factura");
                    }

                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Factura/_CreateCliente", Ex.Message, "_CreateCliente");
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_Codigo);
                    ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
                    ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
                    return View(tbCliente);
                }
                
            }
            tbCliente Cliente = new tbCliente();
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_Codigo);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
            return View(tbCliente);
        }

        public JsonResult ListaProductos()
        {
            IEnumerable<object> Lista = null;
            try
            {
                var UserId = Function.GetUser();
                var SucId = db.tbUsuario.Where(x => x.usu_Id == UserId).Select(p => p.suc_Id).FirstOrDefault();
                var bod_Id = db.tbSucursal.Where(x => x.suc_Id == SucId).Select(p => p.bod_Id).FirstOrDefault();
                Lista = db.SDP_Inv_tbBodegaDetalle_Select_Producto(bod_Id).ToList();
                Lista = db.tbProducto.ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(Lista, JsonRequestBehavior.AllowGet);
        }
    }
}
