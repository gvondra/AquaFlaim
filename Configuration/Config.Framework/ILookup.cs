using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Framework
{
    public interface ILookup
    {
        Guid LookupId { get; }
        bool IsPublic { get; set; }
        string Code { get; set; }
        Dictionary<string, string> Data { get; set; }
        DateTime CreateTimestamp { get; }
        DateTime UpdateTimestamp { get; }
        Task Create(ITransactionHandler transationHandler);
        Task Update(ITransactionHandler transationHandler);
    }
}
