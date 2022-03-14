using AquaFlaim.Interface.Authorization.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Authorization.ViewModel
{
    public class ClientVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private readonly Client _innerClient;
        private readonly List<object> _validators = new List<object>();
        private bool _isNew = false;
        private string _secret;

        public ClientVM(Client client)
        {
            _innerClient = client;
            _validators.Add(new ClientValidator(this));
        }

        public ClientVM() : this(new Client()) { }

        public event PropertyChangedEventHandler PropertyChanged;

        public Client InnerClient => _innerClient;

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

        public Guid? ClientId
        {
            get => _innerClient.ClientId;
            set
            {
                _innerClient.ClientId = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get => _innerClient.Name;
            set
            {
                if (_innerClient.Name != value)
                {
                    _innerClient.Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Secret
        {
            get => _secret;
            set
            {
                if (_secret != value)
                {
                    _secret = value;
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
