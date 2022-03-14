using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Log.Models
{
    public class Trace
    {
        public Guid? TraceId { get; set; }
        public string EventCode { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
