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
    public class EntradaController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: Entrada
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbEntrada = db.tbEntrada.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbProducto);
            return View(tbEntrada.ToList());
        }

        // GET: Entrada/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            if (tbEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tbEntrada);
        }

        // GET: Entrada/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.ent_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.ent_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Descripcion");
            return View();
        }

        // POST: Entrada/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "prod_Id,ent_Existencia,ent_Cantidad,ent_FechaEntrada,ent_UsuarioCrea,ent_FechaCrea")] tbEntrada tbEntrada)
        {
            tbEntrada.ent_UsuarioCrea = 3;
            tbEntrada.ent_FechaCrea = DateTime.Now;
            tbEntrada.ent_FechaEntrada = DateTime.Now;
           
            IEnumerable<object> listEntrada = null;
            string MensajeError = "";
            IEnumerable<object> listInvFi = null;
            string MensajeErrorInv = "";
            var list = (List<InventarioFisico>)Session["InventarioFisico"];
            if (ModelState.IsValid)
            {
                try
                {
                    listEntrada = db.UDP_Inv_tbEntrada_Insert(tbEntrada.prod_Id, 
                                                              tbEntrada.ent_Existencia, 
                                                              tbEntrada.ent_Cantidad, 
                                                              tbEntrada.ent_FechaEntrada, 
                                                              tbEntrada.ent_UsuarioCrea, 
                                                              tbEntrada.ent_FechaCrea);

                    foreach (UDP_Inv_tbEntrada_Insert_Result Resultado in listEntrada)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el detalle, contacte al administrador");
                        return View(tbEntrada);
                    }
                    else
                    {
                        if(list != null && list.Count >=0)
                        {
                            foreach (InventarioFisico Sub in list)
                            {
                                Sub.invf_total = Sub.invf_total + tbEntrada.ent_Cantidad;
                                listInvFi = db.UDP_Inv_InventarioFisico_Update(Sub.invf_id,
                                                                                Sub.prod_Id,
                                                                                Sub.invf_total,
                                                                                Sub.invf_FechaInventario,
                                                                                Sub.invf_UsuarioCrea,
                                                                                Sub.invf_FechaCrea,
                                                                                Sub.invf_UsuarioModifica,
                                                                                Sub.invf_FechaModifica);

                                foreach (UDP_Inv_InventarioFisico_Update_Result Res in listInvFi)
                                {
                                    MensajeErrorInv = Res.MensajeError;
                                }
                                if (MensajeErrorInv.StartsWith("-1"))
                                {
                                    ModelState.AddModelError("", "No se pudo mdoficar el registro, contacte al administrador");
                                    return View(tbEntrada);
                                }
                            }
                                
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }

                //db.tbEntrada.Add(tbEntrada);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ent_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEntrada.ent_UsuarioCrea);
            ViewBag.ent_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEntrada.ent_UsuarioModifica);
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Descripcion", tbEntrada.prod_Id);
            return View(tbEntrada);
        }

        // GET: Entrada/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEntrada tbEntrada = db.tbEntrada.Find(id);
            if (tbEntrada == null)
            {
                return HttpNotFound();
            }
            ViewBag.ent_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEntrada.ent_UsuarioCrea);
            ViewBag.ent_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEntrada.ent_UsuarioModifica);
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Descripcion", tbEntrada.prod_Id);
            return View(tbEntrada);
        }

        // POST: Entrada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "ent_Id,prod_Id,ent_Existencia,ent_Cantidad,ent_FechaEntrada,ent_UsuarioCrea,ent_FechaCrea,ent_UsuarioModifica,ent_FechaModifica")] tbEntrada tbEntrada)
        {
            tbEntrada.ent_UsuarioModifica = 3;
            tbEntrada.ent_FechaModifica = DateTime.Now;
            IEnumerable<object> listEntrada = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listEntrada = db.UDP_Inv_tbEntrada_Update(tbEntrada.ent_Id,
                                                              tbEntrada.prod_Id,
                                                              tbEntrada.ent_Existencia,
                                                              tbEntrada.ent_Cantidad,                                                              
                                                              tbEntrada.ent_FechaEntrada,
                                                              tbEntrada.ent_UsuarioCrea,
                                                              tbEntrada.ent_FechaCrea,
                                                              tbEntrada.ent_UsuarioModifica,
                                                              tbEntrada.ent_FechaModifica);
                    foreach(UDP_Inv_tbEntrada_Update_Result Resultado in listEntrada)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el detalle, contacte al administrador");
                        return View(tbEntrada);
                    }
                }
                catch(Exception Ex)
                {
                    Ex.Message.ToString();
                }
                //db.Entry(tbEntrada).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ent_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEntrada.ent_UsuarioCrea);
            ViewBag.ent_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbEntrada.ent_UsuarioModifica);
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Descripcion", tbEntrada.prod_Id);
            return View(tbEntrada);
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
