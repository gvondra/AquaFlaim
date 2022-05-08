using AquaFlaim.Interface.Configuration.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Configuration.ViewModel
{
    public class LookupVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private readonly Lookup _innerLookup;
        private readonly List<object> _behaviors = new List<object>();
        private bool _isNew = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public LookupVM(Lookup lookup)
        {
            if (lookup.Data == null)
                lookup.Data = new Dictionary<string, string>();
            _innerLookup = lookup;
            _behaviors.Add(new LookupValidator(this));
        }

        public Lookup InnerLookup => _innerLookup;

        public bool IsNew
        {
            get => _isNew;
            set
            {
                if (_isNew != value)
                {
                    _isNew = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Code
        {
            get => _innerLookup.Code;
            set
            {
                if (_innerLookup.Code != value)
                {
                    _innerLookup.Code = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsPublic
        {
            get => _innerLookup.IsPublic;
            set
            {
                if (_innerLookup.IsPublic != value)
                {
                    _innerLookup.IsPublic = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Dictionary<string ,string> Data
        {
            get => _innerLookup.Data;
            set
            {
                _innerLookup.Data = value;
                NotifyPropertyChanged();
            }
        }

        public int ErrorCount => _errors.Count(keyValue => !string.IsNullOrEmpty(keyValue.Value));

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
    }
}
