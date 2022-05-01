using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Forms.Models
{
    public class FormQuestionType
    {
        public int? FormQuestionTypeId { get; set; }
        public int? FormTypeId { get; set; }
        public int? FormSectionTypeId { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
        public short? ResponseType { get; set; }
        public List<string> ResponseList { get; set; }
        public short? ResponseMaxLength { get; set; }
        public bool? IsRequired { get; set; }
        public string ResponseValidationExpression { get; set; }
        public bool? Hidden { get; set; }
        public short? Order { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public DateTime UpdateTimestamp { get; set; }
    }
}
