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


namespace ERP_GMEDINA.Controllers
{

    public class ProductoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: /Producto/
        public ActionResult Index()
        {
            GeneralFunctions Function = new GeneralFunctions();
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("Producto/Index"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }

                if (Function.GetUserRols("Producto/Index"))
                {
                    var tbproducto = db.tbProducto.Include(t => t.tbUsuario).Include(t => t.tbUnidadMedida).Include(t => t.tbProductoSubcategoria);
                    ViewBag.Producto = db.tbBodegaDetalle.ToList();
                    return View(tbproducto.ToList());
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
           
        }

        // GET: /Producto/Details/5
        public ActionResult Details(string id)
        {
            
            GeneralFunctions Function = new GeneralFunctions();
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("Producto/Details"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }
                if (Function.GetUserRols("Producto/Details"))
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }

                    tbProducto tbProducto = db.tbProducto.Find(id);

                    var pcat = db.tbProductoSubcategoria.Find(tbProducto.pscat_Id).pcat_Id;
                    ViewBag.PCAT = db.tbProductoCategoria.Find(pcat).pcat_Nombre;

                    ViewBag.UsuarioCrea = db.tbUsuario.Find(tbProducto.prod_UsuarioCrea).usu_NombreUsuario;
                    var UsuarioModfica = tbProducto.prod_UsuarioModifica;
                    if (UsuarioModfica == null)
                    {
                        ViewBag.UsuarioModifica = "";
                    }
                    else
                    {
                        ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModfica).usu_NombreUsuario;
                    };

