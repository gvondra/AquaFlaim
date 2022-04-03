using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Forms.Models
{
    public class FormSectionType
    {
        public int? FormSectionTypeId { get; set; }
        public int? FormTypeId { get; set; }
        public string Title { get; set; }
        public short? Order { get; set; }
        public bool? Hidden { get; set; }
        public DateTime? CreateTimestamp { get; set; }
        public DateTime? UpdateTimestamp { get; set; }
        public List<FormQuestionType> Questions { get; set; }
    }
}
