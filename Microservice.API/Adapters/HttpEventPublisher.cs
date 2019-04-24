using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microservice.API.Ports;
using Microservice.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Microservice.API.Adapters
{
    public class HttpEventPublisher : IAmAEventPublisher
    {
        private readonly Config _config;
        private readonly HttpClient _httpClient;

        public HttpEventPublisher(IOptions<Config> options)
        {
            _config = options.Value;
            _httpClient = new HttpClient();
        }
        
        public async Task<bool> SendTodo(Todo todo)
        {
            var todoJson = JsonConvert.SerializeObject(todo);

            var response = await _httpClient.PostAsync(_config.TodoEventHandlerUri, new StringContent(todoJson, Encoding.Default, "application/json"));

            return response.IsSuccessStatusCode;
        }
    }
}