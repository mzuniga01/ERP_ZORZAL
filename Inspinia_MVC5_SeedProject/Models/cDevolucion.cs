using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models

{
    [MetadataType(typeof(cDevolucionesMetaData))]
    public partial class tbDevolucion
    {
    }
    public class cDevolucionesMetaData
    {
        [Display(Name = "Codigo Devolución")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string dev_Id { get; set; }

        [Display(Name = "Codigo Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string fact_Codigo { get; set; }

        [Display(Name = "Codigo Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public virtual tbFactura tbFactura1 { get; set; }

        [Display(Name = "Fecha Devolución")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime dev_Fecha { get; set; }

        [Display(Name = "Cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int clte_id { get; set; }

        public string dev_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> dev_FechaCrea { get; set; }
        public string dev_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> dev_FechaModifica { get; set; }

        public virtual tbFactura tbFactura { get; set; }
    }
}