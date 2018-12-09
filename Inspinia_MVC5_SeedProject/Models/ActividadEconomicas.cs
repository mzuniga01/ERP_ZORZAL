using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(ActividadEconomicasMetaData))]
    public partial class tbActividadEconomica
    {
 
    }
    public class ActividadEconomicasMetaData
    {
        [Display(Name = "Número")]
        public short acte_Id { get; set; }

        [Display(Name = "Descripción")]
        public string acte_Descripcion { get; set; }

        [Display(Name = "Usuario Crea")]
        public int acte_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public System.DateTime acte_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> acte_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}",
            HtmlEncode = false)]
        public Nullable<System.DateTime> acte_FechaModifica { get; set; }
    }
}