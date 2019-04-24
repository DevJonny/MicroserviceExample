using System.Net.Http;
using System.Threading.Tasks;
using Microservice.API.Model;
using Microservice.API.Ports;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Microservice.API.Adapters
{
    public class TodoRetriever : IAmATodoRetriever
    {
        private readonly Config _config;
        private readonly HttpClient _httpClient;

        public TodoRetriever(IOptions<Config> options)
        {
            _config = options.Value;
            _httpClient = new HttpClient();
        }

        public async Task<Todo> ById(string id)
        {
            var todoJson = await _httpClient.GetStringAsync(string.Format(_config.StoreSelectTodoById, id));

            return JsonConvert.DeserializeObject<Todo>(todoJson);
        }
    }
}