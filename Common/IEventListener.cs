using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Common
{
    public interface IEventListener
    {
        IAsyncEnumerable<Record> GetAsync(CancellationToken cancellationToken);
    }
}
