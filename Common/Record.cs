using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Record
    {
        public string  Id { get; set; }

        public object Payload { get; set; }

        public Type PayloadType { get; set; }

        public DateTime CreatedDate { get; set; }


        public static Record Create(object payload, Type payloadType)
        {
            return new Record
            {
                CreatedDate = DateTime.UtcNow,
                Payload = payload,
                PayloadType = payloadType,
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}
