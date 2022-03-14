using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Log.Models
{
    public class Exception
    {
        public Guid? ExceptionId { get; set; }
        public string Message { get; set; }
        public string TypeName { get; set; }
        public string Source { get; set; }
        public string AppDomain { get; set; }
        public string TargetSite { get; set; }
        public string StackTrace { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public DateTime? Timestamp { get; set; }
        public Exception InnerException { get; set; }
    }
}
