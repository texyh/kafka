using System;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Serilog;

namespace Kafka.Framework
{
    public sealed class ProducerFactory<TKey, TValue> : IProducerFactory<TKey, TValue>
    {
        private readonly IAsyncSerializer<TValue> _valueSerializer;
        private readonly ProducerConfig _config;
        private readonly ILogger _logger;

        public ProducerFactory(
            ILogger logger,
            IAsyncSerializer<TValue> valueSerializer,
            IOptions<ProducerConfig> options)
        {
            _valueSerializer = valueSerializer ?? throw new ArgumentNullException(nameof(valueSerializer));
            _config = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IProducer<TKey, TValue> CreateProducer() =>
            new ProducerBuilder<TKey, TValue>(_config)
                .SetValueSerializer(_valueSerializer)
                .SetStatisticsHandler((_, statistics) => _logger.Information(statistics))
                .SetErrorHandler((_, error) => _logger.Error("{Reason} ({Code})", error.Reason, error.Code))
                .Build();
    }
}