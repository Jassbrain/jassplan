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
        private TodoItemContext db = new TodoItemContext();
        private JassModelManager mm = new JassModelManager();

        // GET api/TodoList - DONE
        public List<JassActivity> GetTodoLists()
        {
            return mm.ActivitiesGetAll();
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
        [ValidateHttpAntiForgeryToken]
        public HttpResponseMessage PutTodoList(JassActivity todoList)
        {
            try
            {
                mm.ActivitySave(todoList);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/TodoList - DONE
        [ValidateHttpAntiForgeryToken]
        public HttpResponseMessage PostTodoList(JassActivity todoList)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            mm.ActivityCreate(todoList);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, todoList);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = todoList.JassActivityID }));
            return response;
        }

        // DELETE api/TodoList/5
        [ValidateHttpAntiForgeryToken]
        public HttpResponseMessage DeleteTodoList(int id)
        {
            TodoList todoList = db.TodoLists.Find(id);
            if (todoList == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (db.Entry(todoList).Entity.UserId != User.Identity.Name)
            {
                // Trying to delete a record that does not belong to the user
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            TodoListDto todoListDto = new TodoListDto(todoList);
            db.TodoLists.Remove(todoList);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.OK, todoListDto);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}