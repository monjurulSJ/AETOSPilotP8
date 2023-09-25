using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography.X509Certificates;
using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using MQTT.Library;
using System.Text.RegularExpressions;
using MQTT.DataProcessor.Connectors;
using MQTT.DataProcessor.Pipeline;

namespace MQTT.DataProcessor.HostedServices
{
    public class StreamProcessor : BackgroundService
    {
        private readonly DataPipeline _dataPipeline;
        private readonly ISource _source;
        public StreamProcessor(DataPipeline dataPipeline, ISource source)
        {
            _dataPipeline = dataPipeline;
            _source = source;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                _source.Subscribe((payload, topic) =>
                {
                    _dataPipeline.Initialize(topic, payload)
                        .ExtractPayload()
                        .Transform()
                        .Load();
                }, stoppingToken);
            }, stoppingToken);
        }

    }
}
