using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    [MetadataType(typeof(_DepartamentosMetaData))]
    public partial class tbDepartamentos
    {

    }
    public class _DepartamentosMetaData
    {
        [Display(Name ="Código")]
        public string dpto_Codigo { get; set; }
        [Display(Name = "Departamento")]
        public string dpto_Descripcion { get; set; }
    }
   
}