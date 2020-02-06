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

namespace ERP_GMEDINA.Controllers
{
    public class UnidadMedidaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        Helpers Function = new Helpers();
        // GET: /UnidadMedida/
        [SessionManager("UnidadMedida/Index")]
        public ActionResult Index()
        {
            return View(db.tbUnidadMedida.ToList());
        }

        // GET: /UnidadMedida/Details/5
        [SessionManager("UnidadMedida/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            if (tbUnidadMedida == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbUnidadMedida);
        }

        // GET: /UnidadMedida/Create
        [SessionManager("UnidadMedida/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("UnidadMedida/Create")]
        public ActionResult Create([Bind(Include = "uni_Descripcion,uni_Abreviatura,uni_UsuarioCrea,uni_FechaCrea")] tbUnidadMedida tbUnidadMedida)
        {
            if (db.tbUnidadMedida.Any(a => a.uni_Descripcion == tbUnidadMedida.uni_Descripcion))
            {
                ModelState.AddModelError("", "La Unidad de Medida ya Existe, Agregue otra Unidad.");
                return View(tbUnidadMedida);

            }
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> List = null;
                    var MsjError = "0";
                    List = db.UDP_Gral_tbUnidadMedida_Insert(tbUnidadMedida.uni_Descripcion, tbUnidadMedida.uni_Abreviatura, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Gral_tbUnidadMedida_Insert_Result uni in List)
                        MsjError = uni.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("Error", "No se Guardo el registro , Contacte al Administrador");
                        return View(tbUnidadMedida);
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
                    return View(tbUnidadMedida);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(tbUnidadMedida);
        }

        // GET: /UnidadMedida/Edit/5
        [SessionManager("UnidadMedida/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            if (tbUnidadMedida == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbUnidadMedida);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("UnidadMedida/Edit")]
        public ActionResult Edit(int? id, [Bind(Include = "uni_Id,uni_Descripcion,uni_Abreviatura,uni_UsuarioCrea, uni_FechaCrea,uni_UsuarioModifica,uni_FechaModifica")] tbUnidadMedida tbUnidadMedida)
        {
            if (db.tbUnidadMedida.Any(a => a.uni_Descripcion == tbUnidadMedida.uni_Descripcion && a.uni_Id != tbUnidadMedida.uni_Id && a.uni_Abreviatura == tbUnidadMedida.uni_Abreviatura))
            {
                ModelState.AddModelError("", "La Descripcion ya Existe , Agregue otra Unidad.");

            }
            if (ModelState.IsValid)
            {
                try
                {
                    tbUnidadMedida UnidadMedida = db.tbUnidadMedida.Find(id);
                    IEnumerable<object> List = null;
                    var MsjError = "";
                    List = db.UDP_Gral_tbUnidadMedida_Update(tbUnidadMedida.uni_Id,
                                                                tbUnidadMedida.uni_Descripcion,
                                                                tbUnidadMedida.uni_Abreviatura,
                                                                tbUnidadMedida.uni_UsuarioCrea,
                                                                tbUnidadMedida.uni_FechaCrea
                                                                , Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Gral_tbUnidadMedida_Update_Result uni in List)
                        MsjError = uni.MensajeError;

                    if (MsjError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                        return View(tbUnidadMedida);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se guardó el registro , contacte al Administrador");
                    return View(tbUnidadMedida);
                }
            }
            return View(tbUnidadMedida);
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
