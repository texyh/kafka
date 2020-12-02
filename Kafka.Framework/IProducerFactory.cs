using Confluent.Kafka;

namespace Kafka.Framework
{
    public interface IProducerFactory<TKey, TValue>
    {
        IProducer<TKey, TValue> CreateProducer();
    }
}