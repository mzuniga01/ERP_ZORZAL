using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP_ZORZAL.Controllers;
using Inspinia_MVC5_SeedProject.Controllers;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Models
{
    public class GeneralFunctions
    {
        ERP_ZORZALEntities db = new ERP_ZORZALEntities();

        public bool GetUserRols(string sPantalla)
        {
            int UserID = 0;
            bool EsAdmin = false;
            bool Retorno = false;
            List<tbUsuario> Usuario = getUserID();
            
            foreach(tbUsuario User in Usuario)
            {
                UserID = User.usu_Id;
                EsAdmin = User.usu_EsAdministrador;
            }
            if (EsAdmin)
                Retorno = true;
            else
            {
                var Roles = db.SDP_Acce_GetUserRols(UserID, sPantalla);
                if(Roles.Count()>0)
                {
                    Retorno = true;
                }
            }
            return Retorno;
        }

        public List<tbUsuario> getUserID()
        {
            int user = 0;
            List<tbUsuario> UsuarioList = new List<tbUsuario>();
            try
            {
                user = (int)HttpContext.Current.Session["UserLogin"];
                if (user != 0)
                {
                    UsuarioList = db.tbUsuario.Where(s => s.usu_Id == user).ToList();
                }
                return UsuarioList;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return UsuarioList;
            }
        }
    }
}