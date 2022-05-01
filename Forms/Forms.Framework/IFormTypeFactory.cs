using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Framework
{
    public interface IFormTypeFactory
    {
        IFormType Create();
        IFormSectionType CreateSection(IFormType formType);
        IFormQuestionType CreateQuestion(IFormSectionType formSectionType, string code);
        Task<IEnumerable<IFormType>> GetAll(ISettings settings);
        Task<IFormType> Get(ISettings settings, int id);
        Task<IEnumerable<IFormQuestionType>> GetFormQuestionsTypesByFormSectionType(ISettings settings, IFormSectionType formSectionType);
        Task<IEnumerable<IFormQuestionType>> GetFormQuestionsTypesByFormType(ISettings settings, IFormType formType);
        Task<IEnumerable<IFormSectionType>> GetFormSectionsTypesByFormType(ISettings settings, IFormType formType);
    }
}
