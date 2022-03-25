using AquaFlaim.Log.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Log.Data.Framework
{
    public interface IMetricDataFactory
    {
        Task<IEnumerable<string>> GetEventCodes(ISqlSettings settings);
        Task<IEnumerable<MetricData>> GetTopMetricsByTimestamp(ISqlSettings settings, DateTime maxTimestamp);
    }
}
