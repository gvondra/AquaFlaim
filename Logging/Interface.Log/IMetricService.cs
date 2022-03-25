using AquaFlaim.Interface.Log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Log
{
    public interface IMetricService
    {
        Task<IEnumerable<string>> GetEventCodes(ISettings settings);
        Task<IEnumerable<Metric>> GetTopByTimestamp(ISettings settings, DateTime? maxTimestamp);
        Task Create(ISettings settings, params Metric[] metrics);
        Task Create(ISettings settings, string eventCode, double? magnitude, string status = null, Dictionary<string, string> data = null);
    }
}
