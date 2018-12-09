using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(tbPagoMetaData))]

    public partial class tbPago
    {
        [NotMapped]
        public List<cTP> TPList { get; set; }
    }
    public partial class tbPagoMetaData
    {
        [Display(Name = "Número Pago")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int pago_Id { get; set; }
        [Display(Name = "Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public long fact_Id { get; set; }

        [Display(Name = "Tipo Pago")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public short tpa_Id { get; set; }

        [Display(Name = "Fecha ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public System.DateTime pago_FechaElaboracion { get; set; }

        [Display(Name = "Saldo Anterior")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal pago_SaldoAnterior { get; set; }

        [Display(Name = "Saldo Actual")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal pago_TotalPago { get; set; }

        [Display(Name = "Total Cambio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal pago_TotalCambio { get; set; }

        [Display(Name = "Emisor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string pago_Emisor { get; set; }

        [Display(Name = "Cuenta Bancaria")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public short bcta_Id { get; set; }

        [Display(Name = "Fecha de Vencimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public Nullable<System.DateTime> pago_FechaVencimiento { get; set; }

        [Display(Name = "Titular")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string pago_Titular_ { get; set; }

        [Display(Name = "Usuario Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int pago_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public System.DateTime pago_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public Nullable<int> pago_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public Nullable<System.DateTime> pago_FechaModifica { get; set; }


    }
}