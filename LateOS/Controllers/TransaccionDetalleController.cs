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
    public class TransaccionDetalleController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: TransaccionDetalle
        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbTransaccionDetalle = db.tbTransaccionDetalle.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbFactura).Include(t => t.tbEstadoTransaccion);
            return View(tbTransaccionDetalle.ToList());
        }

        // GET: TransaccionDetalle/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTransaccionDetalle tbTransaccionDetalle = db.tbTransaccionDetalle.Find(id);
            if (tbTransaccionDetalle == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbTransaccionDetalle);
        }

        // GET: TransaccionDetalle/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.trad_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.trad_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo");
            ViewBag.estt_Id = new SelectList(db.tbEstadoTransaccion, "estt_Id", "estt_Descripcion");
            return View();
        }

        // POST: TransaccionDetalle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include = "trad_Id,trad_Descripcion,trad_Fecha,fact_Id,trad_UsuarioCrea,trad_FechaCrea,trad_UsuarioModifica,trad_FechaModifica,estt_Id")] tbTransaccionDetalle tbTransaccionDetalle)
        {
            if (ModelState.IsValid)
            {
                db.tbTransaccionDetalle.Add(tbTransaccionDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.trad_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbTransaccionDetalle.trad_UsuarioCrea);
            ViewBag.trad_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbTransaccionDetalle.trad_UsuarioModifica);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "Codigo Factura", tbTransaccionDetalle.fact_Id);
            ViewBag.estt_Id = new SelectList(db.tbEstadoTransaccion, "estt_Id", "estt_Descripcion", tbTransaccionDetalle.estt_Id);
            return View(tbTransaccionDetalle);
        }

        // GET: TransaccionDetalle/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTransaccionDetalle tbTransaccionDetalle = db.tbTransaccionDetalle.Find(id);
            if (tbTransaccionDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.trad_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbTransaccionDetalle.trad_UsuarioCrea);
            ViewBag.trad_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbTransaccionDetalle.trad_UsuarioModifica);
            ViewBag.fact_Id = new SelectList(db.tbFactura, "fact_Id", "fact_Codigo", tbTransaccionDetalle.fact_Id);
            

            var Lista = from Table in db.tbEstadoTransaccion
                        select new
                        {
                            estt_Id = Table.estt_Id,
                            estt_Descripcion = (Table.estt_Descripcion=="E"?"Enviado":
                                                Table.estt_Descripcion == "R" ? "Recibido" :
                                                Table.estt_Descripcion == "C" ? "Camino" :
                                                Table.estt_Descripcion == "A" ? "Almacen" :
                            "hola")
                        };                        

            ViewBag.estt_Id = new SelectList(Lista, "estt_Id", "estt_Descripcion", tbTransaccionDetalle.estt_Id);
            return View(tbTransaccionDetalle);
        }

        // POST: TransaccionDetalle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "trad_Id,trad_Descripcion,trad_Fecha,fact_Id,trad_UsuarioCrea,trad_FechaCrea,trad_UsuarioModifica,trad_FechaModifica,estt_Id")] tbTransaccionDetalle tbTransaccionDetalle)
        {
            tbTransaccionDetalle.trad_UsuarioCrea = 3;
            tbTransaccionDetalle.trad_FechaCrea = DateTime.Now;
            IEnumerable<object> listtraddetalle= null;
            string MensajeError = "";

            if (ModelState.IsValid)
            {
                try
                {
                    listtraddetalle = db.UDP_Vent_tbTransaccionDetalle_Update(tbTransaccionDetalle.trad_Id,
                                                                              tbTransaccionDetalle.trad_Descripcion, tbTransaccionDetalle.trad_Fecha,
                                                                              tbTransaccionDetalle.fact_Id, tbTransaccionDetalle.estt_Id,
                                                                              tbTransaccionDetalle.trad_UsuarioCrea, tbTransaccionDetalle.trad_FechaCrea,
                                                                              tbTransaccionDetalle.trad_UsuarioModifica, tbTransaccionDetalle.trad_FechaModifica);


                      
                    foreach (UDP_Vent_tbTransaccionDetalle_Update_Result Resultado in listtraddetalle)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        return View(tbTransaccionDetalle);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            
            return View(tbTransaccionDetalle);
        }

        // GET: TransaccionDetalle/Delete/5
        [AuthorizeUser(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbTransaccionDetalle tbTransaccionDetalle = db.tbTransaccionDetalle.Find(id);
            if (tbTransaccionDetalle == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbTransaccionDetalle);
        }

        // POST: TransaccionDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTransaccionDetalle tbTransaccionDetalle = db.tbTransaccionDetalle.Find(id);
            db.tbTransaccionDetalle.Remove(tbTransaccionDetalle);
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
