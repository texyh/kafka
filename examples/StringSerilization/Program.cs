using Common;
using Confluent.Kafka;
using Kafka.Framework;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StringSerilization
{
    public class Program
    {
        public const string PERSON_TOPIC = "personsV1";

        static void Main(string[] args)
        {
            var (logger, config) = Bootstrap();

            StartProducing(logger, config);
            
            var consumerFactory = new ConsumerFactory<string, Record>(
                logger,
                new StringDeserializer<Record>(x => JsonConvert.DeserializeObject<Record>(Decode.IntoString(x))),
                KafkaOptions.ForConsumer(config));

            var consumer = consumerFactory.CreateConsumer("Group-1");
            var eventListener = new EventListener(consumer, PERSON_TOPIC, logger);
            var personEventHandler = new PersonEventHandler();
            var addressEventHandler = new AddressEventHandler();
            var eventProcessor = new EventProcessor(eventListener, new List<IEventHandler> { personEventHandler, addressEventHandler }, logger);

            eventProcessor.StartProcessing(CreateCancellation().Token).GetAwaiter().GetResult();
        }


        private static (ILogger logger, IConfiguration config) Bootstrap()
        {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.Console(
                    outputTemplate:
                    "{Timestamp:HH:mm:ss.fff} [{Level}] ({threadId}) {Message}{NewLine}{Exception}")
                .CreateLogger();

            IConfiguration config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("KAFKA_BOOTSTRAP_SERVERS", "192.168.99.100:9092"), //not localhost because i am using docker-machine.
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

        private static void StartProducing(ILogger logger, IConfiguration config)
        {

            var producerFactory = new ProducerFactory<string, Record>(
                logger,
                new StringSerializer<Record>(e => Encode.FromString(JsonConvert.SerializeObject(e))),
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

                var personRecord = Record.Create(person, typeof(Person));
                var addressRecord = Record.Create(address, typeof(Address));

                var personMessage = new Message<string, Record>
                {
                    Key = person.Id.ToString(),
                    Value = personRecord
                };

                var addressMessage = new Message<string, Record>
                {
                    Key = person.Id.ToString(),
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
