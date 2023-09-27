using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using P8.Model.DTO;
using P8.Model.Models;
using P8.Repository.Repositories;

namespace KAFKA.DataProcessor.Services
{
    public class TemperatureService : ITopicService
    {
        private TemperatureDTO _temperatureDTO;

        private ITemperatureRepository _temperatureRepository;
        public TemperatureService(ITemperatureRepository temperatureRepository)
        {
            _temperatureRepository = temperatureRepository;
        }
        public ITopicService Initialize(string jsonPayload)
        {
            _temperatureDTO = JsonConvert.DeserializeObject<TemperatureDTO>(jsonPayload);
          
            return this;
        }

        public ITopicService Load()
        {

            var data= _temperatureRepository.GetAllTemperatures().Result;
            var s = 10;
            // savvve payload to database.
            return this;
        }

        public ITopicService Transform() // modify payload as like as model
        {
            //other logic here

            return this;
        }
    }
}
