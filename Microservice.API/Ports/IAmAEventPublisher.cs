using Microservice.API.Model;

namespace Microservice.API.Ports
{
    public interface IAmAEventPublisher
    {
        bool SendTodo(Todo todo);
    }
}