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
        [Display(Name = "Código")]
        public string dep_Codigo { get; set; }

        [Display(Name = "Departamento")]
        public string dep_Nombre { get; set; }

        [Display(Name = "Creado por")]
        public int dep_UsuarioCrea { get; set; }
        [Display(Name = "Creado el")]
      
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime dep_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> dep_UsuarioModifica { get; set; }
        [Display(Name = "Modificado el")]
        
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> dep_FechaModifica { get; set; }
        
    }

}
