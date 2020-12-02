using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Google.Protobuf.WellKnownTypes;
using Kafka.Framework;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using Timestamp = Google.Protobuf.WellKnownTypes.Timestamp;

namespace ProtoBufSerialization
{
    class Program
    {
        private const string PERSON_TOPIC = "protobuf-persons";
        static void Main(string[] args)
        {
            var (logger, config) = Bootstrap();

            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = config["SCHEMA_REGISTRY_URL"],
            };

            StartProducing(logger, config, schemaRegistryConfig);

            var consumerFactory = new ConsumerFactory<string, Record>(
                logger,
                new ProtobufDeserializer<Record>(),
                KafkaOptions.ForConsumer(config));

            var consumer = consumerFactory.CreateConsumer("Group-1");
            var consumer2 = consumerFactory.CreateConsumer("Group-2");
            var eventListener = new EventListener(consumer, PERSON_TOPIC, logger);
            var eventListener2 = new EventListener(consumer2, PERSON_TOPIC, logger);
            var personEventHandler = new PersonEventHandler();
            var addressEventHandler = new AddressEventHandler();
            var handlers = new List<IEventHandler> { personEventHandler, addressEventHandler };
            var eventProcessor = new EventProcessor(eventListener, handlers, logger);
            var eventProcessor2 = new EventProcessor(eventListener2, handlers, logger);

            eventProcessor.StartProcessing(CreateCancellation().Token).GetAwaiter().GetResult();
            eventProcessor2.StartProcessing(CreateCancellation().Token).GetAwaiter().GetResult();
        }



        private static (ILogger logger, IConfiguration config) Bootstrap()
        {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.Console(
                    outputTemplate:
                    "{Timestamp:HH:mm:ss.fff} [{Level}] ({threadId}) {Message}{NewLine}{Exception}")
                .CreateLogger();

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("KAFKA_BOOTSTRAP_SERVERS", "192.168.99.100:9092"), //not localhost because i am using docker-machine.
                    new KeyValuePair<string, string>("SCHEMA_REGISTRY_URL", "192.168.99.100:8081")
                })
                .Build();

            return (logger, config);
        }

        private static CancellationTokenSource CreateCancellation()
        {
            var cancellation = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancellation.Cancel();
            };
            cancellation.CancelAfter(10000);

            return cancellation;
        }

        private static void StartProducing(ILogger logger, IConfiguration config, SchemaRegistryConfig schemaRegistryConfig)
        {


            var cachedSchemaRegistryClient = new CachedSchemaRegistryClient(schemaRegistryConfig);
            var producerFactory = new ProducerFactory<string, Record>(
                logger,
                new ProtobufSerializer<Record>(cachedSchemaRegistryClient),
                KafkaOptions.ForProducer(config));

            var kafkaProducer = producerFactory.CreateProducer();

            for (int i = 0; i < 10; i++)
            {
                var person = new Person
                {
                    Id = i,
                    Name = $"{nameof(Person.Name)} {i}",
                    Email = $"{nameof(Person.Email)} {i}",
                    Age = 20 + i,
                    PhoneNumber = $"{nameof(Person.PhoneNumber)} {i}"
                };

                var address = new Address
                {
                    PersonId = i,
                    State = $"{nameof(Address.State)} {i}",
                    Street = $"{nameof(Address.Street)} {i}",
                    ZipCode = $"{nameof(Address.ZipCode)} {i}",
                };

                var personRecord = new Record { CreatedDate = Timestamp.FromDateTime(DateTime.UtcNow), Id = Guid.NewGuid().ToString(), Payload = Any.Pack(person) };
                var addressRecord = new Record { CreatedDate = Timestamp.FromDateTime(DateTime.UtcNow), Id = Guid.NewGuid().ToString(), Payload = Any.Pack(address) };

                var personMessage = new Confluent.Kafka.Message<string, Record>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = personRecord
                };

                var addressMessage = new Message<string, Record>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = addressRecord
                };

                logger.Information(
                        "Sending message => Topic: {Topic} Key: {Key} Value: {Value}",
                        PERSON_TOPIC,
                        personMessage.Key,
                        personMessage.Value);

                kafkaProducer.ProduceAsync(PERSON_TOPIC, personMessage)
                    .GetAwaiter()
                    .GetResult();

                logger.Information(
                        "Sending message => Topic: {Topic} Key: {Key} Value: {Value}",
                        PERSON_TOPIC,
                        addressMessage.Key,
                        addressMessage.Value);

                kafkaProducer.ProduceAsync(PERSON_TOPIC, addressMessage)
                    .GetAwaiter()
                    .GetResult();
            }

        }
    }
}
