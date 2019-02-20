using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(ParametrosMetadata))]

    public partial class tbParametro
    {
    }
    public class ParametrosMetadata
    {
        [Display(Name = "Parametro Id")]
        [Required]
        public byte par_Id { get; set; }
        [Display(Name = "Nombre Empresa")]
        [StringLength(100, ErrorMessage = "No puede ingresar mas de 100 caracteres")]
        [Required]
        public string par_NombreEmpresa { get; set; }
        [StringLength(50, ErrorMessage = "No puede ingresar mas de 50 caracteres")]
        [Display(Name = "Telefono Empresa")]
        [Required]
        public string par_TelefonoEmpresa { get; set; }
        [Display(Name = "Correo Empresa")]
        [EmailAddress(ErrorMessage = "El email no tiene el formato correcto")]
        [StringLength(50, ErrorMessage = "No puede ingresar mas de 50 caracteres")]
        [Required]
        public string par_CorreoEmpresa { get; set; }
        [Display(Name = "Logo")]
        public string par_PathLogo { get; set; }
        [Display(Name = "Moneda")]
        [Required]
        public short mnda_Id { get; set; }
        [Display(Name = "Rol Gerente de Tienda")]
        [Required]
        public int par_RolGerenteTienda { get; set; }
        [Display(Name = "Rol Credito-Cobranza")]
        [Required]
        public int par_RolCreditoCobranza { get; set; }
        [Display(Name = "Rol Supervisor de Caja")]
        [Required]
        public int par_RolSupervisorCaja { get; set; }
        [Display(Name = "Rol Cajero")]
        [Required]
        public int par_RolCajero { get; set; }
        [Display(Name = "Rol Auditor")]
        [Required]
        public int par_RolAuditor { get; set; }
        [Display(Name = "Sucursal Principal")]
        [Required]
        public short par_SucursalPrincipal { get; set; }
        public int par_UsuarioCrea { get; set; }
        public System.DateTime par_FechaCrea { get; set; }
        public Nullable<int> par_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> par_FechaModifica { get; set; }
        [Display(Name = "Porcentaje de Descuento TE")]
        [Required]
        public Nullable<decimal> par_PorcentajeDescuentoTE { get; set; }
        [Display(Name = "Consumidor Final")]
        [Required]
        public Nullable<int> par_IdConsumidorFinal { get; set; }
    }
}