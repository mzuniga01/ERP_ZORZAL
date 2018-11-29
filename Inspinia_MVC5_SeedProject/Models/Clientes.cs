using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
   [MetadataType(typeof(_ClientesMetaData))]
    public partial class tbCliente
    {
 
    }
    public class _ClientesMetaData
    {
        [Display(Name = "Id Cliente")]
        public int clte_Id { get; set; }
        [Display(Name = "Id Tipo Documento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public byte tpi_Id { get; set; }
        [Display(Name = "RTN/Identidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_RTN_IDT_PASSP { get; set; }
        [Display(Name = "¿Es Persona natural?")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public bool clte_EsPersonaNatural { get; set; }
        [Display(Name = "Nombres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_Nombre { get; set; }
        [Display(Name = "Apellidos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_Apellido { get; set; }
        [Display(Name = "Fecha Nacimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public System.DateTime clte_FechaNacimiento { get; set; }

        [Display(Name = "Nacionalidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_Nacionalidad { get; set; }
        [Display(Name = "Sexo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_Sexo { get; set; }

        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_Telefono { get; set; }

        [Display(Name = "Nombre comercial")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_NombreComercial { get; set; }

        [Display(Name = "Razon social")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_RazonSocial { get; set; }
        [Display(Name = "Nombre contacto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_ContactoNombre { get; set; }
        [Display(Name = "Correo contacto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_ContactoEmail { get; set; }
        [Display(Name = "Teléfono contacto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_ContactoTel { get; set; }
        [Display(Name = "Fecha Constitución")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public System.DateTime clte_FechaConstitusion { get; set; }
        [Display(Name = "Municipio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string mun_Id { get; set; }
        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_Direccion { get; set; }
        [Display(Name = "Correo Electrónico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_CorreoElectronico { get; set; }
        [Display(Name = "Estado cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_EsActivo { get; set; }
        [Display(Name = "Razón Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_RazonInactivo { get; set; }
        [Display(Name = "Crédito")]
        public bool clte_ConCredito { get; set; }
        [Display(Name = "¿Es Minorista?")]
        public bool clte_EsMinorista { get; set; }
        [Display(Name = "Observaciones")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_Observaciones_ { get; set; }
        [Display(Name = "Usuario Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public System.DateTime clte_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public Nullable<System.DateTime> clte_FechaModifica { get; set; }
    }
}