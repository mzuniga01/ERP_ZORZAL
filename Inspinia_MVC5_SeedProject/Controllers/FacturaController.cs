using CrystalDecisions.CrystalReports.Engine;
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
using System.IO;
using System.Reflection;

namespace ERP_GMEDINA.Controllers
{
    public class FacturaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        private ObjectResult<UDP_Vent_DatosConsumidorFinal_Insert_Result> listConsumidorFinal;

        // GET: /Factura/
        public ActionResult Index()
        {
            var tbFactura = db.UDV_Vent_Busqueda_Factura;
            return View(tbFactura.ToList());
        }

        [HttpPost]
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
                return View(list);
            }
            catch (Exception ex)
            {

                var tbFactura = db.UDV_Vent_Busqueda_Factura;
                Console.Write(ex.Message);
                return View(tbFactura.ToList());
            }

        }

        public DataTable EvaluarNullable(PropertyInfo[] properties)
        {
            DataTable dt = new DataTable();
            DataColumn dc = null;
            foreach (PropertyInfo pi in properties)
            {
                dc = new DataColumn();
                dc.ColumnName = pi.Name;

                if (pi.PropertyType.Name.Contains("Nullable"))
                    dc.DataType = typeof(String);
                else
                    dc.DataType = pi.PropertyType;

                // dc.DataType = pi.PropertyType;
                dt.Columns.Add(dc);
            }
            return dt;
        }

        //public ActionResult ExportImprimir()
        //{
        //    PropertyInfo[] myIntArray;
        //    List<tbFactura> allCustomer = new List<tbFactura>();
        //    ReportDocument rd = new ReportDocument();
        //    foreach (tbFactura pi in allCustomer)
        //    {
        //        myIntArray = new PropertyInfo[](
        //        pi.fact_Codigo,
        //        pi.fact_Fecha,
        //        pi.esfac_Id,
        //        pi.cja_Id,
        //        pi.suc_Id,
        //        pi.clte_Id,
        //        pi.pemi_NumeroCAI,
        //        pi.fact_AlCredito,
        //        pi.fact_DiasCredito,
        //        pi.fact_PorcentajeDescuento,
        //        pi.fact_Vendedor,
        //        pi.clte_Identificacion,
        //        pi.clte_Nombres,
        //        pi.fact_IdentidadTE,
        //        pi.fact_NombresTE,
        //        pi.fact_FechaNacimientoTE,
        //        pi.fact_EsAnulada,
        //        pi.fact_RazonAnulado);
        //    }
        //    try
        //    {              
        //        allCustomer = db.tbFactura.ToList();
               
        //        rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Factura.rpt"));

        //        rd.SetDataSource(allCustomer);

        //        Response.Buffer = false;
        //        Response.ClearContent();
        //        Response.ClearHeaders();
              
        //    }catch(Exception Ex)
        //    {
        //        Ex.Message.ToString();
        //    }

        //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    return File(stream, "application/pdf", "CustomerList.pdf");

        //}



        // GET: /Factura/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }
        public ActionResult _IndexCliente()
        {
            return PartialView();
        }

        public ActionResult _IndexProducto()
        {
            return PartialView();
        }

        public ActionResult _IndexListaPrecio()
        {
            return PartialView();
        }

        // GET: /Factura/Create
        public ActionResult Create()
        {
            tbFactura Factura = new tbFactura();
            if (Session["IDCLIENTE"] == null)
            {
                ViewBag.Iden = 0;
                ViewBag.Identificacion = "";
                ViewBag.Nombres = "";
                ViewBag.Pedid = 0;
                Session["PEDIDO"] = 0;
            }
            else
            {
                int id = (int)Session["IDCLIENTE"];
                ViewBag.Iden = id;
                int? idped = (int)Session["PEDIDO"];
                ViewBag.Pedid = idped;
                string identificacion = (string)Session["IDENTIFICACION"];
                ViewBag.Identificacion = identificacion;
                string nombres = (string)Session["NOMBRES"];
                ViewBag.Nombres = nombres;
            }

            int idUser = 0;
            GeneralFunctions Login = new GeneralFunctions();
            List<tbUsuario> User = Login.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }
            var Fact_Id = db.tbFactura.OrderBy(x => x.fact_Id).Select(x => x.fact_Id).ToList().LastOrDefault();
            ViewBag.fact_Id = Fact_Id + 1;
            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
            ViewBag.suc_Id = db.tbUsuario.Where(x => x.emp_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion");
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
        public ActionResult Create([Bind(Include = "fact_Id,fact_Codigo,fact_Fecha,esfac_Id,cja_Id,suc_Id,clte_Id,pemi_NumeroCAI,fact_AlCredito,fact_DiasCredito,fact_PorcentajeDescuento,fact_Vendedor,clte_Identificacion,clte_Nombres,fact_UsuarioCrea,fact_FechaCrea,fact_UsuarioModifica,fact_FechaModifica,tbUsuario,tbUsuario1")] tbFactura tbFactura)
        {
            if (tbFactura.fact_Vendedor == null)
            {
                tbFactura.fact_Vendedor = "Ninguno";
            }

            var list = (List<tbFacturaDetalle>)Session["Factura"];
            var listTercera = (List<tbFactura>)Session["TerceraEdad"];
            var listConsumidor = (List<DatosConsumidorFinal>)Session["Consumidor"];
            string MensajeError = "";
            string MensajeErrorConsumidor = "";
            var MensajeErrorDetalle = "";
            IEnumerable<object> listFactura = null;
            IEnumerable<object> listFacturaDetalle = null;
            IEnumerable<object> listConsumidorFinal = null;
            if (db.tbFactura.Any(a => a.fact_Codigo == tbFactura.fact_Codigo))
            {
                ModelState.AddModelError("", "Ya existe este Número de Factura.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (listTercera != null)
                    {
                        if (listTercera.Count != 0)
                        {
                            foreach (tbFactura Tercera in listTercera)
                            {
                                tbFactura.fact_IdentidadTE = Tercera.fact_IdentidadTE;
                                tbFactura.fact_NombresTE = Tercera.fact_NombresTE;
                                tbFactura.fact_FechaNacimientoTE = Tercera.fact_FechaNacimientoTE;
                            }
                        }
                    }
                    using (TransactionScope Tran = new TransactionScope())
                    {

                        listFactura = db.UDP_Vent_tbFactura_Insert(
                                                tbFactura.fact_Codigo,
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
                                                tbFactura.fact_RazonAnulado);
                        foreach (UDP_Vent_tbFactura_Insert_Result Factura in listFactura)
                            MensajeError = Factura.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbFactura);
                        }
                        else
                        {
                            if (MensajeError != "-1")
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
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
                                                Detalle.factd_PrecioUnitario
                                                );
                                            foreach (UDP_Vent_tbFacturaDetalle_Insert_Result SPfacturadet in listFacturaDetalle)
                                            {
                                                MensajeErrorDetalle = SPfacturadet.MensajeError;
                                                if (MensajeError.StartsWith("-1"))
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbFactura);
                                                }
                                            }
                                        }
                                    }
                                }
                                if (listConsumidor != null)
                                {
                                    if (listConsumidor.Count != 0)
                                    {
                                        foreach (DatosConsumidorFinal ConsuFinal in listConsumidor)
                                        {
                                            var FacturaD_Id = Convert.ToInt64(MensajeError);
                                            listConsumidorFinal = db.UDP_Vent_DatosConsumidorFinal_Insert(
                                                FacturaD_Id ,
                                                ConsuFinal.confi_Nombres,
                                                ConsuFinal.confi_Telefono,
                                                ConsuFinal.confi_Correo
                                                );
                                            foreach (UDP_Vent_DatosConsumidorFinal_Insert_Result SPConsumidordet in listConsumidorFinal)
                                            {
                                                MensajeErrorConsumidor = SPConsumidordet.MensajeError;
                                                if (MensajeError.StartsWith("-1"))
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbFactura);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbFactura);
                            }

                        }
                        Tran.Complete();
                        Session["IDCLIENTE"] = null;
                        Session["IDENTIFICACION"] = null;
                        Session["NOMBRES"] = null;
                        return RedirectToAction("Index");
                    }                    

                }
                catch (Exception Ex)
                {
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
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion");
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.Producto = db.tbProducto.ToList();
            int idUser = 0;
            GeneralFunctions Login = new GeneralFunctions();
            List<tbUsuario> User = Login.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }
            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
            ViewBag.suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            Session["IDCLIENTE"] = null;
            Session["IDENTIFICACION"] = null;
            Session["NOMBRES"] = null;

            return View(tbFactura);
        }

        // GET: /Factura/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            int idUser = 0;
            GeneralFunctions Login = new GeneralFunctions();
            List<tbUsuario> User = Login.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }
            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
            ViewBag.suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbFactura.cja_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identificacion", tbFactura.clte_Id);
            ViewBag.esfac_Id = new SelectList(db.tbEstadoFactura, "esfac_Id", "esfac_Descripcion", tbFactura.esfac_Id);
            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "mun_Codigo", tbFactura.suc_Id);
            ViewBag.Cliente = db.tbCliente.ToList();
            ViewBag.Producto = db.tbProducto.ToList();
            Session["FacturaEdit"] = null;
            return View(tbFactura);
        }

        // POST: /Factura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                                                tbFactura.fact_UsuarioAutoriza,
                                                tbFactura.fact_FechaAutoriza,
                                                tbFactura.fact_EsAnulada,
                                                tbFactura.fact_RazonAnulado,
                                                tbFactura.fact_UsuarioCrea,
                                                tbFactura.fact_FechaCrea);
                        foreach (UDP_Vent_tbFactura_Update_Result Factura in listFactura)
                            MensajeError = Factura.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbFactura);
                        }
                        else
                        {
                            if (MensajeError != "-1")
                            {
                                if (listEdit != null)
                                {
                                    if (listEdit.Count != 0)
                                    {
                                        foreach (tbFacturaDetalle Detalle in listEdit)
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
                                                Detalle.factd_PrecioUnitario
                                                );
                                            foreach (UDP_Vent_tbFacturaDetalle_Insert_Result SPfacturadet in listFacturaDetalle)
                                            {
                                                MensajeErrorDetalle = SPfacturadet.MensajeError;
                                                if (MensajeError.StartsWith("-1"))
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbFactura);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbFactura);
                            }

                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
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
            int idUser = 0;
            GeneralFunctions Login = new GeneralFunctions();
            List<tbUsuario> User = Login.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }
            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
            ViewBag.suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            return View(tbFactura);
        }

        // GET: /Factura/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbFactura);
        }

        // POST: /Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tbFactura tbFactura = db.tbFactura.Find(id);
            db.tbFactura.Remove(tbFactura);
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


        [HttpPost]
        public JsonResult SaveFacturaDetalle(tbFacturaDetalle FacturaDetalleC)
        {
            List<tbFacturaDetalle> sessionFacturaDetalle = new List<tbFacturaDetalle>();
            var list = (List<tbFacturaDetalle>)Session["Factura"];
            if (list == null)
            {
                sessionFacturaDetalle.Add(FacturaDetalleC);
                Session["Factura"] = sessionFacturaDetalle;
            }
            else
            {
                list.Add(FacturaDetalleC);
                Session["Factura"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult IncrementarProducto(string data_producto)
        {
            var Datos = "";
            if (Session["Factura"] == null)
            {

            }
            else
            {
                var menu = Session["Factura"] as List<tbFacturaDetalle>;

                foreach (var t in menu)
                {
                    if (t.prod_Codigo == data_producto)
                        Datos = data_producto;
                }


            }

            return Json(Datos);
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

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.factd_Id == FacturaDetalleC.factd_Id);
                list.Remove(itemToRemove);
                Session["Factura"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
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
                var MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Vent_tbFacturaDetalle_Update(
                            EditFacturaDetalle.factd_Id,
                            EditFacturaDetalle.factd_Cantidad,
                            EditFacturaDetalle.factd_MontoDescuento,
                            EditFacturaDetalle.factd_PorcentajeDescuento,
                            EditFacturaDetalle.factd_Impuesto,
                            EditFacturaDetalle.factd_PrecioUnitario,
                            EditFacturaDetalle.factd_UsuarioAutoriza,
                            EditFacturaDetalle.factd_FechaAutoriza,
                            EditFacturaDetalle.factd_UsuarioCrea,
                            EditFacturaDetalle.factd_FechaCrea = System.DateTime.Now);
                foreach (UDP_Vent_tbFacturaDetalle_Update_Result FacturaDetalle in list)
                    MensajeError = FacturaDetalle.MensajeError;
                if (MensajeError == "-1")
                {
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
        public ActionResult FacturaPedido(int CodPedido,int CodFactura)
        {
            var list = db.UDP_Vent_tbPedido_Factura(CodPedido,CodFactura).ToList();
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
                                                        tbCliente.clte_Exonerado);
                    foreach (UDP_Vent_tbCliente_Insert_Result cliente in list)
                        MensajeError = cliente.MensajeError;
                    if (MensajeError == "-1")
                    {

                    }
                    else
                    {
                        Session["IDCLIENTE"] = MensajeError;
                        Session["IDENTIFICACION"] = tbCliente.clte_Identificacion;
                        if (tbCliente.clte_EsPersonaNatural)
                        {
                            Session["NOMBRES"] = tbCliente.clte_Nombres + " " + tbCliente.clte_Apellidos;
                        }
                        else
                        {
                            Session["NOMBRES"] = tbCliente.clte_NombreComercial;
                        }

                        return RedirectToAction("Create", "Factura");
                    }

                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "Error al agregar el registro" + Ex.Message.ToString());
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_Codigo);
                    ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
                    ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
                    return View(tbCliente);
                }

                return RedirectToAction("Index");
            }
            tbCliente Cliente = new tbCliente();
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_Codigo);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbCliente.mun_Codigo);
            ViewBag.tpi_Id = new SelectList(db.tbTipoIdentificacion, "tpi_Id", "tpi_Descripcion", tbCliente.tpi_Id);
            return View(tbCliente);
        }

    }
}
