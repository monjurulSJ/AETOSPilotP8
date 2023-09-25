using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAFKA.Library
{
    public class KAFKASettings
    {
        public string BootstrapServers { get; set; }
        public string GroupId { get; set; }

        public IEnumerable<TopicConfig> Topics { get; set; }
        public bool VerboseLog { get; set; }
    }

    public class TopicConfig
    {
        public string Name { get; set; }
    }
}
