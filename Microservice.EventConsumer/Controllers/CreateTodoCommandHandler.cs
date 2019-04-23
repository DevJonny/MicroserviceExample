using Microservice.API;
using Microservice.API.Adapters;
using Microservice.Core;
using Microservice.EventConsumer.Model;
using Microservice.EventConsumer.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.EventConsumer.Controllers
{
    public class CreateTodoCommandHandler
    {
        private readonly Config _config;
        private readonly ICRUDTodos _crudTodos;

        public CreateTodoCommandHandler(Config options)
        {
            _config = options;
            _crudTodos = TodoOperatorFactory.Instance(_config);
        }

        [HttpPost]
        public void Handle(CreateTodoCommand command)
        {
            var todo = new Todo
            {
                Id = command.Id
            };

            _crudTodos.Insert(todo);
        }
    }
}