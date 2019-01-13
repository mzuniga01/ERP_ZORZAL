using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cDevolucionMetaData))]
    public partial class tbDevolucion
    {
    }
    public class cDevolucionMetaData
    {
        [Display(Name = "Codigo Devolución")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public int dev_Id { get; set; }

        [Display(Name = "Codigo Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public long fact_Id { get; set; }

        [Display(Name = "Codigo Caja")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public short cja_Id { get; set; }

        [Display(Name = "Fecha Devolución")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime dev_Fecha { get; set; }

        public Nullable<int> dev_UsuarioCrea { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> dev_FechaCrea { get; set; }
        public Nullable<int> dev_UsuarioModifica { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> dev_FechaModifica { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCaja tbCaja { get; set; }
      
   
    }
}