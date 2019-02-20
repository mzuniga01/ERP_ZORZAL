using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class DocumentoFiscalController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: /DocumentoFiscal/
        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("DocumentoFiscal/Index"))
                    {
                        var tbdocumentofiscal = db.tbDocumentoFiscal.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
                        return View(tbdocumentofiscal.ToList());
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

        // GET: /DocumentoFiscal/Details/5
        public ActionResult Details(string id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("DocumentoFiscal/Details"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbDocumentoFiscal tbDocumentoFiscal = db.tbDocumentoFiscal.Find(id);
                        if (tbDocumentoFiscal == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        return View(tbDocumentoFiscal);
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

        // GET: /DocumentoFiscal/Create
        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("DocumentoFiscal/Create"))
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "dfisc_Id,dfisc_Descripcion,dfisc_UsuarioCrea,dfisc_FechaCrea,dfisc_UsuarioModifica,dfisc_FechaModifica")] tbDocumentoFiscal tbDocumentoFiscal)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("DocumentoFiscal/Create"))
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                //////////Aqui va la lista//////////////
                                string MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbDocumentoFiscal_Insert(tbDocumentoFiscal.dfisc_Id,
                                    tbDocumentoFiscal.dfisc_Descripcion, Function.GetUser(),
                                                Function.DatetimeNow());
                                foreach (UDP_Vent_tbDocumentoFiscal_Insert_Result DocumentoFiscal in list)
                                    MensajeError = DocumentoFiscal.MensajeError;
                                if (MensajeError.StartsWith("-1"))
                                {
                                    Function.InsertBitacoraErrores("DocumentoFiscal/Create", MensajeError, "Create");
                                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                    return View(tbDocumentoFiscal);
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                            catch (Exception Ex)
                            {
                                Function.InsertBitacoraErrores("DocumentoFiscal/Create", Ex.Message.ToString(), "Create");
                                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                return View(tbDocumentoFiscal);
                            }
                        }
                        return View(tbDocumentoFiscal);
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

        // GET: /DocumentoFiscal/Edit/5
        public ActionResult Edit(string id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("DocumentoFiscal/Edit"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbDocumentoFiscal tbDocumentoFiscal = db.tbDocumentoFiscal.Find(id);
                        if (tbDocumentoFiscal == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        return View(tbDocumentoFiscal);
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

        // POST: /DocumentoFiscal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dfisc_Id,dfisc_Descripcion,dfisc_UsuarioCrea,dfisc_FechaCrea,dfisc_UsuarioModifica,dfisc_FechaModifica")] tbDocumentoFiscal tbDocumentoFiscal)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("DocumentoFiscal/Edit"))
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                //////////Aqui va la lista//////////////
                                string MensajeError = "";
                                IEnumerable<object> list = null;
                                list = db.UDP_Vent_tbDocumentoFiscal_Update(tbDocumentoFiscal.dfisc_Id,
                                    tbDocumentoFiscal.dfisc_Descripcion,
                                    tbDocumentoFiscal.dfisc_UsuarioCrea,
                                    tbDocumentoFiscal.dfisc_FechaCrea,
                                    Function.GetUser(),
                                                Function.DatetimeNow());
                                foreach (UDP_Vent_tbDocumentoFiscal_Update_Result DocumentoFiscal in list)
                                    MensajeError = DocumentoFiscal.MensajeError;
                                if (MensajeError.StartsWith("-1"))
                                {
                                    Function.InsertBitacoraErrores("DocumentoFiscal/Edit", MensajeError, "Edit");
                                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                    return View(tbDocumentoFiscal);
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                            catch (Exception Ex)
                            {
                                Function.InsertBitacoraErrores("DocumentoFiscal/Edit", Ex.Message.ToString(), "Edit");
                                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                return View(tbDocumentoFiscal);
                            }
                        }
                        return View(tbDocumentoFiscal);
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
