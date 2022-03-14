using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Log.Data.Framework.Models
{
    public class TraceData : DataManagedStateBase
    {
        [ColumnMapping("TraceId", IsPrimaryKey = true)] public Guid TraceId { get; set; }
        [ColumnMapping("EventCode")] public string EventCode { get; set; }
        [ColumnMapping("Message")] public string Message { get; set; }
        [ColumnMapping("Data")] public string Data { get; set; }
        [ColumnMapping("Timestamp", IsUtc = true)] public DateTime Timestamp { get; set; }
    }
}
