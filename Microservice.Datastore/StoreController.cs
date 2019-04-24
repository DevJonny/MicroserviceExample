using Microservice.Datastore.Model;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Datastore
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Todo> InsertTodo([FromBody] Todo todo)
        {
            Store.Instance()[typeof(Todo)].Add(todo.Id, todo);
            return todo;
        }

        [HttpGet("todo/{id}")]
        public ActionResult<Todo> SelectOneTodo(string id)
        {
            return Store.Instance()[typeof(Todo)][id] as Todo;
        }
    }
}