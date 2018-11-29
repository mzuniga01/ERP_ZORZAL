using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
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
        [Display(Name = "Persona natural")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public bool clte_EsPersonaNatural { get; set; }
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_Nombre { get; set; }
        [Display(Name = "Apellido")]
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

        [Display(Name = "Telefono")]
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
        [Display(Name = "Telefono contacto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_ContactoTel { get; set; }
        [Display(Name = "Fecha Constitucion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public System.DateTime clte_FechaConstitusion { get; set; }
        [Display(Name = "Municipio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string mun_Id { get; set; }
        [Display(Name = "Direccion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_Direccion { get; set; }
        [Display(Name = "Correo Electronico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_CorreoElectronico { get; set; }
        [Display(Name = "Estado cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_EsActivo { get; set; }
        [Display(Name = "Razon Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_RazonInactivo { get; set; }
        [Display(Name = "Credito")]
        public bool clte_ConCredito { get; set; }
        [Display(Name = "Minorista")]
        public bool clte_EsMinorista { get; set; }
        [Display(Name = "Observaciones")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_Observaciones_ { get; set; }
        [Display(Name = "Usuario Creacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public System.DateTime clte_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public string clte_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido ")]
        public Nullable<System.DateTime> clte_FechaModifica { get; set; }
    }
}