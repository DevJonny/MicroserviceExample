namespace Microservice.API
{
    public class Config
    {
        public string BaseEventConsumerUri { get; set; }
        public string BaseDatastoreUri { get; set; }
        public string TodoEventHandlerUri { get; set; }
        public string StoreSelectTodoById { get; set; }
    }
}