using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(rolMetaData))]
    public partial class tbRol    {

    }
    public class rolMetaData
    {
        [Display(Name = "Código")]
        public int rol_Id { get; set; }
        [Display(Name = "Descripción Rol")]
        public string rol_Descripcion { get; set; }
        [Display(Name = "Creado por")]
        public int rol_UsuarioCrea { get; set; }
        [Display(Name = "Creado el")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime rol_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> rol_UsuarioModifica { get; set; }
        [Display(Name = "Modificado el")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> rol_FechaModifica { get; set; }
    }
}