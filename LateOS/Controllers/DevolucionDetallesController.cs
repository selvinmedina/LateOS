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
    public class DevolucionDetallesController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: DevolucionDetalles
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbDevolucionDetalle = db.tbDevolucionDetalle.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbDevolucion);
            return View(tbDevolucionDetalle.ToList());
        }

        // GET: DevolucionDetalles/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucionDetalle tbDevolucionDetalle = db.tbDevolucionDetalle.Find(id);
            if (tbDevolucionDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbDevolucionDetalle);
        }

        // GET: DevolucionDetalles/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.devd_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.devd_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Razon");
            return View();
        }

        // POST: DevolucionDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "dev_Id,prod_Id,prod_Precio,devd_Cantidad,devd_Impuesto,devd_Descuento,devd_UsuarioCrea,devd_FechaCrea")] tbDevolucionDetalle tbDevolucionDetalle)
        {
            tbDevolucionDetalle.devd_UsuarioCrea = 3;
            tbDevolucionDetalle.devd_FechaCrea = DateTime.Now;
            tbDevolucionDetalle.devd_UsuarioModifica = 3;
            tbDevolucionDetalle.devd_FechaModifica = DateTime.Now;
            IEnumerable<object> listDevolucionDet = null;
            string MensajeError = "";

            if (ModelState.IsValid)
            {
                try
                {
                    listDevolucionDet = db.UDP_Vent_tbDevolucionDetalle_Insert(tbDevolucionDetalle.dev_Id,
                                                                               tbDevolucionDetalle.prod_Id,
                                                                               tbDevolucionDetalle.prod_Precio,
                                                                               tbDevolucionDetalle.devd_Cantidad,
                                                                               tbDevolucionDetalle.devd_Impuesto, 
                                                                               tbDevolucionDetalle.devd_Descuento,
                                                                               tbDevolucionDetalle.devd_UsuarioCrea,
                                                                               tbDevolucionDetalle.devd_FechaCrea);
                    foreach (UDP_Vent_tbDevolucionDetalle_Insert_Result Resultado in listDevolucionDet)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                        return View(tbDevolucionDetalle);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }

            ViewBag.devd_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucionDetalle.devd_UsuarioCrea);
            ViewBag.devd_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucionDetalle.devd_UsuarioModifica);
            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Razon", tbDevolucionDetalle.dev_Id);
            return View(tbDevolucionDetalle);
        }

        // GET: DevolucionDetalles/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucionDetalle tbDevolucionDetalle = db.tbDevolucionDetalle.Find(id);
            if (tbDevolucionDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.devd_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucionDetalle.devd_UsuarioCrea);
            ViewBag.devd_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucionDetalle.devd_UsuarioModifica);
            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Razon", tbDevolucionDetalle.dev_Id);
            return View(tbDevolucionDetalle);
        }

        // POST: DevolucionDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "devd_Id,dev_Id,prod_Id,prod_Precio,devd_Cantidad,devd_Impuesto,devd_Descuento,devd_UsuarioCrea,devd_FechaCrea,devd_UsuarioModifica,devd_FechaModifica")] tbDevolucionDetalle tbDevolucionDetalle)
        {
            //tbDevolucionDetalle.devd_UsuarioCrea = 3;
            //tbDevolucionDetalle.devd_FechaCrea = DateTime.Now;
            tbDevolucionDetalle.devd_UsuarioModifica = 3;
            tbDevolucionDetalle.devd_FechaModifica = DateTime.Now;
            IEnumerable<object> listDevolucionDet = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listDevolucionDet = db.UDP_Vent_tbDevolucionDetalle_Update(tbDevolucionDetalle.devd_Id,
                                                                               tbDevolucionDetalle.dev_Id,
                                                                               tbDevolucionDetalle.prod_Id,
                                                                               tbDevolucionDetalle.prod_Precio,
                                                                               tbDevolucionDetalle.devd_Cantidad,
                                                                               tbDevolucionDetalle.devd_Impuesto,
                                                                               tbDevolucionDetalle.devd_Descuento,
                                                                               tbDevolucionDetalle.devd_UsuarioCrea,
                                                                               tbDevolucionDetalle.devd_FechaCrea,
                                                                               tbDevolucionDetalle.devd_UsuarioModifica,
                                                                               tbDevolucionDetalle.devd_FechaModifica);
                                                                               
                    foreach (UDP_Vent_tbDevolucionDetalle_Update_Result Resultado in listDevolucionDet)
                        MensajeError = Resultado.MensajeError;



                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbDevolucionDetalle);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            ViewBag.devd_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucionDetalle.devd_UsuarioCrea);
            ViewBag.devd_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucionDetalle.devd_UsuarioModifica);
            ViewBag.dev_Id = new SelectList(db.tbDevolucion, "dev_Id", "dev_Razon", tbDevolucionDetalle.dev_Id);
            return View(tbDevolucionDetalle);
        }

        // GET: DevolucionDetalles/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucionDetalle tbDevolucionDetalle = db.tbDevolucionDetalle.Find(id);
            if (tbDevolucionDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbDevolucionDetalle);
        }

        // POST: DevolucionDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDevolucionDetalle tbDevolucionDetalle = db.tbDevolucionDetalle.Find(id);
            db.tbDevolucionDetalle.Remove(tbDevolucionDetalle);
            db.SaveChanges();
            return RedirectToAction("Index");
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
