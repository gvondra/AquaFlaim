using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Framework
{
    public interface IFormSectionType
    {
        int FormSectionTypeId { get; }
        int FormTypeId { get; }
        string Title { get; set; }
        short Order { get; set; }
        bool Hidden { get; set; }
        DateTime CreateTimestamp { get; }
        DateTime UpdateTimestamp { get; }

        Task Create(ITransactionHandler transactionHandler);
        Task Update(ITransactionHandler transactionHandler);
        IFormQuestionType CreateQuestionType(string code); 
        Task<IEnumerable<IFormQuestionType>> GetFormQuestionTypes(ISettings settings);
        void AddQuestionType(IFormQuestionType questionType);
        // note this doesn't delete the question
        // it just separates it from this section
        // all question must belong to a section, 
        // so after remove the section, and new section 
        // must be assigned
        void RemoveQuestionType(IFormQuestionType questionType);
        IFormQuestionType SetSection(IFormQuestionType formQuestionType);
    }
}
