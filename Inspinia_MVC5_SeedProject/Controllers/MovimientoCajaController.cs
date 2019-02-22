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
//ESTE es//
namespace ERP_GMEDINA.Controllers
{
    public class MovimientoCajaController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        GeneralFunctions Function = new GeneralFunctions();


        public ActionResult Index()
        {
            var tbmovimientocaja = db.tbMovimientoCaja.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCaja);
            return View(tbmovimientocaja.ToList());
        }


        /////////INICIO APERTURA/////////
        // GET: /MovimientoCaja/

        public ActionResult IndexApertura()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("MovimientoCaja/IndexApertura"))
                {
                    int idUser = 0;
                    GeneralFunctions Login = new GeneralFunctions();
                    List<tbUsuario> User = Login.getUserInformation();
                    foreach (tbUsuario Usuario in User)
                    {
                        idUser = Convert.ToInt32(Usuario.emp_Id);
                    }
                    ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                    var suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                    return View(db.tbMovimientoCaja.ToList());
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
        ///Create Apertura
        public ActionResult CreateApertura()
        {
            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("MovimientoCaja/CreateApertura"))
                {
                    int idUser = 0;
                    GeneralFunctions Login = new GeneralFunctions();
                    List<tbUsuario> User = Login.getUserInformation();
                    foreach (tbUsuario Usuario in User)
                    {
                        idUser = Convert.ToInt32(Usuario.emp_Id);
                    }

                    //////Solicitud Efectivo
                    tbMovimientoCaja MovimientoCaja = new tbMovimientoCaja();
                    tbSolicitudEfectivo SolicitudEfectivo = new tbSolicitudEfectivo();
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", SolicitudEfectivo.mnda_Id);
                    ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", MovimientoCaja.usu_Id);

                    ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                    var suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                    ViewBag.UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_NombreUsuario).SingleOrDefault();
                    ViewBag.mocja_UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();
                    var Cajas = db.tbCaja.Select(s => new { cja_Id = s.cja_Id, cja_Descripcion = s.cja_Descripcion, suc_Id = s.suc_Id}).Where(x => x.suc_Id == suc_Id).ToList();                
                    ViewBag.cja_Id = new SelectList(Cajas, "cja_Id", "cja_Descripcion", MovimientoCaja.cja_Id);

                    /////Vistas Parciales
                    ViewBag.SolicitudEfectivo = db.tbSolicitudEfectivo.ToList();
                    ViewBag.MovimientoCaja = db.tbMovimientoCaja.ToList();
                    Session["SolicitudEfectivo"] = null;
                    return View();
                }
                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // POST: /MovimientoCaja/CreateApertura
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateApertura([Bind(Include = "mocja_Id,cja_Id,mocja_FechaApetura,mocja_UsuarioApertura,usu_Id,mocja_UsuarioCrea,mocja_FechaCrea")] tbMovimientoCaja tbMovimientoCaja)
        {
            int idUser = 0;
            GeneralFunctions Login = new GeneralFunctions();
            List<tbUsuario> User = Login.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }

            tbSolicitudEfectivo tbSolicitudEfectivo = new tbSolicitudEfectivo();
            /////////REMOVE////////
            ModelState.Remove("mocja_UsuarioApertura");
            ModelState.Remove("mocja_UsuarioArquea");
            ModelState.Remove("mocja_FechaArqueo");
            ModelState.Remove("mocja_FechaAceptacion");
            ModelState.Remove("mocja_UsuarioAceptacion");
            /////////VAR//////////;
            bool solef_EsApertura = true;
            bool solef_EsAnulada = false;
            tbMovimientoCaja.mocja_FechaApertura = DateTime.Now;
            tbMovimientoCaja.mocja_FechaCrea = DateTime.Now;
            ///////////VAR SESSION//////////
            var list = (List<tbSolicitudEfectivoDetalle>)Session["SolicitudEfectivo"];
            short moneda = (short)Session["SolicitudEfectivoMoneda"];
            string MensajeError = "";
            string MensajeErrorSolicitud = "";
            string MensajeErrorSolicitudDetalle = "";
            //////////LISTAS///////////////
            IEnumerable<object> listMovimientoCaja = null;
            IEnumerable<object> listSolicitudEfectivo = null;
            IEnumerable<object> listSolicitudEfectivoDetalle = null;

            if (Function.GetUserLogin())
            {
                if (Function.GetUserRols("MovimientoCaja/CreateApertura"))
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            using (TransactionScope Tran = new TransactionScope())
                            {
                                listMovimientoCaja = db.UDP_Vent_tbMovimientoCaja_Apertura_Insert(
                                tbMovimientoCaja.cja_Id,
                                tbMovimientoCaja.mocja_FechaApertura,
                                tbMovimientoCaja.mocja_UsuarioApertura,
                                tbMovimientoCaja.usu_Id,
                                tbMovimientoCaja.mocja_FechaArqueo,
                                Function.GetUser(),
                                tbMovimientoCaja.mocja_FechaAceptacion,
                                Function.GetUser(),
                                Function.GetUser(),
                                Function.DatetimeNow());
                                foreach (UDP_Vent_tbMovimientoCaja_Apertura_Insert_Result apertura in listMovimientoCaja)

                                    MensajeError = apertura.MensajeError;
                                if (MensajeError == "-1")
                                {
                                    Function.InsertBitacoraErrores("MovimientoCaja/CreateApertura", MensajeError, "CreateApertura");
                                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                                    return View(tbMovimientoCaja);
                                }
                                else
                                {
                                    listSolicitudEfectivo = db.UDP_Vent_tbSolicitudEfectivo_Apertura_Insert(
                                            Convert.ToInt32(MensajeError),
                                            solef_EsApertura,
                                            Function.DatetimeNow(),
                                            Function.GetUser(),
                                            moneda,
                                            solef_EsAnulada,
                                            Function.GetUser(),
                                            Function.DatetimeNow());
                                    foreach (UDP_Vent_tbSolicitudEfectivo_Apertura_Insert_Result SolicitudEfectivoMon in listSolicitudEfectivo)
                                        MensajeErrorSolicitud = SolicitudEfectivoMon.MensajeError;
                                    if (MensajeErrorSolicitud == "-1")
                                    {
                                        Function.InsertBitacoraErrores("MovimientoCaja/CreateApertura", MensajeError, "CreateApertura");
                                        ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                        return View(tbMovimientoCaja);
                                    }
                                    else
                                    {
                                        ///////////Solicitud Efectivo Detalle////////////////////
                                        if (MensajeErrorSolicitudDetalle != "-1")
                                        {
                                            if (list != null)
                                            {
                                                if (list.Count != 0)
                                                {
                                                    foreach (tbSolicitudEfectivoDetalle efectivodetalle in list)
                                                    {

                                                        var SolicitudDetalle = Convert.ToInt32(MensajeErrorSolicitud);
                                                        efectivodetalle.solef_Id = SolicitudDetalle;
                                                        listSolicitudEfectivoDetalle = db.UDP_Vent_tbSolicitudEfectivoDetalle_Apertura_Insert(
                                                          Convert.ToInt32(MensajeErrorSolicitud),
                                                           efectivodetalle.deno_Id,
                                                           efectivodetalle.soled_CantidadSolicitada,
                                                           efectivodetalle.soled_CantidadEntregada,
                                                           efectivodetalle.soled_MontoEntregado,
                                                           Function.GetUser(),
                                                           Function.DatetimeNow());
                                                        foreach (UDP_Vent_tbSolicitudEfectivoDetalle_Apertura_Insert_Result SolicitudEfectivoDet in listSolicitudEfectivoDetalle)
                                                        {
                                                            MensajeErrorSolicitudDetalle = SolicitudEfectivoDet.MensajeError;
                                                            if (MensajeErrorSolicitudDetalle == "-1")
                                                            {
                                                                Function.InsertBitacoraErrores("MovimientoCaja/CreateApertura", MensajeError, "CreateApertura");
                                                                ModelState.AddModelError("", "No se pudo agregar el registro detalle");
                                                                return View(tbMovimientoCaja);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "No se pudo agregar el registro");
                                            return View(tbMovimientoCaja);
                                        }

                                    }
                                    Tran.Complete();
                                    return RedirectToAction("IndexApertura");
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            ///
                            ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                            var suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                            ViewBag.UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_NombreUsuario).SingleOrDefault();
                            ViewBag.mocja_UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();
                            ///

                            Function.InsertBitacoraErrores("MovimientoCaja/CreateApertura", MensajeError, "CreateApertura");

                            //Usuario
                            ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.usu_Id);
                            //Caja
                            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
                            ///Sucursal
                            ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
                            ///Moneda
                            ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);

                            Ex.Message.ToString();
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador." + Ex.Message);
                            return View(tbMovimientoCaja);
                        }
                    }

                    ViewBag.suc_Descripcion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Descripcion).SingleOrDefault();
                    var suc_ID = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
                    ViewBag.UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_NombreUsuario).SingleOrDefault();
                    ViewBag.mocja_UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();

                    //Usuario
                    ViewBag.usu_Id = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.usu_Id);
                    ///Sucursal
                    ViewBag.suc_Id = new SelectList(db.tbSucursal, "suc_Id", "suc_Descripcion");
                    //Caja
                    ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
                    ///Moneda
                    ViewBag.mnda_Id = new SelectList(db.tbMoneda, "mnda_Id", "mnda_Nombre", tbSolicitudEfectivo.mnda_Id);
                    return View(tbMovimientoCaja);
                }

                else
                {
                    return RedirectToAction("SinAcceso", "Login");
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
        ///Variable de Sesion 1
        [HttpPost]
        public JsonResult SaveSolicitudEfectivoDetalle(tbSolicitudEfectivoDetalle SolicitudEfectivoDet)
        {
            List<tbSolicitudEfectivoDetalle> sessionSolicitudEfectivoDetalle = new List<tbSolicitudEfectivoDetalle>();
            var list = (List<tbSolicitudEfectivoDetalle>)Session["SolicitudEfectivo"];
            if (list == null)
            {
                sessionSolicitudEfectivoDetalle.Add(SolicitudEfectivoDet);
                Session["SolicitudEfectivo"] = sessionSolicitudEfectivoDetalle;
            }
            else
            {
                list.Add(SolicitudEfectivoDet);
                Session["SolicitudEfectivo"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        /////Variable de Sesion 2
        [HttpPost]
        public JsonResult SaveSolicitudEfectivoMoneda(tbSolicitudEfectivo SolicitudEfectivoMon)
        {
            Session["SolicitudEfectivoMoneda"] = SolicitudEfectivoMon.mnda_Id;
            var Datos = Session["SolicitudEfectivoMoneda"];
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public JsonResult GetDenominacion(int CodMoneda)
        {
            var list = db.spGetDenominacionesMoneda(CodMoneda).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRol(int Usuario)
        {
            var list = db.UPD_Vent_tbUsuario_Rol(Usuario).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        ////////////TERMINO APERTURA////////////




        // GET: /MovimientoCaja/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbMovimientoCaja tbMovimientoCaja = db.tbMovimientoCaja.Find(id);
            if (tbMovimientoCaja == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbMovimientoCaja);
        }

        // GET: /MovimientoCaja/Create
        public ActionResult Create()
        {
            //ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");

            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion");           
            ViewBag.DenominacionArqueo = db.tbDenominacionArqueo.ToList();

            int idUser = 0;
            GeneralFunctions Login = new GeneralFunctions();
            List<tbUsuario> User = Login.getUserInformation();
            foreach (tbUsuario Usuario in User)
            {
                idUser = Convert.ToInt32(Usuario.emp_Id);
            }

            var suc_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.tbSucursal.suc_Id).SingleOrDefault();
            ViewBag.UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_NombreUsuario).SingleOrDefault();
            ViewBag.mocja_UsuarioApertura = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();

            ViewBag.UsuarioArquea = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_NombreUsuario).SingleOrDefault();
            ViewBag.mocja_UsuarioArquea = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();

            ViewBag.UsuarioAceptacion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_NombreUsuario).SingleOrDefault();
            ViewBag.mocja_UsuarioAceptacion = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();

            ViewBag.Cajero = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_NombreUsuario).SingleOrDefault();
            ViewBag.usu_Id = db.tbUsuario.Where(x => x.usu_Id == idUser).Select(x => x.usu_Id).SingleOrDefault();


            tbMovimientoCaja MC = new tbMovimientoCaja();
            MC.cja_Id = 4;
            return View(MC);

        }

        // POST: /MovimientoCaja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="mocja_Id,cja_Id,mocja_FechaApetura,mocja_UsuarioApertura,mocja_FechaArqueo,mocja_UsuarioArquea,mocja_FechaAceptacion,mocja_UsuarioAceptacion,mocja_UsuarioCrea,mocja_FechaCrea,mocja_UsuarioModifica,mocja_FechaModifica")] tbMovimientoCaja tbMovimientoCaja)
        {
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
            ViewBag.deno_Id = new SelectList(db.tbDenominacionArqueo, "deno_Id", "deno_Descripcion", tbMovimientoCaja.cja_Id);
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = string.Empty;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbMovimientoCaja_Insert(tbMovimientoCaja.cja_Id, 
                        tbMovimientoCaja.usu_Id,
                        tbMovimientoCaja.mocja_UsuarioApertura, 
                        tbMovimientoCaja.mocja_FechaArqueo,
                        tbMovimientoCaja.mocja_UsuarioArquea, 
                        tbMovimientoCaja.mocja_FechaAceptacion, 
                        tbMovimientoCaja.mocja_UsuarioAceptacion,
                        Function.GetUser(),
                        Function.DatetimeNow());
                    foreach (UDP_Vent_tbMovimientoCaja_Insert_Result denoarq in list)
                        MensajeError = denoarq.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbMovimientoCaja);
                    }
                    else
                    {

                        return RedirectToAction("Index");
                    }
                }

                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");


                    return View(tbMovimientoCaja);
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            //if (ModelState.IsValid)
            //{
            //    db.tbMovimientoCaja.Add(tbMovimientoCaja);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioCrea);
            ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioModifica);
            ViewBag.deno_Id = new SelectList(db.tbDenominacionArqueo, "deno_Id", "deno_Descripcion", tbMovimientoCaja.cja_Id);
            return View(tbMovimientoCaja);
        }

        // GET: /MovimientoCaja/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbMovimientoCaja tbMovimientoCaja = db.tbMovimientoCaja.Find(id);
            if (tbMovimientoCaja == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioCrea);
            ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioModifica);
            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
            return View(tbMovimientoCaja);
        }

        // POST: /MovimientoCaja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="mocja_Id,cja_Id,mocja_FechaApetura,mocja_UsuarioApertura,mocja_FechaArqueo,mocja_UsuarioArquea,mocja_FechaAceptacion,mocja_UsuarioAceptacion,mocja_UsuarioCrea,mocja_FechaCrea,mocja_UsuarioModifica,mocja_FechaModifica")] tbMovimientoCaja tbMovimientoCaja)
        {

            ViewBag.cja_Id = new SelectList(db.tbCaja, "cja_Id", "cja_Descripcion", tbMovimientoCaja.cja_Id);
            ViewBag.deno_Id = new SelectList(db.tbDenominacionArqueo, "deno_Id", "deno_Descripcion", tbMovimientoCaja.cja_Id);
            if (ModelState.IsValid)
            {
                try
                {
                    //////////Aqui va la lista//////////////
                    var MensajeError = string.Empty;
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbMovimientoCaja_Update(tbMovimientoCaja.mocja_Id, tbMovimientoCaja.mocja_UsuarioCrea, tbMovimientoCaja.mocja_FechaCrea, 
                        Function.GetUser(),
                                    Function.DatetimeNow());
                    foreach (UDP_Vent_tbMovimientoCaja_Update_Result denoarq in list)
                        MensajeError = denoarq.MensajeError;
                    if (MensajeError == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return View(tbMovimientoCaja);
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


                    return View(tbMovimientoCaja);
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            //if (ModelState.IsValid)
            //{
            //    db.Entry(tbMovimientoCaja).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            ViewBag.mocja_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioCrea);
            ViewBag.mocja_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbMovimientoCaja.mocja_UsuarioModifica);
            ViewBag.deno_Id = new SelectList(db.tbDenominacionArqueo, "deno_Id", "deno_Descripcion", tbMovimientoCaja.cja_Id);
            return View(tbMovimientoCaja);
        }

        // GET: /MovimientoCaja/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbMovimientoCaja tbMovimientoCaja = db.tbMovimientoCaja.Find(id);
            if (tbMovimientoCaja == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbMovimientoCaja);
        }

        // POST: /MovimientoCaja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMovimientoCaja tbMovimientoCaja = db.tbMovimientoCaja.Find(id);
            db.tbMovimientoCaja.Remove(tbMovimientoCaja);
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

        [HttpGet]
        public ActionResult Denominaciones()
        {
            var list = db.UDP_Vent_tbDenominacionArqueo_Select().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
