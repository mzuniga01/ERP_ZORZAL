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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace ERP_GMEDINA.Controllers
{
    public class SolicitudEfectivoController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();

        public ActionResult Index()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudEfectivo/Index"))
                {
                    return View(db.UDP_Vent_SolicituEfectivo_Select.Where(a => a.Anulada == false).ToList());
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public ActionResult IndexDetails()
        {
            return View(db.UDP_Vent_SolicituEfectivo_Detalles_Select);
        }

        // GET: /SolicitudEfectivo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            if (tbSolicitudEfectivo == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbSolicitudEfectivo);
        }

        // GET: /SolicitudEfectivo/Create
        public JsonResult GetModena()
        {
            ERP_ZORZALEntities db = new ERP_ZORZALEntities();
            var moneda = db.tbMoneda.Select(x => new { mnda_Id = x.mnda_Id, Text = x.mnda_Nombre }).Distinct();
            return Json(moneda, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDenominacion(short moneda)
        {
            ERP_ZORZALEntities db = new ERP_ZORZALEntities();
            db.Configuration.ProxyCreationEnabled = false;
            List<tbDenominacion> Denomination = db.tbDenominacion.Where(x => x.mnda_Id == moneda).OrderByDescending(x => x.deno_valor).ToList();
            return Json(Denomination, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDenominacionList(int mnda_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<tbDenominacion> DenominacionList = db.tbDenominacion.Where(x => x.mnda_Id == mnda_Id).ToList();
            return Json(DenominacionList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GDatosEncabezados()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudEfectivo/Create"))
                {
                    int idUser = 0;
                    GeneralFunctions Login = new GeneralFunctions();
                    List<tbUsuario> User = Login.getUserInformation();
                    foreach (tbUsuario Usuario in User)
                    {
                        idUser = Convert.ToInt32(Usuario.emp_Id);
                    }
                    var list = db.UDP_Vent_tbSolicitudEfectivo_Recargo(idUser).ToList();
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult GDatosEncabezado()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudEfectivo/Create"))
                {
                    int idUser = 0;
                    GeneralFunctions Login = new GeneralFunctions();
                    List<tbUsuario> User = Login.getUserInformation();
                    foreach (tbUsuario Usuario in User)
                    {
                        idUser = Convert.ToInt32(Usuario.emp_Id);
                    }
                    var list = db.UDP_Vent_tbSolicitudEfectivo_DatosEncabezado(idUser).ToList();
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public ActionResult Create()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("SolicitudEfectivo/Create"))
                    {
                        ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                        ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                        ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
                        ViewBag.Denominacion = db.tbDenominacion.ToList();
                        List<tbMoneda> MonedaList = db.tbMoneda.ToList();
                        ViewBag.MonedaList = new SelectList(MonedaList, "mnda_Id", "mnda_Nombre");
                        ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();
                        Session["Solicitud"] = null;
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

        // POST: /SolicitudEfectivo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "solef_Id,mocja_Id,solef_EsApertura,solef_FechaEntrega,solef_UsuarioEntrega,mnda_Id,solef_EsAnulada,solef_UsuarioCrea,solef_FechaCrea,solef_UsuarioModifica,solef_FechaModifica")] tbSolicitudEfectivo tbSolicitudEfectivo, List<tbSolicitudEfectivoDetalle> procesoData)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("SolicitudEfectivo/Create"))
                    {
                        ViewBag.Denominacion = db.tbDenominacion.ToList();
                        var list = (List<tbSolicitudEfectivoDetalle>)Session["Solicitud"];
                        string MensajeError = "";
                        var MensajeErrorDetalle = "";
                        IEnumerable<object> listSolicitudEfectivo = null;
                        IEnumerable<object> listSolicitudEfectivoDetalle = null;
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                using (TransactionScope Tran = new TransactionScope())
                                {
                                    listSolicitudEfectivo = db.UDP_Vent_tbSolicitudEfectivo_Insert(
                                                            tbSolicitudEfectivo.mocja_Id,
                                                            tbSolicitudEfectivo.mnda_Id,
                                                            Function.GetUser(),
                                                            Function.DatetimeNow()
                                                            );
                                    foreach (UDP_Vent_tbSolicitudEfectivo_Insert_Result SolicitudE in listSolicitudEfectivo)
                                        MensajeError = SolicitudE.MensajeError;
                                    if (MensajeError.StartsWith("-1"))
                                    {
                                        ModelState.AddModelError("", "No se pudo agregar el registro");
                                        return View(tbSolicitudEfectivo);
                                    }
                                    else
                                    {
                                        if (!MensajeError.StartsWith("-1"))
                                        {
                                            if (list != null)
                                            {
                                                if (list.Count != 0)
                                                {
                                                    foreach (tbSolicitudEfectivoDetalle Detalle in list)
                                                    {

                                                        Detalle.solef_Id = Convert.ToInt32(MensajeError);
                                                        listSolicitudEfectivoDetalle = db.UDP_Vent_tbSolicitudEfectivoDetalle_Insert(
                                                            Detalle.solef_Id,
                                                            Detalle.deno_Id,
                                                            Detalle.soled_CantidadSolicitada,
                                                             Function.GetUser(),
                                                             Function.DatetimeNow()
                                                            );
                                                        foreach (UDP_Vent_tbSolicitudEfectivoDetalle_Insert_Result spDetalle in listSolicitudEfectivoDetalle)
                                                        {
                                                            MensajeErrorDetalle = spDetalle.MensajeError;
                                                            if (MensajeError.StartsWith("-1"))
                                                            {
                                                                ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                                return View(tbSolicitudEfectivo);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "No se pudo agregar el registro");
                                            return View(tbSolicitudEfectivo);
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
                                ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
                                ViewBag.Denominacion = db.tbDenominacion.ToList();
                                List<tbMoneda> MonedaList = db.tbMoneda.ToList();
                                ViewBag.MonedaList = new SelectList(MonedaList, "mnda_Id", "mnda_Nombre");
                                ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
                                ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();
                                ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
                            }
                        }
                        ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
                        ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
                        ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
                        ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
                        ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);
                        List<tbMoneda> MonedaLists = db.tbMoneda.ToList();
                        ViewBag.MonedaLists = new SelectList(MonedaLists, "mnda_Id", "mnda_Nombre");
                        ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
                        return View(tbSolicitudEfectivo);
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

        public ActionResult EditEntregaEfectivo(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudEfectivo/EditEntregaEfectivo"))
                {

                    int idUser = 0;
                    GeneralFunctions Login = new GeneralFunctions();
                    List<tbUsuario> User = Login.getUserInformation();
                    foreach (tbUsuario Usuario in User)
                    {
                        idUser = Convert.ToInt32(Usuario.emp_Id);
                    }
                    //////////////////////////////////
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
                    if (tbSolicitudEfectivo == null)
                    {
                        return RedirectToAction("NotFound", "Login");
                    }
                    ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
                    ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
                    //// ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
                    ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
                    //   ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
                    ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);
                    ///////////////////////////
                    ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                    var suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                    ViewBag.UsuarioEntrega = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_NombreUsuario).SingleOrDefault();
                    ViewBag.solef_UsuarioEntrega = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();


                    ViewBag.Denominacion = db.tbDenominacion.ToList();


                    ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();




                    return View(tbSolicitudEfectivo);
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEntregaEfectivo([Bind(Include = "solef_Id,mocja_Id,solef_EsApertura,solef_FechaEntrega,solef_UsuarioEntrega,mnda_Id,solef_EsAnulada,solef_UsuarioCrea,solef_FechaCrea,solef_UsuarioModifica,solef_FechaModifica,tbUsuario,tbUsuario2")] tbSolicitudEfectivo tbSolicitudEfectivo)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("SolicitudEfectivo/EditEntregaEfectivo"))
                {
                    if (ModelState.IsValid)
                    {

                        try
                        {
                            string MensajeError = "";
                            IEnumerable<object> list = null;
                            list = db.UDP_Vent_tbSolicitudEfectivo_Update_Entrega(
                                tbSolicitudEfectivo.solef_Id,
                                tbSolicitudEfectivo.mocja_Id,
                                tbSolicitudEfectivo.solef_EsApertura,
                                tbSolicitudEfectivo.solef_FechaEntrega,
                                tbSolicitudEfectivo.solef_UsuarioEntrega,
                                tbSolicitudEfectivo.mnda_Id,
                                tbSolicitudEfectivo.solef_EsAnulada,
                                tbSolicitudEfectivo.solef_UsuarioCrea,
                                tbSolicitudEfectivo.solef_FechaCrea);
                            foreach (UDP_Vent_tbSolicitudEfectivo_Update_Entrega_Result solicitud in list)
                                MensajeError = solicitud.MensajeError;
                            if (MensajeError == "-1")
                            {
                                ModelState.AddModelError("", "No se pudo actualizar el registro detalle");
                                return View(tbSolicitudEfectivo);
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        catch (Exception Ex)
                        {
                            ModelState.AddModelError("", "No se pudo actualizar el registros" + Ex.Message.ToString());
                            ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
                            ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
                            ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
                            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
                            ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);

                            ViewBag.Denominacion = db.tbDenominacion.ToList();


                            ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();
                        }

                        return RedirectToAction("Index");
                    }
                    ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
                    ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
                    ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
                    ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);
                    return View(tbSolicitudEfectivo);


                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // GET: /SolicitudEfectivo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("SolicitudEfectivo/Edit"))
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index");
                        }
                        tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
                        if (tbSolicitudEfectivo == null)
                        {
                            return RedirectToAction("NotFound", "Login");
                        }
                        ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
                        ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
                        ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
                        ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
                        ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
                        ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);
                        ViewBag.Denominacion = db.tbDenominacion.ToList();
                        ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();
                        return View(tbSolicitudEfectivo);
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

        // POST: /SolicitudEfectivo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "solef_Id,mocja_Id,solef_EsApertura,solef_FechaEntrega,solef_UsuarioEntrega,mnda_Id,solef_EsAnulada,solef_UsuarioCrea,solef_FechaCrea,solef_UsuarioModifica,solef_FechaModifica,tbUsuario,tbUsuario2")] tbSolicitudEfectivo tbSolicitudEfectivo)
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetRol())
                {
                    if (Function.GetUserRols("SolicitudEfectio/Edit"))
                    {
                        ViewBag.Denominacion = db.tbDenominacion.ToList();
                        var list = (List<tbSolicitudEfectivoDetalle>)Session["Solicitud"];
                        string MensajeError = "";
                        var MensajeErrorDetalle = "";
                        IEnumerable<object> listSolicitudEfectivo = null;
                        IEnumerable<object> listSolicitudEfectivoDetalle = null;
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                using (TransactionScope Tran = new TransactionScope())
                                {
                                    listSolicitudEfectivo = db.UDP_Vent_tbSolicitudEfectivo_Update(
                                                            tbSolicitudEfectivo.solef_Id,
                                                            tbSolicitudEfectivo.mocja_Id,
                                                            tbSolicitudEfectivo.solef_EsApertura,
                                                            tbSolicitudEfectivo.mnda_Id,
                                                            tbSolicitudEfectivo.solef_EsAnulada,
                                                            tbSolicitudEfectivo.solef_UsuarioCrea,
                                                            tbSolicitudEfectivo.solef_FechaCrea,
                                                            Function.GetUser(),
                                                    Function.DatetimeNow()
                                                            );
                                    foreach (UDP_Vent_tbSolicitudEfectivo_Update_Result SolicitudE in listSolicitudEfectivo)
                                        MensajeError = SolicitudE.MensajeError;
                                    if (MensajeError.StartsWith("-1"))
                                    {
                                        ModelState.AddModelError("", "No se pudo agregar el registro");
                                        return View(tbSolicitudEfectivo);
                                    }
                                    else
                                    {
                                        if (!MensajeError.StartsWith("-1"))
                                        {
                                            if (list != null)
                                            {
                                                if (list.Count != 0)
                                                {
                                                    foreach (tbSolicitudEfectivoDetalle Detalle in list)
                                                    {

                                                        Detalle.solef_Id = Convert.ToInt32(MensajeError);
                                                        listSolicitudEfectivoDetalle = db.UDP_Vent_tbSolicitudEfectivoDetalle_Insert(
                                                            Detalle.solef_Id,
                                                            Detalle.deno_Id,
                                                            Detalle.soled_CantidadSolicitada,
                                                            Function.GetUser(),
                                                            Function.DatetimeNow());
                                                        foreach (UDP_Vent_tbSolicitudEfectivoDetalle_Insert_Result spDetalle in listSolicitudEfectivoDetalle)
                                                        {
                                                            MensajeErrorDetalle = spDetalle.MensajeError;
                                                            if (MensajeError == "-1")
                                                            {
                                                                ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                                return View(tbSolicitudEfectivo);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "No se pudo agregar el registro");
                                            return View(tbSolicitudEfectivo);
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
                                ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
                                ViewBag.Denominacion = db.tbDenominacion.ToList();
                                List<tbMoneda> MonedaList = db.tbMoneda.ToList();
                                ViewBag.MonedaList = new SelectList(MonedaList, "mnda_Id", "mnda_Nombre");
                                ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
                                ViewBag.SolicitudEdectivoDetalle = db.tbSolicitudEfectivoDetalle.ToList();
                                ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
                            }
                        }
                        ViewBag.solef_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioCrea);
                        ViewBag.solef_UsuarioEntrega = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioEntrega);
                        ViewBag.solef_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSolicitudEfectivo.solef_UsuarioModifica);
                        ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
                        ViewBag.mocja_Id = new SelectList(db.tbMovimientoCaja, "mocja_Id", "mocja_Id", tbSolicitudEfectivo.mocja_Id);
                        List<tbMoneda> MonedaLists = db.tbMoneda.ToList();
                        ViewBag.MonedaLists = new SelectList(MonedaLists, "mnda_Id", "mnda_Nombre");
                        ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre");
                        return View(tbSolicitudEfectivo);
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

        // GET: /SolicitudEfectivo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            if (tbSolicitudEfectivo == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbSolicitudEfectivo);
        }

        // POST: /SolicitudEfectivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSolicitudEfectivo tbSolicitudEfectivo = db.tbSolicitudEfectivo.Find(id);
            db.tbSolicitudEfectivo.Remove(tbSolicitudEfectivo);
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
        public JsonResult SaveSolicitudEfectivoDetalle(tbSolicitudEfectivoDetalle SolicitudEfeDetalleC)
        {
            List<tbSolicitudEfectivoDetalle> sessionSolicitudDetalle = new List<tbSolicitudEfectivoDetalle>();
            var list = (List<tbSolicitudEfectivoDetalle>)Session["Solicitud"];
            if (list == null)
            {
                sessionSolicitudDetalle.Add(SolicitudEfeDetalleC);
                Session["Solicitud"] = sessionSolicitudDetalle;
            }
            else
            {
                list.Add(SolicitudEfeDetalleC);
                Session["Solicitud"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateSolicitudEfectivoDetalle(tbSolicitudEfectivoDetalle EditarSolicitudEfectivoDetalle)
        {
            try
            {
                string MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Vent_tbSolicitudEfectivoDetalle_Update(
                            EditarSolicitudEfectivoDetalle.soled_Id,
                            EditarSolicitudEfectivoDetalle.deno_Id,
                            EditarSolicitudEfectivoDetalle.soled_CantidadSolicitada,
                            EditarSolicitudEfectivoDetalle.soled_CantidadEntregada,
                            EditarSolicitudEfectivoDetalle.soled_MontoEntregado,
                            EditarSolicitudEfectivoDetalle.soled_UsuarioCrea,
                            EditarSolicitudEfectivoDetalle.soled_FechaCrea,
                            Function.GetUser(),
                            Function.DatetimeNow());
                foreach (UDP_Vent_tbSolicitudEfectivoDetalle_Update_Result SolicitudDetalle in list)
                    MensajeError = SolicitudDetalle.MensajeError;
                if (MensajeError == "-1")
                {
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");

                    return PartialView("_EditarSolicitudEfectivoDetalle");
                }
                else
                {
                    return RedirectToAction("Index");
                    }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return PartialView("_EditarSolicitudEfectivoDetalle", EditarSolicitudEfectivoDetalle);
            }
        }

        [HttpPost]
        public JsonResult SaveEditSolicitudEfectivoDetalle(tbSolicitudEfectivoDetalle tbSolicitudEfectivoDetalle)
        {
            string MensajeEdit = "";
            try
            {
                string MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Vent_tbSolicitudEfectivoDetalle_Update(
                            tbSolicitudEfectivoDetalle.soled_Id,
                            tbSolicitudEfectivoDetalle.deno_Id,
                            tbSolicitudEfectivoDetalle.soled_CantidadSolicitada,
                            tbSolicitudEfectivoDetalle.soled_CantidadEntregada,
                            tbSolicitudEfectivoDetalle.soled_MontoEntregado,
                            tbSolicitudEfectivoDetalle.soled_UsuarioCrea,
                            tbSolicitudEfectivoDetalle.soled_FechaCrea,
                            Function.GetUser(),
                            Function.DatetimeNow());
                foreach (UDP_Vent_tbSolicitudEfectivoDetalle_Update_Result solicitudefectivodetalle in list)
                    MensajeError = solicitudefectivodetalle.MensajeError;
                MensajeEdit = "El registro se guardó exitosamente";
                if (MensajeError == "-1")
                {
                    MensajeEdit = "No se pudo actualizar el registro, favor contacte al administrador.";
                    ModelState.AddModelError("", MensajeEdit);
                }
            }
            catch (Exception Ex)
            {
                MensajeEdit = Ex.Message.ToString();
                ModelState.AddModelError("", MensajeEdit);
            }
            return Json(MensajeEdit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AnularSolcitudEfectivo(int solefId, bool Anulada, string Motivo)
        {
            var list = db.UDP_Vent_tbSolicitudEfectivo_EsAnulada(solefId, Anulada, Motivo).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertDetalleSolicitudDetalle(List<tbSolicitudEfectivoDetalle> procesoData)
        {
            if (procesoData == null)
            {
                Session["Solicitud"] = procesoData;
            }
            else
            {
                Session["Solicitud"] = procesoData;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        //______________________________Añadir Detalle_______________________________________//
        [HttpGet]
        public ActionResult GetAddDenominacion(short DENOID)
        {
            var list = db.UDP_Vent_tbSolicitudEfectivoDetalle_Detalle(DENOID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        //__________________DETALLES_____SOLICITUD____________
        [HttpGet]
        public ActionResult GetDtalle(short Solictud)
        {
            var list = db.UDP_Vent_tbSolicitudEfectivo_Details(Solictud).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //''''''''''''''''''''Dweetallle update'''''''''''''''''''''
        [HttpPost]
        public JsonResult SaveEditSolicitudEfectivoDetalleCantidad(tbSolicitudEfectivoDetalle tbSolicitudEfectivoDetalle)
        {
            string MensajeEdit = "";
            try
            {
                string MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Vent_tbSolicitudEfectivoDetalle_CantidadSolicitada_Update(
                            tbSolicitudEfectivoDetalle.soled_Id,
                            tbSolicitudEfectivoDetalle.deno_Id,
                            tbSolicitudEfectivoDetalle.soled_CantidadSolicitada,
                            tbSolicitudEfectivoDetalle.soled_CantidadEntregada,
                            tbSolicitudEfectivoDetalle.soled_MontoEntregado);
                foreach (UDP_Vent_tbSolicitudEfectivoDetalle_CantidadSolicitada_Update_Result solicitudefectivodetalle in list)
                    MensajeError = solicitudefectivodetalle.MensajeError;
                MensajeEdit = "El registro se guardó exitosamente";
                if (MensajeError == "-1")
                {
                    MensajeEdit = "No se pudo actualizar el registro, favor contacte al administrador.";
                    ModelState.AddModelError("", MensajeEdit);
                }
            }
            catch (Exception Ex)
            {
                MensajeEdit = Ex.Message.ToString();
                ModelState.AddModelError("", MensajeEdit);
            }
            return Json(MensajeEdit, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportReport(int? id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "SolicitudEfectivo.rpt"));
            var tbSolicitudEfectivo = db.UDP_Vent_tbSolicitudEfectivo_Imprimir(id).ToList();
            var todo = (from r in tbSolicitudEfectivo
                        where r.solef_Id == id
                        select new
                        {
                            solef_Id = r.solef_Id,
                            cja_Descripcion = r.cja_Descripcion,
                            suc_Descripcion = r.suc_Descripcion,
                            mocja_Id = r.mocja_Id,
                            TipoSolicitus = r.TipoSolicitus,
                            UsuarioEntrega = r.UsuarioEntrega,
                            Cajero = r.Cajero,
                            mnda_Nombre = r.mnda_Nombre,
                            solef_FechaEntrega = DateTime.Parse(r.solef_FechaEntrega.ToString()).ToString("dd / MM / yyyy  hh: mm tt"),
                            MontoSolicitado = (decimal)r.MontoSolicitado,
                            deno_Descripcion = r.deno_Descripcion,
                            soled_CantidadSolicitada = r.soled_CantidadSolicitada,
                            MontoEntregado = (decimal)r.MontoEntregado,
                            soled_CantidadEntregada = r.soled_CantidadEntregada
                        }).ToList();
            // list = db.UDP_Vent_tbSolicitudEfectivo_Details(Solictud).ToList();
            rd.SetDataSource(todo);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}
