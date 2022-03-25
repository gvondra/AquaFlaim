using LogApiModels = AquaFlaim.Interface.Log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Logging.ViewModel
{
    public class MetricVM
    {
        private readonly LogApiModels.Metric _innerMetric;

        public MetricVM(LogApiModels.Metric metric)
        {
            _innerMetric = metric;
        }

        public string EventCode => _innerMetric.EventCode;

        public double? Magnitude => _innerMetric.Magnitude.HasValue ? Math.Round(_innerMetric.Magnitude.Value, 3, MidpointRounding.AwayFromZero) : 0.0;

        public string Status => _innerMetric.Status;

        public Dictionary<string, string> Data => _innerMetric.Data;

        public IEnumerable<string> DataText
        {
            get
            {
                return _innerMetric.Data.Select<KeyValuePair<string, string>, string>(pair => $"{pair.Key} = \"{pair.Value}\"");
            }
        }

        public DateTime Timestamp => _innerMetric.Timestamp.Value.ToLocalTime();
    }
}
