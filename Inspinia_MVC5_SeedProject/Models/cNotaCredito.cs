using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(cNotaCreditoMetaData))]
    public partial class tbNotaCredito
    {
    }
    public class cNotaCreditoMetaData
    {
        [Display(Name = "Id Note Crédito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public short nocre_Id { get; set; }

        [Display(Name = "Código Nota Crédito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string nocre_Codigo { get; set; }

        [Display(Name = "Devolución")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int dev_Id { get; set; }

        [Display(Name = "Cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int clte_Id { get; set; }

        [Display(Name = "Fecha Emisión")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public System.DateTime nocre_FechaEmision { get; set; }

        [Display(Name = "Motivo Emisión")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string nocre_MotivoEmision { get; set; }

        [Display(Name = "Monto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public Nullable<decimal> nocre_Monto { get; set; }

        [Display(Name = "Usuario Agregó")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int nocre_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Agregó")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public System.DateTime nocre_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificó")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public Nullable<int> nocre_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificó")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public Nullable<System.DateTime> nocre_FechaModifica { get; set; }

        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCliente tbCliente { get; set; }
        public virtual tbDevolucion tbDevolucion { get; set; }
    }
}