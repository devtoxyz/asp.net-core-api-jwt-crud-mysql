using AuthCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthCrud.Controllers
{
    public class CreateTodoParameter
    {
        public required string title { get; set; }
        public required string description { get; set; }
    }

    public class UpdateTodoParameter
    {
        public string title { get; set; }
        public string description { get; set; }
    }

    [Route("api/todo")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        ModelContext context = new ModelContext();

        // GET: api/todo
        [HttpGet]
        public IActionResult Get()
        {
            Todo[] todos = context.Todos.Where(p => p.id >= 1).ToArray();
            return Ok(todos);
        }

        // GET api/todo/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Todo todo = context.Todos.Where(p => p.id == id).FirstOrDefault()!;
            if (todo == null)
            {
                return NotFound(new { message = "Cannot Find Item" });
            }
            return Ok(todo);
        }

        // POST api/todo
        [HttpPost]
        public IActionResult Post(CreateTodoParameter parameter)
        {
            Todo todo = new Todo { title = parameter.title, description = parameter.description };
            context.Todos.Add(todo);
            context.SaveChanges();

            return Ok(new { message = "Create Todo Successfully." });
        }

        // PUT api/todo/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateTodoParameter parameter)
        {
            Todo todo = context.Todos.Where(p => p.id == id).FirstOrDefault()!;
            if (todo == null)
            {
                return NotFound(new { message = "Cannot Find Item" });
            }

            if (parameter.title != null)
            {
                todo.title = parameter.title;
            }
            if (parameter.description != null)
            {
                todo.description = parameter.description;
            }
            context.Todos.Update(todo);
            context.SaveChanges();

            return Ok(new { message = "Update Successfully." });
        }

        // DELETE api/todo/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Todo todo = context.Todos.Where(p => p.id == id).FirstOrDefault()!;
            if (todo == null)
            {
                return NotFound(new { message = "Cannot Find Item" });
            }
            context.Todos.Where(p => p.id == id).ExecuteDelete();

            return Ok(new { message = "Delete Successfully." });
        }
    }
}
