using LateOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LateOS.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private V_Usuarios oUsuario;
        private LateOSEntities db = new LateOSEntities();
        private int idRol;

        public AuthorizeUser(int idRol)
        {
            this.idRol = idRol;
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                oUsuario = (V_Usuarios)HttpContext.Current.Session["User"];         
                
                if(oUsuario == null)
                {
                    filterContext.Result = new RedirectResult("~/Login/Index");
                }       
                    
                var rolUsuario = (from d in db.Usuario_Rol
                                    where d.id_Rol == idRol && d.usu_Id == oUsuario.usu_Id
                                    select d.tbRol).ToList();


                if (rolUsuario.ToList().Count() == 0)
                {                    
                    filterContext.Result = new RedirectResult("~/Login/Index");
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
        }
        

    }
}