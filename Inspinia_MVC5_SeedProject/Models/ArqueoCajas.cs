using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(ArqueoCajasMetaData))]

    public partial class tbArqueoCaja
    {
          
    }

    public class ArqueoCajasMetaData
    {
        [Display(Name = "Arqueo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int aqcja_Id { get; set; }


        [Display(Name = "Numero de Caja")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public short cja_Id { get; set; }


        [Display(Name = "Fecha Arqueo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime aqcja_Fecha { get; set; }


        [Display(Name = "Inicio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime aqcja_FechaInicio { get; set; }


        [Display(Name = "Fin")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime aqcja_FechaFin { get; set; }


        [Display(Name = "Monto Inicial")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_SaldoInicial { get; set; }


        [Display(Name = "Monto Final")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_SaldoFinal { get; set; }


        [Display(Name = "Monto Efectivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_MontoEfectivo { get; set; }


        [Display(Name = "Monto Cheque")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_MontoCheque { get; set; }


        [Display(Name = "Monto TC o TD")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_MontoTCoTD { get; set; }


        [Display(Name = "Monto Nota de Credito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_MontoNotaCredito { get; set; }


        [Display(Name = "Monto Cupon")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_MontoCupon { get; set; }


        [Display(Name = "Usuario Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int aqcja_UsuarioCrea { get; set; }


        [Display(Name = "Fecha Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime aqcja_FechaCrea { get; set; }


        [Display(Name = "Usuario Modifica")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public Nullable<int> aqcja_UsuarioModifica { get; set; }


        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> aqcja_FechaModifica { get; set; }
    }
}