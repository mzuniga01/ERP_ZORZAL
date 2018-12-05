using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{

    [MetadataType(typeof(TiposIdenticacionMetaData))]
    public partial class tbTipoIdentificacion
    {
       
    }
    public class TiposIdenticacionMetaData
    {
        [Display(Name = "ID Tipo Identificación")]

        public byte tpi_Id { get; set; }

        [Display(Name = "Tipo Identificación")]
        public string tpi_Descripcion { get; set; }

        [Display(Name = "Usuario Crea")]
        public int tpi_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime tpi_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tpi_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tpi_FechaModifica { get; set; }
    }
}