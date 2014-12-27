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
            if (Request["test"]== "true")
            {
                if (WebSecurity.Login("test", "password", false))
                {
                    FormsAuthentication.SetAuthCookie("test", false);
                    return Redirect("/JassClientJS/tests/test_index_on_iframe.html");
                }
            }
            if (!User.Identity.IsAuthenticated) return View();
            else return Redirect("/JassClientJS/index.html");            
        }
    }
}