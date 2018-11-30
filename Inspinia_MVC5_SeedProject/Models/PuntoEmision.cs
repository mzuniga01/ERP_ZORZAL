using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_ZORZAL.Models
{
    
    [MetadataType(typeof(PuntoEmisionMetaData))]
    public partial class tbPuntoEmision
    {
       
    }
    public class PuntoEmisionMetaData
    {
        [Display(Name ="Id Punto Emisión")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pemi_Id { get; set; }

        [Display(Name = "Id Sucursal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short suc_Id { get; set; }

        [Display(Name = "Número CAI")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string pemi_NumeroCAI { get; set; }
        
        public int pemi_UsuarioCrea { get; set; }
        
        public System.DateTime pemi_FechaCrea { get; set; }
        
        public Nullable<int> pemi_UsuarioModifica { get; set; }
        
        public Nullable<System.DateTime> pemi_FechaModifica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPuntoEmisionDetalle> tbPuntoEmisionDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSucursal> tbSucursal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPuntoEmisionDetalle> tbPuntoEmisionDetalle1 { get; set; }
        public virtual tbSucursal tbSucursal1 { get; set; }
    }
}