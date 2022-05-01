using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Forms.ViewModel
{
    public class QuestionTypeValidator
    {
        private const string MSG_CODE_REQUIRED = "required";
        private const string MSG_TXT_REQUIRED = " is required";
        private readonly static Dictionary<string, Dictionary<string, string>> _messages = new Dictionary<string, Dictionary<string, string>>
        {
            {
                nameof(QuestionTypeVM.Code),
                new Dictionary<string, string>
                {
                    { MSG_CODE_REQUIRED, MSG_TXT_REQUIRED }
                }
            },
            {
                nameof(QuestionTypeVM.Text),
                new Dictionary<string, string>
                {
                    { MSG_CODE_REQUIRED, MSG_TXT_REQUIRED }
                }
            }
        };

        private readonly QuestionTypeVM _questionTypeVM;

        public QuestionTypeValidator(QuestionTypeVM questionTypeVM)
        {
            _questionTypeVM = questionTypeVM;
            questionTypeVM.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(QuestionTypeVM.Code):
                    if (string.IsNullOrEmpty(_questionTypeVM.Code))
                        _questionTypeVM[e.PropertyName] = _messages[e.PropertyName][MSG_CODE_REQUIRED];
                    break;
                case nameof(QuestionTypeVM.Text):
                    if (string.IsNullOrEmpty(_questionTypeVM.Text))
                        _questionTypeVM[e.PropertyName] = _messages[e.PropertyName][MSG_CODE_REQUIRED];
                    break;
            }
        }
    }
}
