using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Data.Framework.Models
{
    public class LookupData : DataManagedStateBase
    {
        [ColumnMapping("LookupId", IsPrimaryKey = true)] public Guid LookupId { get; set; }
        [ColumnMapping("IsPublic")] public bool IsPublic { get; set; }
        [ColumnMapping("Code")] public string Code { get; set; }
        [ColumnMapping("Data")] public string Data { get; set; }
        [ColumnMapping("CreateTimestamp", IsUtc = true)] public DateTime CreateTimestamp { get; set; }
        [ColumnMapping("UpdateTimestamp", IsUtc = true)] public DateTime UpdateTimestamp { get; set; }
    }
}
