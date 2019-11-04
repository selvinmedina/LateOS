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
    public class TipoPagoController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: TipoPago
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbTipoPago = db.tbTipoPago.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            return View(tbTipoPago.ToList());
        }

        // GET: TipoPago/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            if (tbTipoPago == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbTipoPago);
        }

        // GET: TipoPago/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.tipp_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.tipp_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: TipoPago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "tipp_Id,tipp_Descripcion,tipp_UsuarioCrea,tipp_FechaCrea,tipp_UsuarioModifica,tipp_FechaModifica")] tbTipoPago tbTipoPago)
        {
            tbTipoPago.tipp_UsuarioCrea = 3;
            tbTipoPago.tipp_FechaCrea = DateTime.Now;
            IEnumerable<object> listTipoPago = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listTipoPago = db.UDP_Gral_tbTipoPago_Insert(tbTipoPago.tipp_Descripcion,
                                                                 tbTipoPago.tipp_UsuarioCrea,
                                                                 tbTipoPago.tipp_FechaCrea);


                       foreach (UDP_Gral_tbTipoPago_Insert_Result Resultado in listTipoPago)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudp ingresar el registro, contacte al administrador.");
                        return View(tbTipoPago);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }


            return View(tbTipoPago);
        }

        // GET: TipoPago/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            if (tbTipoPago == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.tipp_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbTipoPago.tipp_UsuarioCrea);
            ViewBag.tipp_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbTipoPago.tipp_UsuarioModifica);
            return View(tbTipoPago);
        }

        // POST: TipoPago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "tipp_Id,tipp_Descripcion,tipp_UsuarioCrea,tipp_FechaCrea,tipp_UsuarioModifica,tipp_FechaModifica")] tbTipoPago tbTipoPago)
        {
            //--
            tbTipoPago.tipp_UsuarioModifica = 3;
            tbTipoPago.tipp_FechaModifica = DateTime.Now;
            IEnumerable<object> listTipoPago = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listTipoPago = db.UDP_Gral_tbTipoPago_Update(tbTipoPago.tipp_Id,
                                                                           tbTipoPago.tipp_Descripcion,
                                                                           tbTipoPago.tipp_UsuarioCrea,
                                                                           tbTipoPago.tipp_FechaCrea,
                                                                           tbTipoPago.tipp_UsuarioModifica,
                                                                           tbTipoPago.tipp_FechaModifica);
                    foreach (UDP_Gral_tbTipoPago_Update_Result Resultado in listTipoPago)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                        return View(tbTipoPago);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }



            //--            
            ViewBag.tipp_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbTipoPago.tipp_UsuarioCrea);
            ViewBag.tipp_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbTipoPago.tipp_UsuarioModifica);
            return View(tbTipoPago);
        }

        // GET: TipoPago/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            if (tbTipoPago == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbTipoPago);
        }

        // POST: TipoPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoPago tbTipoPago = db.tbTipoPago.Find(id);
            db.tbTipoPago.Remove(tbTipoPago);
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
