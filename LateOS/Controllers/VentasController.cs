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
    public class VentasController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: Producto


        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                var tbProducto = db.tbProducto.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbProveedor).Include(t => t.tbSubCategoria);
                return View(tbProducto.ToList());
            }
            else
            {
                var tbProducto = db.tbProducto.Where(x => x.tbSubCategoria.cat_Id == id).ToList();
                //db.UDP_Inv_tbProducto_Select_Categoria(idCat).ToList();
                return View(tbProducto);
            }
        }

        public ActionResult profile(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index","Login");
            }
            tbCliente tbcliente = db.tbCliente.Where(x => x.clte_Id == id).FirstOrDefault();
            if (tbcliente == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(tbcliente);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Ventas");
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return RedirectToAction("Index", "Ventas");
            }
            return View(tbProducto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Descripcion");
            ViewBag.subc_Id = new SelectList(db.tbSubCategoria, "subc_Id", "subc_Descripcion");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prod_Id,prod_Codigo,prod_Descripcion,prov_Id,subc_Id,prod_Precio,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica,prod_FechaModifica,prod_Img")] tbProducto tbProducto)
        {
            if (ModelState.IsValid)
            {
                db.tbProducto.Add(tbProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioCrea);
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Descripcion", tbProducto.prov_Id);
            ViewBag.subc_Id = new SelectList(db.tbSubCategoria, "subc_Id", "subc_Descripcion", tbProducto.subc_Id);
            return View(tbProducto);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioCrea);
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Descripcion", tbProducto.prov_Id);
            ViewBag.subc_Id = new SelectList(db.tbSubCategoria, "subc_Id", "subc_Descripcion", tbProducto.subc_Id);
            return View(tbProducto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prod_Id,prod_Codigo,prod_Descripcion,prov_Id,subc_Id,prod_Precio,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica,prod_FechaModifica,prod_Img")] tbProducto tbProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioCrea);
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Descripcion", tbProducto.prov_Id);
            ViewBag.subc_Id = new SelectList(db.tbSubCategoria, "subc_Id", "subc_Descripcion", tbProducto.subc_Id);
            return View(tbProducto);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbProducto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProducto tbProducto = db.tbProducto.Find(id);
            db.tbProducto.Remove(tbProducto);
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
