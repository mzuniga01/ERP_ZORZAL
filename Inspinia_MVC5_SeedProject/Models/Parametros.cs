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
        public byte par_Id { get; set; }
        [Display(Name = "Nombre Empresa")]
        public string par_NombreEmpresa { get; set; }
        [Display(Name = "Telefono Empresa")]
        public string par_TelefonoEmpresa { get; set; }
        [Display(Name = "Correo Empresa")]
        [EmailAddress(ErrorMessage = "El email no tiene el formato")]
        public string par_CorreoEmpresa { get; set; }
        [Display(Name = "Logo")]
        public string par_PathLogo { get; set; }
        [Display(Name = "Moneda")]
        public short mnda_Id { get; set; }
        [Display(Name = "Rol Gerente de Tienda")]
        public int par_RolGerenteTienda { get; set; }
        [Display(Name = "Rol Credito-Cobranza")]
        public int par_RolCreditoCobranza { get; set; }
        [Display(Name = "Rol Supervisor de Caja")]
        public int par_RolSupervisorCaja { get; set; }
        [Display(Name = "Rol Cajero")]
        public int par_RolCajero { get; set; }
        [Display(Name = "Rol Auditor")]
        public int par_RolAuditor { get; set; }
        [Display(Name = "Rol Sucursal Principal")]
        public short par_SucursalPrincipal { get; set; }
        public int par_UsuarioCrea { get; set; }
        public System.DateTime par_FechaCrea { get; set; }
        public Nullable<int> par_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> par_FechaModifica { get; set; }
        [Display(Name = "Porcentaje de Descuento TE")]
        public Nullable<decimal> par_PorcentajeDescuentoTE { get; set; }
        [Display(Name = "Consumidor Final")]
        public Nullable<int> par_IdConsumidorFinal { get; set; }
    }
}