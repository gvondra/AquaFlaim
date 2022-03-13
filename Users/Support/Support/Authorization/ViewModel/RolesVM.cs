using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Authorization.ViewModel
{
    public class RolesVM : INotifyPropertyChanged
    {
        private RoleVM _selectedRole;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<RoleVM> Roles { get; } = new ObservableCollection<RoleVM>();

        public RoleVM SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
