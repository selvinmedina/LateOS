using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LateOS.Models;
using LateOS.Filters;

namespace LateOS.Controllers
{
    public class ListadoPrecioController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: ListadoPrecio
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Descripcion");
            ViewBag.tipd_Id = new SelectList(db.tbTipoDescuento, "tipd_Id", "tipd_Descripcion");
            
            var tbListadoPrecio = db.tbListadoPrecio.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            return View(tbListadoPrecio.ToList());
        }

        public ActionResult _IndexListadoPrecioDetalle(int idListadoPrecio) {
            return PartialView(db.tbListadoPrecioDetalle.Where(x => x.lipd_Id == idListadoPrecio).ToList());
        }

        // GET: ListadoPrecio/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbListadoPrecio tbListadoPrecio = db.tbListadoPrecio.Find(id);
            if (tbListadoPrecio == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbListadoPrecio);
        }

        // GET: ListadoPrecio/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.lip_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.lip_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: ListadoPrecio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "lip_Id,lip_Descripcion,lip_Prioridad,lip_EsActivo,lip_FechaInicio,lip_FechaFin,lip_UsuarioCrea,lip_FechaCrea,lip_UsuarioModifica,lip_FechaModifica")] tbListadoPrecio tbListadoPrecio)
        {
            if (ModelState.IsValid)
            {
                db.tbListadoPrecio.Add(tbListadoPrecio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.lip_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbListadoPrecio.lip_UsuarioCrea);
            ViewBag.lip_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbListadoPrecio.lip_UsuarioModifica);
            return View(tbListadoPrecio);
        }

        // GET: ListadoPrecio/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbListadoPrecio tbListadoPrecio = db.tbListadoPrecio.Find(id);
            if (tbListadoPrecio == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.lip_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbListadoPrecio.lip_UsuarioCrea);
            ViewBag.lip_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbListadoPrecio.lip_UsuarioModifica);
            return View(tbListadoPrecio);
        }

        // POST: ListadoPrecio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "lip_Id,lip_Descripcion,lip_Prioridad,lip_EsActivo,lip_FechaInicio,lip_FechaFin,lip_UsuarioCrea,lip_FechaCrea,lip_UsuarioModifica,lip_FechaModifica")] tbListadoPrecio tbListadoPrecio)
        {
            if (ModelState.IsValid)
            {
                tbListadoPrecio.lip_UsuarioModifica = 3;
                tbListadoPrecio.lip_FechaModifica = DateTime.Now;

                IEnumerable<object> listListadoPrecio = null;
                string MensajeError = "";
                try
                {
                    listListadoPrecio = db.UDP_Vent_tbListadoPrecio_Update(tbListadoPrecio.lip_Id,
                                                                     tbListadoPrecio.lip_Descripcion,
                                                                     tbListadoPrecio.lip_Prioridad,
                                                                     tbListadoPrecio.lip_EsActivo,
                                                                     tbListadoPrecio.lip_FechaInicio,
                                                                     tbListadoPrecio.lip_FechaFin,
                                                                     tbListadoPrecio.lip_UsuarioModifica,
                                                                     tbListadoPrecio.lip_FechaModifica);

                    foreach (UDP_Vent_tbCliente_Update_Result Resultado in listListadoPrecio)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbListadoPrecio);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            ViewBag.lip_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbListadoPrecio.lip_UsuarioCrea);
            ViewBag.lip_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbListadoPrecio.lip_UsuarioModifica);
            return View(tbListadoPrecio);
        }

        public JsonResult _EditListadoPrecioDetalle(int id)
        {
            var ListadoPrecioDetalle = db.UDP_Vent_tbListadoPrecioDetalle_Select(id).ToList();
            return Json(ListadoPrecioDetalle, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateListadoPrecioDetalle(tbListadoPrecioDetalle ListadoPrecioDetalle)
        {
            ListadoPrecioDetalle.lipd_UsuarioModifica = 3;
            ListadoPrecioDetalle.lipd_FechaModifica = DateTime.Now;
            IEnumerable<object> listListadoPrecioDetalle = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listListadoPrecioDetalle = db.UDP_Vent_tbListadoPrecioDetalle_Update(ListadoPrecioDetalle.lipd_Id, 
                                                                                        ListadoPrecioDetalle.lip_Id, 
                                                                                        ListadoPrecioDetalle.prod_Id, 
                                                                                        ListadoPrecioDetalle.tipd_Id, 
                                                                                        ListadoPrecioDetalle.lipd_Precio, 
                                                                                        ListadoPrecioDetalle.lipd_ISV,
                                                                                        ListadoPrecioDetalle.lipd_UsuarioModifica, 
                                                                                        ListadoPrecioDetalle.lipd_FechaModifica);
                    foreach (UDP_Vent_tbListadoPrecioDetalle_Update_Result Resultado in listListadoPrecioDetalle)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo modificar el registro, contacte con el administrador");
                        //return View();
                    }
                    _IndexListadoPrecioDetalle(ListadoPrecioDetalle.lipd_Id);

                }
                catch(Exception Ex)
                {
                    Ex.Message.ToString();
                }
                
                return Json("Exito", JsonRequestBehavior.AllowGet);
            }
            
            return Json("Hola", JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
