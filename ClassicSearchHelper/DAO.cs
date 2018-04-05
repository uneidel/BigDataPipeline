using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicSearchHelper
{
    public class Schedule
    {
        public string interval { get; set; }
    }

    public class Configuration
    {
        public string parsingMode { get; set; }
    }

    public class Parameters
    {
        public Configuration configuration { get; set; }
    }

    public class IndexerObject
    {
        public string name { get; set; }
        public string dataSourceName { get; set; }
        public string targetIndexName { get; set; }
        public Schedule schedule { get; set; }
        public Parameters parameters { get; set; }
    }
}
