using Google.Protobuf.WellKnownTypes;
using System.Threading.Tasks;

namespace ProtoBufSerialization
{
    public interface IEventHandler
    {
        bool CanHandle(Any record);
        Task HandleAsync(Any person);
    }
}