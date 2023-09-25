using Microsoft.Extensions.DependencyInjection;

namespace KAFKA.DataProcessor.Services
{
    public interface ITopicFactory
    {
        ITopicService CreateTopic(string topicName);
    }

    public class TopicFactory : ITopicFactory
    {
        public TopicFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ITopicService CreateTopic(string topicName)
        {
            var scope = _serviceScopeFactory.CreateScope();

            ITopicService topic = topicName.ToLower() switch
            {
                "vehicle" => scope.ServiceProvider.GetRequiredService<VehicleService>(),
                "temperature" => scope.ServiceProvider.GetRequiredService<TemperatureService>(),
                _ => throw new NotImplementedException(),
            };
            return topic;
        }
    }

}
