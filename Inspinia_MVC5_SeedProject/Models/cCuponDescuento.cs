using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(cCuponDescuentoMetaData))]
    public partial class tbCuponDescuento
    {
    }
    public class cCuponDescuentoMetaData
    {
        [Display(Name = "ID Cupón Descuento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int cdto_ID { get; set; }

        [Display(Name = "Sucursal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public short suc_Id { get; set; }

        [Display(Name = "Fecha Emisión")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public System.DateTime cdto_FechaEmision { get; set; }

        [Display(Name = "Fecha Vencimiento")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public System.DateTime cdto_FechaVencimiento { get; set; }

        [Display(Name = "%Descuento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public Nullable<decimal> cdto_PorcentajeDescuento { get; set; }

        [Display(Name = "Monto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public Nullable<decimal> cdto_MontoDescuento { get; set; }

        [Display(Name = "Anulado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public Nullable<bool> cdto_Anulado { get; set; }

        [Display(Name = "Usuario Creó")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int cdto_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creó")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public System.DateTime cdto_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificó")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public Nullable<int> cdto_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificó")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public Nullable<System.DateTime> cdto_FechaModifica { get; set; }

        [Display(Name = "Redimido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string cdto_Redimido { get; set; }

        [Display(Name = "Máximo Monto Descuento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public decimal cdto_MaximoMontoDescuento { get; set; }

        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbSucursal tbSucursal { get; set; }
    }
}