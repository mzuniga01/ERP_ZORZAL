using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(tbDepartamentoMetaData))]
    public partial class tbDepartamento
    {

    }

    public class tbDepartamentoMetaData
    {

        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "Código")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo se permiten números.")]
        public string dep_Codigo { get; set; }

        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "Departamento")]
        public string dep_Nombre { get; set; }

        [Display(Name = "Creado Por")]
        public int dep_UsuarioCrea { get; set; }
        [Display(Name = "Creado El")]
      
        
        public System.DateTime dep_FechaCrea { get; set; }
        [Display(Name = "Modificado Por")]
        public Nullable<int> dep_UsuarioModifica { get; set; }
        [Display(Name = "Modificado El")]

        public Nullable<System.DateTime> dep_FechaModifica { get; set; }
        
    }

}
