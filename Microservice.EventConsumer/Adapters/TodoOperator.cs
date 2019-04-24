using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microservice.EventConsumer.Model;
using Microservice.EventConsumer.Ports;
using Newtonsoft.Json;

namespace Microservice.API.Adapters
{
    public class TodoOperator : ICRUDTodos
    {
        private readonly Config _config;
        private readonly HttpClient _httpClient;

        public TodoOperator(Config config)
        {
            _config = config;
            _httpClient = new HttpClient();
        }

        public async Task<bool> Insert(Todo todo)
        {
            var todoJson = JsonConvert.SerializeObject(todo);
            var response = await _httpClient.PostAsync(_config.StoreInsertTodo, new StringContent(todoJson, Encoding.Default, "application/json"));

            return response.IsSuccessStatusCode;
        }
    }

    public static class TodoOperatorFactory
    {
        private static TodoOperator _todoOperator;

        public static TodoOperator Instance(Config config)
        {
            if (_todoOperator is null)
                return _todoOperator = new TodoOperator(config);

            return _todoOperator;
        }
    }
}