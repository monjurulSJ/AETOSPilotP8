namespace MQTT.Library
{
    public class MQTTSettings
    {
        public string ClientId { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Cert { get; set; }
        public string CertPass { get; set; }
        public bool TLS { get; set; }
        public int ReconnectDelay { get; set; }
        public IEnumerable<TopicConfig> Topics { get; set; }
        public bool VerboseLog { get; set; }
    }

    public class TopicConfig
    {
        public string Name { get; set; }
        public int Qos { get; set; }
    }

}
