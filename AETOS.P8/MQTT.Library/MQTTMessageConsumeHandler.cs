using MQTTnet.Client.Options;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet.Client.Publishing;

namespace MQTT.Library
{
    public class MQTTMessageConsumeHandler
    {
        private IMqttClient client;
        private bool isStop;
        public MQTTSettings _settings { get; set; }

        public async Task Setup(MQTTSettings settings)
        {
            _settings = settings ?? new MQTTSettings();

            if (client == null)
            {
               InitClient();
            }

            if (!client.IsConnected)
            {
               await client.ConnectAsync(CreateClientOptions());
            }
        }
        
        public void process(Action<byte[],string> msgHandle,CancellationToken stoppingToken)
        {
            client.UseApplicationMessageReceivedHandler(e =>
            {
                var topic = e.ApplicationMessage.Topic;
                var payload = e.ApplicationMessage.Payload;

                msgHandle(payload, topic);

            });

        }

        public  void InitClient()
        {
            client = new MqttFactory().CreateMqttClient();

            client.UseConnectedHandler(async e =>
            {
                foreach (var topic in _settings.Topics)
                {
                   await client.SubscribeAsync($@"{topic.Name}", (MqttQualityOfServiceLevel)topic.Qos);
                }
            });

            client.UseDisconnectedHandler(async e =>
            {
                if (isStop) return;

                await Task.Delay(TimeSpan.FromSeconds(_settings.ReconnectDelay));

                try
                {
                    await client.ReconnectAsync();
                }
                catch
                {
                    //Log.Error("Reconnecting failed");
                }
            });

        }

        internal IMqttClientOptions CreateClientOptions()
        {
            return new MqttClientOptionsBuilder()
            .WithClientId(_settings.ClientId)
            .WithTcpServer(_settings.Server, _settings.Port)
                .WithCredentials(_settings.Username, _settings.Password)
            .Build();
        }

        internal MqttClientOptionsBuilderTlsParameters CreateTlsParams()
        {
            if (!_settings.TLS)
            {
                return new MqttClientOptionsBuilderTlsParameters
                {
                    UseTls = false
                };
            }

            if (!string.IsNullOrEmpty(_settings.Cert) && !string.IsNullOrEmpty(_settings.CertPass))
            {
                using var caCert = new X509Certificate2(_settings.Cert, _settings.CertPass);
                return new MqttClientOptionsBuilderTlsParameters
                {
                    UseTls = true,
                    Certificates = new List<X509Certificate2> { caCert },
                    CertificateValidationCallback = (cer, chain, error, o) =>
                    {
                        return true;
                    }
                };
            }
            else
            {
                return new MqttClientOptionsBuilderTlsParameters
                {
                    UseTls = true
                };
            }
        }

    }
}
