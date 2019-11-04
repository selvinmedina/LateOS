using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LateOS.Models;
using System.Web.Routing;

namespace LateOS.Attribute
{
    public class SessionManager: ActionFilterAttribute
    {
        //private readonly string _ScreenId;
        //Helpers funcion = new Helpers();
        //public SessionManager()
        //{

        //}

        //public SessionManager(string screeID)
        //{
        //    _ScreenId = screeID;
        //}

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);
        //    var Route = new RouteValueDictionary(new { action = "Index", controller="Pais"});
        //    var Route1 = new RouteValueDictionary(new { action = "Create", controller = "Pais" });
        //    string UsuarioLogeado = HttpContext.Current.Session["Username"].ToString();
        //    if (funcion.getUser(UsuarioLogeado))
        //        filterContext.Result = new RedirectToRouteResult(Route);
        //    else
        //        filterContext.Result = new RedirectToRouteResult(Route1);

        //    return;           
        //}
    }
}