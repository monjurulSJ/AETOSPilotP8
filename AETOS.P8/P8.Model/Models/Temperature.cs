using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8.Model.Models
{
    public class Temperature
    {
        public int Id { get; set; }
        public int Temp { get; set; }
        public int Psi { get; set; }
        public DateTime Timestamp { get; set; }
        public int DeviceInfoId { get; set; }
        public virtual DeviceInfo DeviceInfo { get; set; }
    }

    public class DeviceInfo
    {
<<<<<<< HEAD
        public int Id { get; set; } 
        public double Longitude { get; set; }
        public double Latitude { get; set; }

=======
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; } 
>>>>>>> dev-angon
    }
}
