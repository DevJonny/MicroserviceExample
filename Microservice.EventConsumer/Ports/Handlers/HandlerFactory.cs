using System;
using System.Linq;
using System.Reflection;
using Microservice.API;
using Microservice.API.Adapters;
using RabbitMQ.Client;

namespace Microservice.EventConsumer.Ports.Handlers
{
    public static class HandlerFactory
    {
        public static void RegisterHandlers(Config config, TodoDatastoreService todoDataService, IModel channel)
        {
            var assembly = Assembly.GetEntryAssembly();
            var allConsumers = assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IAmAConsumer)));

            foreach (var consumer in allConsumers)
            {
                (Activator.CreateInstance(consumer.AsType(), config, todoDataService, channel) as IAmAConsumer).ConfigureReceiver();
            }
        }
    }
}