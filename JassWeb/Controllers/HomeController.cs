using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jassplan.JassServerModelManager;
using Jassplan.Model;
using WebMatrix.WebData;
using System.Web.Security;
using JassWeb.Filters;

namespace JassWeb.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {       
            if (!User.Identity.IsAuthenticated) return View();

            if (User.Identity.Name == "test") {
                var mm = new JassDataModelManager(WebSecurity.CurrentUserName);
                mm.ActivityDeleteAll();
            }
            return Redirect("/JassClientJS/index.html");            
        }
    }
}