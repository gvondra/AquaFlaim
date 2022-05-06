using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Forms.ViewModel
{
    public class QuestionTypeBehavior
    {
        private readonly QuestionTypeVM _questionType;

        public QuestionTypeBehavior(QuestionTypeVM questionType)
        {
            _questionType = questionType;
            SetResponseValidationExpressionVisible();
            if (_questionType.ResponseValidationExpressionVisible && string.IsNullOrEmpty(_questionType.ResponseValidationExpression))
                SetResponseValidationExpression();
            SetResponseMaxLenghtVisible();
            SetResponseListVisible();
            questionType.PropertyChanged += QuestionType_PropertyChanged;
        }

        private void QuestionType_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(QuestionTypeVM.ResponseType):
                    SetResponseValidationExpressionVisible();
                    if (_questionType.ResponseValidationExpressionVisible)
                        SetResponseValidationExpression();
                    SetResponseMaxLenghtVisible();
                    SetResponseListVisible();
                    break;
            }
        }

        private void SetResponseListVisible()
        {
            _questionType.ResponseListVisible = (_questionType.ResponseType == 5); // 5 = choice
        }

        private void SetResponseMaxLenghtVisible()
        {
            _questionType.ResponseMaxLengthVisible = (_questionType.ResponseType == 0); // 0 = Text
        }

        private void SetResponseValidationExpressionVisible()
        {
            if (_questionType.ResponseType == 0 // 0 = Text
                || _questionType.ResponseType == 1 // 1 = Date
                || _questionType.ResponseType == 2 // 2 = Yes, No
                || _questionType.ResponseType == 3 // 3 = Integer
                || _questionType.ResponseType == 4) // 4 = Decimal
            {
                _questionType.ResponseValidationExpressionVisible = true;
            }
            else
            {
                _questionType.ResponseValidationExpressionVisible = false;
            }
        }

        private void SetResponseValidationExpression()
        {
            switch (_questionType.ResponseType)
            {
                case 0: // text
                    _questionType.ResponseValidationExpression = string.Empty;
                    break;
                case 1: // date
                    _questionType.ResponseValidationExpression = @"^20[0-9]{2}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$";
                    break;
                case 2: // yes, no
                    _questionType.ResponseValidationExpression = @"^((false)|(true))$";
                    break; 
                case 3: // integer
                    _questionType.ResponseValidationExpression = @"^(-[0-9]+){0,1}[0-9]*$";
                    break;
                case 4: // decimal
                    _questionType.ResponseValidationExpression = @"^(-[0-9]+){0,1}[0-9]*(\.[0-9]+)?$";
                    break;
            }
        }
    }
}
