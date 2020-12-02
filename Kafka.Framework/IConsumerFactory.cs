using Confluent.Kafka;

namespace Kafka.Framework
{
    public interface IConsumerFactory<TKey, TValue>
    {
        IConsumer<TKey, TValue> CreateConsumer(string consumerGroup);
    }
}