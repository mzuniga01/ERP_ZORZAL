using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(_BancoMetaData))]
    public partial class tbBanco
    {
  
    }
    public class _BancoMetaData
    {
        [Display(Name = "Código Banco")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int ban_Id { get; set; }
        [Display(Name = "Nombre Banco")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string ban_Nombre { get; set; }
        [Display(Name = "Nombre Contacto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string ban_NombreContacto { get; set; }
        [Display(Name = "Teléfono Banco")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string ban_TelefonoContacto { get; set; }
        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string ban_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime ban_FechaCrea { get; set; }
        [Display(Name = "Usuario Modificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string ban_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime ban_FechaModifica { get; set; }

    }
}