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
    public class TodoListController : ApiController
    {
       
        private JassModelManager mm = new JassModelManager();

        // GET api/TodoList - DONE
        public List<JassActivity> GetTodoList()
        {
            return mm.ActivitiesGetAll();
        }

        // GET api/TodoList - DONE
        public List<JassActivity> GetArchiveTodoList()
        {
            return mm.ActivitiesArchiveGetAll();
        }

        // GET api/TodoList/5 - DONE
        public JassActivity GetTodoList(int id)
        {
            JassActivity todoList = mm.ActivityGetById(id);
            if (todoList == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return todoList;
        }

        // PUT api/TodoList/5 - DONE
       // [ValidateHttpAntiForgeryToken]
        public JassActivity PutTodoList(JassActivity todoList)
        {
                mm.ActivitySave(todoList);
            return todoList;
        }

        // POST api/TodoList - DONE
        //[ValidateHttpAntiForgeryToken]
        public JassActivity PostTodoList(JassActivity todoList)
        {
            mm.ActivityCreate(todoList);
            return todoList;
        }

        // DELETE api/TodoList/5
        //[ValidateHttpAntiForgeryToken]
        public HttpResponseMessage DeleteTodoList(int id)
        {
            try
            {
                mm.ActivityDelete(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        protected override void Dispose(bool disposing)
        {
            mm.Dispose();
        }
    }
}