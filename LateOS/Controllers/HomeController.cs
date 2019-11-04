using LateOS.Filters;
using LateOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LateOS.Controllers
{
    public class HomeController : Controller
    {
        private LateOSEntities db = new LateOSEntities();
        // GET: Home
        public ActionResult Index()
        {
            var tbCategoria = db.tbCategoria.ToList();
            return View(tbCategoria.ToList());
        }

        //public ActionResult Index()
        //{
        //    ViewBag.nombre = db.tbCategoria.ToList();
        //    return View();
        //}

        //public ActionResult _IndexCategoria()
        //{
        //    var ListadoCategoria = db.UDP_Inv_tbCategoria_Select_Descripcion().ToList();
        //    return PartialView(ListadoCategoria);
        //}

        //public JsonResult _IndexCategoria(int id)
        //{
        //    var ListadoCategoria = db.UDP_Inv_tbCategoria_Select_Descripcion().ToList();
        //    return Json(ListadoCategoria, JsonRequestBehavior.AllowGet);
        //}

        //
        //ACTIONS : MALCOM MEDINA
        //

        //
        //Retorna Vista : InfoStatus(Charts) 
        [AuthorizeUser(1)]
        public ActionResult InfoStatus()
        {
            return View();
        }

        //
        //GET : USUARIOS POR GENERO
        public JsonResult GET_Gender(int? Cantidad)
        {
            Cantidad = 0;
            var CantidadUsuarios = (from x in db.tbCliente
                                    select x.clte_Id);
            var CantidadHombres = (from x in db.tbCliente
                                   where x.clte_Sexo == "M"
                                   select x.clte_Id);
            var CantidadMujeres = (from x in db.tbCliente
                                   where x.clte_Sexo == "F"
                                   select x.clte_Id);
            var Array = CantidadUsuarios.Count() + "%" + CantidadMujeres.Count() + "%" + CantidadHombres.Count();
            return Json(Array, JsonRequestBehavior.AllowGet);
        }

    }
}