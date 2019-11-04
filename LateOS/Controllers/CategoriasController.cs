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
    public class CategoriasController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        [AuthorizeUser(1)]
        // GET: Categorias
        public ActionResult Index()
        {
            var tbCategoria = db.tbCategoria.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            return View(tbCategoria.ToList());
        }
        public ActionResult _IndexSubCategoria(int idCategoria)
        {
            return PartialView(db.tbSubCategoria.Where(x => x.cat_Id == idCategoria).ToList());
        }

        //public ActionResult GetList()
        //{
        //    List<>
        //    using (db)
        //}

        // GET: Categorias/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCategoria tbCategoria = db.tbCategoria.Find(id);
            if (tbCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbCategoria);
        }

        // GET: Categorias/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.cat_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.cat_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "cat_Id,cat_Descripcion,cat_UsuarioCrea,cat_FechaCrea,cat_UsuarioModifica,cat_FechaModifica")] tbCategoria tbCategoria)
        {
            tbCategoria.cat_UsuarioCrea = 3;
            tbCategoria.cat_FechaCrea = DateTime.Now;
            IEnumerable<object> listCategoria = null;
            string MensajeError = "";
            IEnumerable<object> listSubCategoria = null;
            string MensajeErrorSub = "";
            var list = (List<tbSubCategoria>)Session["tbSubcategoria"];

            if (ModelState.IsValid)
            {
                try
                {
                    listCategoria = db.UDP_Inv_tbCategoria_Insert(tbCategoria.cat_Descripcion,
                                                                  tbCategoria.cat_UsuarioCrea,
                                                                  tbCategoria.cat_FechaCrea);
                        foreach (UDP_Inv_tbCategoria_Insert_Result Resultado in listCategoria)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                    if(MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                        return View(tbCategoria);
                    }
                    else
                    {
                        if(list != null && list.Count != 0)
                        {
                            foreach(tbSubCategoria Sub in list)
                            {
                                listSubCategoria = db.UDP_Inv_tbSubCategoria_Insert(Sub.subc_Descripcion,
                                                                                    Convert.ToInt16(MensajeError),
                                                                                    3,
                                                                                    DateTime.Now);
                                foreach(UDP_Inv_tbSubCategoria_Insert_Result Res in listSubCategoria)
                                {
                                    MensajeErrorSub = Res.MensajeError;
                                }
                                if(MensajeErrorSub.StartsWith("-1"))
                                {
                                    ModelState.AddModelError("", "No se pudo mdoficar el registro, contacte al administrador");
                                    return View(tbCategoria);
                                }
                            }

                        }
                    }
                }
                catch(Exception Ex)
                {
                    Ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            return View(tbCategoria);
        }



        // GET: Categorias/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCategoria tbCategoria = db.tbCategoria.Find(id);
            if (tbCategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCategoria.cat_UsuarioCrea);
            ViewBag.cat_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCategoria.cat_UsuarioModifica);
            return View(tbCategoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "cat_Id,cat_Descripcion,cat_UsuarioCrea,cat_FechaCrea,cat_UsuarioModifica,cat_FechaModifica")] tbCategoria tbCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCategoria.cat_UsuarioCrea);
            ViewBag.cat_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbCategoria.cat_UsuarioModifica);
            return View(tbCategoria);
        }


        public JsonResult saveSubcategoria(tbSubCategoria Subcategoria)
        {
            List<tbSubCategoria> sessionSubcategoria = new List<tbSubCategoria>();
            var list = (List<tbSubCategoria>)Session["tbSubcategoria"];
             if(list == null)
            {
                sessionSubcategoria.Add(Subcategoria);
                Session["tbSubcategoria"] = sessionSubcategoria;
            }
             else
            {
                list.Add(Subcategoria);
                Session["tbSubcategoria"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        public JsonResult removeSubCategoria(string IDSub)
        {
            var list = (List<tbSubCategoria>)Session["tbSubcategoria"];
            if (list != null)
            {
                var itemToRemove = list.Single(x => x.subc_Id == Convert.ToInt16(IDSub));
                list.Remove(itemToRemove);
                Session["tbSubcategoria"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        public JsonResult _EditSubCategoria(int id)
        {
            var Subcategoria = db.UDP_Inv_tbSubCategoria_Select_(id).ToList();
            return Json(Subcategoria, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(1)]
        public JsonResult UpdateSubCategoria(tbSubCategoria SubCategoria)
        {
            IEnumerable<object> listSubCategoria = null;
            try
            {
                listSubCategoria = db.UDP_Inv_tbSubCategoria_Update(SubCategoria.subc_Id,
                                                                    SubCategoria.subc_Descripcion,
                                                                    SubCategoria.cat_Id,
                                                                    SubCategoria.subc_UsuarioCrea,
                                                                    SubCategoria.subc_FechaCrea,
                                                                    3,
                                                                    DateTime.Now);
                
            }
            catch(Exception Ex)
            {
                Ex.Message.ToString();
            }
            
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        // GET: Categorias/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCategoria tbCategoria = db.tbCategoria.Find(id);
            if (tbCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbCategoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCategoria tbCategoria = db.tbCategoria.Find(id);
            db.tbCategoria.Remove(tbCategoria);
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
