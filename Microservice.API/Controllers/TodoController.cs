using System;
using System.Net;
using System.Threading.Tasks;
using Microservice.API.Adapters;
using Microservice.API.Model;
using Microservice.API.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IAmAEventPublisher _eventPublisher;
        private readonly IAmATodoRetriever _todoRetriever;

        public TodoController(
            IAmAEventPublisher eventPublisher,
            DatastoreService datastoreService)
        {
            _eventPublisher = eventPublisher;
            _todoRetriever = datastoreService;
        }
        
        [HttpPost]
        public ActionResult<Todo> Post([FromBody] Todo todo)
        {
            todo.Id = $"{Guid.NewGuid()}";
            
            var successfulPublish = _eventPublisher.SendTodo(todo);
            
            if (successfulPublish)
                return CreatedAtAction(nameof(Get), new {id = todo.Id}, todo);
            
            throw new WebException($"{todo.Id} was not sent successfully");
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> Get(string id)
        {
            return await _todoRetriever.SelectTodoById(id);
        }
    }
}