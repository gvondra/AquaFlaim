using AquaFlaim.Interface.Log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Log
{
    public interface ITraceService
    {
        Task Create(ISettings settings, params Trace[] traces);
        Task Creaet(ISettings settings, string eventCode, string message, Dictionary<string, string> data = null);
    }
}
