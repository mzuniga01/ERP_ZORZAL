using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Models
{
    public class cUtilities
    {
        public static List<cMinorista> MinoristaList()
        {
            List<cMinorista> list = new List<cMinorista>();

            list.Add(new cMinorista()
            {
                ID_MINORISTA = "1",
                DESCRIPCION = "Si"
            });
            list.Add(new cMinorista()
            {
                ID_MINORISTA = "0",
                DESCRIPCION = "No"
            });
            return list;
        }


        public static List<cActivo> EstadoList()
        {
            List<cActivo> list = new List<cActivo>();

            list.Add(new cActivo()
            {
                ID_ACTIVO = "1",
                DESCRIPCION = "Si"
            });
            list.Add(new cActivo()
            {
                ID_ACTIVO = "0",
                DESCRIPCION = "No"
            });
            return list;
        }

        public static List<cTipoCuenta> TipoCuentaList()
        {
            List<cTipoCuenta> list = new List<cTipoCuenta>();

            list.Add(new cTipoCuenta()
            {
                ID_TIPOCUENTA = 1,
                DESCRIPCION = "Ahorro"
            });
            list.Add(new cTipoCuenta()
            {
                ID_TIPOCUENTA = 0,
                DESCRIPCION = "Cheques"
            });
            return list;
        }

        public static List<cGenero> GeneroList()
        {
            List<cGenero> list = new List<cGenero>();

            list.Add(new cGenero()
            {
                ID_GENERO = "H",
                DESCRIPCION = "Hombre"
            });
            list.Add(new cGenero()
            {
                ID_GENERO = "M",
                DESCRIPCION = "Mujer"
            });
            return list;
        }
        public static List<cNacionalidad> NacionalidadList()
        {
            List<cNacionalidad> list = new List<cNacionalidad>();

            list.Add(new cNacionalidad()
            {
                DESCRIPCION = "Hondureña",
            });
            list.Add(new cNacionalidad()
            {
                DESCRIPCION = "Mexicano",
            });
            list.Add(new cNacionalidad()
            {
                DESCRIPCION = "EstadoUnidense"
            });
            return list;
        }

        public static List<cDepartamento> DepartamentoList()
        {
            List<cDepartamento> list = new List<cDepartamento>();

            list.Add(new cDepartamento()
            {
                DESCRIPCION = "Olancho",
            });
            list.Add(new cDepartamento()
            {
                DESCRIPCION = "Atlántida",
            });
            list.Add(new cDepartamento()
            {
                DESCRIPCION = "La Ceiba"
            });
            list.Add(new cDepartamento()
            {
                DESCRIPCION = "Choluteca"
            });
            list.Add(new cDepartamento()
            {
                DESCRIPCION = "Cortes"
            });

            return list;
        }
        public static List<cDenominacion> DenominacionList()
        {
            List<cDenominacion> list = new List<cDenominacion>();

            list.Add(new cDenominacion()
            {
                ID_TipoDenominacion = 1,
                Tipo_Denominacion = "Billete"
            });
            list.Add(new cDenominacion()
            {
                ID_TipoDenominacion = 2,
                Tipo_Denominacion = "Moneda"
            });


            return list;
        }
    }
}

