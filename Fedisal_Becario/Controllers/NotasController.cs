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
    public class NotasController : Controller
    {
        private FedisalEntities1 db = new FedisalEntities1();

        // GET: Notas
        public ActionResult Index()
        {
            String id = Session["ID"].ToString();
            var nota = db.Nota.Where(n => n.Ciclo.idBecario.Equals(id)).Include(n => n.Ciclo).OrderByDescending(n => n.Ciclo.nCiclo);
            return View(nota.ToList());
        }


        // GET: Notas/Create
        public ActionResult Create()
        {
            String id = Session["ID"].ToString();
            ViewBag.idCiclo = new SelectList(db.Ciclo.Where(x=>x.idBecario.Equals(id) && x.evidenciaNotas == false), "idCiclo", "nCiclo");
            return View();
        }

        // POST: Notas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idNota,nombreMateria,nota1,cumplioTercio,idCiclo")] Nota nota)
        {
            var query = (from t in db.Nota where nota.nombreMateria == t.nombreMateria && nota.idCiclo == t.idCiclo select t).Count();
            var cuenta = (from t in db.Nota where nota.nombreMateria == t.nombreMateria select t).Count();
            if (query > 0)
            {
                ModelState.AddModelError("nombreMateria", "La nota ingresada ya existe");
            }
            if (cuenta > 0)
            {
                ModelState.AddModelError("nombreMateria", "La materia ingresada ya existe");
            }
            String id = Session["ID"].ToString();
            if (ModelState.IsValid)
            {
                db.Nota.Add(nota);
                db.SaveChanges();
                TempData["RegistroN"] = "La nota se ingreso correctamente";
                return RedirectToAction("Index");
            }
            ViewBag.idCiclo = new SelectList(db.Ciclo.Where(x => x.idBecario.Equals(id) && x.evidenciaNotas == false), "idCiclo", "nCiclo", nota.idCiclo);
            return View(nota);
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
