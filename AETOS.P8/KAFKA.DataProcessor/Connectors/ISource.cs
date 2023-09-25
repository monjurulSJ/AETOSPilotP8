using Confluent.Kafka;

namespace KAFKA.DataProcessor.Connectors
{
    public interface ISource
    {
        void Subscribe(Action<Message<string, string>, string> eventHandler, CancellationToken token);
    }
}
