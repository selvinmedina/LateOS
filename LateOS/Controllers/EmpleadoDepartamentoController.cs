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
    public class EmpleadoDepartamentoController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: EmpleadoDepartamento
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbEmpleadoDepartamento = db.tbEmpleadoDepartamento.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            return View(tbEmpleadoDepartamento.ToList());
        }

        // GET: EmpleadoDepartamento/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleadoDepartamento tbEmpleadoDepartamento = db.tbEmpleadoDepartamento.Find(id);
            if (tbEmpleadoDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleadoDepartamento);
        }

        // GET: EmpleadoDepartamento/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.empd_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.empd_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: EmpleadoDepartamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "empd_Id,empd_Descripcion,empd_UsuarioCrea,empd_FechaCrea,empd_UsuarioModifica,empd_FechaModifica")] tbEmpleadoDepartamento tbEmpleadoDepartamento)
        {
            //--
            tbEmpleadoDepartamento.empd_UsuarioCrea = 3;
            tbEmpleadoDepartamento.empd_FechaCrea = DateTime.Now;
            IEnumerable<object> listEmpDepto = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listEmpDepto = db.UDP_Gral_tbEmpleadoDepartamento_Insert(tbEmpleadoDepartamento.empd_Descripcion,
                                                                            tbEmpleadoDepartamento.empd_UsuarioCrea,
                                                                            tbEmpleadoDepartamento.empd_FechaCrea);

                    foreach (UDP_Gral_tbPais_Insert_Result Resultado in listEmpDepto)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbEmpleadoDepartamento);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            return View(tbEmpleadoDepartamento);
        }


        // GET: EmpleadoDepartamento/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleadoDepartamento tbEmpleadoDepartamento = db.tbEmpleadoDepartamento.Find(id);
            if (tbEmpleadoDepartamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.empd_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleadoDepartamento.empd_UsuarioCrea);
            ViewBag.empd_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEmpleadoDepartamento.empd_UsuarioModifica);
            return View(tbEmpleadoDepartamento);
        }

        // POST: EmpleadoDepartamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "empd_Id,empd_Descripcion,empd_UsuarioCrea,empd_FechaCrea,empd_UsuarioModifica,empd_FechaModifica")] tbEmpleadoDepartamento tbEmpleadoDepartamento)
        {
            tbEmpleadoDepartamento.empd_UsuarioModifica = 3;
            tbEmpleadoDepartamento.empd_FechaModifica = DateTime.Now;
            //--
            IEnumerable<object> listEmpDepto = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listEmpDepto = db.UDP_Gral_tbEmpleadoDepartamento_Update(tbEmpleadoDepartamento.empd_Id,
                                                                            tbEmpleadoDepartamento.empd_Descripcion,
                                                                            tbEmpleadoDepartamento.empd_UsuarioCrea,
                                                                            tbEmpleadoDepartamento.empd_FechaCrea,
                                                                            tbEmpleadoDepartamento.empd_UsuarioModifica,
                                                                            tbEmpleadoDepartamento.empd_FechaModifica);


                    foreach (UDP_Gral_tbEmpleadoDepartamento_Update_Result Resultado in listEmpDepto)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo mdoficar el registro, contacte al administrador");
                        return View(tbEmpleadoDepartamento);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            return View(tbEmpleadoDepartamento);
        }

        // GET: EmpleadoDepartamento/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleadoDepartamento tbEmpleadoDepartamento = db.tbEmpleadoDepartamento.Find(id);
            if (tbEmpleadoDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleadoDepartamento);
        }

        // POST: EmpleadoDepartamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEmpleadoDepartamento tbEmpleadoDepartamento = db.tbEmpleadoDepartamento.Find(id);
            db.tbEmpleadoDepartamento.Remove(tbEmpleadoDepartamento);
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
