using Microservice.EventConsumer.Model;
using Microservice.EventConsumer.Ports;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Microservice.EventConsumer.Controllers
{
    public class CreateTodoCommandHandler : EventingBasicConsumer
    {
        private readonly IModel _channel;
        private readonly ICRUDTodos _todoDatastore;

        public CreateTodoCommandHandler(ICRUDTodos todoDatastore, IModel channel) 
            : base(channel)
        {
            _todoDatastore = todoDatastore;
            _channel = channel;
        }

        [HttpPost]
        public void Handle(CreateTodoCommand command)
        {
            var todo = new Todo
            {
                Id = command.Id
            };

            _todoDatastore.Insert(todo);
        }
    }
}