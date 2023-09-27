using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8.Model.Models
{
    public class Temperature
    {
        public int id { get; set; }
        public int DeviceId { get; set; }
        public int temp { get; set; }
        public int psi { get; set; }
        public string timestamp { get; set; }
    }

    public class Geo
    {
        public string id { get; set; } 
        public double longitude { get; set; }
        public double latitude { get; set; }

    }
}
