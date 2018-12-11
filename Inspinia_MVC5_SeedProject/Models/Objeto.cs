using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(Objetometatada))]
    public partial class tbObjeto
    {
        
    }
    public class Objetometatada
    {
        [Display(Name ="Número")]
        //[Required(AllowEmptyStrings =false,ErrorMessage ="Campo {0} es requerido")]
        public int obj_Id { get; set; }
        [Display(Name = "Pantalla")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} es requerido")]
        public string obj_Pantalla { get; set; }
        //[Display(Name = "Creado  por")]
        //[Required(AllowEmptyStrings = false)]
        //public int obj_UsuarioCrea { get; set; }
        //[Display(Name = "Creado El")]
        //[Required(AllowEmptyStrings = false)]
        //public System.DateTime obj_FechaCrea { get; set; }
        //[Display(Name = "Modificado Por")]
        //[Required(AllowEmptyStrings = false)]
        //public Nullable<int> obj_UsuarioModifica { get; set; }
        //[Display(Name = "Modificado El")]
        //[Required(AllowEmptyStrings = false)]
        //public Nullable<System.DateTime> obj_FechaModifica { get; set; }
    }
}