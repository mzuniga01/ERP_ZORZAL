using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    [MetadataType(typeof(SolicitudesEfectivoMetaData))]
    public partial class SolicitudesEfectivo
    {

    }
    public class SolicitudesEfectivoMetaData
    {
        [Display(Name = " Solicitud Id ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int solef_Id { get; set; }
        [Display(Name = " Codigo Caja ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string cja_Codigo { get; set; }
        [Display(Name = " Monto Solicitud ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal solef_MontoSolicitud { get; set; }
    }
}