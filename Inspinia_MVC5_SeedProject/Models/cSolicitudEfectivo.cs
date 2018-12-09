using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cSolicitudEfectivoMetaData))]

    public partial class tbSolicitudEfectivo
    {
       
    public class cSolicitudEfectivoMetaData
        {
            [Display(Name = "Númerp")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
            public int solef_Id { get; set; }
            [Display(Name = "Caja")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
            public short cja_Id { get; set; }
            [Display(Name = "Moneda")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
            public short mnda_Id { get; set; }
            [Display(Name = "Monto Solicitado")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
            public decimal solef_MontoSolicitud { get; set; }
           
            public int solef_UsuarioCrea { get; set; }
           
            public System.DateTime solef_FechaCrea { get; set; }
            
            public Nullable<int> solef_UsuarioModifica { get; set; }
           
            public Nullable<System.DateTime> solef_FechaModifica { get; set; }

           
        }
    }
}