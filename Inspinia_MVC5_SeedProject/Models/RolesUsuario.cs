using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(RolesUsuarioMetaData))]
    public partial class tbRolesUsuario
    {
        
    }


    public class RolesUsuarioMetaData
    {
        [Display(Name = "Rol de Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int rolu_Id { get; set; }

        [Display(Name = "Código Rol")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int rol_Id { get; set; }

        [Display(Name = "Id Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int usu_Id { get; set; }

        [Display(Name = "Creado Por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int rolu_UsuarioCrea { get; set; }

        [Display(Name = "Creado El")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime rolu_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public Nullable<int> rolu_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public Nullable<System.DateTime> rolu_FechaModifica { get; set; }
    }

    
}