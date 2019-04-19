using System.Net;
using System.Threading.Tasks;
using Microservice.Core;

namespace Microservice.API.Ports
{
    public interface IAmAEventPublisher
    {
        Task<bool> SendTodo(Todo todo);
    }
}