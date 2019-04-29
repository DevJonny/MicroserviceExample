using System;
using System.Text;
using Microservice.API;
using Microservice.EventConsumer.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Microservice.EventConsumer.Ports.Handlers
{
    public class CreateTodoCommandHandler : EventingBasicConsumer, IAmAConsumer
    {
        private readonly Config _config;
        private readonly IModel _channel;
        private readonly ICRUDTodos _todoDatastore;

        public CreateTodoCommandHandler(Config config, ICRUDTodos todoDatastore, IModel channel) 
            : base(channel)
        {
            _config = config;
            _todoDatastore = todoDatastore;
            _channel = channel;
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