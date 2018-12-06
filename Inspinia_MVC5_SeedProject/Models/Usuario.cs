using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(UsuarioMetaData))]
    public partial class tbUsuario
    {

    }
    public class UsuarioMetaData
    {
        [Display(Name = "ID Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int usu_Id { get; set; }

        [Display(Name = "Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string usu_NombreUsuario { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte[] usu_Password { get; set; }

        [Display(Name = "Nombres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string usu_Nombres { get; set; }

        [Display(Name = "Apellidos ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string usu_Apellidos { get; set; }

        [Display(Name = "Correo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string usu_Correo { get; set; }

        [Display(Name = "Activo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public bool usu_EsActivo { get; set; }

        [Display(Name = "Razón Inactivación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string usu_RazonInactivo { get; set; }

        [Display(Name = "Administrador")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public bool usu_EsAdministrador { get; set; }
    }
}