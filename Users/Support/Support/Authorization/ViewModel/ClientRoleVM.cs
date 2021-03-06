using AuthModels = AquaFlaim.Interface.Authorization.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Authorization.ViewModel
{
    public class ClientRoleVM : INotifyPropertyChanged
    {
        private bool _isActive = false;
        private AuthModels.Role _innerRole;

        public ClientRoleVM(AuthModels.Role role)
        {
            _innerRole = role;
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Name => _innerRole.Name;
        public string PolicyName => _innerRole?.PolicyName;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
