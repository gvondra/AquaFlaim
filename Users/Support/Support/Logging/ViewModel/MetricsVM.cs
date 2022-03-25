using LogApiModels = AquaFlaim.Interface.Log.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AquaFlaim.User.Support.Logging.ViewModel
{
    public class MetricsVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly List<object> _behaviors = new List<object>();
        private readonly ObservableCollection<MetricVM> _metrics = new ObservableCollection<MetricVM>();

        private string _maxTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        public string MaxTimestamp
        {
            get => _maxTimestamp;
            set
            {
                if (_maxTimestamp != value)
                {
                    _maxTimestamp = value;
                    NotifyPropertyChanged();

                }
            }
        }

        public ObservableCollection<MetricVM> Metrics => _metrics;

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get => _errors.ContainsKey(columnName) ? _errors[columnName] : null;
            set => _errors[columnName] = value;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MetricsVM AddValidator()
        {
            _behaviors.Add(new MetricsValidator(this));
            return this;
        }
    }
}
