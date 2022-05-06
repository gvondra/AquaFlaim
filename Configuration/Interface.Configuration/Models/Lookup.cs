using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Configuration.Models
{
    public class Lookup
    {
        public Guid LookupId { get; set; }
        public bool IsPublic { get; set; }
        public string Code { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public DateTime UpdateTimestamp { get; set; }
    }
}
