using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microservice.API;
using Microservice.EventConsumer.Controllers;
using Microservice.EventConsumer.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Microservice.EventConsumer
{
    public class Program
    {
        private const string Todo_Queue = "todo";
        
        public static void Main(string[] args)
        {
            var config = new Config();
            
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build()
                .GetSection("Uris")
                .Bind(config);

            var commandHandler = new CreateTodoCommandHandler(config);
            
            var factory = new ConnectionFactory { HostName = "microserviceexample_rabbitmq_1" };

            var connected = false;
            
            initRabbitConnection();
            
            void initRabbitConnection()
            {
                try
                {
                    using (var connection = factory.CreateConnection())
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(Todo_Queue, false, false, false, null);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body);
                            Console.WriteLine(" [x] Received {0}", message);

                            var command = JsonConvert.DeserializeObject<CreateTodoCommand>(message);

                            commandHandler.Handle(command);
                        };

                        channel.BasicConsume(Todo_Queue, true, consumer);

                        Task.Run(() => Thread.Sleep(Timeout.Infinite)).Wait();
                    }
                }
                catch (BrokerUnreachableException e)
                {
                    Console.WriteLine("RMQ not up yet, delaying for 5000ms");
                    Task.Delay(5000).Wait();
                    initRabbitConnection();
                }
            }
        }
    }
}