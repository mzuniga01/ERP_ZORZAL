using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(InventariosFisicosMetadata))]

    public partial class tbInventarioFisico
    {
       
    }

public class InventariosFisicosMetadata
    {
        [Display(Name = "Inventario Id")]
        public int invf_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string invf_Descripcion { get; set; }

        [Display(Name = "Responsable de Bodega")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string invf_ResponsableBodega { get; set; }

        [Display(Name = "Bodega")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int bod_Id { get; set; }

        [Display(Name = "Estado ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte estif_Id { get; set; }

        [Display(Name = "Fecha de Levantamiento")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime invf_FechaInventario { get; set; }
        public Nullable<System.DateTime> invf_FechaModifica { get; set; }
    }
}