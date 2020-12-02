using System.Collections.Generic;
using System.Threading;

namespace ProtoBufSerialization
{
    public interface IEventListener
    {
        IAsyncEnumerable<Record> GetAsync(CancellationToken cancellationToken);
    }
}