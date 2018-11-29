using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(DocumentoFiscalMetaData))]
    public partial class DocumentoFiscal
    {

    }

    public class DocumentoFiscalMetaData
    {
        [Display(Name = " Codigo Documento ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public byte dfisc_Codigo { get; set; }
        [Display(Name = " Descripcion ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido")]
        public string dfisc_Descripcion { get; set; }
    }
}