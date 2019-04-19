using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Microservice.API.Ports;
using Microservice.Core;
using Newtonsoft.Json;

namespace Microservice.API.Adapters
{
    public class TodoRetriever : IAmATodoRetriever
    {
        private readonly Config _config;
        private readonly HttpClient _httpClient;

        public TodoRetriever(Config config)
        {
            _config = config;
            _httpClient = new HttpClient();
        }

        public async Task<Todo> ById(string id)
        {
            var todoJson = await _httpClient.GetStringAsync(string.Format(_config.StoreSelectTodoById, id));

            return JsonConvert.DeserializeObject<Todo>(todoJson);
        }
    }

    public static class TodoRetrieverFactory
    {
        private static TodoRetriever _todoRetriever;

        public static TodoRetriever Instance(Config config)
        {
            if (_todoRetriever is null)
                return _todoRetriever = new TodoRetriever(config);

            return _todoRetriever;
        }
    }
}