using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Fedisal_Becario.Models;

namespace Fedisal_Becario.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logueo(Fedisal_Becario.Models.LoginViewModel logModel)
        {
            using (FedisalEntities1 ctx = new FedisalEntities1())
            {
                var query = (from log in ctx.Becario where logModel.Codigo == log.idBecario && logModel.Password == log.contrasenna select log).FirstOrDefault();
                if (query == null)
                {
                    logModel.LoginErrorMessage = "No se encontro el usuario solicitado";
                    return View("Index", logModel);
                }
                else
                {
                    Session["ID"] = query.idBecario;
                    Session["IDI"] = query.idInformacion;
                    return RedirectToAction("Index", "Becario");
                }
            }
        }
        public ActionResult LogOut()
        {
            HttpContext.Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult forgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(Fedisal_Becario.Models.ForgotPasswordViewModel forgotModel)
        {
            using (FedisalEntities1 ctx = new FedisalEntities1())
            {
                try
                {
                    var userInfo = ctx.InformacionPersonal.Where(d => d.correoElectronico == forgotModel.Email).First();
                    bool verificar;
                    if (userInfo.correoElectronico == forgotModel.Email)
                    {
                        var becario = ctx.Becario.Where(d => d.idInformacion == userInfo.idInformacion).First();
                        //Bloque de enviar correo
                        bool response = true;
                        MailMessage mensaje = new MailMessage();
                        mensaje.From = new MailAddress("");//parametros
                        mensaje.Subject = "FEDISAL - Becario";
                        mensaje.IsBodyHtml = true;
                        mensaje.Body = "";
                        mensaje.Body += "<h3>Recuperación de Contraseña</h3><br>";
                        mensaje.Body += "<b>Su contraseña es: </b>" + becario.contrasenna;
                        mensaje.BodyEncoding = Encoding.UTF8;
                        mensaje.To.Add(userInfo.correoElectronico);
                        //Fin de logica de armado de mensaje
                        //Configuracion SMTPT
                        SmtpClient clienteSMTP = new SmtpClient();
                        clienteSMTP.Credentials = new NetworkCredential("", "");//parametros
                        clienteSMTP.Port = 587;
                        clienteSMTP.Host = "smtp.gmail.com";
                        clienteSMTP.EnableSsl = true;
                        try
                        {
                            clienteSMTP.Send(mensaje);
                            verificar = response;
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                            response = false;
                            verificar = response;
                        }
                    }
                    else
                    {
                        return RedirectToAction("forgetPassword");
                    }
                    //Fin del bloque
                    if (verificar)
                    {
                        TempData["contrasennaE"] = "La contraseña fue enviada a su correo";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["contrasennaF"] = "Los correos no coinciden con ninguna cuenta";
                        return RedirectToAction("forgetPassword");
                    }
                }
                catch (Exception e)
                {
                    TempData["contrasennaF"] = "Ocurrio un error , revise sus credenciales";
                    return RedirectToAction("forgetPassword");
                }
            }
        }
    }
}
