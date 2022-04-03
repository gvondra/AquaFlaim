using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Data.Framework.Models
{
    public class FormQuestionTypeData : DataManagedStateBase
    {
        [ColumnMapping("FormQuestionTypeId", IsPrimaryKey = true)] public int FormQuestionTypeId { get; set; }
        [ColumnMapping("FormTypeId")] public int FormTypeId { get; set; }
        [ColumnMapping("FormSectionTypeId")] public int FormSectionTypeId { get; set; }
        [ColumnMapping("Code")] public string Code { get; set; }
        [ColumnMapping("Text")] public string Text { get; set; }
        [ColumnMapping("ResponseType")] public short ResponseType { get; set; }
        [ColumnMapping("ResponseList")] public string ResponseList { get; set; }
        [ColumnMapping("ResponseMaxLength")] public short? ResponseMaxLength { get; set; }
        [ColumnMapping("IsRequired")] public bool IsRequired { get; set; }
        [ColumnMapping("ResponseValidationExpression")] public string ResponseValidationExpression { get; set; }
        [ColumnMapping("Hidden")] public bool Hidden { get; set; }
        [ColumnMapping("Order")] public short Order { get; set; }
        [ColumnMapping("CreateTimestamp")] public DateTime CreateTimestamp { get; set; }
        [ColumnMapping("UpdateTimestamp")] public DateTime UpdateTimestamp { get; set; }
    }
}
