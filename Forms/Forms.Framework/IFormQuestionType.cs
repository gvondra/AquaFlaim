using AquaFlaim.CommonCore;
using AquaFlaim.Forms.Framework.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Framework
{
    public interface IFormQuestionType
    {
        int FormQuestionTypeId { get; }
        int FormTypeId { get; }
        int FormSectionTypeId { get; }
        string Code { get; }
        string Text { get; set; }
        ResponseType ResponseType { get; set; }
        List<string> ResponseList { get; set; }
        short? ResponseMaxLength { get; set; }
        bool IsRequired { get; set; }
        string ResponseValidationExpression { get; set; }
        bool Hidden { get; set; }
        short Order { get; set; }
        DateTime CreateTimestamp { get; }
        DateTime UpdateTimestamp { get; }

        Task Create(ITransactionHandler transactionHandler);
        Task Update(ITransactionHandler transactionHandler);
    }
}
