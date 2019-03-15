using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(EstadoPedidoMetaData))]
    public partial class tbEstadoPedido
    {

    }


    public class EstadoPedidoMetaData
    {
        [Display(Name = "Número")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte esped_Id { get; set; }

        [Display(Name = "Descripción")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El campo {0} debe de tener como máximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "El campo {0} no permite números y caracteres especiales")]
        public string esped_Descripcion { get; set; }

        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int esped_UsuarioCrea { get; set; }


        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        
        public System.DateTime esped_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificación")]
        public Nullable<int> esped_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> esped_FechaModifica { get; set; }
    }
}