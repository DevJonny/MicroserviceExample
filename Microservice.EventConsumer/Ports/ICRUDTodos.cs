using System.Threading.Tasks;
using Microservice.EventConsumer.Model;

namespace Microservice.EventConsumer.Ports
{
    public interface ICRUDTodos
    {
        Task<bool> Insert(Todo todo);
    }
}