using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microservice.EventConsumer.Model;
using Microservice.EventConsumer.Ports;
using Newtonsoft.Json;

namespace Microservice.API.Adapters
{
    public class TodoDatastoreService : ICRUDTodos
    {
        private readonly Config _config;
        private readonly HttpClient _httpClient;

        public TodoDatastoreService(Config config)
        {
            _config = config;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_config.BaseDatastoreUri);
        }

        public async Task<bool> Insert(Todo todo)
        {
            var todoJson = JsonConvert.SerializeObject(todo);
            var response = await _httpClient.PostAsync(_config.StoreInsertTodo, new StringContent(todoJson, Encoding.Default, "application/json"));

            return response.IsSuccessStatusCode;
        }
    }

    public static class TodoDatastoreServiceFactory
    {
        private static TodoDatastoreService _todoDatastoreService;

        public static TodoDatastoreService Instance(Config config)
        {
            if (_todoDatastoreService is null)
                return _todoDatastoreService = new TodoDatastoreService(config);

            return _todoDatastoreService;
        }
    }
}