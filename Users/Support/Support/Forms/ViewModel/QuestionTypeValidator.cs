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
        private const string MSG_CODE_GT_ZERO = "gt-zero";
        private const string MSG_TXT_GT_ZERO = " must greater than zero";
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
            },
            {
                nameof(QuestionTypeVM.ResponseMaxLength),
                new Dictionary<string, string>
                {
                    { MSG_CODE_GT_ZERO, MSG_TXT_GT_ZERO  }
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
            _questionTypeVM[e.PropertyName] = null;
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
                case nameof(QuestionTypeVM.ResponseMaxLength):                    
                    if (_questionTypeVM.ResponseMaxLength.HasValue && _questionTypeVM.ResponseMaxLength.Value <= 0)
                        _questionTypeVM[e.PropertyName] = _messages[e.PropertyName][MSG_CODE_GT_ZERO];
                    break;
            }
        }
    }
}
