using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Library
{
    public class KafkaProducer:IDisposable
    {
        private  IProducer<Null, string> _producer;
        public  void Setup(string bootstrapServers, string clientId)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = clientId,
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }
        public void Produce(string topic, string data)
        {
            var message = new Message<Null,
                string>
            {
                Value = data
            };
            _producer.Produce(topic,message);
        }
        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
