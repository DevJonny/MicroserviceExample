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
        private readonly Uris _uris;

        public DatastoreService(HttpClient client, IOptions<Uris> uris)
        {
            _httpClient = client;
            _uris = uris.Value;
            
            _httpClient.BaseAddress = new Uri(_uris.BaseDatastoreUri);
        }

        public async Task<Todo> SelectTodoById(string id)
        {
            var uri = string.Format(_uris.StoreSelectTodoById, id);
            var response = await _httpClient.GetStringAsync(uri);
            var todo = JsonConvert.DeserializeObject<Todo>(response);

            return todo;
        }
    }
}