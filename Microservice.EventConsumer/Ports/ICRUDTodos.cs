using System.Threading.Tasks;
using Microservice.Core;

namespace Microservice.EventConsumer.Ports
{
    public interface ICRUDTodos
    {
        Task<bool> Insert(Todo todo);
    }
}