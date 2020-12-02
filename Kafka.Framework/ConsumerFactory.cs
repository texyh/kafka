using System;
using System.Linq;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Microsoft.Extensions.Options;
using Serilog;

namespace Kafka.Framework
{
    public sealed class ConsumerFactory<TKey, TValue> : IConsumerFactory<TKey, TValue>
    {
        private readonly IAsyncDeserializer<TValue> _valueDeserializer;
        private readonly ConsumerConfig _options;
        private readonly ILogger _logger;

        public ConsumerFactory(
            ILogger logger,
            IAsyncDeserializer<TValue> valueDeserializer,
            IOptions<ConsumerConfig> options)
        {
            _valueDeserializer = valueDeserializer ?? throw new ArgumentNullException(nameof(valueDeserializer));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IConsumer<TKey, TValue> CreateConsumer(string consumerGroup)
        {
            _options.GroupId = consumerGroup;

            return new ConsumerBuilder<TKey, TValue>(_options)
                .SetValueDeserializer(_valueDeserializer.AsSyncOverAsync())
                .SetStatisticsHandler((_, statistics) =>
                {
                    _logger.Information(statistics);
                })
                .SetErrorHandler((_, error) =>
                {
                    _logger.Error("{Reason} ({Code})", error.Reason, error.Code);
                })
                .SetLogHandler((_, message) =>
                {
                    _logger.Information(message.Message);
                })
                .SetPartitionsAssignedHandler((_, tps) =>_logger.Information("Assigned partitions {Partitions}", tps.Select(tp => tp.ToString())))
                .Build();
        }
    }
}
