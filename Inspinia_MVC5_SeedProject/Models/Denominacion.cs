using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(DenominacionMetaData))]
    public partial class tbDenominacion
    {
        [NotMapped]
        public List<cDenominacion> DenominacionList { get; set; }    
    }

    public class DenominacionMetaData
    {
        [Display(Name = "Número")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short deno_Id { get; set; }
        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string deno_Descripcion { get; set; }
        [Display(Name = "Tipo Denominación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte deno_Tipo { get; set; }
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        //[Range(0.1, 0.1000, ErrorMessage = "Entre {1} y {2}")]
        [Range(0.01, 500.00, ErrorMessage = "El valor Ingresado No es Valido")]
        [Display(Name = "Valor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal deno_valor { get; set; }
        [Display(Name = "Moneda")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short mnda_Id { get; set; }
        public int deno_UsuarioCrea { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime deno_FechaCrea { get; set; }
        public Nullable<int> deno_UsuarioModifica { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> deno_FechaModifica { get; set; }

    }
}