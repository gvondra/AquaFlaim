using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Forms.ViewModel
{
    public class SectionTypeValidator
    {
        private const string MSG_CODE_REQUIRED = "required";
        private const string MSG_TXT_REQUIRED = " is required";
        private readonly static Dictionary<string, Dictionary<string, string>> _messages = new Dictionary<string, Dictionary<string, string>>
        {
            {
                nameof(SectionTypeVM.Title),
                new Dictionary<string, string>
                {
                    { MSG_CODE_REQUIRED, MSG_TXT_REQUIRED }
                }
            }
        };

        private readonly SectionTypeVM _section;

        public SectionTypeValidator(SectionTypeVM section)
        {
            _section = section;
            _section.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SectionTypeVM.Title):
                    if (string.IsNullOrEmpty(_section.Title))
                        _section[e.PropertyName] = _messages[e.PropertyName][MSG_CODE_REQUIRED];
                    break;
            }
        }
    }
}
