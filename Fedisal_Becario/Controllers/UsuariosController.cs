using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fedisal_Becario.Models;

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
            if (ModelState.IsValid)
            {
                db.Entry(informacionPersonal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(informacionPersonal);
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
