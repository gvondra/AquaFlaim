using AquaFlaim.Interface.Forms.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Forms.ViewModel
{
    public class SectionTypeVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private TypeVM _type;
        private readonly FormSectionType _sectionType;
        private readonly ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();
        private readonly List<object> _validators = new List<object>();
        private readonly ObservableCollection<QuestionTypeVM> _questions = new ObservableCollection<QuestionTypeVM>();

        public event PropertyChangedEventHandler PropertyChanged;

        public SectionTypeVM(FormSectionType sectionType,
            TypeVM type)
        {
            _type = type;
            _sectionType = sectionType;
            _validators.Add(new SectionTypeValidator(this));
            if (sectionType.Questions == null)
                sectionType.Questions = new List<FormQuestionType>();
            foreach (FormQuestionType questionType in sectionType.Questions)
            {
                _questions.Add(new QuestionTypeVM(questionType, this));
            }
        }

        public int? FormSectionTypeId => _sectionType.FormSectionTypeId;

        public ObservableCollection<QuestionTypeVM> Questions => _questions;

        public FormSectionType InnerSectionType => _sectionType;
        
        public TypeVM Type => _type;

        public string Title
        {
            get => _sectionType.Title;
            set
            {
                if (_sectionType.Title != value)
                {
                    _sectionType.Title = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool Hidden
        {
            get => _sectionType.Hidden ?? false;
            set
            {
                if (!_sectionType.Hidden.HasValue || _sectionType.Hidden.Value != value)
                {
                    _sectionType.Hidden = value;
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
