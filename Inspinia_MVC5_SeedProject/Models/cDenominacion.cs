using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(cDenominacionMetaData))]


    public partial class tbDenominacion
    {
       
    }

    public class cDenominacionMetaData
    {
        [Display(Name = "Número Denominación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short deno_Id { get; set; }
        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string deno_Descripcion { get; set; }
        [Display(Name = "Denominación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte deno_Tipo { get; set; }
        [Display(Name = "Valor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal deno_valor { get; set; }
        [Display(Name = "Moneda")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short mnda_Id { get; set; }
       
        public int deno_UsuarioCrea { get; set; }
        
        public System.DateTime deno_FechaCrea { get; set; }
        
        public Nullable<int> deno_UsuarioModifica { get; set; }
        
        public Nullable<System.DateTime> deno_FechaModifica { get; set; }

        
    }
}