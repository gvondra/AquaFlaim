using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Log.Data.Framework.Models
{
    public class MetricData : DataManagedStateBase
    {
        [ColumnMapping("MetricId", IsPrimaryKey = true)] public Guid MetricId { get; set; }
        [ColumnMapping("EventCode")] public string EventCode { get; set; }
        [ColumnMapping("Magnitude")] public double? Magnitude { get; set; }
        [ColumnMapping("Status")] public string Status { get; set; }
        [ColumnMapping("Data")] public string Data { get; set; }
        [ColumnMapping("Timestamp", IsUtc = true)] public DateTime Timestamp { get; set; }
    }
}
