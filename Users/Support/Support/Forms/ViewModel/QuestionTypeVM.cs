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
        private SectionTypeVM _sectionType;
        private readonly FormQuestionType _questionType;
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private readonly List<object> _validators = new List<object>();

        public event PropertyChangedEventHandler PropertyChanged;

        public QuestionTypeVM(FormQuestionType questionType,
            SectionTypeVM sectionType)
        {
            _sectionType = sectionType;
            _questionType = questionType;
            _validators.Add(new QuestionTypeValidator(this));
        }        

        public FormQuestionType InnerQuestionType => _questionType;

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

        public bool Hidden
        {
            get => _questionType.Hidden ?? false;
            set
            {
                if (_questionType.Hidden != value)
                {
                    _questionType.Hidden = value;
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
