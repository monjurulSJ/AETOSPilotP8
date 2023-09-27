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
    public class Reading
    {
        public int Id { get; set; }
        public int Temp { get; set; }
        public int Psi { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class Geo
    {
        public double Long { get; set; }
        public double Lat { get; set; }
    }
}
