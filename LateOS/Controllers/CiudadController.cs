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
    public class CiudadController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: Ciudad
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbCiudad = db.tbCiudad.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbPais);
            return View(tbCiudad.ToList());
        }

        // GET: Ciudad/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCiudad tbCiudad = db.tbCiudad.Find(id);
            if (tbCiudad == null)
            {
                return HttpNotFound();
            }
            return View(tbCiudad);
        }

        // GET: Ciudad/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.ciu_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.ciu_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.pais_Id = new SelectList(db.tbPais, "pais_Id", "pais_Descripcion");
            return View();
        }

        // POST: Ciudad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "ciu_Id,ciu_Descripcion,pais_Id,ciu_UsuarioCrea,ciu_FechaCrea,ciu_UsuarioModifica,ciu_FechaModifica")] tbCiudad tbCiudad)
        {
            //--
            tbCiudad.ciu_UsuarioCrea = 3;
            tbCiudad.ciu_FechaCrea = DateTime.Now;
            IEnumerable<object> listCiudad = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    db.UDP_Gral_tbCiudad_Insert(tbCiudad.ciu_Descripcion,
                                                            tbCiudad.pais_Id,
                                                            tbCiudad.ciu_UsuarioCrea,
                                                            tbCiudad.ciu_FechaCrea);


                    foreach (UDP_Gral_tbCiudad_Insert_Result Resultado in listCiudad)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbCiudad);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            //--
            ViewBag.ciu_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCiudad.ciu_UsuarioCrea);
            ViewBag.ciu_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCiudad.ciu_UsuarioModifica);
            ViewBag.pais_Id = new SelectList(db.tbPais, "pais_Id", "pais_Descripcion", tbCiudad.pais_Id);
            return View(tbCiudad);
        }

        // GET: Ciudad/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCiudad tbCiudad = db.tbCiudad.Find(id);
            if (tbCiudad == null)
            {
                return HttpNotFound();
            }
            ViewBag.ciu_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCiudad.ciu_UsuarioCrea);
            ViewBag.ciu_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCiudad.ciu_UsuarioModifica);
            ViewBag.pais_Id = new SelectList(db.tbPais, "pais_Id", "pais_Descripcion", tbCiudad.pais_Id);
            return View(tbCiudad);
        }

        // POST: Ciudad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "ciu_Id,ciu_Descripcion,pais_Id,ciu_UsuarioCrea,ciu_FechaCrea,ciu_UsuarioModifica,ciu_FechaModifica")] tbCiudad tbCiudad)
        {
            tbCiudad.ciu_UsuarioModifica = 3;
            tbCiudad.ciu_FechaModifica = DateTime.Now;
            //--
            IEnumerable<object> listCiudad = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listCiudad = db.UDP_Gral_tbCiudad_Update(tbCiudad.ciu_Id,
                                                            tbCiudad.ciu_Descripcion,
                                                            tbCiudad.pais_Id,
                                                            tbCiudad.ciu_UsuarioCrea,
                                                            tbCiudad.ciu_FechaCrea,
                                                            tbCiudad.ciu_UsuarioModifica,
                                                            tbCiudad.ciu_FechaModifica);

                    foreach (UDP_Gral_tbCiudad_Update_Result Resultado in listCiudad)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo mdoficar el registro, contacte al administrador");
                        return View(tbCiudad);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            //--
            ViewBag.ciu_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCiudad.ciu_UsuarioCrea);
            ViewBag.ciu_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCiudad.ciu_UsuarioModifica);
            ViewBag.pais_Id = new SelectList(db.tbPais, "pais_Id", "pais_Descripcion", tbCiudad.pais_Id);
            return View(tbCiudad);
        }

        // GET: Ciudad/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCiudad tbCiudad = db.tbCiudad.Find(id);
            if (tbCiudad == null)
            {
                return HttpNotFound();
            }
            return View(tbCiudad);
        }

        [AuthorizeUser(1)]
        // POST: Ciudad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCiudad tbCiudad = db.tbCiudad.Find(id);
            db.tbCiudad.Remove(tbCiudad);
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
