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
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }
    }
}