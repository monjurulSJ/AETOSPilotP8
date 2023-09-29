using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8.Model.Responses
{
    public class VechileSpeed
    {
        public int DeviceId { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<int, double> HourlyMax { get; set; }
        public Dictionary<int, double> HourlyMin { get; set; }
    }
}
