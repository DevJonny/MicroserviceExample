using System.Text;
using Microservice.API.Model;
using Microservice.API.Ports;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Microservice.API.Adapters
{
    public class RMQEventPublisher : IAmAEventPublisher
    {
        private readonly Queues _queues;
        private readonly ILogger<RMQEventPublisher> _logger;
        private readonly ConnectionFactory _connectionFactory;

        public RMQEventPublisher(IOptions<Queues> queues, ILogger<RMQEventPublisher> logger)
        {
            _queues = queues.Value;
            _logger = logger;
            _connectionFactory = new ConnectionFactory { HostName = "microserviceexample_rabbitmq_1" };
        }
        
        public bool SendTodo(Todo todo)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(_queues.CreateTodoCommandQueue, false, false, false, null);
                
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(todo));

                channel.BasicPublish("", _queues.CreateTodoCommandQueue, null, body);
                
                _logger.LogInformation(" RMQEventPublisher Sent {0}", body);
            }

            return true;
        }
    }
}