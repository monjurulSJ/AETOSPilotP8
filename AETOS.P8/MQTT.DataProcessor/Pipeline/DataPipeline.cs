using System;
using System.Text;
using MQTT.DataProcessor.Extentions;
using MQTT.DataProcessor.Services;

namespace MQTT.DataProcessor.Pipeline
{
    public class DataPipeline
    {
        private string _payload;
        private string _topicName;
        private string _deviceId;
        public ITopicService TopicService { get; private set; }
        private readonly IExtractService _extractService;

        public DataPipeline(IExtractService extractService)
        {
            _extractService = extractService;
        }

        public DataPipeline Initialize(string topic, byte[] payload)
        {
            // Log.Information($"Processing {topic}");

            _topicName = topic.GetTopicName();
            _deviceId = topic.GetDeviceId();
            _payload = Encoding.UTF8.GetString(payload);

            return this;
        }

        public DataPipeline ExtractPayload()
        {
            try
            {
                TopicService = _extractService.ExtractPayloads(_topicName, _deviceId, _payload);
            }
            catch (Exception ex)
            {
                //   throw new TopicProcessingException(_deviceId, _payload, ExceptionReason.ProcessError, ex);
            }

            return this;
        }

        public DataPipeline Transform()
        {
            try
            {
                // Log.Information("Processing {Device} and {Payload}", _deviceId, _payload);

                TopicService.Transform();
            }
            catch (Exception ex)
            {
                //  throw new TopicProcessingException(_deviceId, _payload, ExceptionReason.ProcessError, ex);
            }

            return this;
        }

        public DataPipeline Load()
        {
            try
            {
                TopicService.Load();
            }
            catch (Exception ex)
            {
                // throw new TopicProcessingException(_deviceId, _payload, ExceptionReason.ProcessError, ex);
            }

            return this;
        }
    }
}
