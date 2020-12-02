using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Kafka.Framework
{
    public sealed class StringDeserializer<T> : IAsyncDeserializer<T>
    {
        private readonly Func<byte[], T> _decode;

        public StringDeserializer(Func<byte[], T> decode)
        {
            _decode = decode ?? throw new ArgumentNullException(nameof(decode));
        }

        public Task<T> DeserializeAsync(ReadOnlyMemory<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull) return default;

            return Task.FromResult(_decode(data.ToArray()));
        }
    }
}