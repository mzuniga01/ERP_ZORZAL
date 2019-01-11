using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(ConsultarExistenciaProductos))]
    public partial class UDV_Inv_Consultar_Existencias_Productos
 
    {
    }
    public class ConsultarExistenciaProductos
    {
        [Display(Name = "Sucursal")]
        public string suc_Descripcion { get; set; }
        [Display(Name = "Bodega")]
        public string bod_Nombre { get; set; }
        [Display(Name = "Producto")]
        public string prod_Descripcion { get; set; }
        [Display(Name = "Marca")]
        public string prod_Marca { get; set; }
        [Display(Name = "Existente")]
        public Nullable<decimal> bodd_CantidadExistente { get; set; }
        [Display(Name = "Minima")]
        public decimal bodd_CantidadMinima { get; set; }
        [Display(Name = "Numero Bodega")]
        public int bod_Id { get; set; }
        [Display(Name = "Codigo De Producto")]
        public string prod_Codigo { get; set; }
    }
}