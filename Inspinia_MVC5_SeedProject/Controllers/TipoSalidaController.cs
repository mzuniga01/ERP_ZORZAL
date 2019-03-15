//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using ERP_GMEDINA.Models;
//using ERP_GMEDINA.Attribute;
//using ERP_GMEDINA.Dataset;
//using ERP_GMEDINA.Dataset.ReportesTableAdapters;
//using ERP_GMEDINA.Reports;
//using CrystalDecisions.CrystalReports.Engine;
//using System.IO;
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
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using ERP_GMEDINA.Dataset;
using ERP_GMEDINA.Dataset.ReportesTableAdapters;
using ERP_GMEDINA.Reports;

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
            if (db.tbTipoSalida.Any(a => a.tsal_Descripcion == tbTipoSalida.tsal_Descripcion))
            {
                ModelState.AddModelError("", "La Descripcion ya Existe.");
                ViewBag.UsuarioCrea = db.tbUsuario.Find(tbTipoSalida.tsal_UsuarioCrea).usu_NombreUsuario;
            }
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
        public ActionResult Movimiento_Entre_Fechas()
        {
            var Encargado = Usuario();
            var EncargadoName = db.tbUsuario.Where(x => x.usu_Id == Encargado).Select(i => new { i.usu_Nombres, i.usu_Apellidos }).FirstOrDefault();

            ReportDocument rd = new ReportDocument();
            Stream stream = null;
            ERP_GMEDINA.Reports.Movimiento_Entre_Fechas inv = new ERP_GMEDINA.Reports.Movimiento_Entre_Fechas();
            ERP_GMEDINA.Dataset.Reportes SalidaDST = new ERP_GMEDINA.Dataset.Reportes();

            var SalidaTableAdapter = new UDV_Inv_Movimiento_Entre_FechasTableAdapter();

            try
            {
                SalidaTableAdapter.Fill(SalidaDST.UDV_Inv_Movimiento_Entre_Fechas);

                inv.SetDataSource(SalidaDST);
                inv.SetParameterValue("Usuario", EncargadoName.usu_Nombres + " " + EncargadoName.usu_Apellidos);
                stream = inv.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                inv.Close();
                inv.Dispose();

                string fileName = "Movimiento_Entre_Fechas.pdf";
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                return File(stream, "application/pdf");
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                throw;
            }
             
        }
        public int Usuario()
        {
            int idUser = 0;
            try
            {
                List<tbUsuario> User = Function.getUserInformation();
                foreach (tbUsuario Usuario in User)
                {
                    idUser = Convert.ToInt32(Usuario.emp_Id);
                }
                return idUser;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return 0;
            }
        }
        [HttpPost]
        public JsonResult GetTipoSalidaExist(string Descripcion)
        {
            var list = db.tbTipoSalida.Where(s => s.tsal_Descripcion == Descripcion).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
    
       
}
