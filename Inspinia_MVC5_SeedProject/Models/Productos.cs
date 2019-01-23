using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    

    [MetadataType(typeof(ProductosMetadata))]


    public partial class tbProducto
    {
        [NotMapped]
        [Display(Name = "Categoria")]
        public string pcat_Id { get; set; }


    }

    public class ProductosMetadata
    {
        [Display(Name = "Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //[RegularExpression("^[A-Z]{4}-[0-9]{4}-[A-Z0-9]{4}$|[A-Z0-9]{14}", ErrorMessage = "No es un Codigo válido. Ejemplo AAAA-9999-AA999")]
        public string prod_Codigo { get; set; }

        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Descripcion { get; set; }

        [Display(Name = "Marca")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Marca { get; set; }

        [Display(Name = "Modelo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Modelo { get; set; }

        [Display(Name = "Talla")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Talla { get; set; }

        [Display(Name = "Color")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Color { get; set; }

        [Display(Name = "SubCategoria")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pscat_Id { get; set; }

        [Display(Name = "Unidad Medida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int uni_Id { get; set; }

        [Display(Name = "Usuario")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int prod_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime prod_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<int> prod_UsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<System.DateTime> prod_FechaModifica { get; set; }

        [Display(Name = "Es Activo?")]
        public bool prod_EsActivo { get; set; }

        [Display(Name = "Razon Inactivación")]        
        public string prod_Razon_Inactivacion { get; set; }

        //[Display(Name = "Precio")]    
        //public Nullable<int> listp_Id { get; set; }

        [Display(Name = "Codigo de Barras")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression("^[A-Z0-9]{14}$|^[A-Z0-9]{13}$|^[A-Z0-9]{12}$|^[A-Z0-9]{10}$|^[A-Z0-9]{8}$", ErrorMessage = "Debe ser Un Codigo de Barras")]
        public string prod_CodigoBarras { get; set; }

        [Display(Name = "Carrelativo")]
        public Nullable<int> prod_Correlativo { get; set; }


        public virtual tbUsuario tbUsuario { get; set; }
        //public virtual tbProductoCategoria tbProductoCategoria { get; set; }
        public virtual tbUnidadMedida tbUnidadMedida { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbBodegaDetalle> tbBodegaDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEntradaDetalle> tbEntradaDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbInventarioFisicoDetalle> tbInventarioFisicoDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbDevolucionDetalle> tbDevolucionDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       
        public virtual ICollection<tbFacturaDetalle> tbFacturaDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbListadoPrecioDetalle> tbListadoPrecioDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPedidoDetalle> tbPedidoDetalle { get; set; }
    }
}