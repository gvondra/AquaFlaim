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
    public class FormType : IFormType
    {
        private readonly FormTypeData _data;
        private readonly IFormTypeDataSaver _dataSaver;
        private readonly IFormTypeFactory _factory;
        private List<IFormSectionType> _sections;
        private List<IFormSectionType> _newSections;

        public FormType(FormTypeData data,
            IFormTypeDataSaver dataSaver,
            IFormTypeFactory factory)
        {
            _data = data;   
            _dataSaver = dataSaver;
            _factory = factory;
        }

        public int FormTypeId => _data.FormTypeId;

        public string Title { get => _data.Title; set => _data.Title = value; }

        public DateTime CreateTimestamp => _data.CreateTimestamp;

        public DateTime UpdateTimestamp => _data.UpdateTimestamp;

        public void AddSectionType(IFormSectionType sectionType)
        {
            if (_newSections == null)
                _newSections = new List<IFormSectionType>();
            _newSections.Add(sectionType);
        }

        public async Task Create(ITransactionHandler transactionHandler)
        {
            await _dataSaver.Create(transactionHandler, _data);
            await SaveSections(transactionHandler);
        }

        public IFormSectionType CreateSectionType()
        {
            return _factory.CreateSection(this);
        }

        public Task<IEnumerable<IFormQuestionType>> GetFormQuestions(ISettings settings)
        {
            return _factory.GetFormQuestionsTypesByFormType(settings, this);
        }

        public async Task<IEnumerable<IFormSectionType>> GetFormSections(ISettings settings)
        {
            if (_sections == null)
                _sections = (await _factory.GetFormSectionsTypesByFormType(settings, this)).ToList();
            IEnumerable<IFormSectionType> result = _sections ?? new List<IFormSectionType>();
            if (_newSections != null)
                result = result.Concat(_newSections);
            return result.OrderBy(q => q.Order);
        }

        public async Task Update(ITransactionHandler transactionHandler)
        {
            await _dataSaver.Update(transactionHandler, _data);
            await SaveSections(transactionHandler);
        }

        private async Task SaveSections(ITransactionHandler transactionHandler)
        {
            if (_sections != null)
            {
                foreach (IFormSectionType sectionType in _sections)
                {
                    await sectionType.Update(transactionHandler);
                }
            }
            if (_newSections != null)
            {
                foreach (IFormSectionType sectionType in _newSections)
                {
                    await sectionType.Create(transactionHandler);
                }
            }
            _sections = null;
            _newSections = null;        
        }
    }
}
