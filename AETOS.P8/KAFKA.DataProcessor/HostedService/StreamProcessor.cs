using KAFKA.DataProcessor.Connectors;
using KAFKA.DataProcessor.Pipeline;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAFKA.DataProcessor.HostedService
{
    public class StreamProcessor:BackgroundService
    {
        private readonly ISource _source;
        private readonly DataPipeline _dataPipeline;
        public StreamProcessor(ISource source, DataPipeline dataPipeline)
        {
            _source = source;
            _dataPipeline = dataPipeline;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                _source.Subscribe((payload, topic) =>
                {
                    _dataPipeline.Initialize(topic, payload.ToString())
                        .ExtractPayload()
                        .Transform()
                        .Load();
                }, stoppingToken);
            }, stoppingToken);
        }
    }
}
