using AquaFlaim.User.Support.Logging.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Logging
{
    public class MetricsValidator
    {
        private const string CODE_IS_REQUIRED = "is-required";
        private const string CODE_INVALID_TS = "invlid-ts";

        private const string MSG_IS_REQUIRED = " is required ";
        private const string MSG_INVVALID_TS = " invalid timestamp";

        private static Dictionary<string, Dictionary<string, string>> _errorMessages = new Dictionary<string, Dictionary<string, string>>
        {
            {
                nameof(MetricsVM.MaxTimestamp),
                new Dictionary<string, string>
                { 
                    { CODE_IS_REQUIRED, MSG_IS_REQUIRED },
                    { CODE_INVALID_TS, MSG_INVVALID_TS }
                }
            }
        };

        private readonly MetricsVM _metricVM;

        public MetricsValidator(MetricsVM metricVM)
        {
            _metricVM = metricVM;
            metricVM.PropertyChanged += MetricVM_PropertyChanged;
        }

        private void MetricVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MetricsVM.MaxTimestamp):
                    ValidateMaxTimestamp(e.PropertyName, _metricVM.MaxTimestamp);
                    break;
            }
        }

        private void ValidateMaxTimestamp(string propertyName, string maxTimestamp)
        {
            DateTime dateTime;
            _metricVM[propertyName] = null;
            if (string.IsNullOrEmpty(maxTimestamp))
                _metricVM[propertyName] = _errorMessages[propertyName][CODE_IS_REQUIRED];
            else if (!DateTime.TryParse(maxTimestamp, out dateTime))
                _metricVM[propertyName] = _errorMessages[propertyName][CODE_INVALID_TS];
        }
    }
}
