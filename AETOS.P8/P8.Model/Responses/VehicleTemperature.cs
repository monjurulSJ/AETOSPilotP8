using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8.Model.Responses
{
    public class VehicleTemperature
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<int, double> HourlyAverages { get; set; }
    }
}
