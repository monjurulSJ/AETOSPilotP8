using P8.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8.Model.DTO
{
    public class TemperatureDTO
    {
        public Reading Reading { get; set; }
        public Geo Geo { get; set; }
    }
    public class Reading:Temperature
    {
       
    }

    public class Geo:DeviceInfo
    {
        public double Long { get; set; }
        public double Lat { get; set; }
    }
}
