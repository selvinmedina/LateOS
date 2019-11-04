using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LateOS.Models;
using System.IO;
using LateOS.Filters;
using LateOS.Models;

namespace LateOS.Controllers
{
    public class ProductoController : Controller
    {
        private LateOSEntities db = new LateOSEntities();
        private LateOS.Models.Helpers help = new LateOS.Models.Helpers();
        // GET: Producto

        [AuthorizeUser(1)]
        public ActionResult Index()
        {
            var tbProducto = db.tbProducto.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).Include(t => t.tbProveedor).Include(t => t.tbSubCategoria);
            return View(tbProducto.ToList());
        }

        #region Detalles del Producto
        // GET: Producto/Details/5
        [AuthorizeUser(1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return RedirectToAction("Index");
            }
            return View(tbProducto);
        }
        #endregion

        #region Crear Producto
        [HttpPost]
        [AuthorizeUser(1)]
        public ActionResult CrearImagenProducto(tbProductoImagen producto, HttpPostedFileBase agregarImagenproducto, int idProd)
        {
            producto.proi_UsuarioCrea = 3;
            producto.proi_FechaCrea = DateTime.Now;
            #region Declaración de Variables
            IEnumerable<object> listImagenProducto = null;
            string MensajeError = "";
            string nombreArchivo = "";
            string direccion = "";
            string rutaCompleta = "";
            #endregion

            if (ModelState.IsValid)
            {
                if (agregarImagenproducto != null && agregarImagenproducto.ContentLength > 0)
                {
                    nombreArchivo = help.CrearNombreImagen(10) + Path.GetExtension(agregarImagenproducto.FileName);
                    direccion = "Content/img/imgProductos/";
                    rutaCompleta = direccion + nombreArchivo;

                }
                try
                {
                    listImagenProducto = db.UDP_Inv_tbProductoImagen_Insert(idProd, rutaCompleta, producto.proi_UsuarioCrea, producto.proi_FechaCrea);

                    //Recorer el resultado de la imagen
                    foreach (UDP_Inv_tbProductoImagen_Insert_Result Resultado in listImagenProducto)
                    {
                        MensajeError = Resultado.MensajeError;
                    }

                    //Verificar si fue correcta la insersión
                    if (MensajeError.StartsWith("-1")) //Si fue incorrecta
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");
                    }
                    else
                    {
                        agregarImagenproducto.SaveAs(Server.MapPath("~/" + rutaCompleta));
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Producto/Create
        [AuthorizeUser(1)]
        public ActionResult Create()
        {
            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario");
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Descripcion");
            ViewBag.subc_Id = new SelectList(db.tbSubCategoria, "subc_Id", "subc_Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Create([Bind(Include =
            "prod_Id,prod_Codigo,prod_Descripcion,prov_Id,subc_Id,prod_Precio,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica,prod_FechaModifica,prod_Img")]
            tbProducto tbProducto,
            HttpPostedFileBase imgProducto,
            HttpPostedFileBase[] imgProductosInfo)
        {
            tbProducto.prod_UsuarioCrea = 3;
            tbProducto.prod_FechaCrea = DateTime.Now;
            IEnumerable<object> listProducto = null;
            string MensajeError = "";
            IEnumerable<object> listProdImg = null;
            string MensajeErrorProd = "";
            //var list = (List<tbSubCategoria>)Session["tbSubcategoria"];
            string h = tbProducto.prod_Codigo;
            if (ModelState.IsValid || tbProducto.prod_Descripcion != null)
            {
                try
                {
                    if (imgProducto != null && imgProducto.ContentLength > 0)
                    {
                        string ruta = help.CrearNombreImagen(10) + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(imgProducto.FileName);
                        string direccion = "Content/img/imgCatalogo/";
                        tbProducto.prod_Img = direccion+ruta;
                    }

                    imgProducto.SaveAs(Server.MapPath("~/"+tbProducto.prod_Img));

                    listProducto = db.UDP_Inv_tbProducto_Insert(help.CrearCodigoProducto(30),
                                                                tbProducto.prod_Descripcion,
                                                                tbProducto.prov_Id,
                                                                tbProducto.subc_Id,
                                                                tbProducto.prod_Precio,
                                                                tbProducto.prod_UsuarioCrea,
                                                                tbProducto.prod_FechaCrea,
                                                                tbProducto.prod_Img);

                    foreach (UDP_Inv_tbProducto_Insert_Result Resultado in listProducto)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador.");

                        ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioCrea);
                        ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
                        ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Descripcion", tbProducto.prov_Id);
                        ViewBag.subc_Id = new SelectList(db.tbSubCategoria, "subc_Id", "subc_Descripcion", tbProducto.subc_Id);
                        return View(tbProducto);
                    }
                    else
                    {
                        if (imgProductosInfo != null && imgProductosInfo.Length > 0)
                        {
                            foreach (var img in imgProductosInfo)
                            {
                                string ruta2 = help.CrearNombreImagen(10)+DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(img.FileName);
                                string direccion = "Content/img/imgProductos/";
                                string rutaCompleta = direccion + ruta2;

                                img.SaveAs(Server.MapPath("~/"+rutaCompleta));
                                listProdImg = db.UDP_Inv_tbProductoImagen_Insert(Convert.ToInt16(MensajeError),
                                                                                 rutaCompleta,
                                                                                 3,
                                                                                 DateTime.Now);
                                foreach (UDP_Inv_tbProductoImagen_Insert_Result Res in listProdImg)
                                {
                                    MensajeErrorProd = Res.MensajeError;
                                }
                                if (MensajeErrorProd.StartsWith("-1"))
                                {
                                    ModelState.AddModelError("", "No se pudo mdoficar el registro, contacte al administrador");

                                    ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioCrea);
                                    ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
                                    ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Descripcion", tbProducto.prov_Id);
                                    ViewBag.subc_Id = new SelectList(db.tbSubCategoria, "subc_Id", "subc_Descripcion", tbProducto.subc_Id);
                                    return View(tbProducto);
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
            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioCrea);
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Descripcion", tbProducto.prov_Id);
            ViewBag.subc_Id = new SelectList(db.tbSubCategoria, "subc_Id", "subc_Descripcion", tbProducto.subc_Id);
            return View(tbProducto);
        }
        #endregion

        #region Editar Producto
        // GET: Producto/Edit/5
        [AuthorizeUser(1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioCrea);
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Descripcion", tbProducto.prov_Id);
            ViewBag.subc_Id = new SelectList(db.tbSubCategoria, "subc_Id", "subc_Descripcion", tbProducto.subc_Id);
            return View(tbProducto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(1)]
        public ActionResult Edit([Bind(Include = "prod_Id,prod_Codigo,prod_Descripcion,prov_Id,subc_Id,prod_Precio,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica,prod_FechaModifica,prod_Img")] tbProducto tbProducto, HttpPostedFileBase imgProducto)
        {
            if (ModelState.IsValid)
            {
                if (imgProducto != null && imgProducto.ContentLength > 0)
                {
                    int cantidad = tbProducto.prod_Img.Length;
                    string nombreImagen= tbProducto.prod_Img.Substring(24, cantidad -24);
                    var file = Path.Combine(HttpContext.Server.MapPath("/Content/img/imgCatalogo/"), nombreImagen);
                    if (System.IO.File.Exists(file))
                        System.IO.File.Delete(file);

                    string ruta = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(imgProducto.FileName);
                    string direccion = "Content/img/imgCatalogo/";
                    imgProducto.SaveAs(Server.MapPath("~/" + direccion + ruta));
                    tbProducto.prod_Img = direccion + ruta;
                }



                tbProducto.prod_UsuarioModifica = 3;
                tbProducto.prod_FechaModifica = DateTime.Now;
                IEnumerable<object> listProducto = null;
                string MensajeError = "";
                try
                {
                    listProducto = db.UDP_Inv_tbProducto_Update(tbProducto.prod_Id,
                                                                tbProducto.prod_Codigo,
                                                                tbProducto.prod_Descripcion,
                                                                tbProducto.prov_Id,
                                                                tbProducto.subc_Id,
                                                                tbProducto.prod_Precio,
                                                                tbProducto.prod_UsuarioCrea,
                                                                tbProducto.prod_FechaCrea,
                                                                tbProducto.prod_UsuarioModifica,
                                                                tbProducto.prod_FechaModifica,
                                                                tbProducto.prod_Img);
                    foreach (UDP_Inv_tbProducto_Update_Result Resultado in listProducto)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioCrea);
                        ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
                        ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Descripcion", tbProducto.prov_Id);
                        ViewBag.subc_Id = new SelectList(db.tbSubCategoria, "subc_Id", "subc_Descripcion", tbProducto.subc_Id);
                        return View(tbProducto);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            ViewBag.prod_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioCrea);
            ViewBag.prod_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreUsuario", tbProducto.prod_UsuarioModifica);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Descripcion", tbProducto.prov_Id);
            ViewBag.subc_Id = new SelectList(db.tbSubCategoria, "subc_Id", "subc_Descripcion", tbProducto.subc_Id);
            return View(tbProducto);
        }
        #endregion

        #region Borrar Producto
        // GET: Producto/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbProducto tbProducto = db.tbProducto.Find(id);
        //    if (tbProducto == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbProducto);
        //}

        //// POST: Producto/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tbProducto tbProducto = db.tbProducto.Find(id);
        //    db.tbProducto.Remove(tbProducto);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public JsonResult _EliminarProductoImagen(int idProdImg)
        {
            IEnumerable<object> listPI = null;
            var consulta = (from i in db.tbProductoImagen
                           where i.proi_Id == idProdImg
                           select i.proi_imagen).First();
            int cantidad = consulta.Length;
            string sMensaje = "";
            try
            {
                listPI = db.UDP_Inv_tbProductoImagen_Delete(idProdImg);
                foreach (UDP_Inv_tbProductoImagen_Delete_Result resultado in listPI)
                    sMensaje = resultado.MensajeError;

                if(sMensaje == "-1")
                {
                    
                }
                else
                {
                    string nombreImagen = consulta.Substring(27, cantidad - 27);
                    var file = Path.Combine(HttpContext.Server.MapPath("/Content/img/imgProductos/"), nombreImagen);
                    System.IO.File.Delete(file);
                }
            }
            catch (Exception)
            {
                
            }

            //

            return Json("bien", JsonRequestBehavior.AllowGet);
        }
        #endregion

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
