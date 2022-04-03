using AquaFlaim.CommonCore;
using AquaFlaim.Forms.Data.Framework;
using AquaFlaim.Forms.Data.Framework.Models;
using AquaFlaim.Forms.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Core
{
    public class FormSectionType : IFormSectionType
    {
        private readonly FormSectionTypeData _data;
        private readonly IFormSectionTypeDataSaver _dataSaver;
        private readonly IFormType _formType;
        private readonly IFormTypeFactory _factory;
        private List<IFormQuestionType> _questions;
        private List<IFormQuestionType> _newQuestions;

        public FormSectionType(FormSectionTypeData data,
            IFormSectionTypeDataSaver dataSaver,
            IFormType formType,
            IFormTypeFactory factory)
        {
            _data = data;
            _dataSaver = dataSaver;
            _formType = formType;
            _factory = factory;
        }

        public int FormSectionTypeId => _data.FormSectionTypeId;

        public int FormTypeId { get => _data.FormTypeId; private set => _data.FormTypeId = value; }

        public string Title { get => _data.Title; set => _data.Title = value; }
        public short Order { get => _data.Order; set => _data.Order = value; }
        public bool Hidden { get => _data.Hidden; set => _data.Hidden = value; }

        public DateTime CreateTimestamp => _data.CreateTimestamp;

        public DateTime UpdateTimestamp => _data.UpdateTimestamp;

        public void AddQuestionType(IFormQuestionType questionType)
        {
            if (_newQuestions == null)
                _newQuestions = new List<IFormQuestionType>();
            _newQuestions.Add(questionType);
        }

        public async Task Create(ITransactionHandler transactionHandler)
        {
            FormTypeId = _formType.FormTypeId;
            await _dataSaver.Create(transactionHandler, _data);
            await SaveQuestions(transactionHandler);
        }

        public async Task<IEnumerable<IFormQuestionType>> GetFormQuestionTypes(ISettings settings)
        {
            if (_questions == null)
                _questions = (await _factory.GetFormQuestionsTypesByFormSectionType(settings, this)).ToList();
            IEnumerable<IFormQuestionType> result = _questions ?? new List<IFormQuestionType>();
            if (_newQuestions != null)
                result = result.Concat(_newQuestions);
            return result.OrderBy(q => q.Order);
        }

        public async Task Update(ITransactionHandler transactionHandler)
        {
            await _dataSaver.Update(transactionHandler, _data);
            await SaveQuestions(transactionHandler);
        } 

        public IFormQuestionType CreateQuestionType(string code)
        {
            return _factory.CreateQuestion(this, code);
        }

        private async Task SaveQuestions(ITransactionHandler transactionHandler)
        {
            if (_questions != null)
            {
                foreach (IFormQuestionType questionType in _questions)
                {
                    await questionType.Update(transactionHandler);
                }
            }
            if (_newQuestions != null)
            {
                foreach (IFormQuestionType questionType in _newQuestions)
                {
                    await questionType.Create(transactionHandler);
                }
            }
            _questions = null;
            _newQuestions = null;
        }
    }
}
