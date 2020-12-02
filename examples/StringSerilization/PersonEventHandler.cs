using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StringSerilization
{
    public class PersonEventHandler : IEventHandler
    {
        public bool CanHandle(Type payloadType)
        {
            return payloadType == typeof(Person);
        }

        public Task HandleAsync(object @event)
        {
            var person = (Person)@event;

            Console.WriteLine($"person:: {person.ToString()}");

            return Task.CompletedTask;
        }
    }
}
