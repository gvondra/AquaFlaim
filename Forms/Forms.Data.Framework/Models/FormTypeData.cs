using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Data.Framework.Models
{
    public class FormTypeData : DataManagedStateBase
    {
        [ColumnMapping("FormTypeId", IsPrimaryKey = true)] public int FormTypeId { get; set; }
        [ColumnMapping("Title")] public string Title { get; set; }
        [ColumnMapping("CreateTimestamp")] public DateTime CreateTimestamp { get; set; }
        [ColumnMapping("UpdateTimestamp")] public DateTime UpdateTimestamp { get; set; }
    }
}
