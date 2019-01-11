using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_GMEDINA.Controllers
{
    public class ConsultarExistenciaProductosController : Controller
    {
        private ERP_ZORZALEntities db = new ERP_ZORZALEntities();
        // GET: ConsultarExistenciaProductos
        public ActionResult Index()
        {

            return View(db.UDV_Inv_Consultar_Existencias_Productos);
        }
    }
}