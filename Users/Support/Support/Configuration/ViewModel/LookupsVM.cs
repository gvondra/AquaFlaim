using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Configuration.ViewModel
{
    public class LookupsVM : INotifyPropertyChanged
    {
        private ObservableCollection<string> _codes = new ObservableCollection<string>();
        private readonly List<object> _behaviors = new List<object>();
        //private LookupVM _selectedLookup;

        public event PropertyChangedEventHandler PropertyChanged;
        
        //public LookupVM SelectedLookup
        //{
        //    get => _selectedLookup;
        //    set
        //    {
        //        _selectedLookup = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        public ObservableCollection<string> Codes => _codes;
        
        public void AddBehavior(object behavior) => _behaviors.Add(behavior);

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
