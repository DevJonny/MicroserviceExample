namespace Microservice.API
{
    public class Config
    {
        public string CreateTodoCommandQueue { get; set; }
        public string BaseDatastoreUri { get; set; }
        public string StoreInsertTodo { get; set; }
        public string StoreSelectTodoById { get; set; }
    }
}