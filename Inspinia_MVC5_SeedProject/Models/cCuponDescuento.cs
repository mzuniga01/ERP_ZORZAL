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
        [Display(Name = "Id Cupón Descuento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int cdto_ID { get; set; }

        [Display(Name = "Código Cupón Descuento")]
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

        [Display(Name = "Usuario Agregó")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int cdto_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Agregó")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public System.DateTime cdto_FechaCrea { get; set; }

        [Display(Name = "Fecha Modificó")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public Nullable<int> cdto_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificó")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public Nullable<System.DateTime> cdto_FechaModifica { get; set; }

        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbSucursal tbSucursal { get; set; }
    }
}