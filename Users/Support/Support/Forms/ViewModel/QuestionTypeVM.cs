using AquaFlaim.Interface.Forms.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Forms.ViewModel
{
    public class QuestionTypeVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private static Tuple<short, string>[] _responseTypes = new Tuple<short, string>[]
        {
            new Tuple<short, string>(0, "Text"),
            new Tuple<short, string>(1, "Date"),
            new Tuple<short, string>(2, "Yes, No"),
            new Tuple<short, string>(3, "Integer"),
            new Tuple<short, string>(4, "Decimal"),
            new Tuple<short, string>(5, "Choice")
        };
        private SectionTypeVM _sectionType;
        private readonly FormQuestionType _questionType;
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private readonly List<object> _behaviors = new List<object>();
        private bool _isNew = false;
        private bool _responseValidationExpressionVisible;
        private bool _responseMaxLengthVisible;
        private bool _responseListVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        public QuestionTypeVM(FormQuestionType questionType,
            SectionTypeVM sectionType)
        {
            if (!questionType.ResponseType.HasValue)
                questionType.ResponseType = 0; // default response type to Text
            _sectionType = sectionType;
            _questionType = questionType;
            _behaviors.Add(new QuestionTypeValidator(this));
            _behaviors.Add(new QuestionTypeBehavior(this));
        }        

        public FormQuestionType InnerQuestionType => _questionType;

        public Tuple<short, string>[] ResponseTypes => _responseTypes;

        public bool IsNew
        {
            get => _isNew;
            set
            {
                if (_isNew != value)
                {
                    _isNew = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsRequired
        {
            get => _questionType.IsRequired ?? false;
            set
            {
                if (!_questionType.IsRequired.HasValue || _questionType.IsRequired.Value != value)
                {
                    _questionType.IsRequired = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool Hidden
        {
            get => _questionType.Hidden ?? false;
            set
            {
                if (!_questionType.Hidden.HasValue || _questionType.Hidden.Value != value)
                {
                    _questionType.Hidden = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public SectionTypeVM SectionType
        {
            get => _sectionType;
            set
            {
                if (_sectionType != value)
                {
                    _sectionType.Questions.Remove(this);
                    FormSectionType formSectionType = _sectionType.InnerSectionType;
                    formSectionType.Questions.Remove(_questionType);
                    _sectionType = value;
                    formSectionType = _sectionType.InnerSectionType;
                    formSectionType.Questions.Add(_questionType);
                    _questionType.FormSectionTypeId = _sectionType.FormSectionTypeId;
                    _sectionType.Questions.Add(this);                    
                    NotifyPropertyChanged();
                }
            }
        }

        public IEnumerable<SectionTypeVM> Sections => _sectionType.Type.Sections;

        public string Code
        {
            get => _questionType.Code;
            set
            {
                if (_questionType.Code != value)
                {
                    _questionType.Code = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Text
        {
            get => _questionType.Text;
            set
            {
                if (_questionType.Text != value)
                {
                    _questionType.Text = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public short ResponseType
        {
            get => _questionType.ResponseType ?? 0; // zero defaults this property to Text
            set
            {
                if (!_questionType.ResponseType.HasValue || _questionType.ResponseType.Value != value)
                {
                    _questionType.ResponseType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool Required
        {
            get => _questionType.IsRequired ?? false;
            set
            {
                if (_questionType.IsRequired != value)
                {
                    _questionType.IsRequired = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ResponseValidationExpression
        {
            get => _questionType.ResponseValidationExpression;
            set
            {
                if (_questionType.ResponseValidationExpression != value)
                {
                    _questionType.ResponseValidationExpression = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ResponseValidationExpressionVisible
        {
            get => _responseValidationExpressionVisible;
            set
            {
                if (_responseValidationExpressionVisible != value)
                {
                    _responseValidationExpressionVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public short? ResponseMaxLength
        {
            get => _questionType.ResponseMaxLength;
            set
            {
                if (_questionType.ResponseMaxLength != value)
                {
                    _questionType.ResponseMaxLength = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ResponseMaxLengthVisible
        {
            get => _responseMaxLengthVisible;
            set
            {
                if (_responseMaxLengthVisible != value)
                {
                    _responseMaxLengthVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ResponseList
        {
            get => string.Join("\n", _questionType.ResponseList);
            set
            {
                if (value == null) value = string.Empty;
                _questionType.ResponseList = value.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                NotifyPropertyChanged();
            }
        }

        public bool ResponseListVisible
        {
            get => _responseListVisible;
            set
            {
                if (_responseListVisible != value)
                {
                    _responseListVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get => _errors.ContainsKey(columnName) ? _errors[columnName] : null;
            set => _errors[columnName] = value;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
