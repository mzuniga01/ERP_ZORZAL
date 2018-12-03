using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    
    [MetadataType(typeof(ListaPreciosMetaData))]

    public class tbListaPrecios
    {
    }

    public class ListaPreciosMetaData
    {

        [Display(Name = "Id Lista Precio")]
        [Required]
        public int listp_Id { get; set; }
        [Display(Name = "Id Listado Activp")]
        [Required]
        public Nullable<bool> listp_EsActivo { get; set; }
        [Display(Name = "Usuario Crea")]
        [Required]
        public int listp_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        [Required]
        public System.DateTime listp_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        [Required]
        public Nullable<int> listp_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        [Required]
        public Nullable<System.DateTime> listp_FechaModifica { get; set; }

    }
}