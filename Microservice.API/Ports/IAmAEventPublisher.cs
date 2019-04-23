using Microservice.Core;

namespace Microservice.API.Ports
{
    public interface IAmAEventPublisher
    {
        bool SendTodo(Todo todo);
    }
}