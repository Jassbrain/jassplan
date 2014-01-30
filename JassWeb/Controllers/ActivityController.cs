using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JassWeb.Filters;
using JassWeb.Models;
using Jassplan.Model;
using Jassplan.JassServerModelManager;

namespace JassWeb.Controllers
{
    [Authorize]
    public class JassActivityController : ApiController
    {
        private JassModelManager mm = new JassModelManager();


        public List<JassActivity> GetJassActivities()
        {
            return mm.ActivitiesGetAll();
        }

        // GET api/JassActivity/5
        public JassActivity GetJassActivity(int id)
        {
            JassActivity todoList = mm.ActivityGetById(id);
            if (todoList == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return todoList;
        }

        // PUT api/JassActivity/5
        [ValidateHttpAntiForgeryToken]
        public HttpResponseMessage PutJassActivity(JassActivity todoList)
        {
            try
            {
                mm.ActivitySave(todoList);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/JassActivity
        [ValidateHttpAntiForgeryToken]
        public HttpResponseMessage PostJassActivity(JassActivity todoList)
        {
            mm.ActivityCreate(todoList);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, todoList);
            return response;
        }

        // DELETE api/JassActivity/5
        [ValidateHttpAntiForgeryToken]
        public HttpResponseMessage DeleteJassActivity(int id)
        {
            mm.ActivityDelete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            mm.Dispose();
            base.Dispose(disposing);
        }
    }
}