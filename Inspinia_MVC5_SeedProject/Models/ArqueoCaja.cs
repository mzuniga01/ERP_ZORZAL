using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    [MetadataType(typeof(ArqueoCajaMetaData))]
    public partial class ArqueoCaja
    {

    }
    public class ArqueoCajaMetaData
    {
        [Display(Name = "Arqueo caja Codigo ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int aqcja_Codigo { get; set; }
        [Display(Name = " Sucursal Codigo ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string sucur_Codigo { get; set; }

        [Display(Name = " Caja Codigo ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string cja_Codigo { get; set; }
        [Display(Name = " Fecha Arqueo ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime aqcja_Fecha { get; set; }
        [Display(Name = " Hora Inicio ")]
        [DisplayFormat(DataFormatString = "{0:H:mm:ss}", ApplyFormatInEditMode = true)]
        public System.DateTime aqcja_HoraInicio { get; set; }
        [Display(Name = " Hora Final ")]
        [DisplayFormat(DataFormatString = "{0:H:mm:ss}", ApplyFormatInEditMode = true)]
        public System.DateTime aqcja_HoraFinal { get; set; }
        [Display(Name = " Saldo Inicial ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_SaldoInicial { get; set; }
        [Display(Name = " Saldo Final ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_SaldoFinal { get; set; }
        [Display(Name = " Monto Efectivo ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_MontoEfectivo { get; set; }
        [Display(Name = " Monto Cheque")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_MontoCheque { get; set; }
        [Display(Name = " Total ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_Total { get; set; }
        [Display(Name = " Diferencia ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public decimal aqcja_Diferencia { get; set; }

    }
}