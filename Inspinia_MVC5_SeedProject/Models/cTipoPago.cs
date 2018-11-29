using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(TipoPagoMetaData))]
    public partial class tbTipoPago
    {
        
    }
    public class TipoPagoMetaData
    {
        [Display(Name = "ID Tipo Pago")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int tpa_Id { get; set; }

        [Display(Name = "Descripción Tipo Pago")]
        public string tpa_Descripcion { get; set; }

        [Display(Name = "Emisor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string tpa_Emisor { get; set; }

        [Display(Name = "Cuenta")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string tpa_Cuenta { get; set; }

        [Display(Name = "Fecha Vencimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string tpa_FechaVencimiento { get; set; }

        [Display(Name = "Titular")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string tpa_Titular { get; set; }
        public string tpa_UsuarioCrea { get; set; }
        public System.DateTime tpa_FechaCrea { get; set; }
        public string tpa_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tpa_FechaModifica { get; set; }
    }
}