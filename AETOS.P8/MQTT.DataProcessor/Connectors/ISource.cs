namespace MQTT.DataProcessor.Connectors
{
    public interface ISource
    {
        Task Subscribe(Action<byte[], string> eventHandler, CancellationToken token);
    }
}
