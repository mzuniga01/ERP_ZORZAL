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
using System.Data.SqlClient;

//using (SqlConnection conexion = new SqlConnection(conexionString))
//{
//   SqlCommand cmd = new SqlCommand("SP_Valores", conexion);
//cmd.CommandType = CommandType.StoredProcedure;
//   cmd.Parameters.Add(new SqlParameter("@IDMATERIA", 1));
//   SqlParameter NroInscritosParametro = new SqlParameter("@NROINSCRITOS", 0);
//NroInscritosParametro.Direction = ParameterDirection.Output;
//   cmd.Parameters.Add(NroInscritosParametro);
//   conexion.Open();
//   cmd.ExecuteNonQuery();                                       
//   int nroInscritos = Int32.Parse(cmd.Parameters["@NROINSCRITOS"].Value.ToString());
//conexion.Close();
//   return nroInscritos;
//}

namespace ERP_GMEDINA.Controllers
{

    public class ProductoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Producto/
        public ActionResult Index()
        {
            var tbproducto = db.tbProducto.Include(t => t.tbUsuario).Include(t => t.tbUnidadMedida).Include(t => t.tbProductoSubcategoria);
            return View(tbproducto.ToList());
        }

        // GET: /Producto/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                return HttpNotFound();
            }
            return View(tbProducto);

  
        }
        
        // GET: /Producto/Create
        public ActionResult Create()
        {
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
            List<tbProductoCategoria> tbProductoCategoriaList = db.tbProductoCategoria.ToList();
            //ViewBag.tbProductoCategoriaList = new SelectList(tbProductoCategoriaList, "pcat_Id", "pcat_Nombre");
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");           
            return View();
        }

        public JsonResult GetValue(int pcat_Id,int pscat_Id,string prod_Codigo)
        {
            ObjectParameter Output = new ObjectParameter("prod_Codigo", typeof(string));          
            var list = db.SP_Valores(pcat_Id, pscat_Id, Output);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubCategoriaProducto(int CodCategoria)
        {
            var list = db.spGetSubCategoriaProducto(CodCategoria).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetScatList(int pcat_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<tbProductoSubcategoria> tbProductoSubcategoriaList = db.tbProductoSubcategoria.Where(x => x.pcat_Id == pcat_Id).ToList();
            return Json(tbProductoSubcategoriaList, JsonRequestBehavior.AllowGet);
        }

        


// POST: /Producto/Create
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prod_Codigo,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prod_CodigoBarras")] tbProducto tbProducto)
        {
            if (ModelState.IsValid)
            {
                //db.tbProducto.Add(tbProducto);
                //db.SaveChanges();

                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Inv_tbProducto_Insert(tbProducto.prod_Codigo, tbProducto.prod_Descripcion, tbProducto.prod_Marca, tbProducto.prod_Modelo, tbProducto.prod_Talla, tbProducto.prod_Color, tbProducto.pscat_Id, tbProducto.uni_Id,tbProducto.prod_CodigoBarras);
                    foreach (UDP_Inv_tbProducto_Insert_Result Producto in List )
                        MsjError = Producto.MensajeError;

                    if (MsjError == "-1")
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
                    return RedirectToAction("Index");
                }
                
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            

            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioCrea);
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion", tbProducto.pscat_Id);
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
            return View(tbProducto);
        }

        // GET: /Producto/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProducto tbProducto = db.tbProducto.Find(id);           

            if (tbProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion ", tbProducto.pscat_Id);
            List<tbProductoCategoria> tbProductoCategoriaList = db.tbProductoCategoria.ToList();
            ViewBag.tbProductoCategoriaList = new SelectList(tbProductoCategoriaList, "pcat_Id", "pcat_Nombre");
            ViewBag.pcat_Id = new SelectList(tbProductoCategoriaList, "pcat_Id", "pcat_Nombre",tbProducto.pscat_Id);
            return View(tbProducto);
        }

        public JsonResult GetCategoriaProducto(int codsubcategoria)
        {
            var list = db.spGetCategoriaProducto(codsubcategoria).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        // POST: /Producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id,[Bind(Include = "prod_Codigo,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prod_UsuarioCrea,prod_FechaCrea,prod_EsActivo,prod_Razon_Inactivacion,prod_CodigoBarras")] tbProducto tbProducto)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tbProducto).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");

                try
                {
                    tbProducto vtbProducto = db.tbProducto.Find(id);
                    
                    IEnumerable<object> List = null;
                    var MsjError = "";                   
                    List = db.UDP_Inv_tbProducto_Update(tbProducto.prod_Codigo, tbProducto.prod_Descripcion, tbProducto.prod_Marca, tbProducto.prod_Modelo, tbProducto.prod_Talla, tbProducto.prod_Color, tbProducto.pscat_Id, tbProducto.uni_Id, vtbProducto.prod_UsuarioCrea, vtbProducto.prod_FechaCrea, tbProducto.prod_EsActivo,tbProducto.prod_Razon_Inactivacion,tbProducto.prod_CodigoBarras);
                    foreach (UDP_Inv_tbProducto_Update_Result producto in List)
                        MsjError = producto.MensajeError;

                    if (MsjError == "-1")
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
            List<tbProductoCategoria> tbProductoCategoriaList = db.tbProductoCategoria.ToList();
            ViewBag.tbProductoCategoriaList = new SelectList(tbProductoCategoriaList, "pcat_Id", "pcat_Nombre");
            return View(tbProducto);
        }
       

        // GET: /Producto/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return HttpNotFound();
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
    }
}
