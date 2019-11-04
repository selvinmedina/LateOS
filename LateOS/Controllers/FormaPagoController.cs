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
    public class FormaPagoController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: FormaPago
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbFormaPago = db.tbFormaPago.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            return View(tbFormaPago.ToList());
        }

        // GET: FormaPago/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFormaPago tbFormaPago = db.tbFormaPago.Find(id);
            if (tbFormaPago == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbFormaPago);
        }

        // GET: FormaPago/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.forp_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.forp_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: FormaPago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "forp_Id,forp_Descripcion,forp_UsuarioCrea,forp_FechaCrea,forp_UsuarioModifica,forp_FechaModifica")] tbFormaPago tbFormaPago)
        {
            tbFormaPago.forp_UsuarioCrea = 3;
            tbFormaPago.forp_FechaCrea = DateTime.Now;
            string MensajeError = "";
            IEnumerable<object> listFormPago = null;
            if (ModelState.IsValid)
            {
                try
                {
                    listFormPago = db.UDP_Vent_tbFormaPago_Insert(tbFormaPago.forp_Descripcion,
                                                                  tbFormaPago.forp_UsuarioCrea,
                                                                  tbFormaPago.forp_FechaCrea);


                    foreach (UDP_Vent_tbFormaPago_Insert_Result Resultado in listFormPago)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbFormaPago);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }

            
            return View(tbFormaPago);
        }

        // GET: FormaPago/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbFormaPago tbFormaPago = db.tbFormaPago.Find(id);
            if (tbFormaPago == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.forp_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbFormaPago.forp_UsuarioCrea);
            ViewBag.forp_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbFormaPago.forp_UsuarioModifica);
            return View(tbFormaPago);
        }

        // POST: FormaPago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "forp_Id,forp_Descripcion,forp_UsuarioCrea,forp_FechaCrea,forp_UsuarioModifica,forp_FechaModifica")] tbFormaPago tbFormaPago)
        {

            tbFormaPago.forp_UsuarioModifica = 3;
            tbFormaPago.forp_FechaModifica = DateTime.Now;
            string MensajeError = "";
            IEnumerable<object> listFormPago = null;
            if (ModelState.IsValid)
            {
                try
                {
                    listFormPago = db.UDP_Vent_tbFormaPago_Update(tbFormaPago.forp_Id,
                                                                  tbFormaPago.forp_Descripcion,
                                                                  tbFormaPago.forp_UsuarioCrea,
                                                                  tbFormaPago.forp_FechaCrea,
                                                                  tbFormaPago.forp_UsuarioModifica,
                                                                  tbFormaPago.forp_FechaModifica);


                    foreach (UDP_Vent_tbFormaPago_Update_Result Resultado in listFormPago)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbFormaPago);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
           
            return View(tbFormaPago);
        }

        // GET: FormaPago/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbFormaPago tbFormaPago = db.tbFormaPago.Find(id);
            if (tbFormaPago == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbFormaPago);
        }

        // POST: FormaPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbFormaPago tbFormaPago = db.tbFormaPago.Find(id);
            db.tbFormaPago.Remove(tbFormaPago);
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
