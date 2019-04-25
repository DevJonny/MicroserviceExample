using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microservice.EventConsumer.Model;
using Microservice.EventConsumer.Ports;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Microservice.API.Adapters
{
    public class DatastoreService : ICRUDTodos
    {
        private readonly HttpClient _httpClient;
        private readonly Config _config;

        public DatastoreService(HttpClient httpClient, IOptions<Config> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
            
            _httpClient.BaseAddress = new Uri(config.Value.BaseDatastoreUri);
        }

        public async Task<string> InsertTodo(Todo todo)
        {
            var json = JsonConvert.SerializeObject(todo);
            var content = new StringContent(json, Encoding.Default, "application/json");
            var response = await _httpClient.PostAsync(_config.StoreInsertTodo, content);

            response.EnsureSuccessStatusCode();
            
            return response.Headers.Location.ToString();
        }
    }
}