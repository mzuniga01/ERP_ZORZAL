using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(EstadoFacturaMetaData))]
    public partial class tbEstadoFactura
    {

    }

    public class EstadoFacturaMetaData
    {
        [Display(Name = "Estado Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte esfac_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="El campo {0} es requerido")]
        public string esfac_Descripcion { get; set; }

        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int esfac_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime esfac_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificación")]
        public Nullable<int> esfac_UsuarioModifica { get; set; }


        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> esfac_FechaModifica { get; set; }
    }

   
}