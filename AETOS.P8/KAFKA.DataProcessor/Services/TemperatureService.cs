using KAFKA.DataProcessor.Models;
using Newtonsoft.Json;
using P8.Repository.Repositories;

namespace KAFKA.DataProcessor.Services
{
    public class TemperatureService : ITopicService
    {
        private Temperature _temperaturePayload;
        private ITemperatureRepository _temperatureRepository;
        public TemperatureService(ITemperatureRepository temperatureRepository)
        {
            _temperatureRepository = temperatureRepository;
        }
        public ITopicService Initialize(string jsonPayload)
        {
            _temperaturePayload = JsonConvert.DeserializeObject<Temperature>(jsonPayload);
            return this;
        }

        public ITopicService Load()
        {

            return this;
        }

        public ITopicService Transform()
        {
            //other logic here

            return this;
        }
    }
}
