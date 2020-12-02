using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProtoBufSerialization
{
    public class PersonEventHandler : IEventHandler
    {
        public bool CanHandle(Any payload)
        {
            return payload.Is(Person.Descriptor);
        }

        public Task HandleAsync(Any @event)
        {
            var person = @event.Unpack<Person>();
            Console.WriteLine($"person:: {person.ToString()}");

            return Task.CompletedTask;
        }
    }
}
