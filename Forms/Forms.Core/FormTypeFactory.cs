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
    public class FormTypeFactory : IFormTypeFactory
    {
        private readonly IFormTypeDataFactory _dataFactory;
        private readonly IFormSectionTypeDataFactory _sectionTypeDataFactory;
        private readonly IFormQuestionTypeDataFactory _questionTypeDataFactory;
        private readonly IFormTypeDataSaver _dataSaver;
        private readonly IFormQuestionTypeDataSaver _questionTypeDataSaver;
        private readonly IFormSectionTypeDataSaver _sectionTypeDataSaver;

        public FormTypeFactory(IFormTypeDataFactory dataFactory,
            IFormSectionTypeDataFactory sectionTypeDataFactory,
            IFormQuestionTypeDataFactory questionTypeDataFactory,
            IFormTypeDataSaver dataSaver,
            IFormQuestionTypeDataSaver questionTypeDataSaver,
            IFormSectionTypeDataSaver sectionTypeDataSaver)
        {
            _dataFactory = dataFactory;
            _sectionTypeDataFactory = sectionTypeDataFactory;
            _questionTypeDataFactory = questionTypeDataFactory;
            _dataSaver = dataSaver;
            _questionTypeDataSaver = questionTypeDataSaver;
            _sectionTypeDataSaver = sectionTypeDataSaver;
        }

        private FormType Create(FormTypeData data) => new FormType(data, _dataSaver, this);
        private FormQuestionType Create(FormQuestionTypeData data, IFormSectionType sectionType) => new FormQuestionType(data, _questionTypeDataSaver, sectionType);
        private FormQuestionType Create(FormQuestionTypeData data) => new FormQuestionType(data, _questionTypeDataSaver);
        private FormSectionType Create(FormSectionTypeData data, IFormType formType) => new FormSectionType(data, _sectionTypeDataSaver, formType, this);

        public IFormType Create() => Create(new FormTypeData());

        public IFormQuestionType CreateQuestion(IFormSectionType formSectionType, string code)
        {
            return Create(
                new FormQuestionTypeData { Code = code },
                formSectionType
                );
        }

        public IFormSectionType CreateSection(IFormType formType) => Create(new FormSectionTypeData(), formType);

        public async Task<IFormType> Get(ISettings settings, int id)
        {
            FormType result = null;
            FormTypeData data = await _dataFactory.Get(new DataSettings(settings), id);
            if (data != null)
                result = Create(data);
            return result;
        }

        public async Task<IEnumerable<IFormType>> GetAll(ISettings settings)
        {
            return (await _dataFactory.GetAll(new DataSettings(settings)))
                .Select<FormTypeData, IFormType>(data => Create(data));
        }

        public async Task<IEnumerable<IFormQuestionType>> GetFormQuestionsTypesByFormSectionType(ISettings settings, IFormSectionType formSectionType)
        {
            return (await _questionTypeDataFactory.GetByFormSectionTypeId(new DataSettings(settings), formSectionType.FormSectionTypeId))
                .Select<FormQuestionTypeData, IFormQuestionType>(data => Create(data, formSectionType));
        }

        public async Task<IEnumerable<IFormSectionType>> GetFormSectionsTypesByFormType(ISettings settings, IFormType formType)
        {
            return (await _sectionTypeDataFactory.GetByFormTypeId(new DataSettings(settings), formType.FormTypeId))
                .Select<FormSectionTypeData, IFormSectionType>(data => Create(data, formType));
        }

        public async Task<IEnumerable<IFormQuestionType>> GetFormQuestionsTypesByFormType(ISettings settings, IFormType formType)
        {
            return (await _questionTypeDataFactory.GetByFormTypeId(new DataSettings(settings), formType.FormTypeId))
                .Select<FormQuestionTypeData, IFormQuestionType>(data => Create(data));
        }
    }
}
