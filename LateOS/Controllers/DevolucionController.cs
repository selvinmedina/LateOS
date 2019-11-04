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
    public class DevolucionController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: Devolucion
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbDevolucion = db.tbDevolucion.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbFactura);
            return View(tbDevolucion.ToList());
        }

        // GET: Devolucion/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            if (tbDevolucion == null)
            {
                return HttpNotFound();
            }
            return View(tbDevolucion);
        }

        // GET: Devolucion/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.dev_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.dev_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Id");
            return View();
        }

        // POST: Devolucion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "fact_Id,dev_Razon,dev_Fecha")] tbDevolucion tbDevolucion)
        {
            tbDevolucion.dev_Id = 1;
            tbDevolucion.dev_Fecha = DateTime.Now;
            tbDevolucion.dev_UsuarioCrea = 3;
            tbDevolucion.dev_FechaCrea = DateTime.Now;
            //tbDevolucion.dev_UsuarioModifica = 3;
            //tbDevolucion.dev_FechaModifica = DateTime.Now;
            IEnumerable<object> listDevolucion = null;
            string MensajeError = "";

            if (ModelState.IsValid)
            {
                try
                {
                    listDevolucion = db.UDP_Vent_tbDevolucion_Insert(tbDevolucion.fact_Id,
                                                                     tbDevolucion.dev_Razon,
                                                                     tbDevolucion.dev_Fecha,
                                                                     tbDevolucion.dev_UsuarioCrea,
                                                                     tbDevolucion.dev_FechaCrea);
                    foreach (UDP_Vent_tbDevolucion_Insert_Result Resultado in listDevolucion)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                        return View(tbDevolucion);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }

            ViewBag.dev_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioCrea);
            ViewBag.dev_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioModifica);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Id", tbDevolucion.fact_Id);
            return View(tbDevolucion);
        }

        // GET: Devolucion/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            if (tbDevolucion == null)
            {
                return HttpNotFound();
            }
            ViewBag.dev_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioCrea);
            ViewBag.dev_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioModifica);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Id", tbDevolucion.fact_Id);
            return View(tbDevolucion);
        }

        // POST: Devolucion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "dev_Id,fact_Id,dev_Razon,dev_Fecha,dev_UsuarioCrea,dev_FechaCrea,dev_UsuarioModifica,dev_FechaModifica")] tbDevolucion tbDevolucion)
        {
        //    tbDevolucion.dev_UsuarioCrea = 3;
        //    tbDevolucion.dev_FechaCrea = DateTime.Now;
            tbDevolucion.dev_UsuarioModifica = 3;
            tbDevolucion.dev_FechaModifica = DateTime.Now;
            IEnumerable<object> listDevolucion = null;
            string MensajeError = "";

            if (ModelState.IsValid)
            {
                try
                {
                    listDevolucion = db.UDP_Vent_tbDevolucion_Update(tbDevolucion.dev_Id,
                                                                     tbDevolucion.fact_Id,
                                                                     tbDevolucion.dev_Razon,
                                                                     tbDevolucion.dev_Fecha,
                                                                     tbDevolucion.dev_UsuarioCrea,
                                                                     tbDevolucion.dev_FechaCrea,
                                                                     tbDevolucion.dev_UsuarioModifica,
                                                                     tbDevolucion.dev_FechaModifica);

                    foreach (UDP_Vent_tbDevolucion_Update_Result Resultado in listDevolucion)
                        MensajeError = Resultado.MensajeError;



                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbDevolucion);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
              }
            ViewBag.dev_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioCrea);
            ViewBag.dev_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbDevolucion.dev_UsuarioModifica);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Id", tbDevolucion.fact_Id);
            return View(tbDevolucion);
        }

        // GET: Devolucion/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            if (tbDevolucion == null)
            {
                return HttpNotFound();
            }
            return View(tbDevolucion);
        }

        // POST: Devolucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDevolucion tbDevolucion = db.tbDevolucion.Find(id);
            db.tbDevolucion.Remove(tbDevolucion);
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
