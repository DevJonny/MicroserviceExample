using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microservice.API;
using Microservice.API.Adapters;
using Microservice.EventConsumer.Ports.Handlers;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace Microservice.EventConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            initRabbitConnection();
        }
        
        private static void initRabbitConnection()
        {
            var rabbitMqConnectionFactory = new ConnectionFactory { HostName = "microserviceexample_rabbitmq_1" };

            var config = buildConfig();
            var todoDataService = TodoDatastoreServiceFactory.Instance(config);
            
            try
            {
                using (var connection = rabbitMqConnectionFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    new CreateTodoCommandHandler(config, todoDataService, channel).ConfigureReceiver();

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

        private static Config buildConfig()
        {
            var config = new Config();

            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build()
                .GetSection("Uris")
                .Bind(config);
            return config;
        }
    }
}