using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microservice.API.Model;
using Microservice.API.Ports;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Microservice.API.Adapters
{
    public class DatastoreService : IAmATodoRetriever
    {
        private readonly HttpClient _httpClient;
        private readonly Config _config;

        public DatastoreService(HttpClient client, IOptions<Config> config)
        {
            _httpClient = client;
            _config = config.Value;
            
            _httpClient.BaseAddress = new Uri(_config.BaseDatastoreUri);
        }

        public async Task<Todo> SelectTodoById(string id)
        {
            var uri = string.Format(_config.StoreSelectTodoById, id);
            var response = await _httpClient.GetStringAsync(uri);
            var todo = JsonConvert.DeserializeObject<Todo>(response);

            return todo;
        }
    }
}