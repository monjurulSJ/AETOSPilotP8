using System;
using System.Text.Json;
using System.Text;
using System.Linq;
using MQTT.DataProcessor.Services;

namespace MQTT.DataProcessor.Pipeline
{
    public interface IExtractService
    {
        ITopicService ExtractPayloads(string topic, string deviceId, string payload);
    }

    public class ExtractPayload : IExtractService
    {
        private readonly ITopicFactory _topicFactory;

        public ExtractPayload(
            ITopicFactory topicFactory)
        {
            _topicFactory = topicFactory;
        }

        public ITopicService ExtractPayloads(string topicName, string deviceId, string payload)
        {

            // if (DeviceIsNotValid(deviceId, topicName))
            //{
            // throw new TopicProcessingException(deviceId, payload, ExceptionReason.NotProcess, null);
            //}


            return _topicFactory.CreateTopic(topicName).Initialize(payload);
        }

        protected bool DeviceIsNotValid(string deviceId, string topicName)
        {
            return false;
        }
    }
}
