using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(ActividadesEconomicasMetaData))]


    public partial class tbActividadEconomica
    {

    }


    public class ActividadesEconomicasMetaData
    {
        [Display (Name ="Id Actividad")]
        [Required]
        public int acte_Id { get; set; }
        [Display(Name = "Descripción")]
        [Required]
        public string acte_Descripcion { get; set; }
     
        public string acte_UsuarioCrea { get; set; }
        public System.DateTime acte_FechaCrea { get; set; }
        public string acte_UsuarioModifica { get; set; }
        public System.DateTime acte_FechaModifica { get; set; }
    }
}