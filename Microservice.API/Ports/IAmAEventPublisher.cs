using System.Threading.Tasks;
using Microservice.API.Model;

namespace Microservice.API.Ports
{
    public interface IAmAEventPublisher
    {
        Task<string> SendTodo(Todo todo);
    }
}