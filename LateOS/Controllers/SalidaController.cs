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
    public class SalidaController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: Salida
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbSalida = db.tbSalida.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbProducto);
            return View(tbSalida.ToList());
        }

        // GET: Salida/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            if (tbSalida == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbSalida);
        }

        // GET: Salida/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.sali_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.sali_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Codigo");
            return View();
        }

        // POST: Salida/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "sali_Id,prod_Id,sali_Existencia,sali_Cantidad,sali_FechaEntrada,sali_UsuarioCrea,sali_FechaCrea,sali_UsuarioModifica,sali_FechaModifica")] tbSalida tbSalida)
        {
            if (ModelState.IsValid)
            {
                db.tbSalida.Add(tbSalida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sali_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbSalida.sali_UsuarioCrea);
            ViewBag.sali_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbSalida.sali_UsuarioModifica);
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Codigo", tbSalida.prod_Id);
            return View(tbSalida);
        }

        // GET: Salida/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            if (tbSalida == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.sali_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbSalida.sali_UsuarioCrea);
            ViewBag.sali_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbSalida.sali_UsuarioModifica);
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Codigo", tbSalida.prod_Id);
            return View(tbSalida);
        }

        // POST: Salida/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "sali_Id,prod_Id,sali_Existencia,sali_Cantidad,sali_FechaEntrada,sali_UsuarioCrea,sali_FechaCrea,sali_UsuarioModifica,sali_FechaModifica")] tbSalida tbSalida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSalida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sali_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbSalida.sali_UsuarioCrea);
            ViewBag.sali_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbSalida.sali_UsuarioModifica);
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Codigo", tbSalida.prod_Id);
            return View(tbSalida);
        }

        // GET: Salida/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSalida tbSalida = db.tbSalida.Find(id);
            if (tbSalida == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbSalida);
        }

        // POST: Salida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSalida tbSalida = db.tbSalida.Find(id);
            db.tbSalida.Remove(tbSalida);
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
