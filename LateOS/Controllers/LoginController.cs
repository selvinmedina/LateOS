using LateOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LateOS.Models.ViewModel;
using LateOS.Helpers;
using System.Net.Mail;

namespace LateOS.Controllers
{
    public class LoginController : Controller
    {
        LateOSEntities db = new LateOSEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            Session["rol"] = null;
            Session["User"] = null;

            return RedirectToAction("Index", "Ventas");
        }

        [HttpPost]
        public ActionResult Index(string User, string Pass)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(Pass);
                byte[] hash;
                using (SHA512 shaM = new SHA512Managed())
                {
                    hash = shaM.ComputeHash(data);
                }

                using (LateOSEntities db = new LateOSEntities())
                {
                    V_Usuarios oUser = (from d in db.V_Usuarios
                                        where d.clte_Correo == User.Trim() && d.usu_Password == hash
                                        select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña invalida";
                        Session["User"] = null;
                        return View();
                    }
                    Session["User"] = oUser;

                    V_Usuarios objSesesion = (V_Usuarios)Session["User"];
                    Usuario_Rol ruta = (from d in db.Usuario_Rol
                                        where d.usu_Id == objSesesion.usu_Id
                                        select d).FirstOrDefault();

                    if (ruta.tbRol.nombre == "admin")
                    {
                        Session["rol"] = "admin";
                        return RedirectToAction("InfoStatus", "Home");
                    }
                    else
                    {
                        Session["rol"] = "cliente";
                        return RedirectToAction("Index", "Home");
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }

        //Recuperación de la cuenta por correo electrónico
        public ActionResult StartRecovery()
        {
            RecoveryViewModel model = new RecoveryViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult StartRecovery(RecoveryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string token = Generales.GetSha256(Guid.NewGuid().ToString());

                using (Models.LateOSEntities db = new Models.LateOSEntities())
                {
                    var oUser = db.V_Usuarios.Where(d => d.clte_Correo == model.Email).FirstOrDefault();
                    if (oUser != null)
                    {
                        tbUsuarios tbUser = db.tbUsuarios.Where(x => x.usu_Id == oUser.usu_Id).FirstOrDefault();
                        if(tbUser != null)
                        {
                            tbUser.usu_Token = token;
                            db.Entry(tbUser).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }                       

                        //enviar mail
                        Generales.SendEmail(oUser.clte_Correo, token);
                    }
                }
                ViewBag.Message = "El correo se envió correctamente";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Se produjo un error";
                return View();
            }
        }


        [HttpGet]
        public ActionResult Recovery(string token)
        {
            RecoveryPasswordViewModel model = new RecoveryPasswordViewModel();
            model.token = token;
            using (LateOSEntities db = new LateOSEntities())
            {
                if (model.token == null || model.token.Trim().Equals(""))
                {
                    return View("Index");
                }
                var oUser = db.tbUsuarios.Where(d => d.usu_Token == model.token).FirstOrDefault();
                if (oUser == null)
                {
                    ViewBag.Error = "Tu token ha expirado";
                    return View("Index");

                }
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult Recovery(RecoveryPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var data = Encoding.UTF8.GetBytes(model.Password);
                byte[] hash;
                using (SHA512 shaM = new SHA512Managed())
                {
                    hash = shaM.ComputeHash(data);
                }

                using (LateOSEntities db = new LateOSEntities())
                {
                    var oUser = db.tbUsuarios.Where(d => d.usu_Token == model.token).FirstOrDefault();

                    if (oUser != null)
                    {
                        oUser.usu_Password = hash;
                        oUser.usu_Token = null;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ViewBag.Message = "Contraseña modificada con éxito";
            return View("Index");
        }

        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(string identidad, string nombre, string Apellido , DateTime FechaNacimiento, string sexo , string Telefono , string correo, string password)
        {
            string UserName = nombre + Apellido;

            IEnumerable<object> listCategoria = null;
            string MensajeError = "";

            try
            {
                listCategoria = db.UDP_Acce_tbUsuario_Insert(UserName, password);


                foreach (UDP_Acce_tbUsuario_Insert_Result Resultado in listCategoria)
                {
                    MensajeError = Resultado.MensajeError;
                }
                if (MensajeError.StartsWith("-1"))
                {
                    ViewBag.Error("Error al registrase, contacte al administrador.");
                    return View();
                }
                else
                {
                    tbCliente objCliente = new tbCliente();
                    objCliente.clte_Identidad = identidad;
                    objCliente.clte_Nombre = nombre;
                    objCliente.clte_Apellido = Apellido;
                    objCliente.clte_FechaNacimiento = FechaNacimiento;
                    objCliente.clte_Sexo = sexo;
                    objCliente.clte_Telefono = Telefono;
                    objCliente.clte_Correo = correo;
                    objCliente.usu_Id = Convert.ToInt16(MensajeError);
                    objCliente.clte_UsuarioCrea = 3;
                    objCliente.clte_FechaCrea = DateTime.Now;

                    db.tbCliente.Add(objCliente);
                    db.SaveChanges();

                    Usuario_Rol usuarioRol = new Usuario_Rol();
                    usuarioRol.id_Rol = 2;
                    usuarioRol.usu_Id = Convert.ToInt16(MensajeError);
                    db.Usuario_Rol.Add(usuarioRol);
                    db.SaveChanges();

                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = "Se produjo un error al registrar, contacte al administrador";
                return View();
            }
            ViewBag.Message = "Usuario registrado correctamente, inicie sesión";
            return RedirectToAction("Index");
        }

        //Respaldo Helpers
        //#region "HELPERS GENERALES"
        //private string GetSha256(string str)
        //{

        //    SHA256 sha256 = SHA256Managed.Create();
        //    ASCIIEncoding encoding = new ASCIIEncoding();
        //    byte[] stream = null;
        //    StringBuilder sb = new StringBuilder();
        //    stream = sha256.ComputeHash(encoding.GetBytes(str));
        //    for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
        //    return sb.ToString();
        //}

        //private void SendEmail(string EmailDestino, string token)
        //{
        //    string urlDomain = "http://localhost:52593/";
        //    string EmailOrigen = "willian1997.wd@gmail.com";
        //    string Contraseña = "honduras504720";
        //    string url = urlDomain + "/Login/Recovery/?token=" + token;
        //    MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperación de contraseña",
        //        "<p>Correo para recuperación de contraseña</p><br>" +
        //        "<a href='" + url + "'>Click para recuperar</a>");

        //    oMailMessage.IsBodyHtml = true;

        //    SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
        //    oSmtpClient.EnableSsl = true;
        //    oSmtpClient.UseDefaultCredentials = false;
        //    oSmtpClient.Port = 587;
        //    oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

        //    oSmtpClient.Send(oMailMessage);

        //    oSmtpClient.Dispose();
        //}

        //#endregion
    }
}