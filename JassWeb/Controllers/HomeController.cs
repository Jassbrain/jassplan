using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jassplan.JassServerModelManager;
using Jassplan.Model;

namespace JassWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string returnUrl)
        {
            JassModelManager mm = new JassModelManager();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}