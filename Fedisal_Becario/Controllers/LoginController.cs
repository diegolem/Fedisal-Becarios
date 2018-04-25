using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Logueo(Fedisal_Becario.Models.LoginViewModel logModel) {
            using (FedisalEntities1 ctx = new FedisalEntities1() ) {
                var query = (from log in ctx.Becario where logModel.Codigo == log.idBecario && logModel.Password == log.contrasenna select log).FirstOrDefault();
                if(query == null) {
                    logModel.LoginErrorMessage = "No se encontro el usuario solicitado";
                    return View("Index",logModel);
                }else {
                    Session["ID"] = query.idBecario;
                    return RedirectToAction("Index", "Becario");
                }
            }
        }
        public ActionResult LogOut() {
            HttpContext.Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
