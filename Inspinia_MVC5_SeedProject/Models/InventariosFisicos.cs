using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(InventariosFisicosMetadata))]

    public partial class tbInventarioFisico
    {
       
    }

public class InventariosFisicosMetadata
    {
        [Display(Name = "Inventario Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
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
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime invf_FechaInventario { get; set; }

        [Display(Name = "Creado Por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int invf_UsuarioCrea { get; set; }

        [Display(Name = "Creado el")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime invf_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<int> invf_UsuarioModifica { get; set; }

        [Display(Name = "Modificado el")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> invf_FechaModifica { get; set; }
    }
}