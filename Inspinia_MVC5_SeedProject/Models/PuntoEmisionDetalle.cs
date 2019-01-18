using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(PuntoEmisionDetalleMetaData))]
    public partial class tbPuntoEmisionDetalle
    {
        

    }
    public class PuntoEmisionDetalleMetaData
    {
        [Display(Name = "Código Punto Emisión Detalle")]        
        public int pemid_Id { get; set; }

        [Display(Name = "Código Punto Emisión")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Numero CAI es requerido")]
        public int pemi_Id { get; set; }

        [Display(Name ="Documento Fiscal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string dfisc_Id { get; set; }

        [Display(Name = "Rango Inicial")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, MinimumLength = 19, ErrorMessage = "El campo {0} debe tener 19 caracteres")]
        public string pemid_RangoInicio { get; set; }

        [Display(Name = "Rango Final")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, MinimumLength = 19, ErrorMessage = "El campo {0} debe tener el mismo formato de Rango Inicial")]
        public string pemid_RangoFinal { get; set; }

        [Display(Name = "Número Actual")]
        public string pemid_NumeroActual { get; set; }

        [Display(Name = "Fecha Límite Emisión")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime pemid_FechaLimite { get; set; }

        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pemid_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime pemid_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificación")]
        public Nullable<int> pemid_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificó")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> pemid_FechaModifica { get; set; }
    }
}