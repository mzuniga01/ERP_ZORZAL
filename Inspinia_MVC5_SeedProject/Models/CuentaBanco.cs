using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(_CuentasBancoMetaData))]

    public partial class tbCuentasBanco
    {
        [NotMapped]
        public List<cTipoCuenta> TipoCuentaList { get; set; }
    }

    public class _CuentasBancoMetaData
    {
        [Display(Name = "Número")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short bcta_Id { get; set; }
        [Display(Name = "Banco")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short ban_Id { get; set; }
        [Display(Name = "Moneda")]
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}", HtmlEncode = false)]
        public System.DateTime bcta_FechaApertura { get; set; }
        [Display(Name = "Número Cuenta")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string bcta_Numero { get; set; }
        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string bcta_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime bcta_FechaCrea { get; set; }
        [Display(Name = "Usuario Modificación")]
        public Nullable<int> bcta_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> bcta_FechaModifica { get; set; }

        
    }

}