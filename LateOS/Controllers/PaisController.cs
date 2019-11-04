// EL DEL PROYECTO
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LateOS.Models;
using LateOS.Attribute;
using LateOS.Filters;

namespace LateOS.Controllers
{
    public class PaisController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: Pais
        //[SessionManager("Pais/Index")]
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbPais = db.tbPais.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            return View(tbPais.ToList());
        }

        public ActionResult _IndexCiudad(int idPais)
        {
            return PartialView(db.tbPais.Where(x => x.pais_Id == idPais).ToList());
        }
        // GET: Pais/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbPais tbPais = db.tbPais.Find(id);
            if (tbPais == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbPais);
        }

        // GET: Pais/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.pais_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.pais_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Pais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "pais_Id,pais_Descripcion,pais_UsuarioModifica,pais_FechaModifica")] tbPais tbPais)
        {
            tbPais.pais_UsuarioCrea = 3;
            tbPais.pais_FechaCrea = DateTime.Now;
            var list = (List<tbCiudad>)Session["tbCiudad"];
            IEnumerable<object> listPais = null;
            IEnumerable<object> listCiudad = null;
            string MensajeError = "";
            string MensajeErrorCiu = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listPais = db.UDP_Gral_tbPais_Insert(tbPais.pais_Descripcion,
                                              tbPais.pais_UsuarioCrea,
                                              tbPais.pais_FechaCrea);

                    foreach (UDP_Gral_tbPais_Insert_Result Resultado in listPais)
                    {
                        MensajeError = Resultado.MensajeError;
                    }                        

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbPais);
                    }
                    else
                    {
                        if (list != null && list.Count != 0)
                        {
                            foreach (tbCiudad Ciu in list)
                            {
                                db.UDP_Gral_tbCiudad_Insert(Ciu.ciu_Descripcion, 
                                                                        Convert.ToInt32(MensajeError),
                                                                        3,
                                                                        DateTime.Now);

                                foreach (UDP_Gral_tbCiudad_Insert_Result Res in listCiudad)
                                    MensajeErrorCiu = Res.MensajeError;
                                if (MensajeErrorCiu.StartsWith("-1"))
                                {
                                    ModelState.AddModelError("", "No se pudo ingresar el detalle, contacte al administrador.");

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

            return View(tbPais);
        }

        // GET: Pais/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbPais tbPais = db.tbPais.Find(id);
            if (tbPais == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.pais_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbPais.pais_UsuarioCrea);
            ViewBag.pais_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbPais.pais_UsuarioModifica);
            return View(tbPais);
        }

        // POST: Pais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "pais_Id,pais_Descripcion,pais_UsuarioCrea,pais_FechaCrea,pais_UsuarioModifica,pais_FechaModifica")] tbPais tbPais)
        {
            tbPais.pais_UsuarioModifica = 3;
            tbPais.pais_FechaModifica = DateTime.Now;
            //--
            IEnumerable<object> listPais = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listPais = db.UDP_Gral_tbPais_Update(tbPais.pais_Id,
                                                        tbPais.pais_Descripcion,
                                                        tbPais.pais_UsuarioCrea,
                                                        tbPais.pais_FechaCrea,
                                                        tbPais.pais_UsuarioModifica,
                                                        tbPais.pais_FechaModifica);

                    foreach (UDP_Gral_tbPais_Update_Result Resultado in listPais)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo mdoficar el registro, contacte al administrador");
                        return View(tbPais);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            //--            
            ViewBag.pais_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbPais.pais_UsuarioCrea);
            ViewBag.pais_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbPais.pais_UsuarioModifica);
            return View(tbPais);
        }

        public JsonResult saveCiudad(tbCiudad Ciudad)
        {
            List<tbCiudad> sessionCiudad = new List<tbCiudad>();
            var list = (List<tbCiudad>)Session["tbCiudad"];
            if (list == null)
            {
                sessionCiudad.Add(Ciudad);
                Session["tbCiudad"] = sessionCiudad;
            }
            else
            {
                list.Add(Ciudad);
                Session["tbCiudad"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        public JsonResult removeCiudad(string IDCiu)
        {
            var list = (List<tbCiudad>)Session["tbCiudad"];
            if (list != null)
            {
                var itemToRemove = list.Single(x => x.ciu_Id == Convert.ToInt32(IDCiu));
                list.Remove(itemToRemove);
                Session["tbCiudad"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        public JsonResult _EditCiudad(int id)
        {
            var Ciudad = db.UDP_Gral_tbCiudad_Select_(id).ToList();
            return Json(Ciudad, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateCiudad(tbCiudad Ciudad)
        {
            IEnumerable<object> listCiudad = null;
            try
            {
                listCiudad = db.UDP_Gral_tbCiudad_Update(Ciudad.ciu_Id, 
                                                         Ciudad.ciu_Descripcion,
                                                         Ciudad.pais_Id,
                                                         Ciudad.ciu_UsuarioCrea,
                                                         Ciudad.ciu_FechaCrea,
                                                         3,
                                                         DateTime.Now);

            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return Json("Exito", JsonRequestBehavior.AllowGet);
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
