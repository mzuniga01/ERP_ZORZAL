using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(TipoPagoMetaData))]
    public partial class tbTipoPago
    {
    }
    public partial class TipoPagoMetaData
    {

        [Display(Name = "Número")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public short tpa_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string tpa_Descripcion { get; set; }

        [Display(Name = "Emisor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public bool tpa_Emisor { get; set; }

        [Display(Name = "Cuenta de Banco")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public bool tpa_Cuenta { get; set; }

        [Display(Name = "Fecha de Vencimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public bool tpa_FechaVencimiento { get; set; }

        [Display(Name = "Titular")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public bool tpa_Titular { get; set; }


        [Display(Name = "Usuario Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int tpa_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public System.DateTime tpa_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public Nullable<int> tpa_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public Nullable<System.DateTime> tpa_FechaModifica { get; set; }

    }
}