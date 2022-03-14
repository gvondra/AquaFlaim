using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Log.Data.Framework.Models
{
    public class ExceptionData : DataManagedStateBase
    {
        [ColumnMapping("ExceptionId", IsPrimaryKey = true)] public Guid ExceptionId { get; set; }
        [ColumnMapping("ParentExceptionId")] public Guid? ParentExceptionId { get; set; }
        [ColumnMapping("Message")] public string Message { get; set; }
        [ColumnMapping("TypeName")] public string TypeName { get; set; }
        [ColumnMapping("Source")] public string Source { get; set; }
        [ColumnMapping("AppDomain")] public string AppDomain { get; set; }
        [ColumnMapping("TargetSite")] public string TargetSite { get; set; }
        [ColumnMapping("StackTrace")] public string StackTrace { get; set; }
        [ColumnMapping("Data")] public string Data { get; set; }
        [ColumnMapping("Timestamp", IsUtc = true)] public DateTime Timestamp { get; set; }
    }
}
