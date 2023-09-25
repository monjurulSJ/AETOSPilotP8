using Confluent.Kafka;
using Kafka.Library;
using KAFKA.Library;
using Microsoft.Extensions.Configuration;
using System.Runtime;

namespace KAFKA.DataProcessor.Connectors
{
    public class KAFKASource : ISource
    {
        private readonly KafkaConsumer _kafkaConsumer;
        private readonly IConfiguration _config;
        public KAFKASource(IConfiguration configuration, KafkaConsumer kafkaConsumer)
        {
            _config = configuration;
            _kafkaConsumer = kafkaConsumer;
        }

        public void Subscribe(Action<Message<string, string>, string> eventHandler, CancellationToken token)
        {
            var kAFKASettings = _config.GetSection("KAFKA").Get<KAFKASettings>(); ;

            string[] topics = kAFKASettings.Topics.Select(a => a.Name).ToArray();

            _kafkaConsumer.Setup(kAFKASettings.BootstrapServers, kAFKASettings.GroupId);

            _kafkaConsumer.Process(topics, (payload, topic) =>
            {
                eventHandler(payload, topic);

            }, token);
        }
    }
}
