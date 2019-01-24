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
            
            try
            {
                UserID = (int)HttpContext.Current.Session["UserLogin"];
                EsAdmin = (bool)HttpContext.Current.Session["UserLoginEsAdmin"];
                if(EsAdmin)
                {
                    Retorno = true;
                }
                else
                {
                    var list = (IEnumerable<SDP_Acce_GetUserRols_Result>)HttpContext.Current.Session["UserLoginRols"];
                    var BuscarList = list.Where(x=> x.obj_Referencia == sPantalla);
                    int Conteo = BuscarList.Count();
                    if (Conteo>0)
                        Retorno = true;
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                Retorno = false;
            }
            return Retorno;
            //List<tbUsuario> Usuario = getUserInformation(); 
            //foreach(tbUsuario User in Usuario)
            //{
            //    UserID = User.usu_Id;
            //    EsAdmin = User.usu_EsAdministrador;
            //}
            //if (EsAdmin)
            //    Retorno = true;
            //else
            //{
            //    var Roles = db.SDP_Acce_GetUserRols(UserID, sPantalla);
            //    if (Roles.Count() > 0)
            //    {
            //        Retorno = true;
            //    }
            //}

        }

        public List<tbUsuario> getUserInformation()
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

        public bool GetUserLogin()
        {
            bool state = false;
            int user = 0;
            try
            {
                user = (int)HttpContext.Current.Session["UserLogin"];
                if (user != 0)
                   state = true;
            }
            catch(Exception Ex)
            {
                Ex.Message.ToString();
                state = false;
            }
            return state;
        }

        public int GetUser()
        {
            int user = 0;
            try
            {
                user = (int)HttpContext.Current.Session["UserLogin"];
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return user;            
        }

        public DateTime DatetimeNow()
        {
            DateTime dt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-6)).DateTime;
            return dt;
        }

        public void InsertBitacoraErrores(string sPantalla, string biteMensajeError, string biteAccion)
        {
            IEnumerable<object> List = null;
            int objID = 0;
            try
            {
                var BuscarList = db.tbObjeto.Where(x => x.obj_Referencia == sPantalla);
                foreach (tbObjeto Obj in BuscarList)
                    objID = Obj.obj_Id;
                List = db.UDP_Acce_tbBitacoraErrores_Insert(objID, GetUser(), DatetimeNow(), biteMensajeError, biteAccion);
            }
            catch(Exception Ex)
            {
                Ex.Message.ToString();
            }
        }
    }
}