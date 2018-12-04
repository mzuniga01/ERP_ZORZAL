using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    
    [MetadataType(typeof(PuntoEmisionMetaData))]
    public partial class tbPuntoEmision
    {
       
    }
    public class PuntoEmisionMetaData
    {
        [Display(Name ="Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pemi_Id { get; set; }

        //[Display(Name = "Sucursal")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //public short suc_Id { get; set; }

        [Display(Name = "Número CAI")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string pemi_NumeroCAI { get; set; }
        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]

        public int pemi_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public System.DateTime pemi_FechaCrea { get; set; }
        [Display(Name = "Usuario Modificación")]

        public Nullable<int> pemi_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public Nullable<System.DateTime> pemi_FechaModifica { get; set; }

    
    }
}