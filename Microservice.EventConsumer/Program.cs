using System;
using System.IO;
using System.Text;
using Microservice.API;
using Microservice.EventConsumer.Controllers;
using Microservice.EventConsumer.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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
            
            var factory = new ConnectionFactory { HostName = "localhost" };
            
            using(var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(Todo_Queue,false,false,false,null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);

                    var command = JsonConvert.DeserializeObject<CreateTodoCommand>(message);
                    
                    commandHandler.Handle(command);
                };
                
                channel.BasicConsume(Todo_Queue,true, consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}