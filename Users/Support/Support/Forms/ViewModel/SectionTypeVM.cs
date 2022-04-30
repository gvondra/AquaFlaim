using AquaFlaim.Interface.Forms.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Forms.ViewModel
{
    public class SectionTypeVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly FormSectionType _sectionType;
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private readonly List<object> _validators = new List<object>();

        public event PropertyChangedEventHandler PropertyChanged;

        public SectionTypeVM(FormSectionType sectionType)
        {
            _sectionType = sectionType;
            _validators.Add(new SectionTypeValidator(this));
        }

        public string Title
        {
            get => _sectionType.Title;
            set
            {
                if (_sectionType.Title != value)
                {
                    _sectionType.Title = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool Hidden
        {
            get => _sectionType.Hidden ?? false;
            set
            {
                if (!_sectionType.Hidden.HasValue || _sectionType.Hidden.Value != value)
                {
                    _sectionType.Hidden = value;
                    NotifyPropertyChanged();
                }
            }
        }

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
