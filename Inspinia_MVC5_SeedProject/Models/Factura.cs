using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(FacturaMetaData))]

    public partial class tbFactura
    {
       
    }

    public class FacturaMetaData
    {
        public long fact_Id { get; set; }
        [Display(Name = "Número Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string fact_Codigo { get; set; }
        [Display(Name = "Fecha")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}", HtmlEncode = false)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public System.DateTime fact_Fecha { get; set; }
        [Display(Name = "Estado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public byte esfac_Id { get; set; }
        [Display(Name = "Caja")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public short cja_Id { get; set; }
        [Display(Name = "Sucursal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int suc_Id { get; set; }
        public int clte_Id { get; set; }
        [Display(Name = "CAI")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string pemi_NumeroCAI { get; set; }
        [Display(Name = "Al Crédito")]
        public bool fact_AlCredito { get; set; }
        [Display(Name = "Días Crédito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int fact_DiasCredito { get; set; }
        [Display(Name = "Porcentaje Descuento")]
        public decimal fact_PorcentajeDescuento { get; set; }
        [Display(Name = "Vendedor")]
        public string fact_Vendedor { get; set; }
        [Display(Name = "RTN")]
        public string clte_Identificacion { get; set; }
        [Display(Name = "Cliente")]
        public string clte_Nombres { get; set; }
        [Display(Name = "Identidad")]
        public string fact_IdentidadTE { get; set; }
        [Display(Name = "Nombres")]
        public string fact_NombresTE { get; set; }
        [Display(Name = "Fecha Nacimiento")]
        public Nullable<System.DateTime> fact_FechaNacimientoTE { get; set; }
        [Display(Name = "Razón Anulada")]
        public string fact_RazonAnulado { get; set; }

    }
}