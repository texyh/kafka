using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class EventProcessor
    {
        private readonly IEventListener _eventListener;

        private readonly IList<IEventHandler> _eventHandlers;

        private readonly ILogger _logger;

        public EventProcessor(IEventListener eventListener, IList<IEventHandler> eventHandlers, ILogger logger)
        {
            _eventHandlers = eventHandlers;
            _eventListener = eventListener;
            _logger = logger;
        }

        public async Task StartProcessing(CancellationToken cancellationToken)
        {
            await foreach (var record in _eventListener.GetAsync(cancellationToken))
            {
                var handler = _eventHandlers.FirstOrDefault(x => x.CanHandle(record.PayloadType));

                if(handler == null)
                {
                    continue;
                }

                var payload = record.Payload as JObject;

                if(payload == null)
                {
                    _logger.Warning("Payload is null");
                }

                await handler.HandleAsync(payload.ToObject(record.PayloadType));
            }
        }
    }
}
