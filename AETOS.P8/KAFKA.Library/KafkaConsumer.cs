using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Kafka.Library
{
    public class KafkaConsumer
    {
        private  IConsumer<string, string> _consumer;

        public void Setup(string bootstrapServers, string groupId)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true
            };

            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }


        public void Process(string[] topics, Action<Message<string,string>,string> msgHandler, CancellationToken token)
        {
            _consumer.Subscribe(topics);

            while (!token.IsCancellationRequested)
            {
                try
                {
                    var cr = _consumer.Consume(token);
                    msgHandler(cr.Message,cr.Topic);
                   

                }

                catch (Exception ex)
                {
                   
                }
            }

            _consumer.Close();
            _consumer.Dispose();
        }

    }
}
