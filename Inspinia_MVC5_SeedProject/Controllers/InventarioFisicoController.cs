using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class InventarioFisicoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /InventarioFisico/
        public ActionResult Index()
        {
            var tbinventariofisico = db.tbInventarioFisico.Include(t => t.tbEstadoInventarioFisico);
            return View(tbinventariofisico.ToList());
        }

        // GET: /InventarioFisico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbInventarioFisico.invf_UsuarioCrea).usu_NombreUsuario;
            var UsuarioModfica = tbInventarioFisico.invf_UsuarioModifica;
            if (UsuarioModfica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModfica).usu_NombreUsuario;
            };
            if (tbInventarioFisico == null)
            {
                return HttpNotFound();
            }
            ViewBag.estif_Id = new SelectList(db.tbEstadoInventarioFisico, "estif_Id", "estif_Descripcion");
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.invf_Id = new SelectList(db.tbInventarioFisico, "invf_Id", "invf_Descripcion");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Codigo");
            ViewBag.prod_Descripcion = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbInventarioFisico);
        }

        // GET: /InventarioFisico/Create
        public ActionResult Create()
        {
            ViewBag.estif_Id = new SelectList(db.tbEstadoInventarioFisico, "estif_Id", "estif_Descripcion");
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.invf_Id = new SelectList(db.tbInventarioFisico, "invf_Id", "invf_Descripcion");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Codigo");
            ViewBag.prod_Descripcion = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.tbProducto.ToList();
            return View();
        }

        public ActionResult _IndexInvFisicoDetalle()
        {
            return View();
        }

        public ActionResult _IndexProductos()
        {
            return View();
        }

        public ActionResult Detalle()
        {
            return View();
        }

        public ActionResult Editar()
        {
            return View();
        }
        // POST: /InventarioFisico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="invf_Id,invf_Descripcion,invf_ResponsableBodega,bod_Id,estif_Id,invf_FechaInventario")] tbInventarioFisico tbInventarioFisico)
        {
            ViewBag.estif_Id = new SelectList(db.tbEstadoInventarioFisico, "estif_Id", "estif_Descripcion");
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.invf_Id = new SelectList(db.tbInventarioFisico, "invf_Id", "invf_Descripcion");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Codigo");
            ViewBag.prod_Descripcion = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> List = null;
                    string MsjError = "";
                    List = db.UDP_Inv_tbInventarioFisico_Insert(tbInventarioFisico.invf_Descripcion,tbInventarioFisico.invf_ResponsableBodega,tbInventarioFisico.bod_Id,tbInventarioFisico.estif_Id,tbInventarioFisico.invf_FechaInventario);
                    foreach (UDP_Inv_tbInventarioFisico_Insert_Result InventarioFisico in List)
                        MsjError = InventarioFisico.MensajeError;

                    if (MsjError.Substring(0,2) == "-1")
                    {
                        ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                    }
                    else
                    {
                        db.tbInventarioFisico.Add(tbInventarioFisico);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }


                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                }
                //db.tbInventarioFisico.Add(tbInventarioFisico);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");

            }
            ViewBag.estif_Id = new SelectList(db.tbEstadoInventarioFisico, "estif_Id", "estif_Descripcion", tbInventarioFisico.estif_Id);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.invf_Id = new SelectList(db.tbInventarioFisico, "invf_Id", "invf_Descripcion");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Codigo");
            ViewBag.prod_Descripcion = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbInventarioFisico);
        }

        // GET: /InventarioFisico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbInventarioFisico.invf_UsuarioCrea).usu_NombreUsuario;
            var UsuarioModfica = tbInventarioFisico.invf_UsuarioModifica;
            if (UsuarioModfica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModfica).usu_NombreUsuario;
            };
            if (tbInventarioFisico == null)
            {
                return HttpNotFound();
            }
            ViewBag.estif_Id = new SelectList(db.tbEstadoInventarioFisico, "estif_Id", "estif_Descripcion", tbInventarioFisico.estif_Id);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.invf_Id = new SelectList(db.tbInventarioFisico, "invf_Id", "invf_Descripcion");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Codigo");
            ViewBag.prod_Descripcion = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbInventarioFisico);
        }

        // POST: /InventarioFisico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id,[Bind(Include="invf_Id,invf_Descripcion,invf_ResponsableBodega,bod_Id,estif_Id,invf_FechaInventario,invf_UsuarioCrea,invf_FechaCrea")] tbInventarioFisico tbInventarioFisico)
        {
            
            if (ModelState.IsValid)
            {
                    try
                    {
                        tbInventarioFisico pInventarioFisico = db.tbInventarioFisico.Find(id);
                        IEnumerable<object> List = null;
                        string MsjError = "";
                        List = db.UDP_Inv_tbInventarioFisico_Update(tbInventarioFisico.invf_Id,tbInventarioFisico.invf_Descripcion, tbInventarioFisico.invf_ResponsableBodega, tbInventarioFisico.bod_Id, tbInventarioFisico.estif_Id, tbInventarioFisico.invf_FechaInventario, pInventarioFisico.invf_UsuarioCrea, pInventarioFisico.invf_FechaCrea);
                        foreach (UDP_Inv_tbInventarioFisico_Update_Result InventarioFisico in List)
                            MsjError = InventarioFisico.MensajeError;

                        if (MsjError.Substring(0, 2) == "-1")
                        {
                            ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                        }
                        else
                        {
                            db.tbInventarioFisico.Add(tbInventarioFisico);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }


                    }
                    catch (Exception Ex)
                    {
                        Ex.Message.ToString();
                        ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
                    }
                    //db.tbInventarioFisico.Add(tbInventarioFisico);
                    //db.SaveChanges();
                    return RedirectToAction("Index");
            }
            ViewBag.estif_Id = new SelectList(db.tbEstadoInventarioFisico, "estif_Id", "estif_Descripcion", tbInventarioFisico.estif_Id);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.invf_Id = new SelectList(db.tbInventarioFisico, "invf_Id", "invf_Descripcion");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Codigo");
            ViewBag.prod_Descripcion = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbInventarioFisico);
        }

        // GET: /InventarioFisico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            if (tbInventarioFisico == null)
            {
                return HttpNotFound();
            }
            return View(tbInventarioFisico);
        }

        // POST: /InventarioFisico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInventarioFisico tbInventarioFisico = db.tbInventarioFisico.Find(id);
            db.tbInventarioFisico.Remove(tbInventarioFisico);
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
        public JsonResult GuardarInventarioDetalle(tbInventarioFisicoDetalle inventariofisicodetalle)
        {
            List<tbInventarioFisicoDetalle> sessionInventarioFisicoDetalle = new List<tbInventarioFisicoDetalle>();
            var list = (List<tbInventarioFisicoDetalle>)Session["CreateInvFisicoDetalle"];
            if (list == null)
            {
                sessionInventarioFisicoDetalle.Add(inventariofisicodetalle);
                Session["CreateInvFisicoDetalle"] = sessionInventarioFisicoDetalle;
            }
            else
            {
                list.Add(inventariofisicodetalle);
                Session["CreateInvFisicoDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }
    }
}


