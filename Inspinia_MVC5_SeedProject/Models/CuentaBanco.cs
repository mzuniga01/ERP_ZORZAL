using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(_CuentasBancoMetaData))]

    public partial class tbCuentasBanco
    {
    }

    public class _CuentasBancoMetaData
    {
        [Display(Name = "Código Cuenta de Banco")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short bcta_Id { get; set; }
        [Display(Name = "Código Banco")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short ban_Id { get; set; }
        [Display(Name = "Código de Moneda")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short mnda_Id { get; set; }
        [Display(Name = "Tipo Cuenta")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte bcta_TipoCuenta { get; set; }
        [Display(Name = "Total Crédito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal bcta_TotalCredito { get; set; }
        [Display(Name = "Total Débito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal bcta_TotalDebito { get; set; }
        [Display(Name = "Fecha Apertura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}", HtmlEncode = false)]
        public System.DateTime bcta_FechaApertura { get; set; }
        [Display(Name = "Número Cuenta")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string bcta_Numero { get; set; }
        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string bcta_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime bcta_FechaCrea { get; set; }
        [Display(Name = "Usuario Modificación")]
        public Nullable<int> bcta_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> bcta_FechaModifica { get; set; }
    }

}