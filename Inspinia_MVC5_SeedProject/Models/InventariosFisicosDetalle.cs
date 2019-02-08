using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(InventariosFisicosDetalleMetadata))]


public partial class tbInventarioFisicoDetalle
{
    

}

    public class InventariosFisicosDetalleMetadata
    {
    [Display(Name = "Id Inventario Fisico Detalle")]
    public int invfd_Id { get; set; }

    [Display(Name = "Id Inventario Fisico")]
    public int invf_Id { get; set; }

    [Display(Name = "Código de Producto")]
    public string prod_Codigo { get; set; }


        [Display(Name = "Cantidad Fisica")]
    public decimal invfd_Cantidad { get; set; }

    [Display(Name = "Cantidad Sistema")]
    public decimal invfd_CantidadSistema { get; set; }

        [Display(Name = "Unidad de Medida")]
        public int uni_Id { get; set; }

    [Display(Name = "Creado por")]
    public int invfd_UsuarioCrea { get; set; }

    [Display(Name = "Creado el")]
    public System.DateTime invfd_FechaCrea { get; set; }

    [Display(Name = "Modificado en")]
    public Nullable<int> invfd_UsuarioModifica { get; set; }

    [Display(Name = "Modificado el")]
    public Nullable<System.DateTime> invfd_FechaModifica { get; set; }

    }
}