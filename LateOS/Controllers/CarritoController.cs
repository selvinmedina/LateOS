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
    public class CarritoController : Controller
    {
        private LateOSEntities db = new LateOSEntities();

        // GET: Carrito
        [AuthorizeUser(2)]
        public ActionResult Index(int? id)
        {
            V_Usuarios oUsuario = (V_Usuarios)Session["User"];
            id = oUsuario.clte_Id;
            var tbCarrito = db.tbCarrito.Where(x => x.tbCliente.clte_Id == id).ToList();
            return View(tbCarrito.ToList());
        }

        // GET: Carrito/Create
        [AuthorizeUser(2)]
        public ActionResult Create()
        {
            ViewBag.car_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.car_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identidad");
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Codigo");
            return View();
        }

        [AuthorizeUser(2)]
        public ActionResult AddProduct(int id)
        {
            V_Usuarios oUsuario = (V_Usuarios)Session["User"];
            int car_Id = 0;
            int resultado = 0;
            int? cantidad = 0;
            DateTime? fechaOrden = null;
            string ErrorInsert = "";
            string ErrorUpdate = "";
            IEnumerable<object> listCarritoSelect = null;
            IEnumerable<object> listCarritoInsert = null;
            IEnumerable<object> listCarritoUpdate = null;
            try
            {
                listCarritoSelect = db.UDP_Inv_tbCarrito_Select1(oUsuario.clte_Id, id);

                foreach (UDP_Inv_tbCarrito_Select1_Result res in listCarritoSelect)
                {
                    car_Id = res.car_Id;
                    resultado = res.car_Id;
                    fechaOrden = res.car_FechaOrden;
                    cantidad = res.car_Cantidad;
                }

                if (resultado > 0)
                {
                    listCarritoUpdate = db.UDP_Inv_tbCarrito_Update(car_Id, oUsuario.clte_Id, id, fechaOrden, cantidad + 1);

                    foreach (UDP_Inv_tbCarrito_Update_Result res in listCarritoUpdate)
                    {
                        ErrorUpdate = res.MensajeError;
                    }


                    if (ErrorUpdate == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                        return Json("error", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    listCarritoInsert = db.UDP_Inv_tbCarrito_Insert(oUsuario.clte_Id, id, 1);

                    foreach (UDP_Inv_tbCarrito_Insert_Result res in listCarritoInsert)
                    {
                        ErrorInsert = res.MensajeError;
                    }


                    if (ErrorInsert == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                        return Json("error", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return Json("bien", JsonRequestBehavior.AllowGet);
        }


        [AuthorizeUser(2)]
        public JsonResult RestProduct(int id)
        {
            V_Usuarios oUsuario = (V_Usuarios)Session["User"];
            int car_Id = 0;
            int resultado = 0;
            int? cantidad = 0;
            DateTime? fechaOrden = null;
            string ErrorUpdate = "";
            IEnumerable<object> listCarritoSelect = null;
            IEnumerable<object> listCarritoUpdate = null;
            try
            {
                listCarritoSelect = db.UDP_Inv_tbCarrito_Select1(oUsuario.clte_Id, id);

                foreach (UDP_Inv_tbCarrito_Select1_Result res in listCarritoSelect)
                {
                    car_Id = res.car_Id;
                    resultado = res.car_Id;
                    fechaOrden = res.car_FechaOrden;
                    cantidad = res.car_Cantidad;
                }

                if (resultado > 0)
                {
                    listCarritoUpdate = db.UDP_Inv_tbCarrito_Update(car_Id, oUsuario.clte_Id, id, fechaOrden, cantidad - 1);

                    foreach (UDP_Inv_tbCarrito_Update_Result res in listCarritoUpdate)
                    {
                        ErrorUpdate = res.MensajeError;
                    }


                    if (ErrorUpdate == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                        return Json("error", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return Json("bien", JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(2)]
        public JsonResult DeleteProduct(int id)
        {
            string ErrorDelete = "";
            IEnumerable<object> listCarritoDelete = null;
            try
            {
                listCarritoDelete = db.UDP_Inv_tbCarrito_Delete(id);

                foreach (UDP_Inv_tbCarrito_Delete_Result res in listCarritoDelete)
                {
                    ErrorDelete = res.MensajeError;
                }

                if (ErrorDelete == "-1")
                {
                    ModelState.AddModelError("", "No se pudo eliminar el registro, contacte al administrador.");
                    return Json("error", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return Json("bien", JsonRequestBehavior.AllowGet);
        }
        [AuthorizeUser(2)]
        public JsonResult FacturaCarrito(int id)
        {
            V_Usuarios oUsuario = (V_Usuarios)Session["User"];
            string ErrorInsert = "";
            IEnumerable<object> listFacturaCarrito = null;
            var consulta = (from fact in db.tbCarrito
                            where fact.clte_Id == oUsuario.clte_Id && fact.tbProducto.prod_Precio > 0
                            select new
                            {
                                prodprecio = fact.tbProducto.prod_Precio,
                                cantidadProd = fact.car_Cantidad,
                                prodId = fact.prod_Id
                            }).ToList();
            tbCarrito carr = db.tbCarrito.Where(x => x.clte_Id == oUsuario.clte_Id).FirstOrDefault();
            

            foreach (var item in consulta)
            {
                try
                {
                    listFacturaCarrito = db.UDP_Vent_tbFacturaDetalle_Insert(5,
                                                                            item.prodId,
                                                                            item.cantidadProd,
                                                                            item.prodprecio,
                                                                            Convert.ToDecimal(item.cantidadProd * 0.15),
                                                                            0,
                                                                            3,
                                                                            DateTime.Now);

                    foreach (UDP_Vent_tbFacturaDetalle_Insert_Result res in listFacturaCarrito)
                    {
                        ErrorInsert = res.MensajeError;
                    }


                    if (ErrorInsert == "-1")
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                        return Json("error", JsonRequestBehavior.AllowGet);
                    }
                    if (carr != null)
                    {
                        db.tbCarrito.Remove(carr);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            return Json("bien", JsonRequestBehavior.AllowGet);
        }



        [AuthorizeUser(2)]
        public ActionResult DetailsFactura()
        {
            V_Usuarios oUsuario = (V_Usuarios)Session["User"];
            var tbCarrito = db.tbCarrito.Where(x => x.tbCliente.clte_Id == oUsuario.clte_Id).ToList();
            return View(tbCarrito.ToList());
        }
        // POST: Carrito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(2)]
        public ActionResult Create([Bind(Include = "car_Id,clte_Id,prod_Id,car_FechaOrden,car_Cantidad")] tbCarrito tbCarrito, int? id)
        {
            V_Usuarios oUsuario = (V_Usuarios)Session["User"];
            tbCarrito.clte_Id = oUsuario.clte_Id;
            tbCarrito.car_FechaOrden = DateTime.Now;
            tbCarrito.prod_Id = id;
            tbCarrito.car_Cantidad = 1;
            if (ModelState.IsValid)
            {
                db.tbCarrito.Add(tbCarrito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbCarrito);
        }

        // GET: Carrito/Edit/5
        [AuthorizeUser(2)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbCarrito tbCarrito = db.tbCarrito.Find(id);
            if (tbCarrito == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identidad", tbCarrito.clte_Id);
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Codigo", tbCarrito.prod_Id);
            return View(tbCarrito);
        }

        // POST: Carrito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(2)]
        public ActionResult Edit([Bind(Include = "car_Id,clte_Id,prod_Id,car_FechaOrden,car_Cantidad")] tbCarrito tbCarrito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCarrito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clte_Id = new SelectList(db.tbCliente, "clte_Id", "clte_Identidad", tbCarrito.clte_Id);
            ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Codigo", tbCarrito.prod_Id);
            return View(tbCarrito);
        }

        // GET: Carrito/Delete/5
        [AuthorizeUser(2)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCarrito tbCarrito = db.tbCarrito.Find(id);
            if (tbCarrito == null)
            {
                return HttpNotFound();
            }
            return View(tbCarrito);
        }

        // POST: Carrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(2)]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCarrito tbCarrito = db.tbCarrito.Find(id);
            db.tbCarrito.Remove(tbCarrito);
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