                    if (tbProducto == null)
                    {
                        return RedirectToAction("NotFound", "Login");
                    }
                    return View(tbProducto);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");

        }
        
        // GET: /Producto/Create
        public ActionResult Create()
        {
           

            GeneralFunctions Function = new GeneralFunctions();
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("Producto/Create"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }
                if (Function.GetUserRols("Producto/Create"))
                {
                    ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria.Where(x => x.pcat_EsActivo == 1), "pcat_Id", "pcat_Nombre");
                    ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");                                      
                    ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
                    ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
                    return View();
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
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

        //[HttpPost]
        //public JsonResult GetScatList(int pcat_Id)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    List<tbProductoSubcategoria> tbProductoSubcategoriaList = db.tbProductoSubcategoria.Where(x => x.pcat_Id == pcat_Id).ToList();
        //    return Json(tbProductoSubcategoriaList, JsonRequestBehavior.AllowGet);
        //}




        // POST: /Producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prod_Codigo,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prod_CodigoBarras,prod_UsiuarioCrea,prod_FechaCrea,pcat_Id")] tbProducto tbProducto, int pcat_Id)
        {
            if (db.tbProducto.Any(a => a.prod_CodigoBarras == tbProducto.prod_CodigoBarras))
            {
                ModelState.AddModelError("", "El Codigo de Barras ya Existe.");
            }
            if (ModelState.IsValid)
                    {                        
                        try
                        {                           
                            var MsjError = "";
                            IEnumerable<object> List = null;
                            List = db.UDP_Inv_tbProducto_Insert(tbProducto.prod_Codigo, 
                                                                    tbProducto.prod_Descripcion, 
                                                                    tbProducto.prod_Marca, 
                                                                    tbProducto.prod_Modelo, 
                                                                    tbProducto.prod_Talla, 
                                                                    tbProducto.prod_Color, 
                                                                    tbProducto.pscat_Id, 
                                                                    tbProducto.uni_Id,
                                                                    tbProducto.prod_CodigoBarras,
                                                                    Function.GetUser(),
                                                                    DateTime.Now
                                                                    );
                                foreach (UDP_Inv_tbProducto_Insert_Result Producto in List)
                                    MsjError = Producto.MensajeError;

                                if (MsjError == "-1")
                                {
                                    ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                                    return View(tbProducto);
                                }                               
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                   
                         }
                            catch (Exception Ex)
                            {
                                Ex.Message.ToString();
                                ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                                ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
                                //ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                                //ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                                ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
                                ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
                                return View(tbProducto);
                    
                            }

            }

            tbProducto producto = new tbProducto();
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
            //ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");

            return View(tbProducto);
        }
                


        // GET: /Producto/Edit/5
        public ActionResult Edit(string id)
        {
           

            //GeneralFunctions Function = new GeneralFunctions();
            if (Function.GetUserLogin())
            {
                if (Function.Sesiones("Producto/Edit"))
                {

                }
                else
                {
                    return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                }
                if (Function.GetUserRols("Cliente/Edit"))
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
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");

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
        public ActionResult Edit(string id,[Bind(Include = "prod_Codigo,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prod_EsActivo,prod_Razon_Inactivacion,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica,prod_FechaModifica,prod_CodigoBarras,pcat_Id")] tbProducto tbProducto,int pcat_Id)
        {
            if (ModelState.IsValid)
            {               

                try
                {
                    tbProducto vtbProducto = db.tbProducto.Find(id);
                    
                    IEnumerable<object> List = null;
                    var MsjError = "";                   
                    List = db.UDP_Inv_tbProducto_Update(tbProducto.prod_Codigo, 
                                                        tbProducto.prod_Descripcion, 
                                                        tbProducto.prod_Marca, 
                                                        tbProducto.prod_Modelo, 
                                                        tbProducto.prod_Talla, 
                                                        tbProducto.prod_Color, 
                                                        tbProducto.pscat_Id, 
                                                        tbProducto.uni_Id,
                                                        tbProducto.prod_CodigoBarras,
                                                        vtbProducto.prod_UsuarioCrea, 
                                                        vtbProducto.prod_FechaCrea,
                                                        Function.GetUser(),
                                                        DateTime.Now
                                                        );
                    foreach (UDP_Inv_tbProducto_Update_Result producto in List)
                        MsjError = producto.MensajeError;

                    if (MsjError=="-1")
                    {
                        ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
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
            //List<tbProductoCategoria> tbProductoCategoriaList = db.tbProductoCategoria.ToList();
            //ViewBag.tbProductoCategoriaList = new SelectList(tbProductoCategoriaList, "pcat_Id", "pcat_Nombre");
            return View(tbProducto);
        }
       

        // GET: /Producto/Delete/5
        public ActionResult Delete(string id)
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

        // POST: /Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbProducto tbProducto = db.tbProducto.Find(id);
            db.tbProducto.Remove(tbProducto);
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
        //[HttpPost]
        //public JsonResult EstadoInactivar(string prod_Codigo, bool Activo, string Razon_Inactivacion, int prod_UsuarioModifica, DateTime prod_FechaModifica)
        //{
        //    var list = db.UDP_Inv_tbProducto_Estado_Prueba(prod_Codigo, Helpers.ProductoInactivo, Razon_Inactivacion, prod_UsuarioModifica, prod_FechaModifica).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult Estadoactivar(string prod_Codigo, bool Activo, string Razon_Inactivacion, int prod_UsuarioModifica, DateTime prod_FechaModifica)
        //{
        //    var list = db.UDP_Inv_tbProducto_Estado_Prueba(prod_Codigo, Helpers.ProductoActivo, Razon_Inactivacion,prod_UsuarioModifica,prod_FechaModifica).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult EstadoInactivar(string prod_Codigo, bool Activo, string Razon_Inactivacion)
        {
            var list = db.UDP_Inv_tbProducto_Estado(prod_Codigo, Helpers.ProductoInactivo, Razon_Inactivacion).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Estadoactivar(string prod_Codigo, bool Activo, string Razon_Inactivacion)
        {
            var list = db.UDP_Inv_tbProducto_Estado(prod_Codigo, Helpers.ProductoActivo, Razon_Inactivacion).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


    }
}
