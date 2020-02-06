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
using System.Data.Entity.Core.Objects;
using ERP_GMEDINA.Attribute;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using ERP_GMEDINA.Reports;
using ERP_GMEDINA.Dataset;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;

namespace ERP_GMEDINA.Controllers
{
    public class ProductoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        private Helpers Function = new Helpers();

        // GET: /Producto/
        [SessionManager("Producto/Index")]
        public ActionResult Index()
        {
            var tbproducto = db.tbProducto.Include(t => t.tbUsuario).Include(t => t.tbUnidadMedida).Include(t => t.tbProductoSubcategoria);
            ViewBag.Producto = db.tbBodegaDetalle.ToList();
            return View(tbproducto.ToList());
        }

        // GET: /Producto/Details/5
        [SessionManager("Producto/Details")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbProducto);
        }

        // GET: /Producto/Create
        [SessionManager("Producto/Create")]
        public ActionResult Create()
        {
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria.Where(x => x.pcat_EsActivo == 1), "pcat_Id", "pcat_Nombre");
            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.prov_Id = new SelectList(db.tbProveedor,"prov_Id","prov_Nombre");
            return View();
        }

        [HttpPost]
        public JsonResult GetValue(string pcat_Id, string pscat_Id)
        {
            ObjectParameter Output = new ObjectParameter("prod_Codigo", typeof(string));
            var Categoria = Convert.ToInt32(pcat_Id);
            var SubCategoria = Convert.ToInt32(pscat_Id);
            //var MsjError = "";
            var list = db.UDP_Inv_tbProducto_ValorCodigo(Categoria, SubCategoria, Output);
            foreach (UDP_Inv_tbProducto_ValorCodigo_Result Producto in list)
                ViewBag.prod_Codigo = Producto.MensajeError;
            //ViewBag.prod_Codigo = list;
            return Json(ViewBag.prod_Codigo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSubCategoriaProducto(int CodCategoria)
        {
            var list = db.UDP_Inv_tbProducto_GetSubCategoria(CodCategoria).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Producto/Create")]
        public ActionResult Create([Bind(Include = "prod_Codigo,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prod_CodigoBarras,prod_UsiuarioCrea,prod_FechaCrea,prov_Id,pcat_Id")] tbProducto tbProducto, int pcat_Id)
        {
            if (db.tbProducto.Any(a => a.prod_CodigoBarras == tbProducto.prod_CodigoBarras))
            {
                ModelState.AddModelError("", "El Codigo de Barras ya Existe.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    string MsjError = "";
                    IEnumerable<object> List = null;
                    List = db.UDP_Inv_tbProducto_Insert(tbProducto.prod_Codigo,
                                                            tbProducto.prod_Descripcion,
                                                            tbProducto.prod_Marca,
                                                            tbProducto.prod_Modelo,
                                                            tbProducto.prod_Talla,
                                                            tbProducto.prod_Color,
                                                            tbProducto.pscat_Id,
                                                            tbProducto.uni_Id,
                                                            Helpers.ProductoActivo,
                                                            tbProducto.prod_CodigoBarras,
                                                            tbProducto.prov_Id,
                                                            Function.GetUser(),
                                                            Function.DatetimeNow()
                                                            );
                    foreach (UDP_Inv_tbProducto_Insert_Result Producto in List)
                        MsjError = Producto.MensajeError;

                    if (MsjError.StartsWith("-1"))
                    {
                       
                        ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
                        ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
                        ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
                        ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");
                        Function.InsertBitacoraErrores("Producto/Create", MsjError, "Create");
                        ModelState.AddModelError("", "No se Pudo Insertar el Registro, Favor Contacte al Administrador.");
                        return View(tbProducto);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Function.InsertBitacoraErrores("Producto/Create", Ex.Message.ToString(), "Create");
                    ModelState.AddModelError("", "No se Pudo Insertar el Registro, Favor Contacte al Administrador.");
                    ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
                    ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
                    ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
                    ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");
                    return View(tbProducto);
                }
            }

            tbProducto producto = new tbProducto();
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");

            return View(tbProducto);
        }

        // GET: /Producto/Edit/5
        [SessionManager("Producto/Edit")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProducto tbProducto = db.tbProducto.Find(id);

            if (tbProducto == null)
            {
                return RedirectToAction("NotFound", "Login");
            }

            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria.Where(x => x.pcat_EsActivo == 1), "pcat_Id", "pcat_Nombre", tbProducto.tbProductoSubcategoria.tbProductoCategoria.pcat_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre",tbProducto.prov_Id);
            var Categoria = tbProducto.tbProductoSubcategoria.tbProductoCategoria.pcat_Id; ;
            var Sucategoria = db.tbProductoSubcategoria.Select(s => new
            {
                pscat_Id = s.pscat_Id,
                pscat_Descripcion = s.pscat_Descripcion,
                pcat_Id = s.pcat_Id
            }).Where(x => x.pcat_Id == Categoria).ToList();
            ViewBag.pscat_Id = new SelectList(Sucategoria, "pscat_Id", "pscat_Descripcion", tbProducto.pscat_Id);
            return View(tbProducto);
        }

        [HttpPost]
        public JsonResult GetCategoriaProducto(int codsubcategoria)
        {
            var list = db.UDP_Inv_tbProducto_GetCategoria(codsubcategoria).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // POST: /Producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Producto/Edit")]
        public ActionResult Edit(string id, [Bind(Include = "prod_Codigo,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prod_EsActivo,prod_Razon_Inactivacion,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica,prod_FechaModifica,prod_CodigoBarras,prov_Id,pcat_Id")] tbProducto tbProducto, int pcat_Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbProducto vtbProducto = db.tbProducto.Find(id);
                    IEnumerable<object> List = null;
                    string MsjError = "";
                    List = db.UDP_Inv_tbProducto_Update(tbProducto.prod_Codigo,
                                                        tbProducto.prod_Descripcion,
                                                        tbProducto.prod_Marca,
                                                        tbProducto.prod_Modelo,
                                                        tbProducto.prod_Talla,
                                                        tbProducto.prod_Color,
                                                        tbProducto.pscat_Id,
                                                        tbProducto.uni_Id,
                                                        tbProducto.prod_Razon_Inactivacion,
                                                        tbProducto.prod_CodigoBarras,
                                                        tbProducto.prov_Id,
                                                        vtbProducto.prod_UsuarioCrea,
                                                        vtbProducto.prod_FechaCrea,
                                                        Function.GetUser(),
                                                        Function.DatetimeNow()
                                                        );
                    foreach (UDP_Inv_tbProducto_Update_Result producto in List)
                        MsjError = producto.MensajeError;

                    if (MsjError.StartsWith("-1"))
                    {
                        ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
                        ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion ", tbProducto.pscat_Id);
                        ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbProducto.prov_Id);
                        Function.InsertBitacoraErrores("Producto/Edit", MsjError, "Edit");
                        ModelState.AddModelError("", "No se Pudo Actualizar el registro, Favor Contacte al Administrador.");
                        return View(tbProducto);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
                    ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion ", tbProducto.pscat_Id);
                    ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbProducto.prov_Id);
                    Function.InsertBitacoraErrores("Producto/Edit", Ex.Message.ToString(), "Edit");
                    ModelState.AddModelError("", "No se Pudo Actualizar el registro, Favor Contacte al Administrador.");
                    return View(tbProducto);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioCrea);
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion ", tbProducto.pscat_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbProducto.prov_Id);
            return View(tbProducto);
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
        public JsonResult EstadoInactivar(tbProducto tbProducto, string Razon_Inactivacion)
        {
            tbProducto vProducto = db.tbProducto.Find(tbProducto.prod_Codigo);
            try
            {
                var list = db.UDP_Inv_tbProducto_Update_RazonInactivacion(tbProducto.prod_Codigo, Helpers.ProductoInactivo, Razon_Inactivacion, vProducto.prod_UsuarioCrea, vProducto.prod_FechaCrea, Function.GetUser(), Function.DatetimeNow()).ToList();
                return Json("Éxito", JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Function.InsertBitacoraErrores("Producto/EstadoInactivar", Ex.Message.ToString(), "EstadoInactivar");
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EstadoActivar(string prod_Codigo)
        {
            try
            {
                tbProducto obj = db.tbProducto.Find(prod_Codigo);
                tbProducto producto = new tbProducto();
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProducto_Estado_Prueba(prod_Codigo, Helpers.ProductoActivo);
                foreach (UDP_Inv_tbProducto_Estado_Prueba_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro contacte con el administrador");
                    return RedirectToAction("Edit/" + prod_Codigo);
                }
                else
                {
                    return RedirectToAction("Edit/" + prod_Codigo);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro contacte con el administrador");
                return RedirectToAction("Edit/" + prod_Codigo);
            }
            //return RedirectToAction("Index");
        }

        public ActionResult GenerarReporte(int pscat_Id)
        {

            ReportDocument rd = new ReportDocument();
            Stream stream = null;
            CatalogoProducto ProductoRV = new CatalogoProducto();
            Reportes ProductoDST = new Reportes();

            var ProductoTableAdapter = new UDV_Inv_CatalogoProductosTableAdapter();

            try
            {
                ProductoTableAdapter.Fill(ProductoDST.UDV_Inv_CatalogoProductos, pscat_Id);

                ProductoRV.SetDataSource(ProductoDST);
                stream = ProductoRV.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                ProductoRV.Close();
                ProductoRV.Dispose();

                string fileName = "CatalogoProducto.pdf";
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