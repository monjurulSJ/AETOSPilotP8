using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using P8.Model.DTO;
using P8.Model.Models;
using P8.Model.Responses;
using P8.Repository.Repositories;

namespace KAFKA.DataProcessor.Services
{
    public class TemperatureService : ITopicService
    {
        private TemperatureDTO _temperatureDTO;

        private Temperature _temperature;
        private DeviceInfo _deviceInfo;

        public readonly IMapper _mapper;
        private ITemperatureRepository _temperatureRepository;
        public TemperatureService(ITemperatureRepository temperatureRepository, IMapper mapper)
        {
            _temperatureRepository = temperatureRepository;
            _mapper = mapper;
        }
        public ITopicService Initialize(string jsonPayload)
        {
            _temperatureDTO = JsonConvert.DeserializeObject<TemperatureDTO>(jsonPayload);
          
            return this;
        }

        public ITopicService Load()
        {
     
            try
            {
                var deviceInfo= _temperatureRepository.GetDeviceInfoByDeviceId(_temperatureDTO.Reading.Id).Result;
                if (deviceInfo!=null)  // save to the database for deviceTemperature
                {
                    _temperature.DeviceInfoId = deviceInfo.Id;
                    _temperatureRepository.SaveVehicleTemperature(_temperature);
                }
                return this;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ITopicService Transform() // modify payload as like as model
        {
            //other logic here

            _temperature = _mapper.Map<Temperature>(_temperatureDTO.Reading);
            _deviceInfo = _mapper.Map<DeviceInfo>(_temperatureDTO.Geo);

            _temperature.CreatedAt = DateTime.Now.ToUniversalTime();
            _temperature.Timestamp = _temperature.Timestamp.ToUniversalTime();


            return this;
        }
    }
}
