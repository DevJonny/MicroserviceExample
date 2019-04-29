namespace Microservice.API
{
    public class Uris
    {
        public string BaseDatastoreUri { get; set; }
        public string StoreSelectTodoById { get; set; }
    }

    public class Queues
    {
        public string CreateTodoCommandQueue { get; set; }   
    }
}