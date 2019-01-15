using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(tbDocumentoFiscalMetaData))]

    public partial class tbDocumentoFiscal
    {
    }
    public class tbDocumentoFiscalMetaData
    {
        [Display(Name = " Codigo Documento ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public byte dfisc_Id { get; set; }
        [Display(Name = " Descripción ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string dfisc_Descripcion { get; set; }

        [Display(Name = "Usuario Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int dfisc_UsuarioCrea { get; set; }
        

        [Display(Name = "Fecha Creación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime dfisc_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificación")]
        public Nullable<int> dfisc_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificación")]

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> dfisc_FechaModifica { get; set; }
    }

}


