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
    public class CicloController : Controller
    {
        private FedisalEntities1 db = new FedisalEntities1();

        // GET: Ciclo
        public ActionResult Index()
        {
            String id = Session["ID"].ToString();
            var ciclo = db.Ciclo.Where(c => c.idBecario.Equals(id)).Include(c => c.Becario).OrderByDescending(c=> c.nCiclo);
            return View(ciclo.ToList());
        }

        // GET: Ciclo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ciclo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "anio,nCiclo")] Ciclo ciclo)
        {
            var query = (from t in db.Ciclo where ciclo.anio == t.anio && ciclo.nCiclo == t.nCiclo select t).Count();
            if(ciclo.anio > DateTime.Now.Year || ciclo.anio < DateTime.Now.Year){
                ModelState.AddModelError("anio","Debe ingresar un año válido");
            }
            if(query > 0) {
                ModelState.AddModelError("nCiclo","El ciclo que ha ingresado ya existe");
            }
            if (ModelState.IsValid)
            {
                String id = Session["ID"].ToString();
                ciclo.idBecario = id;
                ciclo.evidenciaNotas = false;
                db.Ciclo.Add(ciclo);
                db.SaveChanges();
                TempData["RegistroC"] = "El ciclo se ingreso correctamente";
                return RedirectToAction("Index");
            }
            ViewBag.idBecario = new SelectList(db.Becario, "idBecario", "idPrograma", ciclo.idBecario);
            return View(ciclo);
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
