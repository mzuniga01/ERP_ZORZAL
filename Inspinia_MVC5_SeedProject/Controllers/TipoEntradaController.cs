using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_ZORZAL.Controllers
{
    public class TipoEntradaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        // GET: /TipoEntrada/
        public ActionResult Index()
        {
            return View(db.tbTipoEntrada.ToList());
        }

        // GET: /TipoEntrada/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoEntrada tbTipoEntrada = db.tbTipoEntrada.Find(id);
            if (tbTipoEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoEntrada);
        }

        // GET: /TipoEntrada/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TipoEntrada/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="tent_Id,tent_Descripcion,tent_UsuarioCrea,tent_FechaCrea,tent_UsarioModifica,tent_FechaCrea")] tbTipoEntrada tbTipoEntrada)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> list = null;
                    var MsjError = "";
                    list = db.UDP_Inv_tbTipoEntrada_Insert(tbTipoEntrada.tent_Descripcion);
                    //list = db.udp_inv_tbtipoentrada_insert(tbtipoentrada.tent_descripcion,);
                    foreach (UDP_Inv_tbTipoEntrada_Insert_Result TipoEntrada in list)
                        MsjError = TipoEntrada.MensajeError;
                    if (MsjError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo almacenar el registro");
                        return View(tbTipoEntrada);
                    }
                    else
                    {
                        //db.tbTipoEntrada.Add(tbTipoEntrada);
                        //db.SaveChanges();
                        return RedirectToAction("Index");

                    }                        
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro");
                }
                return RedirectToAction("Index");
            }

            return View(tbTipoEntrada);
        }
       
        // GET: /TipoEntrada/Edit/5
        public ActionResult Edit(byte? id)
        {
            ViewBag.id = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoEntrada tbTipoEntrada = db.tbTipoEntrada.Find(id);
            ViewBag.UsuarioCrea_N = db.tbUsuario.Find(tbTipoEntrada.tent_UsuarioCrea).usu_NombreUsuario;
            ViewBag.UsuarioCrea_A = db.tbUsuario.Find(tbTipoEntrada.tent_UsuarioCrea).usu_Apellidos;
            var UsuarioModfica = tbTipoEntrada.tent_UsuarioModifica;
            if (UsuarioModfica == null)
            {
                ViewBag.UsuarioModifica = "";
            }
            else
            {
                ViewBag.UsuarioModifica_N = db.tbUsuario.Find(UsuarioModfica).usu_NombreUsuario;
                ViewBag.UsuarioModifica_A = db.tbUsuario.Find(UsuarioModfica).usu_NombreUsuario;
            };
            if (tbTipoEntrada == null)
            {
                return HttpNotFound();
            }
            ViewBag.tent_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioCrea);
            ViewBag.tent_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioModifica);

            return View(tbTipoEntrada);
        }

        // POST: /TipoEntrada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(byte? id,[Bind(Include= "tent_Id,tent_Descripcion,tent_UsuarioCrea,tent_FechaCrea, tent_UsuarioModifica, tent_FechaModifica")] tbTipoEntrada tbTipoEntrada)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbTipoEntrada TipoEntrada = db.tbTipoEntrada.Find(id);
                    IEnumerable<object> list = null;
                    string MsjError = "";
                    list = db.UDP_Inv_tbTipoEntrada_Update(tbTipoEntrada.tent_Id
                                                            ,tbTipoEntrada.tent_Descripcion
                                                            , tbTipoEntrada.tent_UsuarioCrea
                                                            , tbTipoEntrada.tent_FechaCrea);
                    foreach (UDP_Inv_tbTipoEntrada_Update_Result tent in list)
                        MsjError = tent.MensajeError;

                    if (MsjError.Substring(0, 2) == "-1")
                    {
                        ModelState.AddModelError("", "No se guardo el cambio");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //db.Entry(tbTipoEntrada).State = EntityState.Modified;
                        //db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se guardo el cambio");
                }
                return RedirectToAction("Index");
            }
            ViewBag.tent_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioCrea);
            ViewBag.tent_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoEntrada.tent_UsuarioModifica);
            return View(tbTipoEntrada);
        }

        // GET: /TipoEntrada/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoEntrada tbTipoEntrada = db.tbTipoEntrada.Find(id);
            if (tbTipoEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoEntrada);
        }

        // POST: /TipoEntrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbTipoEntrada tbTipoEntrada = db.tbTipoEntrada.Find(id);
            db.tbTipoEntrada.Remove(tbTipoEntrada);
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
