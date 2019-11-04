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
    public class TipoDescuentoController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: TipoDescuento
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbTipoDescuento = db.tbTipoDescuento.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            return View(tbTipoDescuento.ToList());
        }

        // GET: TipoDescuento/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoDescuento tbTipoDescuento = db.tbTipoDescuento.Find(id);
            if (tbTipoDescuento == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbTipoDescuento);
        }

        // GET: TipoDescuento/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.tipd_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.tipd_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: TipoDescuento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "tipd_Id,tipd_Descripcion,tipd_Porcentaje,tipd_UsuarioCrea,tipd_FechaCrea,tipd_UsuarioModifica,tipd_FechaModifica")] tbTipoDescuento tbTipoDescuento)
        {
            tbTipoDescuento.tipd_UsuarioCrea = 3;
            tbTipoDescuento.tipd_FechaCrea = DateTime.Now;
            IEnumerable<object> listTipoDescuento = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listTipoDescuento = db.UDP_Gral_tbTipoDescuento_Insert(tbTipoDescuento.tipd_Descripcion,
                                                                           tbTipoDescuento.tipd_Porcentaje,
                                                                           tbTipoDescuento.tipd_UsuarioCrea,
                                                                           tbTipoDescuento.tipd_FechaCrea);
                       foreach (UDP_Gral_tbTipoDescuento_Insert_Result Resultado in listTipoDescuento)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                     if (MensajeError.StartsWith("-1"))
                        {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                        return View(tbTipoDescuento);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            return View(tbTipoDescuento);
        }

        // GET: TipoDescuento/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoDescuento tbTipoDescuento = db.tbTipoDescuento.Find(id);
            if (tbTipoDescuento == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.tipd_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbTipoDescuento.tipd_UsuarioCrea);
            ViewBag.tipd_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbTipoDescuento.tipd_UsuarioModifica);
            return View(tbTipoDescuento);
        }

        // POST: TipoDescuento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "tipd_Id,tipd_Descripcion,tipd_Porcentaje,tipd_UsuarioCrea,tipd_FechaCrea,tipd_UsuarioModifica,tipd_FechaModifica")] tbTipoDescuento tbTipoDescuento)
        {

            tbTipoDescuento.tipd_UsuarioModifica = 3;
            tbTipoDescuento.tipd_FechaModifica = DateTime.Now;
            IEnumerable<object> listTipoDescuento = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listTipoDescuento = db.UDP_Gral_tbTipoDescuento_Update(tbTipoDescuento.tipd_Id,
                                                                           tbTipoDescuento.tipd_Descripcion,
                                                                           tbTipoDescuento.tipd_Porcentaje,
                                                                           tbTipoDescuento.tipd_UsuarioCrea,
                                                                           tbTipoDescuento.tipd_FechaCrea,
                                                                           tbTipoDescuento.tipd_UsuarioModifica,
                                                                           tbTipoDescuento.tipd_FechaModifica);
                    foreach (UDP_Gral_tbTipoDescuento_Update_Result Resultado in listTipoDescuento)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                        return View(tbTipoDescuento);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
        
    
            return View(tbTipoDescuento);
        }

        // GET: TipoDescuento/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTipoDescuento tbTipoDescuento = db.tbTipoDescuento.Find(id);
            if (tbTipoDescuento == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbTipoDescuento);
        }

        // POST: TipoDescuento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoDescuento tbTipoDescuento = db.tbTipoDescuento.Find(id);
            db.tbTipoDescuento.Remove(tbTipoDescuento);
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
