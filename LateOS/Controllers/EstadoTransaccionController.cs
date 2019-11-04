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
    public class EstadoTransaccionController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: EstadoTransaccion
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbEstadoTransaccion = db.tbEstadoTransaccion.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);   
            return View(tbEstadoTransaccion.ToList());
        }

        // GET: EstadoTransaccion/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoTransaccion tbEstadoTransaccion = db.tbEstadoTransaccion.Find(id);
            if (tbEstadoTransaccion == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoTransaccion);
        }

        // GET: EstadoTransaccion/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.estt_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.estt_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: EstadoTransaccion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "estt_Id,estt_Descripcion,estt_UsuarioCrea,estt_FechaCrea,estt_UsuarioModifica,estt_FechaModifica")] tbEstadoTransaccion tbEstadoTransaccion)
        {
            tbEstadoTransaccion.estt_UsuarioCrea = 3;
            tbEstadoTransaccion.estt_FechaCrea = DateTime.Now;
            string MensajeError = "";
            IEnumerable<object> listEsttran = null;
            if (ModelState.IsValid)
            {
                try
                {
                    listEsttran = db.UDP_Vent_tbEstadoTransaccion_Insert(tbEstadoTransaccion.estt_Descripcion,
                                                                          tbEstadoTransaccion.estt_UsuarioCrea,
                                                                          tbEstadoTransaccion.estt_FechaCrea);
                    foreach (UDP_Vent_tbEstadoTransaccion_Insert_Result Resultado in listEsttran)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbEstadoTransaccion);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }

           
            return View(tbEstadoTransaccion);
        }

        // GET: EstadoTransaccion/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoTransaccion tbEstadoTransaccion = db.tbEstadoTransaccion.Find(id);
            if (tbEstadoTransaccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.estt_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEstadoTransaccion.estt_UsuarioCrea);
            ViewBag.estt_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEstadoTransaccion.estt_UsuarioModifica);
            return View(tbEstadoTransaccion);
        }

        // POST: EstadoTransaccion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "estt_Id,estt_Descripcion,estt_UsuarioCrea,estt_FechaCrea,estt_UsuarioModifica,estt_FechaModifica")] tbEstadoTransaccion tbEstadoTransaccion)
        {
            tbEstadoTransaccion.estt_UsuarioModifica = 3;
            tbEstadoTransaccion.estt_FechaModifica= DateTime.Now;
            string MensajeError = "";
            IEnumerable<object> listEsttran = null;
            if (ModelState.IsValid)
            {
                try
                {
                    listEsttran = db.UDP_Vent_tbEstadoTransaccion_Update(tbEstadoTransaccion.estt_Id,
                                                                           tbEstadoTransaccion.estt_Descripcion,
                                                                          tbEstadoTransaccion.estt_UsuarioCrea,
                                                                          tbEstadoTransaccion.estt_FechaCrea,
                                                                          tbEstadoTransaccion.estt_UsuarioModifica,
                                                                          tbEstadoTransaccion.estt_FechaModifica);
                    foreach (UDP_Vent_tbEstadoTransaccion_Update_Result Resultado in listEsttran)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbEstadoTransaccion);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            
            return View(tbEstadoTransaccion);
        }

        // GET: EstadoTransaccion/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadoTransaccion tbEstadoTransaccion = db.tbEstadoTransaccion.Find(id);
            if (tbEstadoTransaccion == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadoTransaccion);
        }

        // POST: EstadoTransaccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEstadoTransaccion tbEstadoTransaccion = db.tbEstadoTransaccion.Find(id);
            db.tbEstadoTransaccion.Remove(tbEstadoTransaccion);
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
