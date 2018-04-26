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
        public decimal presupuestoEjecutado()
        {
            String id = Session["ID"].ToString();
            var cicloId = db.Ciclo.Where(p=>p.idBecario == id);
            foreach (var item in cicloId)
            {
                int codigoCiclo = item.idCiclo;
                var desembolsos = db.Desembolso.Where(t => t.idCiclo == codigoCiclo).Sum(t=> t.monto);
                if (desembolsos.Value > 0)
                {
                    return Convert.ToDecimal(desembolsos);
                }
            }
            return 0;
        }
        public decimal presupuestoPorEjecutar(decimal suma)
        {
            decimal resultado = 0;
            String id = Session["ID"].ToString();
            var cicloId = db.Ciclo.Where(p => p.idBecario == id);
            foreach (var item in cicloId)
            {
                int codigoCiclo = item.idCiclo;
                var desembolsos = db.Desembolso.Where(t => t.idCiclo == codigoCiclo).Sum(t => t.monto);
                if (desembolsos.Value > 0)
                {
                    resultado = suma - Convert.ToDecimal(desembolsos);
                    return resultado;
                }
            }
            return 0;
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
