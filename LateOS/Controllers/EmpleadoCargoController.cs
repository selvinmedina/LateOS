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
    public class EmpleadoCargoController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: EmpleadoCargo
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbEmpleadoCargo = db.tbEmpleadoCargo.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbEmpleadoDepartamento);
            return View(tbEmpleadoCargo.ToList());
        }

        // GET: EmpleadoCargo/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleadoCargo tbEmpleadoCargo = db.tbEmpleadoCargo.Find(id);
            if (tbEmpleadoCargo == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleadoCargo);
        }

        // GET: EmpleadoCargo/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.empc_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.empc_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.empd_Id = new SelectList(db.tbEmpleadoDepartamento, "empd_Id", "empd_Descripcion");
            return View();
        }

        // POST: EmpleadoCargo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "empc_Id,empc_Descripcion,empd_Id,empc_UsuarioCrea,empc_FechaCrea,empc_UsuarioModifica,empc_FechaModifica")] tbEmpleadoCargo tbEmpleadoCargo)
        {
            //--
            tbEmpleadoCargo.empc_UsuarioCrea = 3;
            tbEmpleadoCargo.empc_FechaCrea = DateTime.Now;
            IEnumerable<object> listEmpCargo = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listEmpCargo = db.UDP_Gral_tbEmpleadoCargo_Insert(tbEmpleadoCargo.empc_Descripcion,
                                                                    tbEmpleadoCargo.empd_Id,
                                                                    tbEmpleadoCargo.empc_UsuarioCrea,
                                                                    tbEmpleadoCargo.empc_FechaCrea);

                    foreach (UDP_Gral_tbEmpleadoCargo_Insert_Result Resultado in listEmpCargo)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbEmpleadoCargo);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            //--
            ViewBag.empc_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleadoCargo.empc_UsuarioCrea);
            ViewBag.empc_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleadoCargo.empc_UsuarioModifica);
            ViewBag.empd_Id = new SelectList(db.tbEmpleadoDepartamento, "empd_Id", "empd_Descripcion", tbEmpleadoCargo.empd_Id);
            return View(tbEmpleadoCargo);
        }

        // GET: EmpleadoCargo/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleadoCargo tbEmpleadoCargo = db.tbEmpleadoCargo.Find(id);
            if (tbEmpleadoCargo == null)
            {
                return HttpNotFound();
            }
            ViewBag.empc_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleadoCargo.empc_UsuarioCrea);
            ViewBag.empc_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleadoCargo.empc_UsuarioModifica);
            ViewBag.empd_Id = new SelectList(db.tbEmpleadoDepartamento, "empd_Id", "empd_Descripcion", tbEmpleadoCargo.empd_Id);
            return View(tbEmpleadoCargo);
        }

        // POST: EmpleadoCargo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "empc_Id,empc_Descripcion,empd_Id,empc_UsuarioCrea,empc_FechaCrea,empc_UsuarioModifica,empc_FechaModifica")] tbEmpleadoCargo tbEmpleadoCargo)
        {
            tbEmpleadoCargo.empc_UsuarioModifica = 3;
            tbEmpleadoCargo.empc_FechaModifica = DateTime.Now;
            //--
            IEnumerable<object> listEmpCargo = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listEmpCargo = db.UDP_Gral_tbEmpleadoCargo_Update(tbEmpleadoCargo.empc_Id,
                                                                    tbEmpleadoCargo.empc_Descripcion,
                                                                    tbEmpleadoCargo.empd_Id,
                                                                    tbEmpleadoCargo.empc_UsuarioModifica,
                                                                    tbEmpleadoCargo.empc_FechaModifica);

                    foreach (UDP_Gral_tbEmpleadoCargo_Update_Result Resultado in listEmpCargo)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo mdoficar el registro, contacte al administrador");
                        return View(tbEmpleadoCargo);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            ViewBag.empc_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleadoCargo.empc_UsuarioCrea);
            ViewBag.empc_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleadoCargo.empc_UsuarioModifica);
            ViewBag.empd_Id = new SelectList(db.tbEmpleadoDepartamento, "empd_Id", "empd_Descripcion", tbEmpleadoCargo.empd_Id);
            return View(tbEmpleadoCargo);
        }

        // GET: EmpleadoCargo/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleadoCargo tbEmpleadoCargo = db.tbEmpleadoCargo.Find(id);
            if (tbEmpleadoCargo == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleadoCargo);
        }

        // POST: EmpleadoCargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEmpleadoCargo tbEmpleadoCargo = db.tbEmpleadoCargo.Find(id);
            db.tbEmpleadoCargo.Remove(tbEmpleadoCargo);
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
