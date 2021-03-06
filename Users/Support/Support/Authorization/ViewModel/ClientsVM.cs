using AuthModels = AquaFlaim.Interface.Authorization.Models;
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
    public class ClientsVM : INotifyPropertyChanged
    {
        private ClientVM _selectedClient;
        private List<AuthModels.Role> _allRoles;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ClientVM> Clients { get; } = new ObservableCollection<ClientVM>();

        public ClientVM SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
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
