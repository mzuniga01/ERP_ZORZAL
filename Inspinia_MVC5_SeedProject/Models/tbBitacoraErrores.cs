//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbBitacoraErrores
    {
        public int bite_Id { get; set; }
        public Nullable<int> obj_Id { get; set; }
        public Nullable<int> bite_Usuario { get; set; }
        public Nullable<System.DateTime> bite_Fecha { get; set; }
        public string bite_MensajeError { get; set; }
        public string bite_Accion { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbObjeto tbObjeto { get; set; }
    }
}
