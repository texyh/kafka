using Common;
using Confluent.Kafka;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StringSerilization
{
    public class EventListener : IEventListener
    {

        private readonly IConsumer<string, Record> _consumer;

        private readonly string _topic;

        private readonly ILogger _logger;

        public EventListener(IConsumer<string, Record> consumer, string topic, ILogger logger)
        {
            _consumer = consumer;
            _topic = topic;
            _logger = logger;
        }

        public async IAsyncEnumerable<Record> GetAsync(CancellationToken cancellationToken)
        {
            
            _consumer.Subscribe(_topic);
            _logger.Information($"subscribe to topic {_topic}");

            while (!cancellationToken.IsCancellationRequested)
            {
                var record = new Record();
                await Task.Yield();

                try
                {
                    var result = _consumer.Consume(cancellationToken);

                    if (result.IsPartitionEOF)
                    {
                        _logger.Information("Got partition EOF at {TopicPartitionOffset}", result.TopicPartitionOffset);
                        continue;
                    }

                    record = result.Value;

                    if (record == null)
                    {
                        throw new Exception($"record at partiton {result.TopicPartitionOffset} is invalid");
                    }

                }
                catch (OperationCanceledException ex)
                {
                    _logger.Warning($"Consume operation from topic {_topic} cancelled", ex);
                    continue;
                }

                yield return record;

                _consumer.Commit();
            }
        }
    }
}
