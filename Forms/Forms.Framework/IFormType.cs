using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Framework
{
    public interface IFormType
    {
        int FormTypeId { get; }
        string Title { get; set; }
        DateTime CreateTimestamp { get; }
        DateTime UpdateTimestamp { get; }

        Task Create(ITransactionHandler transactionHandler);
        Task Update(ITransactionHandler transactionHandler);
        Task<IEnumerable<IFormSectionType>> GetFormSections(ISettings settings);        
        Task<IEnumerable<IFormQuestionType>> GetFormQuestions(ISettings settings);
        IFormSectionType CreateSectionType();
        void AddSectionType(IFormSectionType sectionType);
    }
}
