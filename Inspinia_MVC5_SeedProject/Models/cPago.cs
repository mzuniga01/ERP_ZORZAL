using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    [MetadataType(typeof(cPagoMetaData))]
    public partial class tbPago
    {
    }
    public class cPagoMetaData
    {
        [Display(Name = "ID Pago")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pago_Id { get; set; }

        [Display(Name = "Codigo Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string fact_Codigo { get; set; }

        [Display(Name = "ID Tipo Pago")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int tpa_Id { get; set; }

        [Display(Name = "Total Pago")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal pago_Totalpagos { get; set; }

        [Display(Name = "Total Cambio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal pago_Totalcambio { get; set; }

        [Display(Name = "Emisor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string pago_Emisor { get; set; }

        [Display(Name = "Cuenta")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string pago_Cuenta { get; set; }

        [Display(Name = "Fecha Vencimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> pago_FechaVencimiento { get; set; }

        [Display(Name = "Pago Titular")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string pago_Titular_ { get; set; }
        public string pago_UsuarioCrea { get; set; }
        public System.DateTime pago_FechaCrea { get; set; }
        public string pago_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pago_FechaModifica { get; set; }
    }
}