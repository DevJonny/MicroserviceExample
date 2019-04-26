using System.Threading.Tasks;
using Microservice.API.Model;

namespace Microservice.API.Ports
{
    public interface IAmATodoRetriever
    {
        Task<Todo> SelectTodoById(string id);
    }
}