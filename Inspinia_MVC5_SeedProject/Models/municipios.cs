using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(municipiosMetaData))]
    public partial class tbMunicipio
    {
    }
    public class municipiosMetaData
    {
        [Display(Name = "Codigo Municipio")]
        public string mun_Codigo { get; set; }
        [Display(Name = "Codigo Departamento")]
        public string dep_Codigo { get; set; }
        [Display(Name = "Nombre Municipio")]
        public string mun_Nombre { get; set; }
        [Display(Name = "Creado por")]
        public int mun_UsuarioCrea { get; set; }
        [Display(Name = "Creado el")]
        public System.DateTime mun_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> mun_UsuarioModifica { get; set; }
        [Display(Name = "Modificado el")]
        public Nullable<System.DateTime> mun_FechaModifica { get; set; }
    }
}