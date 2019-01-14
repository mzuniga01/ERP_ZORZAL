using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class Helpers
    {
        public const int Anulado = 4;
        public const int RTN = 3;
        public const int ID = 2;

        //Estados Cliente 
        public const bool ClienteActivo = true;
        public const bool ClienteInactivo = false;


        //Estado Solicitud Credito
        public const int SolicitudPendiente = 1;
        public const int SolicitudAprobado = 2;
        public const int SolicitudDenegado = 3;
    }
}