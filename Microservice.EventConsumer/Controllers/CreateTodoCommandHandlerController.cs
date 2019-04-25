using System.Threading.Tasks;
using Microservice.API;
using Microservice.API.Adapters;
using Microservice.EventConsumer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Microservice.EventConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateTodoCommandHandlerController : ControllerBase
    {
        private readonly DatastoreService _datastoreService;
        private readonly Config _config;

        public CreateTodoCommandHandlerController(DatastoreService datastoreService, IOptions<Config> config)
        {
            _datastoreService = datastoreService;
            _config = config.Value;
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Post([FromBody] CreateTodoCommand command)
        {
            var todo = new Todo
            {
                Id = command.Id
            };

            var location = await _datastoreService.InsertTodo(todo);

            return Created(location, todo);
        }
    }
}