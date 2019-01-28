using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(estadomovimientosMetaData))]
    public partial class tbEstadoMovimiento
    {
       
    }

    public class estadomovimientosMetaData
    {
      
        [Display(Name = "Número")]
        public byte estm_Id { get; set; }

        [Display(Name = "Descripción")]
[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "Máximo {1} caracteres")]
        public string estm_Descripcion { get; set; }
       
        //[Display(Name = "Creado por")]
        //public int estm_UsuarioCrea { get; set; }
     
        //[Display(Name = "Creado el")]
        ////[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //public System.DateTime estm_FechaCrea { get; set; }
      
        //[Display(Name = "Modificado por")]
        //public Nullable<int> estm_UsuarioModifica { get; set; }
        
        //[Display(Name = "Modificado el")]
        ////[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //public Nullable<System.DateTime> estm_FechaModifica { get; set; }
    }
}