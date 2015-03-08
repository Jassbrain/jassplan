using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Jassplan.JassServerModelManager;
using Jassplan.Model;
using WebMatrix.WebData;
using System.Web.Security;
using System.Xml.XPath;
using JassWeb.Filters;
using System.IO;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;

namespace JassWeb.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {       
            if (!User.Identity.IsAuthenticated) return View();
            return Redirect("/JassClientJS/index.html?user=" + User.Identity.Name);            
        }

        public ActionResult Logs()
        {
              string path = HttpContext.Server.MapPath("~/Logs/");

              var logFiles = System.IO.Directory.EnumerateFiles(path);

              if (logFiles.Count() > 0)
              {
                  var logPath = logFiles.First().ToString();
                  ViewBag.logs = System.IO.File.ReadAllText(logPath);
              }
            return View();
        }

        public ActionResult RemoveLogs()
        {
            string path = HttpContext.Server.MapPath("~/Logs/");
            var logFiles = System.IO.Directory.EnumerateFiles(path);


            foreach (var file in logFiles) {
                var logPath = file.ToString();
                System.IO.File.Delete(logPath);
            }
            return View("Logs");
        }

        public string Main()
        {
            string result = "";

            CalendarService calendarConnection;

            ClientSecrets secrets = new ClientSecrets
            {
                ClientId = "473177441662-h57ba7mlrtkcgkb15ivd4srfjb4fdps8.apps.googleusercontent.com",
                ClientSecret = "thRD95BupH7H1UZaqoZUHFk3",
            };

            try
            {
                result = "we starting ";
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secrets,
                    new string[]
                    {
                        CalendarService.Scope.Calendar
                    },
                    "user",
                    CancellationToken.None)
                    .Result;

                result = "we pass 1 thing ";

                var initializer = new BaseClientService.Initializer();
                initializer.HttpClientInitializer = credential;
                initializer.ApplicationName = "Jassplan";
                calendarConnection = new CalendarService(initializer);

                result = "we are here after calendar connection";
            }
            catch (Exception ex)
            {
                result = result +  ex.Message;
            }

            return result;
        }
    }
}