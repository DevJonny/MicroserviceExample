using System;
using System.Threading.Tasks;
using Microservice.API;
using Microservice.API.Adapters;
using Microservice.Core;
using Microservice.Datastore;
using Microservice.EventConsumer.Model;
using Microservice.EventConsumer.Ports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microservice.EventConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateTodoCommandHandlerController : ControllerBase
    {
        private readonly Config _config;
        private readonly ILogger _logger;
        private readonly ICRUDTodos _crudTodos;

        public CreateTodoCommandHandlerController(IOptions<Config> options, ILogger<CreateTodoCommandHandlerController> logger)
        {
            _config = options.Value;
            _logger = logger;
            _crudTodos = TodoOperatorFactory.Instance(_config);
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Post([FromBody] CreateTodoCommand command)
        {
            var todo = new Todo
            {
                Id = command.Id
            };

            _crudTodos.Insert(todo);
            
            return Created(string.Format(_config.StoreSelectTodoById, command.Id), todo);
        }
    }
}