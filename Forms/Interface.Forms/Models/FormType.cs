using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Forms.Models
{
    public class FormType
    {
        public int? FormTypeId { get; set; }
        public string Title { get; set; }
        public DateTime? CreateTimestamp { get; set; }
        public DateTime? UpdateTimestamp { get; set; }
        public List<FormSectionType> Sections { get; set; }
    }
}
