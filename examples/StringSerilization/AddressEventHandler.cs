using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StringSerilization
{
    public class AddressEventHandler : IEventHandler
    {
        public bool CanHandle(Type payloadType)
        {
            return payloadType == typeof(Address);
        }

        public Task HandleAsync(object @event)
        {
            var address = (Address)@event;

            Console.WriteLine($"address:: {address.ToString()}");

            return Task.CompletedTask;
        }
    }
}
