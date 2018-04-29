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
    public class DesembolsoController : Controller
    {
        private FedisalEntities1 db = new FedisalEntities1();

        // GET: Desembolso
        public ActionResult Index()
        {
            String id = Session["ID"].ToString();
            var desembolso = db.Desembolso.Where(d=> d.Ciclo.idBecario == id).Include(d => d.Ciclo).Include(d => d.TipoDesembolso);
            return View(desembolso.ToList());
        }
        public decimal gastosLibros()
        {
            String id = Session["ID"].ToString();
            var idTipoDesembolso = db.TipoDesembolso.Where(d => d.nombre == "Libro").First();
            var gastosLibros = db.Desembolso.Where(d => d.Ciclo.idBecario == id && d.idTipoDesembolso == idTipoDesembolso.idTipoDesembolso).Sum(d => d.monto);
            if (gastosLibros >0)
            {
                return Convert.ToDecimal(gastosLibros);
            }else
            {
                return 0;
            }
        }
        public decimal gastosColegiatura()
        {
            String id = Session["ID"].ToString();
            var idTipoDesembolso = db.TipoDesembolso.Where(d => d.nombre == "Colegitura").First();
            var gastosColegiatura = db.Desembolso.Where(d => d.Ciclo.idBecario == id && d.idTipoDesembolso == idTipoDesembolso.idTipoDesembolso).Sum(d => d.monto);
            if (gastosColegiatura > 0)
            {
                return Convert.ToDecimal(gastosColegiatura);
            }
            else
            {
                return 0;
            }
        }
        public decimal gastosManutencion()
        {
            String id = Session["ID"].ToString();
            var idTipoDesembolso = db.TipoDesembolso.Where(d => d.nombre == "Manutención").First();
            var gastosManutencion = db.Desembolso.Where(d => d.Ciclo.idBecario == id && d.idTipoDesembolso == idTipoDesembolso.idTipoDesembolso).Sum(d => d.monto);
            if (gastosManutencion > 0)
            {
                return Convert.ToDecimal(gastosManutencion);
            }
            else
            {
                return 0;
            }
        }
        public decimal gastosMatricula()
        {
            String id = Session["ID"].ToString();
            var idTipoDesembolso = db.TipoDesembolso.Where(d => d.nombre == "Matricula").First();
            var gastosMatricula = db.Desembolso.Where(d => d.Ciclo.idBecario == id && d.idTipoDesembolso == idTipoDesembolso.idTipoDesembolso).Sum(d => d.monto);
            if (gastosMatricula > 0)
            {
                return Convert.ToDecimal(gastosMatricula);
            }
            else
            {
                return 0;
            }
        }
        public decimal gastosOtros()
        {
            String id = Session["ID"].ToString();
            var idTipoDesembolso = db.TipoDesembolso.Where(d => d.nombre == "Otros").First();
            var gastosOtros = db.Desembolso.Where(d => d.Ciclo.idBecario == id && d.idTipoDesembolso == idTipoDesembolso.idTipoDesembolso).Sum(d => d.monto);
            if (gastosOtros > 0)
            {
                return Convert.ToDecimal(gastosOtros);
            }
            else
            {
                return 0;
            }
        }
        public decimal gastosGraduacion()
        {
            String id = Session["ID"].ToString();
            var idTipoDesembolso = db.TipoDesembolso.Where(d => d.nombre == "Trabajo de Graduación").First();
            var gastosGraduacion = db.Desembolso.Where(d => d.Ciclo.idBecario == id && d.idTipoDesembolso == idTipoDesembolso.idTipoDesembolso).Sum(d => d.monto);
            if (gastosGraduacion > 0)
            {
                return Convert.ToDecimal(gastosGraduacion);
            }
            else
            {
                return 0;
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
