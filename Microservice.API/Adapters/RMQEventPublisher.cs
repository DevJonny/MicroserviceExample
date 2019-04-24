using System.Text;
using Microservice.API.Ports;
using Microservice.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Microservice.API.Adapters
{
    public class RMQEventPublisher : IAmAEventPublisher
    {
        private readonly ILogger _logger;
        private readonly ConnectionFactory _connectionFactory;

        private const string Todo_Queue = "todo";

        public RMQEventPublisher(ILogger logger)
        {
            _logger = logger;
            _connectionFactory = new ConnectionFactory {HostName = "localhost"};
        }
        
        public bool SendTodo(Todo todo)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(Todo_Queue, false, false, false, null);
                
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(todo));

                channel.BasicPublish("", Todo_Queue, null, body);
                
                _logger.LogInformation(" RMQEventPublisher Sent {0}", body);
            }

            return true;
        }
    }
}