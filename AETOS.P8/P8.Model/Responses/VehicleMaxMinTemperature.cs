using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8.Model.Responses
{
    public class VehicleMaxMinTemperature
    {
        public int DeviceId { get; set; }
        public DateTime TargetDateTime {  get; set; }
        public int MaxTemperature { get; set; }
        public int MinTemperature { get; set; }
    }
}
