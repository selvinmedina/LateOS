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
    public class EstadoFacturaController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: EstadoFactura
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbEstadoFactura = db.tbEstadoFactura.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            return View(tbEstadoFactura.ToList());
        }

        // GET: EstadoFactura/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoFactura tbEstadoFactura = db.tbEstadoFactura.Find(id);
            if (tbEstadoFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoFactura);
        }

        // GET: EstadoFactura/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.estf_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.estf_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: EstadoFactura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "estf_Id,estf_Descripcion,estf_UsuarioCrea,estf_FechaCrea,estf_UsuarioModifica,estf_FechaModifica")] tbEstadoFactura tbEstadoFactura)
        {
            tbEstadoFactura.estf_UsuarioCrea = 3;
            tbEstadoFactura.estf_FechaCrea = DateTime.Now;
            string MensajeError = "";
            IEnumerable<object> listEstFac = null;
            if (ModelState.IsValid)
            {
                try
                {
                    listEstFac = db.UDP_Vent_tbEstadoFactura_Insert(tbEstadoFactura.estf_Descripcion,
                                                                          tbEstadoFactura.estf_UsuarioCrea,
                                                                          tbEstadoFactura.estf_FechaCrea);
                    foreach (UDP_Vent_tbEstadoFactura_Insert_Result Resultado in listEstFac)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbEstadoFactura);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }

            
            return View(tbEstadoFactura);
        }

        // GET: EstadoFactura/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoFactura tbEstadoFactura = db.tbEstadoFactura.Find(id);
            if (tbEstadoFactura == null)
            {
                return HttpNotFound();
            }
            ViewBag.estf_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEstadoFactura.estf_UsuarioCrea);
            ViewBag.estf_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEstadoFactura.estf_UsuarioModifica);
            return View(tbEstadoFactura);
        }

        // POST: EstadoFactura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "estf_Id,estf_Descripcion,estf_UsuarioCrea,estf_FechaCrea,estf_UsuarioModifica,estf_FechaModifica")] tbEstadoFactura tbEstadoFactura)
        {
            tbEstadoFactura.estf_UsuarioModifica = 3;
            tbEstadoFactura.estf_FechaModifica = DateTime.Now;
            string MensajeError = "";
            IEnumerable<object> listEstFac = null;
            if (ModelState.IsValid)
            {
                try
                {
                    listEstFac = db.UDP_Vent_tbEstadoFactura_Update(tbEstadoFactura.estf_Id,
                                                                         tbEstadoFactura.estf_Descripcion,
                                                                          tbEstadoFactura.estf_UsuarioCrea,
                                                                          tbEstadoFactura.estf_FechaCrea,
                                                                          tbEstadoFactura.estf_UsuarioModifica,
                                                                          tbEstadoFactura.estf_FechaModifica);
                    foreach (UDP_Vent_tbEstadoFactura_Update_Result Resultado in listEstFac)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbEstadoFactura);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            
            return View(tbEstadoFactura);
        }

        // GET: EstadoFactura/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoFactura tbEstadoFactura = db.tbEstadoFactura.Find(id);
            if (tbEstadoFactura == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoFactura);
        }

        // POST: EstadoFactura/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //     tbEstadoFactura = db.tbEstadoFactura.Find(id);
        //    db.tbEstadoFactura.Remove(tbEstadoFactura);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
