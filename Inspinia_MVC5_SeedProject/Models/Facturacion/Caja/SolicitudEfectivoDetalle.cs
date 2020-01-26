using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(SolicitudEfectivoDetalleMetaData))]
    public partial class tbSolicitudEfectivoDetalle
    {
        
    }
    public class SolicitudEfectivoDetalleMetaData
    {
        [Display(Name = "Número")]
        
        public int soled_Id { get; set; }
        [Display(Name = "Número Solictud")]
       
        public int solef_Id { get; set; }
        [Display(Name = "Denominacion")]
      
        public short deno_Id { get; set; }
        [Display(Name = "Cantidad Solicitada")]
     
        public short soled_CantidadSolicitada { get; set; }
        [Display(Name = "Cantidad Entregada")]
        
        public short soled_CantidadEntregada { get; set; }
        [Display(Name = "Monto de Entregado")]        
        public decimal soled_MontoEntregado { get; set; }
        [Display(Name = "Usuario Creación")]        
        public int soled_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creación")]        
        public System.DateTime soled_FechaCrea { get; set; }
        [Display(Name = "Usuario Modificación")]       
        public Nullable<int> soled_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modificación")]       
        public Nullable<System.DateTime> soled_FechaModifica { get; set; }


      

      

    }
}