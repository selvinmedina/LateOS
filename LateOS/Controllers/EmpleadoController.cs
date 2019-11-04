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
    public class EmpleadoController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: Empleado
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbEmpleado = db.tbEmpleado.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbUsuarios2).Include(t => t.tbEmpleadoCargo);
            return View(tbEmpleado.ToList());
        }

        // GET: Empleado/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleado);
        }

        // GET: Empleado/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.emp_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.usu_Id = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.empc_id = new SelectList(db.tbEmpleadoCargo, "empc_Id", "empc_Descripcion");
            return View();
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "emp_Id,emp_Identidad,emp_Nombres,emp_Apellidos,emp_FechaNacimiento,emp_Sexo,emp_CorreoElectronico,emp_Telefono,emp_FechaIngreso,emp_EsActivo,empc_id,usu_Id,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica")] tbEmpleado tbEmpleado)
        {
            tbEmpleado.emp_UsuarioCrea = 3;
            tbEmpleado.emp_FechaCrea = DateTime.Now;
            tbEmpleado.emp_FechaIngreso = DateTime.Now;
            tbEmpleado.emp_EsActivo = true;
            tbEmpleado.usu_Id = 3;
            IEnumerable<object> listempleado = null;
            string MensajeError = "";

            if (ModelState.IsValid)
            {
               try
                {
                    listempleado = db.UDP_Gral_tbEmpleado_Insert(tbEmpleado.emp_Identidad, tbEmpleado.emp_Nombres, 
                                                                 tbEmpleado.emp_Apellidos, tbEmpleado.emp_FechaNacimiento,
                                                                 tbEmpleado.emp_Sexo, tbEmpleado.emp_CorreoElectronico, 
                                                                 tbEmpleado.emp_Telefono, tbEmpleado.emp_FechaIngreso, 
                                                                 tbEmpleado.emp_EsActivo, tbEmpleado.empc_id, 
                                                                 tbEmpleado.usu_Id, tbEmpleado.emp_UsuarioCrea, tbEmpleado.emp_FechaCrea);
                    foreach (UDP_Gral_tbEmpleado_Insert_Result Resultado in listempleado)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbEmpleado);
                    }
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }

            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleado.emp_UsuarioCrea);
            ViewBag.emp_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleado.emp_UsuarioModifica);
            ViewBag.usu_Id = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleado.usu_Id);
            ViewBag.empc_id = new SelectList(db.tbEmpleadoCargo, "empc_Id", "empc_Descripcion", tbEmpleado.empc_id);
            return View(tbEmpleado);
        }

        // GET: Empleado/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleado.emp_UsuarioCrea);
            ViewBag.emp_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleado.emp_UsuarioModifica);
            ViewBag.usu_Id = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleado.usu_Id);
            ViewBag.empc_id = new SelectList(db.tbEmpleadoCargo, "empc_Id", "empc_Descripcion", tbEmpleado.empc_id);
            return View(tbEmpleado);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "emp_Id,emp_Identidad,emp_Nombres,emp_Apellidos,emp_FechaNacimiento,emp_Sexo,emp_CorreoElectronico,emp_Telefono,emp_FechaIngreso,emp_EsActivo,empc_id,usu_Id,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica")] tbEmpleado tbEmpleado)
        {
            tbEmpleado.emp_UsuarioModifica = 3;
            tbEmpleado.emp_FechaModifica = DateTime.Now;
            
            tbEmpleado.usu_Id = 3;
            IEnumerable<object> listempleado = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listempleado = db.UDP_Gral_tbEmpleado_Update(tbEmpleado.emp_Id,
                                                                 tbEmpleado.emp_Identidad, tbEmpleado.emp_Nombres,
                                                                 tbEmpleado.emp_Apellidos, tbEmpleado.emp_FechaNacimiento,
                                                                 tbEmpleado.emp_Sexo, tbEmpleado.emp_CorreoElectronico,
                                                                 tbEmpleado.emp_Telefono, tbEmpleado.emp_FechaIngreso,
                                                                 tbEmpleado.emp_EsActivo, tbEmpleado.empc_id,
                                                                 tbEmpleado.usu_Id, tbEmpleado.emp_UsuarioCrea, tbEmpleado.emp_FechaCrea,
                                                                 tbEmpleado.emp_UsuarioModifica, tbEmpleado.emp_FechaModifica);
                    foreach (UDP_Gral_tbEmpleado_Update_Result Resultado in listempleado)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbEmpleado);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleado.emp_UsuarioCrea);
            ViewBag.emp_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleado.emp_UsuarioModifica);
            ViewBag.usu_Id = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleado.usu_Id);
            ViewBag.empc_id = new SelectList(db.tbEmpleadoCargo, "empc_Id", "empc_Descripcion", tbEmpleado.empc_id);
            return View(tbEmpleado);
        }

        // GET: Empleado/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            db.tbEmpleado.Remove(tbEmpleado);
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
