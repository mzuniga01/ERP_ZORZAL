using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(MonedasMetaData))]
    public partial class tbMoneda
    {
    }

    public class MonedasMetaData
    {
        [Display(Name = "Id Moneda")]
        [Required ]
        public int mnda_Id { get; set; }
        [Display(Name = "Código ISO")]
        [Required ]
        public string mnda_Iso { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        public string mnda_Nombre { get; set; }
        [Display(Name = "Usuario Crea")]
        [Required]
        public string mnda_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        [Required]
        public System.DateTime mnda_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        [Required]
        public string mnda_UsuarioModifica { get; set; }
      
       
    }

   
}