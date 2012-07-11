using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Todo.Models;

namespace Todo.Controllers
{
    using Todo = Models.Todo;

    public class TodosController : ApiController
    {
        private TodoDbContext db = new TodoDbContext();

        // GET api/Todos
        public IEnumerable<Todo> GetTodoes()
        {
            return db.Todos.AsEnumerable();
        }

        // GET api/Todos/5
        public Todo GetTodo(Guid id)
        {
            Todo todo = db.Todos.Find(id);
            if (todo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return todo;
        }

        // PUT api/Todos/5
        public HttpResponseMessage PutTodo(Guid id, Todo todo)
        {
            if (ModelState.IsValid && id == todo.Id)
            {
                db.Entry(todo).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, todo);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Todos
        public HttpResponseMessage PostTodo(Todo todo)
        {
            if (ModelState.IsValid)
            {
                db.Todos.Add(todo);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, todo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = todo.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Todos/5
        public HttpResponseMessage DeleteTodo(Guid id)
        {
            Todo todo = db.Todos.Find(id);
            if (todo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Todos.Remove(todo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, todo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}