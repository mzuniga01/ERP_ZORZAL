using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(municipiosMetaData))]
    public partial class tbMunicipio
    {
    }
    public class municipiosMetaData
    {
        [Display(Name = "Código Municipio")]
        public string mun_Codigo { get; set; }

        [Display(Name = "Código Departamento")]
        public string dep_Codigo { get; set; }
        [Display(Name = "Municipio")]
        public string mun_Nombre { get; set; }
        [Display(Name = "Creado Por")]
        public int mun_UsuarioCrea { get; set; }
        [Display(Name = "Creado El")]
        public System.DateTime mun_FechaCrea { get; set; }
        [Display(Name = "Modificado Por")]
        public Nullable<int> mun_UsuarioModifica { get; set; }
        [Display(Name = "Modificado El")]
        public Nullable<System.DateTime> mun_FechaModifica { get; set; }
    }
}