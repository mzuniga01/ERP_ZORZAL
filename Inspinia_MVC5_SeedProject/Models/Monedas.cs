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
        [Display(Name="Id Moneda")]
        [Required]
        public short mnda_Id { get; set; }
        [Display(Name = "Abreviatura")]
        [Required]
        public string mnda_Iso { get; set; }
        [Display(Name = "Nombre Moneda")]
        
        public string mnda_Nombre { get; set; }
        public int mnda_UsuarioCrea { get; set; }
        public System.DateTime mnda_FechaCrea { get; set; }
        public Nullable<int> mnda_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> mnda_FechaModifica { get; set; }
    }
}