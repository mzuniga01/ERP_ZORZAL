using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(EstadoInventariosFisicoMetadata))]

    public partial class tbEstadoInventarioFisico
    {
    }
    public class EstadoInventariosFisicoMetadata
    {
       [Display(Name ="Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte estif_Id { get; set; }
        [Display(Name ="Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string estif_Descripcion { get; set; }
        public int estif_UsuarioCrea { get; set; }
        public System.DateTime estif_FechaCrea { get; set; }
        public Nullable<int> estif_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> estif_FechaModifica { get; set; }

    }
}