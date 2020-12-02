using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Kafka.Framework
{
    public static class KafkaOptions
    {
        public static IOptions<ProducerConfig> ForProducer(IConfiguration config)
        {
            return Options.Create(new ProducerConfig
            {
                BootstrapServers = config["KAFKA_BOOTSTRAP_SERVERS"],
                SecurityProtocol = SecurityProtocol.Plaintext
            });
        }

        public static IOptions<ConsumerConfig> ForConsumer(IConfiguration config)
        {
            var requestTimeoutInMs = 60000;

            return Options.Create(new ConsumerConfig
            {
                BootstrapServers = config["KAFKA_BOOTSTRAP_SERVERS"],
                SecurityProtocol = SecurityProtocol.Plaintext,
                SocketTimeoutMs = requestTimeoutInMs,
                SessionTimeoutMs = 30000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                BrokerVersionFallback = "1.0.0",
                EnableAutoCommit = false,
                EnablePartitionEof = true
            });
        }

    }
}