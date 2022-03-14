using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Authorization.ViewModel
{
    public class ClientValidator
    {
        private const string MSG_CODE_REQUIRED = "required";
        private const string MSG_TXT_REQUIRED = " is required";
        private readonly Dictionary<string, Dictionary<string, string>> _messages = new Dictionary<string, Dictionary<string, string>>
        {
            {
                nameof(ClientVM.Name),
                new Dictionary<string, string>
                {
                    { MSG_CODE_REQUIRED, MSG_TXT_REQUIRED }
                }
            }
        };

        private readonly ClientVM _clientVM;

        public ClientValidator(ClientVM clientVM)
        {
            _clientVM = clientVM;
            _clientVM.PropertyChanged += OnProperyChanged;
        }

        public void OnProperyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ClientVM.Name):
                    if (string.IsNullOrEmpty(_clientVM.Name))
                        _clientVM[e.PropertyName] = _messages[e.PropertyName][MSG_CODE_REQUIRED];
                    break;
            }
        }
    }
}
