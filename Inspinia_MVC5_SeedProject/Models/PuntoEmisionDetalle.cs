using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(PuntoEmisionDetalleMetaData))]
    public partial class tbPuntoEmisionDetalle
    {

        
    }
    public class PuntoEmisionDetalleMetaData
    {
        [Display(Name = "Id Punto Emisión Detalle")]
        public int pemid_Id { get; set; }

        [Display(Name = "Id Punto Emisión")]
        public int pemi_Id { get; set; }

        [Display(Name ="Documento Fiscal")]
        public byte dfisc_Id { get; set; }

        [Display(Name = "Rango Inicio")]
        public string pemid_RangoInicio { get; set; }

        [Display(Name = "Rango Final")]
        public string pemid_RangoFinal { get; set; }

        [Display(Name = "Fecha Límite")]
        public System.DateTime pemid_FechaLimite { get; set; }

        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pemid_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime pemid_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificación")]
        public Nullable<int> pemid_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificó")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> pemid_FechaModifica { get; set; }
    }
}