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
        //[DataType(DataType.Date)]
        public System.DateTime fact_Fecha { get; set; }
        [Display(Name = "Estado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public byte esfac_Id { get; set; }
        [Display(Name = "Caja")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public short cja_Id { get; set; }
        [Display(Name = "Sucursal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public short suc_Id { get; set; }
        [Display(Name = "Cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int clte_Id { get; set; }
        [Display(Name = "CAI")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string pemi_NumeroCAI { get; set; }
        [Display(Name = "Al Crédito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public bool fact_AlCredito { get; set; }
        [Display(Name = "Días Crédito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int fact_DiasCredito { get; set; }
        [Display(Name = "Porcentaje Crédito")]
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal fact_PorcentajeDescuento { get; set; }
        [Display(Name = "Autorizar Descuento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public bool fact_AutorizarDescuento { get; set; }
        [Display(Name = "Vendedor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string fact_Vendedor { get; set; }
        [Display(Name = "RTN")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string clte_Identificacion { get; set; }
        [Display(Name = "Cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string clte_Nombres { get; set; }

        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int fact_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime fact_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificación")]
        public Nullable<int> fact_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fact_FechaModifica { get; set; }
    }
}