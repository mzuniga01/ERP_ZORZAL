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
        [Display(Name = "Id")]
        public byte esped_Id { get; set; }

        [Display(Name = "Estado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string esped_Descripcion { get; set; }

        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int esped_UsuarioCrea { get; set; }


        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime esped_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificación")]
        public Nullable<int> esped_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> esped_FechaModifica { get; set; }
    }
}