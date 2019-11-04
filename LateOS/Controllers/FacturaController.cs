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
    public class FacturaController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: Factura
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbFactura = db.tbFactura.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbTipoPago).Include(t => t.tbCliente).Include(t => t.tbFormaPago);
            return View(tbFactura.ToList());
        }
        public ActionResult _IndexFacturaDetalle(int idFactura)
        {
            return PartialView(db.tbFacturaDetalle.Where(x => x.fact_Id == idFactura).ToList());
        }



        // GET: Factura/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return RedirectToAction("Index","Factura");
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return RedirectToAction("Index","Factura");
            }
            return View(tbFactura);
        }

        // GET: Factura/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            //ViewBag.tipp_Id = new SelectList(db.tbTipoPago, "tipp_Id", "tipp_Descripcion");
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identidad");
            ViewBag.forp_Id = new SelectList(db.tbFormaPago, "forp_Id", "forp_Descripcion");
            return View();
        }

        // POST: Factura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "fact_Id,fact_Codigo,fact_Fecha,clte_Id,tipp_Id,forp_Id,fact_UsuarioCrea,fact_FechaCrea,fact_UsuarioModifica,fact_FechaModifica")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                db.tbFactura.Add(tbFactura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.tipp_Id = new SelectList(db.tbTipoPago, "tipp_Id", "tipp_Descripcion", tbFactura.tipp_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identidad", tbFactura.clte_Id);
            ViewBag.forp_Id = new SelectList(db.tbFormaPago, "forp_Id", "forp_Descripcion", tbFactura.forp_Id);
            return View(tbFactura);
        }

        // GET: Factura/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index","Factura");
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return RedirectToAction("Index", "Factura");
            }
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.tipp_Id = new SelectList(db.tbTipoPago, "tipp_Id", "tipp_Descripcion", tbFactura.tipp_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identidad", tbFactura.clte_Id);
            ViewBag.forp_Id = new SelectList(db.tbFormaPago, "forp_Id", "forp_Descripcion", tbFactura.forp_Id);
            return View(tbFactura);
        }

        // POST: Factura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "fact_Id,fact_Codigo,fact_Fecha,clte_Id,tipp_Id,forp_Id,fact_UsuarioCrea,fact_FechaCrea,fact_UsuarioModifica,fact_FechaModifica")] tbFactura tbFactura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbFactura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fact_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioCrea);
            ViewBag.fact_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbFactura.fact_UsuarioModifica);
            ViewBag.tipp_Id = new SelectList(db.tbTipoPago, "tipp_Id", "tipp_Descripcion", tbFactura.tipp_Id);
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identidad", tbFactura.clte_Id);
            ViewBag.forp_Id = new SelectList(db.tbFormaPago, "forp_Id", "forp_Descripcion", tbFactura.forp_Id);
            return View(tbFactura);
        }


        // GET: Factura/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index","Factura");
            }
            tbFactura tbFactura = db.tbFactura.Find(id);
            if (tbFactura == null)
            {
                return RedirectToAction("Index", "Factura");
            }
            return View(tbFactura);
        }

        // POST: Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbFactura tbFactura = db.tbFactura.Find(id);
            db.tbFactura.Remove(tbFactura);
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
