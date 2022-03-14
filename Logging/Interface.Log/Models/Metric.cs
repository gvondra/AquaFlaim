using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Log.Models
{
    public class Metric
    {
        public Guid? MetricId { get; set; }
        public string EventCode { get; set; }
        public double? Magnitude { get; set; }
        public string Status { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
