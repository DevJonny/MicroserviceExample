using Microservice.API.Adapters;

namespace Microservice.EventConsumer.Ports
{
    public interface ISetupTodoDatastoreService
    {
        void AddTodoDatastoreService(TodoDatastoreService todoDatastoreService);
    }
}