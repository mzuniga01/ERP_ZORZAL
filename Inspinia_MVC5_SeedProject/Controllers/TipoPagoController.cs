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
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class TipoPagoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        [SessionManager("TipoPago/Index")]

        // GET: /TipoPago/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("TipoPago/Index"))
                    {
                        var tbtipopago = db.tbTipoPago.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
                        return View(tbtipopago.ToList());
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

        // GET: /TipoPago/Details/5
        [SessionManager("TipoPago/Details")]
        public ActionResult Details(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("TipoPago/Details"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
                        if (tbTipoPago == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        return View(tbTipoPago);
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

        // GET: /TipoPago/Create
        [SessionManager("TipoPago/Create")]
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("TipoPago/Create"))
                    {
                        return View();
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

        // POST: /TipoPago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("TipoPago/Create")]
        public ActionResult Create([Bind(Include = "tpa_Id,tpa_Descripcion,tpa_Emisor,tpa_Cuenta,tpa_FechaVencimiento,tpa_Titular,tpa_UsuarioCrea,tpa_FechaCrea,tpa_UsuarioModifica,tpa_FechaModifica")] tbTipoPago tbTipoPago)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("TipoPago/Create"))
                       
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                if (db.tbTipoPago.Any(a => a.tpa_Descripcion == tbTipoPago.tpa_Descripcion))
                                {
                                    ModelState.AddModelError("", "Ya existe este tipo de pago.");
                                    return View(tbTipoPago);
                                }
                                else
                                {
                                    string MensajeError = "";
                                    IEnumerable<object> list = null;
                                    list = db.UDP_Vent_tbTipoPago_Insert(tbTipoPago.tpa_Descripcion, tbTipoPago.tpa_Emisor, tbTipoPago.tpa_Cuenta, tbTipoPago.tpa_FechaVencimiento, tbTipoPago.tpa_Titular, Function.GetUser(), Function.DatetimeNow());
                                        foreach (UDP_Vent_tbTipoPago_Insert_Result tipopago in list)
                                            MensajeError = tipopago.MensajeError.ToString();
                                    if (MensajeError.StartsWith("-1"))
                                    {
                                        Function.InsertBitacoraErrores("TipoPago/Create", MensajeError, "Create");
                                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                        return View(tbTipoPago);
                                    }
                                    else
                                    {
                                        return RedirectToAction("Index");
                                    }

                                    }
                            }
                            catch (Exception Ex)
                            {
                                Function.InsertBitacoraErrores("TipoPago/Create", Ex.Message.ToString(), "Create");
                                ModelState.AddModelError("", "No se ha podido ingresar el registro, favor contacte al administrador ");
                                return View(tbTipoPago);
                            }    
                        }
                        
                        return View(tbTipoPago);
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

        // GET: /TipoPago/Edit/5
        [SessionManager("TipoPago/Edit")]
        public ActionResult Edit(short? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("TipoPago/Edit"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
                        if (tbTipoPago == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                       
                        return View(tbTipoPago);
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

        // POST: /TipoPago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("TipoPago/Edit")]
        public ActionResult Edit([Bind(Include= "tpa_Id,tpa_Descripcion,tpa_Emisor,tpa_Cuenta,tpa_FechaVencimiento,tpa_Titular,tpa_UsuarioCrea,tpa_FechaCrea,tpa_UsuarioModifica,tpa_FechaModifica, tbUsuario, tbUsuario1")] tbTipoPago tbTipoPago)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("TipoPago/Edit"))
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                //if (db.tbTipoPago.Any(a => a.tpa_Descripcion == tbTipoPago.tpa_Descripcion))
                                //{
                                //    ModelState.AddModelError("", "Ya existe  tipo de pago.");
                                //    return View(tbTipoPago);
                                //}
                                //else
                                //{
                                string MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbTipoPago_Update(tbTipoPago.tpa_Id, tbTipoPago.tpa_Descripcion, tbTipoPago.tpa_Emisor, tbTipoPago.tpa_Cuenta, tbTipoPago.tpa_FechaVencimiento, tbTipoPago.tpa_Titular, tbTipoPago.tpa_UsuarioCrea, tbTipoPago.tpa_FechaCrea, Function.GetUser(), Function.DatetimeNow());
                                foreach (UDP_Vent_tbTipoPago_Update_Result tipopago in list)
                                    MensajeError = tipopago.MensajeError.ToString();
                                if (MensajeError.StartsWith("-1"))
                                {
                                    Function.InsertBitacoraErrores("TipoPago/Edit", MensajeError, "Edit");
                                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                                    return View(tbTipoPago);
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                                //}
                            }
                            catch (Exception Ex)
                            {
                                Function.InsertBitacoraErrores("TipoPago/Edit", Ex.Message.ToString(), "Edit");
                                ModelState.AddModelError("", "No se ha podido actualizar el registro, favor contacte al administrador");
                                return View(tbTipoPago);
                            }
                        }
                        
                        return View(tbTipoPago);
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

        // GET: /TipoPago/Delete/
        // POST: /TipoPago/Delete/5
        

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
