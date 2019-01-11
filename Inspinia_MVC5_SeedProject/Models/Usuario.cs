using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(UsuarioMetaData))]
    public partial class tbUsuario
    {
        [NotMapped]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string ConfirmarPassword { get; set; }
    }
    public class UsuarioMetaData
    {
        [Display(Name = "ID Usuario")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int usu_Id { get; set; }

        [Display(Name = "Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string usu_NombreUsuario { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte[] usu_Password { get; set; }

        [Display(Name = "Nombres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression("([A-Za-z ])*", ErrorMessage = "Solo se admiten letras.")]
        public string usu_Nombres { get; set; }

        [Display(Name = "Apellidos ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression("([A-Za-z ])*", ErrorMessage = "Solo se admiten letras.")]
        public string usu_Apellidos { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$", ErrorMessage = "No es un email válido.")]
        [Display(Name = "Correo")]
        public string usu_Correo { get; set; }

        [Display(Name = "Activo")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public bool usu_EsActivo { get; set; }

        [Display(Name = "Razón Inactivación")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string usu_RazonInactivo { get; set; }

        [Display(Name = "Administrador")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public bool usu_EsAdministrador { get; set; }

        

        //[NotMapped]
        //public byte[] PasswordActual { get; set; }

        

       
    }
}