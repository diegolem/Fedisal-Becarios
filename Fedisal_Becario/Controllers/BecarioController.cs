using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fedisal_Becario.Models;

namespace Fedisal_Becario.Controllers
{
    public class BecarioController : Controller
    {
        // GET: Becario
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create() {
            return View();
        }
        public ActionResult RegistrarNota() {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult RegistroNotas(Nota modelNota,Ciclo modelCiclo) {
        //    string id = Session["ID"].ToString();
        //    using (FedisalEntities1 ctx = new FedisalEntities1()) {
        //        if(ModelState.IsValid) {
        //            ctx.Ciclo.Add(modelCiclo);
        //            ctx.SaveChanges();
        //            var idCiclo = ctx.Ciclo.OrderByDescending(x => x.idCiclo).First().idCiclo;
        //            modelNota.idCiclo = idCiclo;
        //            ctx.Nota.Add(modelNota);
        //            return RedirectToAction("Index");
        //        }else {
        //            return RedirectToAction("RegistroNotas");
        //        }
        //    }
        //}
    }
}