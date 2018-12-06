using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(objetoMetaData))]
    public partial class tbObjeto
    {
    }
    public class objetoMetaData
    {
        [Display(Name = "Numero")]
        public int obj_Id { get; set; }
        [Display(Name = "Objeto")]
        public string obj_Pantalla { get; set; }
        [Display(Name = "Creado por")]
        public int obj_UsuarioCrea { get; set; }
        [Display(Name = "Creado el")]
        public System.DateTime obj_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> obj_UsuarioModifica { get; set; }
        [Display(Name = "Modificado el")]
        public Nullable<System.DateTime> obj_FechaModifica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbAccesoRol> tbAccesoRol { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}