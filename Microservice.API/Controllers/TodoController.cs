using System;
using System.Threading.Tasks;
using Microservice.API.Adapters;
using Microservice.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly DatastoreService _datastoreService;
        private readonly EventConsumerService _eventConsumerService;


        public TodoController(DatastoreService datastoreService, EventConsumerService eventConsumerService)
        {
            _datastoreService = datastoreService;
            _eventConsumerService = eventConsumerService;
        }
        
        [HttpPost]
        public async Task<ActionResult<Todo>> Post([FromBody] Todo todo)
        {
            todo.Id = $"{Guid.NewGuid()}";
            
            await _eventConsumerService.SendTodo(todo);
            
            return CreatedAtAction(nameof(Get), new {id = todo.Id}, todo);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> Get(string id)
        {
            var todo = await _datastoreService.SelectTodoById(id);

            return Ok(todo);
        }
    }
}