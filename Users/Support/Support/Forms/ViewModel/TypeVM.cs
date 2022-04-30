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
    public class TypeVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly FormType _formType;
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private readonly List<object> _validators = new List<object>();

        public TypeVM(FormType formType)
        {
            _formType = formType;
            _validators.Add(new TypeValidator(this));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public FormType InnerFormType => _formType;

        public string Title
        {
            get => _formType.Title;
            set
            {
                if (_formType.Title != value)
                {
                    _formType.Title = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool HasErrors => _errors.Count > 0;

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
