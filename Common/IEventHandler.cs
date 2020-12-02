using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IEventHandler
    {
        Task HandleAsync(object @event);

        bool CanHandle(Type payloadType);
    }
}
