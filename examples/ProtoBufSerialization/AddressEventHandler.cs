using Google.Protobuf.WellKnownTypes;
using System;
using System.Threading.Tasks;

namespace ProtoBufSerialization
{
    public class AddressEventHandler : IEventHandler
    {
        public bool CanHandle(Any payload)
        {
            return payload.Is(Address.Descriptor);
        }

        public Task HandleAsync(Any @event)
        {
            var address = @event.Unpack<Address>();

            Console.WriteLine($"address:: {address.ToString()}");

            return Task.CompletedTask;
        }
    }
}
