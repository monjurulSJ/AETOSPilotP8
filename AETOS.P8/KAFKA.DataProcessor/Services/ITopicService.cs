namespace KAFKA.DataProcessor.Services
{
    public interface ITopicService
    {
        public ITopicService Initialize(string jsonPayload);
        public ITopicService Transform();
        public ITopicService Load();
    }
}
