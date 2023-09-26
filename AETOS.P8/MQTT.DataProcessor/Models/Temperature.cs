using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.DataProcessor.Models
{
    public class Temperature
    {
        public string id { get; set; }
        public string temp { get; set; }
        public string psi { get; set; }
        public string timestamp { get; set; }
    }

    public class Geo
    {
        public string id { get; set; }
        public string longtd { get; set; }
        public string lat { get; set; }

    }
}
