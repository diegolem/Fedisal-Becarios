using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fedisal_Becario.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;


namespace Fedisal_Becario.Controllers
{
    public class UsuariosController : Controller
    {
        private FedisalEntities1 db = new FedisalEntities1();
        // GET: Usuarios
        public ActionResult Index()
        {
            string id = Session["IDI"].ToString();
            var informacion = db.InformacionPersonal.Where(d=> d.idInformacion.ToString() == id);
            return View(informacion.ToList());
        }
        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformacionPersonal informacionPersonal = db.InformacionPersonal.Find(id);
            if (informacionPersonal == null)
            {
                return HttpNotFound();
            }
            return View(informacionPersonal);
        }
        public ActionResult Change()
        {
           return View();
        }
        public string getName()
        {
            String id = Session["ID"].ToString();
            var nombreUser = db.Becario.Where(d => d.idBecario == id).Select(d=> d.InformacionPersonal.nombres);
            return Convert.ToString(nombreUser);
        }
        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idInformacion,nombres,apellidos,dui,fechaNacimiento,direccionResidencia,telefono,correoElectronico")] InformacionPersonal informacionPersonal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(informacionPersonal).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["CambiarD"] = "El usuario se modifico correctamente";
                    return RedirectToAction("Index");
                }
                return View(informacionPersonal);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(Fedisal_Becario.Models.ResetPasswordViewModel cambiarPass)
        {
            bool verificar;
            cambiarPass.Code = "";
            string id = Session["IDI"].ToString();
            var correo = db.InformacionPersonal.Where(d => d.idInformacion.ToString() == id).First();
            if (cambiarPass.Password == cambiarPass.ConfirmPassword)
            {
                string idB = Session["ID"].ToString();
                var becario = db.Becario.Where(d => d.idBecario.ToString() == idB).First();
                becario.contrasenna = cambiarPass.ConfirmPassword;
                db.SaveChanges();
                //Bloque de enviar correo
                bool response = true;
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("");//parametros
                mensaje.Subject = "FEDISAL - Becario";
                mensaje.IsBodyHtml = true;
                mensaje.Body = "";
                mensaje.Body += "<b>La contraseña nueva es: </b>" + cambiarPass.ConfirmPassword;
                mensaje.BodyEncoding = Encoding.UTF8;
                mensaje.To.Add(correo.correoElectronico);
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
            }else
            {
                TempData["ErrorC"] = "Verifique si las contraseñas son iguales";
                return RedirectToAction("Change");
            }
            //Fin del bloque
            if (verificar)
            {
                TempData["cambiarC"] = "La contraseña nueva fue enviada a su correo";
                return RedirectToAction("Index");
            }else
            {
                TempData["ErrorC"] = "Verifique si las contraseñas son iguales";
                return RedirectToAction("Change");
            }
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
