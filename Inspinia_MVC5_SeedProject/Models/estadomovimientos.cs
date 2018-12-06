using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    [MetadataType(typeof(estadomovimientosMetaData))]
    public partial class tbEstadoMovimiento
    {
       
    }

    public class estadomovimientosMetaData
    {
        [Display(Name = "Numero Movimiento")]
        public byte estm_Id { get; set; }
        [Display(Name = "Descripción")]
        public string estm_Descripcion { get; set; }
        [Display(Name = "Creado por")]
        public int estm_UsuarioCrea { get; set; }
        [Display(Name = "Creado el")]
        public System.DateTime estm_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> estm_UsuarioModifica { get; set; }
        [Display(Name = "Modificado el")]
        public Nullable<System.DateTime> estm_FechaModifica { get; set; }
    }
}