using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Forms.ViewModel
{
    public class TypeValidator
    {
        private const string MSG_CODE_REQUIRED = "required";
        private const string MSG_TXT_REQUIRED = " is required";
        private readonly static Dictionary<string, Dictionary<string, string>> _messages = new Dictionary<string, Dictionary<string, string>>
        {
            {
                nameof(TypeVM.Title),
                new Dictionary<string, string>
                {
                    { MSG_CODE_REQUIRED, MSG_TXT_REQUIRED }
                }
            }
        };

        private readonly TypeVM _type;

        public TypeValidator(TypeVM type)
        {
            _type = type;
            _type.PropertyChanged += PropertyChanged;
        }

        private void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(TypeVM.Title):
                    if (string.IsNullOrEmpty(_type.Title))
                        _type[e.PropertyName] = _messages[e.PropertyName][MSG_CODE_REQUIRED];
                    break;
            }
        }
    }
}
