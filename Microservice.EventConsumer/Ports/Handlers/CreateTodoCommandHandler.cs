using System;
using System.Text;
using Microservice.API;
using Microservice.API.Adapters;
using Microservice.EventConsumer.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Microservice.EventConsumer.Ports.Handlers
{
    public class CreateTodoCommandHandler : EventingBasicConsumer, IAmAConsumer, ISetupTodoDatastoreService
    {
        private readonly Config _config;
        private readonly IModel _channel;
        private ICRUDTodos _todoDatastore;

        public CreateTodoCommandHandler(Config config, IModel channel) : base(channel)
        {
            _config = config;
            _channel = channel;
        }

        public CreateTodoCommandHandler(Config config, ICRUDTodos todoDatastore, IModel channel) 
            : base(channel)
        {
            _config = config;
            _todoDatastore = todoDatastore;
            _channel = channel;
        }

        public void AddTodoDatastoreService(TodoDatastoreService todoDatastoreService)
        {
            _todoDatastore = todoDatastoreService;
        }

        public void ConfigureReceiver()
        {
            _channel.QueueDeclare(_config.CreateTodoCommandQueue, false, false, false, null);
            
            Received += (channel, rmqEvent) =>
            {
                var body = rmqEvent.Body;
                var message = Encoding.UTF8.GetString(body);
                
                Console.WriteLine($" [x] Received {message}");

                var command = JsonConvert.DeserializeObject<CreateTodoCommand>(message);
                
                var todo = new Todo
                {
                    Id = command.Id
                };

                _todoDatastore.Insert(todo);
                
            };

            _channel.BasicConsume(_config.CreateTodoCommandQueue, true, this);
        }
    }
}