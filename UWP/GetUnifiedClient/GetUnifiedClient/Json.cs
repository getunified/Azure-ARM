using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetUnifiedClient
{
    public class JMessage
    {
        public string id { get; set; }
        public string cmd { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string cts { get; set; }
        public string rssi { get; set; }
        public string cvu { get; set; }
        public string cv { get; set; }
        public string ofs { get; set; }
        public string ucl { get; set; }
        public string lcl { get; set; }
        public string exm { get; set; }
        public string rtm { get; set; }
        public string ack { get; set; }
        public string alm { get; set; }
    }
}
