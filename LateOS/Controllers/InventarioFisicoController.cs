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
    public class InventarioFisicoController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: InventarioFisico
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Descripcion");
            var inventarioFisico = db.InventarioFisico.Include(i => i.tbUsuarios).Include(i => i.tbUsuarios1).Include(i => i.tbProducto);
            return View(inventarioFisico.ToList());
        }

        // GET: InventarioFisico/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            InventarioFisico inventarioFisico = db.InventarioFisico.Find(id);
            if (inventarioFisico == null)
            {
                return RedirectToAction("Index");
            }
            return View(inventarioFisico);
        }

        // GET: InventarioFisico/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.invf_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.invf_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Codigo");
            return View();
        }

        // POST: InventarioFisico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "invf_id,prod_Id,invf_total,invf_FechaInventario,invf_UsuarioCrea,invf_FechaCrea,invf_UsuarioModifica,invf_FechaModifica")] InventarioFisico inventarioFisico)
        {
            if (ModelState.IsValid)
            {
                db.InventarioFisico.Add(inventarioFisico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.invf_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", inventarioFisico.invf_UsuarioCrea);
            ViewBag.invf_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", inventarioFisico.invf_UsuarioModifica);
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Codigo", inventarioFisico.prod_Id);
            return View(inventarioFisico);
        }

        // GET: InventarioFisico/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            InventarioFisico inventarioFisico = db.InventarioFisico.Find(id);
            if (inventarioFisico == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.invf_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", inventarioFisico.invf_UsuarioCrea);
            ViewBag.invf_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", inventarioFisico.invf_UsuarioModifica);
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Codigo", inventarioFisico.prod_Id);
            return View(inventarioFisico);
        }

        // POST: InventarioFisico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "invf_id,prod_Id,invf_total,invf_FechaInventario,invf_UsuarioCrea,invf_FechaCrea,invf_UsuarioModifica,invf_FechaModifica")] InventarioFisico inventarioFisico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventarioFisico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.invf_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", inventarioFisico.invf_UsuarioCrea);
            ViewBag.invf_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", inventarioFisico.invf_UsuarioModifica);
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Codigo", inventarioFisico.prod_Id);
            return View(inventarioFisico);
        }

        public JsonResult _EditInventario(int id)
        {
            var inventario = db.UDP_Inv_InventarioFisico_Select(id).ToList();
            return Json(inventario, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _UpdateInventario(InventarioFisico inventario)
        {
            var invent = db.UDP_Inv_InventarioFisico_Update(inventario.invf_id,
                                                            inventario.prod_Id,
                                                            inventario.invf_total,
                                                            inventario.invf_FechaInventario,
                                                            3,
                                                            DateTime.Now,
                                                            inventario.invf_UsuarioModifica,
                                                            inventario.invf_FechaModifica);
            return Json(inventario, JsonRequestBehavior.AllowGet);
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
