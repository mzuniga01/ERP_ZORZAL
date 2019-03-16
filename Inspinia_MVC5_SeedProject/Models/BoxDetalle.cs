using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(BoxDetalleMetaData))]
    public class BoxDetalleMetaData
    {
        [Display(Name = "Codigo de Caja")]
        public int boxd_Id { get; set; }

        [Display(Name = "Cod. Caja")]
        public string box_Codigo { get; set; }

        [Display(Name = "Cod. Producto")]
        public string prod_Codigo { get; set; }
        
        [Display(Name = "Cantidad")]
        public decimal boxd_Cantidad { get; set; }

        [Display(Name = "Creado Por")]

        public int boxd_UsuarioCrea { get; set; }

        [Display(Name = "Creado el")]
        public System.DateTime boxd_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public Nullable<int> boxd_UsuarioModifica { get; set; }

        [Display(Name = "Modificado el")]
        public Nullable<System.DateTime> boxd_FechaModifica { get; set; }
    }

        public partial class tbBoxDetalle
        {
           
        }
    }