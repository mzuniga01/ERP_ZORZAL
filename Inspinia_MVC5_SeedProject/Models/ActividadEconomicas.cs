using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(ActividadEconomicaMetaData))]
    public partial class tbActividadEconomica
    {
 
    }
    public class ActividadEconomicaMetaData
    {
        [Display(Name = "Número")]
        public short acte_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string acte_Descripcion { get; set; }

        [Display(Name = "Usuario Crea")]
        public int acte_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime acte_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> acte_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> acte_FechaModifica { get; set; }
    }
}