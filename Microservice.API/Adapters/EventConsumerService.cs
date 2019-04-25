using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microservice.API.Model;
using Microservice.API.Ports;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Microservice.API.Adapters
{
    public class EventConsumerService : IAmAEventPublisher
    {
        private readonly HttpClient _httpClient;
        private readonly Config _config;

        public EventConsumerService(HttpClient client, IOptions<Config> config)
        {
            _httpClient = client;
            _config = config.Value;
            
            _httpClient.BaseAddress = new Uri(_config.BaseEventConsumerUri);
        }

        public async Task<string> SendTodo(Todo todo)
        {
            var json = JsonConvert.SerializeObject(todo);
            var content = new StringContent(json, Encoding.Default, "application/json");
            
            var response = await _httpClient.PostAsync(_config.TodoEventHandlerUri, content);

            response.EnsureSuccessStatusCode();

            return response.Headers.Location.ToString();
        }
    }
}