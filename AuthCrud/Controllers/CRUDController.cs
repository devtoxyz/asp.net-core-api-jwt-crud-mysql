using AuthCrud.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthCrud.Controllers
{
    public class CreateTodoParameter
    {
        public required string title { get; set; }
        public required string description { get; set; }
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
            Todo[] todos = context.Todos.ToArray();
            return Ok();
        }

        // GET api/<CRUDController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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

        // PUT api/<CRUDController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CRUDController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
