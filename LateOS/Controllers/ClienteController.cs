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
    public class ClienteController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: Cliente
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbCliente = db.tbCliente.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbUsuarios2);
            return View(tbCliente.ToList());
        }

        // GET: Cliente/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            return View(tbCliente);
        }

        // GET: Cliente/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.usu_Id = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "clte_Id,clte_Identidad,clte_Nombre,clte_Apellido,clte_FechaNacimiento,clte_Sexo,clte_Telefono,clte_Correo,usu_Id,clte_UsuarioCrea,clte_FechaCrea,clte_UsuarioModifica,clte_FechaModifica")] tbCliente tbCliente)
        {
            tbCliente.usu_Id = 3;
            tbCliente.clte_UsuarioCrea = 3;
            tbCliente.clte_FechaCrea = DateTime.Now;

            IEnumerable<object> listCliente = null;
            string MensajeError = "";

            IEnumerable<object> listaClienteDirecciones = null;
            string MensajeErrorDir = "";
            var list = (List<tbClienteDireccion>)Session["tbClienteDirecciones"];
            if (ModelState.IsValid)
            {
                //db.tbCliente.Add(tbCliente);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                try
                {
                    listCliente = db.UDP_Vent_tbCliente_Insert(tbCliente.clte_Identidad,
                                                               tbCliente.clte_Nombre,
                                                               tbCliente.clte_Apellido,
                                                               tbCliente.clte_FechaNacimiento,
                                                               tbCliente.clte_Sexo,
                                                               tbCliente.clte_Telefono,
                                                               tbCliente.clte_Correo,
                                                               tbCliente.usu_Id,
                                                               tbCliente.clte_UsuarioCrea,
                                                               tbCliente.clte_FechaCrea);

                    foreach (UDP_Vent_tbCliente_Insert_Result Resultado in listCliente)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbCliente);
                    }
                    else
                    {
                        if (list != null && list.Count != 0)
                        {
                            foreach (tbClienteDireccion Dir in list)
                            {
                                listaClienteDirecciones = db.UDP_Vent_tbClienteDireccion_Insert(Dir.clted_Descripcion,
                                                                                    Convert.ToInt16(MensajeError),
                                                                                    2,
                                                                                    3,
                                                                                    DateTime.Now);


                                foreach (UDP_Vent_tbClienteDireccion_Insert_Result Res in listaClienteDirecciones)
                                {
                                    MensajeErrorDir = Res.MensajeError;
                                }
                                if (MensajeErrorDir.StartsWith("-1"))
                                {
                                    ModelState.AddModelError("", "No se pudo ingresar el detalle, contacte al administrador");
                                    return View(tbCliente);
                                }
                            }

                        }
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }

            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioCrea);
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioModifica);
            ViewBag.usu_Id = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCliente.usu_Id);
            return View(tbCliente);
        }


        public ActionResult _IndexClienteDireccion(int clte_Id)
        {
            return PartialView(db.tbClienteDireccion.Where(x => x.clte_Id == clte_Id).ToList());

        }
        // GET: Cliente/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioCrea);
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioModifica);
            ViewBag.usu_Id = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCliente.usu_Id);
            return View(tbCliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "clte_Id,clte_Identidad,clte_Nombre,clte_Apellido,clte_FechaNacimiento,clte_Sexo,clte_Telefono,clte_Correo,usu_Id,clte_UsuarioCrea,clte_FechaCrea,clte_UsuarioModifica,clte_FechaModifica")] tbCliente tbCliente)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tbCliente).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");

                tbCliente.usu_Id = 3;
                tbCliente.clte_UsuarioCrea = 3;
                tbCliente.clte_FechaCrea = DateTime.Now;
                tbCliente.clte_UsuarioModifica = 3;
                tbCliente.clte_FechaModifica= DateTime.Now;

                IEnumerable<object> listCliente = null;
                string MensajeError = "";
                try
                {
                    listCliente = db.UDP_Vent_tbCliente_Update(tbCliente.clte_Id,
                                                               tbCliente.clte_Identidad,
                                                               tbCliente.clte_Nombre,
                                                               tbCliente.clte_Apellido,
                                                               tbCliente.clte_FechaNacimiento,
                                                               tbCliente.clte_Sexo,
                                                               tbCliente.clte_Telefono,
                                                               tbCliente.clte_Correo,
                                                               tbCliente.usu_Id,
                                                               tbCliente.clte_UsuarioCrea,
                                                               tbCliente.clte_FechaCrea,
                                                               tbCliente.clte_UsuarioModifica,
                                                               tbCliente.clte_FechaModifica);

                    foreach (UDP_Vent_tbCliente_Update_Result Resultado in listCliente)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbCliente);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            ViewBag.clte_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioCrea);
            ViewBag.clte_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCliente.clte_UsuarioModifica);
            ViewBag.usu_Id = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCliente.usu_Id);
            return View(tbCliente);
        }



        public JsonResult saveDirecciones(tbClienteDireccion ClienteDireccion)
        {
            List<tbClienteDireccion> sessionClienteDireccion= new List<tbClienteDireccion>();
            var list = (List<tbClienteDireccion>)Session["tbClienteDirecciones"];
            if (list == null)
            {
                sessionClienteDireccion.Add(ClienteDireccion);
                Session["tbClienteDirecciones"] = sessionClienteDireccion;
            }
            else
            {
                list.Add(ClienteDireccion);
                Session["tbClienteDirecciones"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        public JsonResult removeDirecciomes(string IDDireccion)
        {
            var list = (List<tbClienteDireccion>)Session["tbClienteDirecciones"];
            if (list != null)
            {
                var itemToRemove = list.Single(x => x.clted_Id == Convert.ToInt16(IDDireccion));
                list.Remove(itemToRemove);
                Session["tbClienteDirecciones"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }




        // GET: Cliente/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            return View(tbCliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCliente tbCliente = db.tbCliente.Find(id);
            db.tbCliente.Remove(tbCliente);
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

        //
        //Actions : MALCOM MEDINA
        //

        //GET : CLIENTE ADMIN
        [AuthorizeUser(1)]
        public ActionResult Index_Admin()
        {
            var tbCliente = db.tbCliente.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbUsuarios2);
            return View(tbCliente.ToList());
        }

        //
        //Recuperar data del Cliente
        public JsonResult _DataCliente(int id)
        {
            var Cliente = db.UDP_Vent_tbCliente_Select_(id).ToList();
            return Json(Cliente, JsonRequestBehavior.AllowGet);
        }

        //
        //UpdateCliente
        public JsonResult _UpdateCliente(tbCliente Cliente)
        {
            var FechaNacimiento = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", Cliente.clte_FechaNacimiento));
            var clte = db.UDP_Vent_tbCliente_Update(Cliente.clte_Id,
                                                    Cliente.clte_Identidad,
                                                    Cliente.clte_Nombre,
                                                    Cliente.clte_Apellido,
                                                    FechaNacimiento,
                                                    Cliente.clte_Sexo,
                                                    Cliente.clte_Telefono,
                                                    Cliente.clte_Correo,
                                                    3,
                                                    3,
                                                    Cliente.clte_FechaCrea,
                                                    3,
                                                    DateTime.Now);
            return Json(Cliente, JsonRequestBehavior.AllowGet);
        }
    }
}
