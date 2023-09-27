using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8.Model.Models
{ 
    public class DeviceInfo
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
