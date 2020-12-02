using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace StringSerilization
{
    public sealed class StringSerializer<T> : IAsyncSerializer<T>, ISerializer<T>
    {
        private readonly Func<T, byte[]> _encode;

        public StringSerializer(Func<T, byte[]> encode)
        {
            _encode = encode ?? throw new ArgumentNullException(nameof(encode));
        }

        public Task<byte[]> SerializeAsync(T data, SerializationContext context)
        {
            if (data == null) return null;

            return Task.FromResult(_encode(data));
        }

        public byte[] Serialize(T data, SerializationContext context)
        {
            if (data == null) return null;

            return _encode(data);
        }
    }
}