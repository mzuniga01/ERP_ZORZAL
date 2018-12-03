using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(tbPuntoEmisionDetalleMetaData))]
    public class PuntoEmisionDetalle
    {

    }
    public partial class tbPuntoEmisionDetalleMetaData
    {
        [Display(Name = "Punto Emision Detalle")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int pemid_Id { get; set; }
        [Display(Name = "Punto Emision")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int pemi_Id { get; set; }
        [Display(Name = "Documentos fiscales")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public byte dfisc_Id { get; set; }
        [Display(Name = "Rango inicial")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string pemid_RangoInicio { get; set; }
        [Display(Name = "Rango final")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string pemid_RangoFinal { get; set; }
        [Display(Name = "Fecha limite")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public System.DateTime pemid_FechaLimite { get; set; }
        [Display(Name = "Usuario crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public int pemid_UsuarioCrea { get; set; }
        [Display(Name = "Fecha crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public System.DateTime pemid_FechaCrea { get; set; }
        [Display(Name = "Usuario modifica")]
        public Nullable<int> pemid_UsuarioModifica { get; set; }
        [Display(Name = "Fecha modifica")]
        public Nullable<System.DateTime> pemid_FechaModifica { get; set; }
    }
}