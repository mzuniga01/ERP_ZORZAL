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

namespace ERP_ZORZAL.Controllers
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
        public ActionResult Create()
        {
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega");
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion");

            //vista parcial de entrada detalle
            ViewBag.ent_Id = new SelectList(db.tbEntrada, "ent_Id", "ent_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.bod_Idd = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.Producto = db.tbProducto.ToList();
            return View();
        }

        // POST: /Entrada/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ent_NumeroFormato,ent_FechaElaboracion,bod_Id,estm_Id,prov_Id,ent_FacturaCompra,ent_FechaCompra,fact_Id,ent_RazonDevolucion,ent_BodegaDestino,tent_Id")] tbEntrada tbEntrada)
        {

            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbEntrada.bod_Id);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbEntrada.estm_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.prov_Id);
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            ViewBag.ent_BodegaDestino = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbEntrada.ent_BodegaDestino);
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Inv_tbEntrada_Insert(tbEntrada.ent_NumeroFormato, tbEntrada.ent_FechaElaboracion, tbEntrada.bod_Id, tbEntrada.estm_Id, tbEntrada.prov_Id, tbEntrada.ent_FacturaCompra, tbEntrada.ent_FechaCompra, tbEntrada.fact_Id, tbEntrada.ent_RazonDevolucion, tbEntrada.ent_BodegaDestino, tbEntrada.tent_Id);
                    foreach (UDP_Inv_tbEntrada_Insert_Result Entrada in List)
                        MsjError = Entrada.MensajeError;

                    if (MsjError.Substring(0, 2) == "-1")
                    {
                        ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }


                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro, Contacte al Administrador");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(tbEntrada);
        }
        // GET: /Entrada/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
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

            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbEntrada.bod_Id);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbEntrada.estm_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.prov_Id);
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            //vista parcial de entrada detalle
            ViewBag.ent_Id = new SelectList(db.tbEntrada, "ent_Id", "ent_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.Producto = db.tbProducto.ToList();
            return View(tbEntrada);
        }

        // POST: /Entrada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

            //para añadir datos temporales a la tabla
        [HttpPost]
        public JsonResult Guardardetalleentrada(tbEntrada entradadetalle)
        {
            List<tbEntrada> sessionCasoExito = new List<tbEntrada>();
            var list = (List<tbEntrada>)Session["CrearDetalleEntrada"];
            if (list == null)
            {
                sessionCasoExito.Add(entradadetalle);
                Session["CrearDetalleEntrada"] = sessionCasoExito;
            }
            else
            {
                list.Add(entradadetalle);
                Session["CrearDetalleEntrada"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }
        //para borrar registros en la tabla temporal
        [HttpPost]
        public JsonResult Eliminardetalleentrada(tbEntrada eliminardetalle)
        {
            var list = (List<tbEntrada>)Session["CrearDetalleEntrada"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.ent_Id == eliminardetalle.ent_Id);
                list.Remove(itemToRemove);
                Session["CrearDetalleEntrada"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "ent_Id,ent_NumeroFormato,ent_FechaElaboracion,bod_Id,estm_Id,prov_Id,ent_FacturaCompra,ent_FechaCompra,fact_Id,ent_RazonDevolucion,ent_BodegaDestino,tent_Id,ent_UsuarioCrea,ent_FechaCrea")] tbEntrada tbEntrada)
        {
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbEntrada.bod_Id);
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbEntrada.estm_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbEntrada.prov_Id);
            ViewBag.tent_Id = new SelectList(db.tbTipoEntrada, "tent_Id", "tent_Descripcion", tbEntrada.tent_Id);
            ViewBag.ent_BodegaDestino = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbEntrada.ent_BodegaDestino);
            ViewBag.Producto = db.tbProducto.ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    tbEntrada vEntrada = db.tbEntrada.Find(id);
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Inv_tbEntrada_Update(tbEntrada.ent_Id, tbEntrada.ent_NumeroFormato, tbEntrada.ent_FechaElaboracion, tbEntrada.bod_Id, tbEntrada.estm_Id, tbEntrada.prov_Id, tbEntrada.ent_FacturaCompra, tbEntrada.ent_FechaCompra, tbEntrada.fact_Id, tbEntrada.ent_RazonDevolucion, tbEntrada.ent_BodegaDestino, tbEntrada.tent_Id, vEntrada.ent_UsuarioCrea, vEntrada.ent_FechaCrea);
                    foreach (UDP_Inv_tbEntrada_Update_Result entrada in List)
                        MsjError = entrada.MensajeError;

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
            return View(tbEntrada);
        }

        // GET: /Entrada/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = await db.tbEntrada.FindAsync(id);
            if (tbEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbEntrada);
        }

        // POST: /Entrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tbEntrada tbEntrada = await db.tbEntrada.FindAsync(id);
            db.tbEntrada.Remove(tbEntrada);
            await db.SaveChangesAsync();
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
        public ActionResult _EditarDetalleEntrada()
        {
            return View();
        }

        public ActionResult _CrearDetalleEntrada()
        {
            return View();
        }

        public ActionResult _DetallesDeEntrada()
        {
            return View();
        }
        public ActionResult _IndexDetalleEntrada()
        {
            return View();
        }
    }
}
