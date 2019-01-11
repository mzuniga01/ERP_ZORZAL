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
using System.Transactions;

namespace ERP_ZORZAL.Controllers
{
    public class SalidaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /Salida/
        public ActionResult Index()
        {
            var tbsalida = db.tbSalida.Include(t => t.tbBodega).Include(t => t.tbEstadoMovimiento).Include(t => t.tbFactura).Include(t => t.tbTipoSalida);
            return View(tbsalida.ToList());
        }
       

        // GET: /Salida/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            ViewBag.UsuarioCrea = db.tbUsuario.Find(tbSalida.sal_UsuarioCrea).usu_NombreUsuario;
            var UsuarioModifica = tbSalida.sal_UsuarioModifica;
            if (UsuarioModifica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModifica).usu_Nombres;
            };
            if (tbSalida == null)
            {
                return HttpNotFound();
            }
            ViewBag.ent_Id = new SelectList(db.tbSalida, "sal_Id", "sal_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            return View(tbSalida);
        }
      

        // GET: /Salida/Create
        public ActionResult Create()
        {
            //ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega");
            ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.box_Codigo = new SelectList(db.tbBox, "box_Codigo", "box_Descripcion");
            ViewBag.estm_Descripcion = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo");
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion");

            ViewBag.sald_Id = new SelectList(db.tbSalidaDetalle, "sadl_Id", "sald_Id");
            ViewBag.sal_Id = new SelectList(db.tbSalida, "sal_Id", "sal_Id");
            ViewBag.prod_Codigo = new SelectList(db.tbProducto, "prod_Codigo", "prod_Descripcion");
            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.Producto = db.tbProducto.ToList();


            return View();
        }

        // POST: /Salida/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bod_Id,fact_Id,sal_FechaElaboracion,estm_Id,tsal_Id,sal_RazonDevolucion")] tbSalida tbSalida)
        {
            ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.box_Codigo = new SelectList(db.tbBox, "box_Codigo", "box_Descripcion");
            ViewBag.estm_Descripcion = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo");
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion");

            var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];
            var MensajeError = "0";
            var MensajeErrorDetalle = "0";
            IEnumerable<object> listSalida = null;
            IEnumerable<object> listSalidaDetalle = null;
            if (ModelState.IsValid)
            {
                try
                {
                    using (TransactionScope Tran = new TransactionScope())
                    {
                        listSalida = db.UDP_Inv_tbSalida_Insert(tbSalida.bod_Id, tbSalida.fact_Id, tbSalida.sal_FechaElaboracion, tbSalida.estm_Id, tbSalida.tsal_Id, tbSalida.sal_RazonDevolucion);
                        foreach (UDP_Inv_tbSalida_Insert_Result Salida in listSalida)
                            MensajeError = Salida.MensajeError;
                        if (MensajeError == "-1")
                        {
                            ModelState.AddModelError("", "No se pudo agregar el registro");
                            return View(tbSalida);
                        }
                        else
                        {
                            var MsjError = Convert.ToInt32(MensajeError);
                            if (MsjError == tbSalida.sal_Id)
                            {
                                if (list != null)
                                {
                                    if (list.Count != 0)
                                    {
                                        foreach (tbSalidaDetalle Detalle in list)
                                        {
                                            var box_Codigo = "";
                                            Detalle.box_Codigo = MensajeError;
                                            listSalidaDetalle = db.UDP_Inv_tbSalidaDetalle_Insert(
                                                Detalle.sal_Id,
                                                Detalle.prod_Codigo,
                                                Detalle.sal_Cantidad,
                                                box_Codigo
                                                );
                                            foreach (UDP_Inv_tbSalida_Insert_Result spDetalle in listSalidaDetalle)
                                            {
                                                MensajeErrorDetalle = spDetalle.MensajeError;
                                                if (MensajeError == "-1")
                                                {
                                                    ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                    return View(tbSalida);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo agregar el registro");
                                return View(tbSalida);
                            }

                        }
                        Tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo agregar el registros" + Ex.Message.ToString());
                    ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                    ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");

                    ViewBag.Denominacion = db.tbDenominacion.ToList();
                    List<tbMoneda> MonedaList = db.tbMoneda.ToList();
                    ViewBag.MonedaList = new SelectList(MonedaList, "mnda_Id", "mnda_Nombre");

                    ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();
                }

            }

            return View(tbSalida);
        }




        //{
            
        //    if (ModelState.IsValid)
        //    {

        //        try
        //        {
        //            IEnumerable<object> List = null;
        //            var MsjError = "";
        //List = db.UDP_Inv_tbSalida_Insert(tbSalida.bod_Id, tbSalida.fact_Id, tbSalida.sal_FechaElaboracion, tbSalida.estm_Id, tbSalida.tsal_Id, tbSalida.sal_RazonDevolucion);
        //            foreach (UDP_Inv_tbSalida_Insert_Result Salida in List)
        //                MsjError = Salida.MensajeError;

        //            if (MsjError == "-1")
        //            {
        //                ModelState.AddModelError("", "No se guardo el registro, Contacte al Administrador");
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index");
        //            }


        //        }
        //        catch (Exception Ex)
        //        {
        //            Ex.Message.ToString();
        //            ModelState.AddModelError("", "No se Guardo el registro , Contacte al Administrador");
        //        }
        //    }
        //    else
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors);
        //    }

        //    return View(tbSalida);
        //}

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            //ViewBag.UsuarioCrea = db.tbUsuario.Find(tbSalida.sal_UsuarioCrea).usu_NombreUsuario;
            var UsuarioModifica = tbSalida.sal_UsuarioModifica;
            if (UsuarioModifica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica = db.tbUsuario.Find(UsuarioModifica).usu_NombreUsuario;
            };
            if (tbSalida == null)
            {
                return HttpNotFound();
            }
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSalida.bod_Id);
            ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            ViewBag.estm_Id = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion", tbSalida.estm_Id);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbSalida.fact_Id);
            ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion", tbSalida.tsal_Id);

            return View(tbSalida);
        }

        // POST: /Salida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public JsonResult SaveSalidaDetalle(tbSalidaDetalle SalidaDetalle)
        {
            List<tbSalidaDetalle> sessionSalidaDetalle = new List<tbSalidaDetalle>();
            var list = (List<tbSalidaDetalle>)Session["SalidaDetalle"];
            if (list == null)
            {
                sessionSalidaDetalle.Add(SalidaDetalle);
                Session["SalidaDetalle"] = sessionSalidaDetalle;
            }
            else
            {
                list.Add(SalidaDetalle);
                Session["SalidaDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveSalidaDetalle(tbSalidaDetalle SalidaDetalle)
        {
            var list = (List<tbSalidaDetalle>)Session["tbSalidaDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.sald_UsuarioCrea == SalidaDetalle.sald_UsuarioCrea);
                list.Remove(itemToRemove);
                Session["tbSalidaDetalle"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include= "sal_Id,bod_Id,fact_Id,sal_FechaElaboracion,estm_Id,box_Codigo,tsal_Id, sal_RazonDevolucion,sal_UsuarioCrea,sal_FechaCrea, tbUsuario, tbUsuario1")]tbSalida tbSalida)
        {
            if (ModelState.IsValid)
            {
                ViewBag.bod_Nombre = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
                ViewBag.box_Codigo = new SelectList(db.tbBox, "box_Codigo", "box_Descripcion");
                ViewBag.estm_Descripcion = new SelectList(db.tbEstadoMovimiento, "estm_Id", "estm_Descripcion");
                ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo");
                ViewBag.tsal_Id = new SelectList(db.tbTipoSalida, "tsal_Id", "tsal_Descripcion");
                try
                {
                    tbSalida vtbSalida = db.tbSalida.Find(id);

                    IEnumerable<object> List = null;
                    string MsjError = "";
                    //List = db.UDP_Inv_tbSalida_Update(tbSalida.sal_Id, tbSalida.bod_Id, tbSalida.fact_Id, tbSalida.sal_FechaElaboracion, tbSalida.estm_Id, tbSalida.box_Codigo, tbSalida.tsal_Id, tbSalida.sal_RazonDevolucion, vtbSalida.sal_UsuarioCrea, vtbSalida.sal_FechaCrea);
                    foreach (UDP_Inv_tbSalida_Update_Result Salida in List)
                        MsjError = Salida.MensajeError;

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
                    ModelState.AddModelError("", "No se Actualizo el registro , Contacte al Administrador");
                }
            }
            return View(tbSalida);
        }

        // GET: /Salida/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            if (tbSalida == null)
            {
                return HttpNotFound();
            }
            return View(tbSalida);
        }

        // POST: /Salida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSalida tbSalida = db.tbSalida.Find(id);
            db.tbSalida.Remove(tbSalida);
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


        

        public ActionResult _EditarDetalle()
        {
            return View();
        }

        public ActionResult _CreateSalidaDetalle()
        {
            return View();
        }

        public ActionResult _DetalleSalida()
        {
            return View();
        }
        public ActionResult _IndexSalidaDetalle()
        {
            return View();
        }




    }
    }

