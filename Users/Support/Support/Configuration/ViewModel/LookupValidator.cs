using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Configuration.ViewModel
{
    public class LookupValidator
    {
        private const string MSG_CODE_REQUIRED = "required";
        private const string MSG_TXT_REQUIRED = " is required";
        private readonly static Dictionary<string, Dictionary<string, string>> _messages = new Dictionary<string, Dictionary<string, string>>
        {
            {
                nameof(LookupVM.Code),
                new Dictionary<string, string>
                {
                    { MSG_CODE_REQUIRED, MSG_TXT_REQUIRED }
                }
            }
        };

        private readonly LookupVM _lookup;

        public LookupValidator(LookupVM lookup)
        {
            _lookup = lookup;
            ValidateCode(nameof(LookupVM.Code), lookup.Code);
            lookup.PropertyChanged += Lookup_PropertyChanged;
        }

        private void Lookup_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(LookupVM.Code):
                    ValidateCode(e.PropertyName, _lookup.Code);
                    break;
            }
        }

        private void ValidateCode(string propertyName, string code)
        {
            _lookup[propertyName] = null;
            if (string.IsNullOrEmpty(code))
                _lookup[propertyName] = _messages[propertyName][MSG_CODE_REQUIRED];
        }
    }
}
