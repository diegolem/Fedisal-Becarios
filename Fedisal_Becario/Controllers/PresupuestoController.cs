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
    public class PresupuestoController : Controller
    {
        private FedisalEntities1 db = new FedisalEntities1();

        // GET: Presupuesto
        public ActionResult Index()
        {
            String id = Session["ID"].ToString();
            var presupuestoBecas = db.PresupuestoBecas.Where(p=>p.idBecario == id).Include(p => p.Becario);
            return View(presupuestoBecas.ToList());
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
