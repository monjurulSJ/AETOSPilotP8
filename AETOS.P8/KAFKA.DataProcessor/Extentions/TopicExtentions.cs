namespace KAFKA.DataProcessor.Extentions
{
    public static class TopicExtentions
    {
        public static string GetDeviceId(this string topic)
        {
            var deviceId = topic.Split('/')[1];
            return deviceId;
        }

        public static string GetTopicName(this string topic)
        {
            var topicKey = topic.Split('/')[2];
            return topicKey.ToLowerInvariant();
        }
    }
}
