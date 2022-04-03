using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Data.Framework.Models
{
    public class FormSectionTypeData : DataManagedStateBase
    {
        [ColumnMapping("FormSectionTypeId", IsPrimaryKey = true)] public int FormSectionTypeId { get; set; }
        [ColumnMapping("FormTypeId")] public int FormTypeId { get; set; }
        [ColumnMapping("Title")] public string Title { get; set; }
        [ColumnMapping("Order")] public short Order { get; set; }
        [ColumnMapping("Hidden")] public bool Hidden { get; set; }
        [ColumnMapping("CreateTimestamp")] public DateTime CreateTimestamp { get; set; }
        [ColumnMapping("UpdateTimestamp")] public DateTime UpdateTimestamp { get; set; }
    }
}
