using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(TipoIdentificacionesMetaData))]

    public partial class tbTipoIdentificacion
    {


    }



    public class TipoIdentificacionesMetaData
    {

     
        [Display(Name ="Id Tipo Identificación")]
        [Required]
        public byte tpi_Id { get; set; }
        [Display(Name = "Descripción")]
        [Required]
        public string tpi_Descripcion { get; set; }
        public string tpi_UsuarioCrea { get; set; }
        public System.DateTime tpi_FechaCrea { get; set; }
        public string tpi_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tpi_FechaModifica { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tbCliente> tbCliente { get; set; }
    }
}