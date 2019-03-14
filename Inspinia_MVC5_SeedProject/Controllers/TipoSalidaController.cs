using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Attribute;

namespace ERP_ZORZAL.Controllers
{
    public class TipoSalidaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: /TipoSalida/
        [SessionManager("TipoSalida/Index")]
        public ActionResult Index()
        {
            return View(db.tbTipoSalida.ToList());
        }

        // GET: /TipoSalida/Details/5
        [SessionManager("TipoSalida/Details")]
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoSalida tbTipoSalida = db.tbTipoSalida.Find(id);
            if (tbTipoSalida == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbTipoSalida);
        }

        // GET: /TipoSalida/Create
        [SessionManager("TipoSalida/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("TipoSalida/Create")]
        public ActionResult Create([Bind(Include = "tsal_Id,tsal_Descripcion,tsal_UsuarioCrea,tsal_FechaCrea,tsal_UsarioModifica,tsal_FechaCrea")] tbTipoSalida tbTipoSalida)
        {
            if (db.tbTipoSalida.Any(a => a.tsal_Descripcion == tbTipoSalida.tsal_Descripcion))
            {
                ModelState.AddModelError("", "La Descripcion ya Existe.");

            }
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "0";
                    List = db.UDP_Inv_tbTipoSalida_Insert(tbTipoSalida.tsal_Descripcion, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Inv_tbTipoSalida_Insert_Result TipoSalida in List)
                        MsjError = TipoSalida.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("Error", "No se Guardo el registro , Contacte al Administrador");
                        return View(tbTipoSalida);
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

                    return View(tbTipoSalida);
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(tbTipoSalida);
        }


        // GET: /TipoSalida/Edit/5
        [SessionManager("TipoSalida/Edit")]
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoSalida tbTipoSalida = db.tbTipoSalida.Find(id);
           
            if (tbTipoSalida == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbTipoSalida);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("TipoSalida/Edit")]
        public ActionResult Edit(byte? id, [Bind(Include= "tsal_Id,tsal_Descripcion,tsal_UsuarioCrea,tsal_FechaCrea,tsal_UsuarioModifica,tsal_FechaModifica")] tbTipoSalida tbTipoSalida)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tbTipoSalida TipoSalida = db.tbTipoSalida.Find(id);
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Inv_tbTipoSalida_Update(tbTipoSalida.tsal_Id,
                                                            tbTipoSalida.tsal_Descripcion
                                                            , tbTipoSalida.tsal_UsuarioCrea
                                                                 , tbTipoSalida.tsal_FechaCrea
                                                             , Function.GetUser()
                                                                , Function.DatetimeNow());
                    foreach (UDP_Inv_tbTipoSalida_Update_Result tsal in List)
                        MsjError = tsal.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("Error", "No se Guardo el registro , Contacte al Administrador");
                        return View(tbTipoSalida);
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
                    return View(tbTipoSalida);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(tbTipoSalida);
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
        public JsonResult GetTipoSalidaExist(string Descripcion)
        {
            var list = db.tbTipoSalida.Where(s => s.tsal_Descripcion == Descripcion).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
     

    }
}
