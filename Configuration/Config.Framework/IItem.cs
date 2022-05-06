using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Framework
{
    public interface IItem
    {
        Guid ItemId { get; }
        bool IsPublic { get; set; }
        string Code { get; set; }
        dynamic Data { get; set; }
        DateTime CreateTimestamp { get; }
        DateTime UpdateTimestamp { get; }
        Task Create(ITransactionHandler transationHandler);
        Task Update(ITransactionHandler transationHandler);
    }
}
