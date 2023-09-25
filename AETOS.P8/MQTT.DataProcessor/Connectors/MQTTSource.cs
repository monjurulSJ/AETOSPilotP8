using Microsoft.Extensions.Configuration;
using MQTT.Library;
using System.Runtime;

namespace MQTT.DataProcessor.Connectors
{
    public class MQTTSource : ISource
    {
        private readonly MQTTMessageConsumeHandler _mQTTClientHandle;
        private readonly IConfiguration _config;
        public MQTTSettings _settings { get; set; }
        public MQTTSource(IConfiguration configuration, MQTTMessageConsumeHandler mQTTClientHandle)
        {
            _config = configuration;
            _mQTTClientHandle = mQTTClientHandle;
        }

        public async Task Subscribe(Action<byte[], string> eventHandler, CancellationToken token)
        {
            _settings = _config.GetSection("MQTT").Get<MQTTSettings>();

            await _mQTTClientHandle.Setup(_settings);

            _mQTTClientHandle.process((payload, topic) =>
            {
                if (topic.Contains("v1"))
                {
                    eventHandler(payload, topic);
                }

            }, token);
        }
    }
}
