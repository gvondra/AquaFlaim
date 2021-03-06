using AuthModels = AquaFlaim.Interface.Authorization.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AquaFlaim.User.Support.Authorization.ViewModel
{
    public class FindUserVM : INotifyPropertyChanged
    {
        private string _findAddress;
        private AuthModels.User _user;
        private List<AuthModels.Role> _allRoles;
        private ObservableCollection<FindUserRoleVM> _roles = new ObservableCollection<FindUserRoleVM>();

        public event PropertyChangedEventHandler PropertyChanged;

        // the email address to be searched for
        public string FindAddress
        {
            get => _findAddress;
            set
            {
                if (_findAddress != value)
                {
                    _findAddress = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<FindUserRoleVM> Roles => _roles;

        public AuthModels.User User
        {
            get => _user;
            set
            {
                _user = value;
                NotifyPropertyChanged();
            }
        }

        public List<AuthModels.Role> AllRoles
        {
            get => _allRoles;
            set
            {
                _allRoles = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
