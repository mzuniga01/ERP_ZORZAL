using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
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

        [Display(Name = "Sucursal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int suc_Id { get; set; }

        [Display(Name = "Fecha Emisión")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", HtmlEncode = false)]
        public System.DateTime cdto_FechaEmision { get; set; }

        [Display(Name = "Fecha Vencimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:dd/MM/yyyy}",
            HtmlEncode = false)]
        public System.DateTime cdto_FechaVencimiento { get; set; }

        [Display(Name = "% Descuento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        [Range(0,100)]
        public Nullable<decimal> cdto_PorcentajeDescuento { get; set; }

        [Display(Name = "Monto Descuento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public Nullable<decimal> cdto_MontoDescuento { get; set; }

        [Display(Name = "Máximo Monto Descuento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public decimal cdto_MaximoMontoDescuento { get; set; }

        [Display(Name = "Compra Mínima")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public decimal cdto_CantidadCompraMinima { get; set; }

        [Display(Name = "Redimido")]
        public bool cdto_Redimido { get; set; }

        [Display(Name = "Anulado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public bool cdto_Anulado { get; set; }
        //public Nullable<bool> cdto_Anulado { get; set; }

        [Display(Name = "Usuario Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int cdto_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime cdto_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> cdto_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> cdto_FechaModifica { get; set; }

        [Display(Name = "Fecha Redención")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:dd/MM/yyyy}",
            HtmlEncode = false)]
        public System.DateTime cdto_FechaRedencion { get; set; }

        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbSucursal tbSucursal { get; set; }
    }
}