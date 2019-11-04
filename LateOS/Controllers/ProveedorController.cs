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
    public class ProveedorController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: Proveedor
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbProveedor = db.tbProveedor.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            return View(tbProveedor.ToList());
        }

        // GET: Proveedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbProveedor);
        }

        // GET: Proveedor/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.prov_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.prov_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Proveedor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "prov_Id,prov_Descripcion,prov_UsuarioCrea,prov_FechaCrea,prov_UsuarioModifica,prov_FechaModifica")] tbProveedor tbProveedor)
        {
            tbProveedor.prov_UsuarioCrea = 3;
            tbProveedor.prov_FechaCrea = DateTime.Now;

            IEnumerable<object> listProveedor = null;
            string MensajeError = "";

            if (ModelState.IsValid)
            {
                //db.tbProveedor.Add(tbProveedor);
                //db.SaveChanges();
                //return RedirectToAction("Index");

                try
                {
                    listProveedor = db.UDP_Inv_tbProveedor_Insert(tbProveedor.prov_Descripcion,                                                                         
                                                                  tbProveedor.prov_UsuarioCrea,
                                                                  tbProveedor.prov_FechaCrea);
                    foreach (UDP_Inv_tbProveedor_Insert_Result Resultado in listProveedor)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                        return View(tbProveedor);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }

            ViewBag.prov_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProveedor.prov_UsuarioCrea);
            ViewBag.prov_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProveedor.prov_UsuarioModifica);
            return View(tbProveedor);
        }




        // GET: Proveedor/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.prov_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProveedor.prov_UsuarioCrea);
            ViewBag.prov_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProveedor.prov_UsuarioModifica);
            return View(tbProveedor);
        }

        // POST: Proveedor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "prov_Id,prov_Descripcion,prov_UsuarioCrea,prov_FechaCrea,prov_UsuarioModifica,prov_FechaModifica")] tbProveedor tbProveedor)
        {
            tbProveedor.prov_UsuarioModifica = 3;
            tbProveedor.prov_FechaModifica = DateTime.Now;

            IEnumerable<object> listProveedor = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {             
                
                try
                {
                    listProveedor = db.UDP_Inv_tbProveedor_Update(tbProveedor.prov_Id,
                                                               tbProveedor.prov_Descripcion,
                                                               tbProveedor.prov_UsuarioCrea,
                                                               tbProveedor.prov_FechaCrea,
                                                               tbProveedor.prov_UsuarioModifica,
                                                               tbProveedor.prov_FechaModifica
                                                               );

                    foreach (UDP_Inv_tbProveedor_Update_Result Resultado in listProveedor)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbProveedor);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");

                //db.Entry(tbProveedor).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.prov_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProveedor.prov_UsuarioCrea);
            ViewBag.prov_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProveedor.prov_UsuarioModifica);
            return View(tbProveedor);
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
