using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(BodegaMetadata))]
    public partial class tbBodega
    {
    }

    public class BodegaMetadata
    {
        [Display(Name = "Número")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int bod_Id { get; set; }

        [Display(Name = "Nombre ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string bod_Nombre { get; set; }

        //[Display(Name = "Municipio")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //public virtual tbMunicipio tbMunicipio { get; set; }

        [Display(Name = "Responsable ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string bod_ResponsableBodega { get; set; }

        [Display(Name = "Dirección ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string bod_Direccion { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Se requiere un correo electrónico")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Es un Correo Electronico")]
        public string bod_Correo { get; set; }

        [Display(Name = "Telèfono ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string bod_Telefono { get; set; }

        //[Display(Name = "Usuario")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //public Nullable<int> usu_Id { get; set; }

        [Display(Name = "Municipio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string mun_Codigo { get; set; }

        [Display(Name = "Estado")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public byte bod_EsActiva { get; set; }

        //[Display(Name = "Creado Por")]
        ////[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //public int bod_UsuarioCrea { get; set; }

        //[Display(Name = "Creado El")]
        ////[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public System.DateTime bod_FechaCrea { get; set; }

        //[Display(Name = "Modificado Por")]
        ////[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //public Nullable<int> bod_UsuarioModifica { get; set; }

        //[Display(Name = "Modificado El")]
        ////[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public Nullable<System.DateTime> bod_FechaModifica { get; set; }

       
        

    }
}