using System.Threading.Tasks;
using Microservice.Core;

namespace Microservice.API.Ports
{
    public interface IAmATodoRetriever
    {
        Task<Todo> ById(string id);
    }
}