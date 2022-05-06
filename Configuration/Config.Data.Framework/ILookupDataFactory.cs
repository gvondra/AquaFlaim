using AquaFlaim.Config.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Data.Framework
{
    public interface ILookupDataFactory
    {
        Task<IEnumerable<string>> GetAllCodes(ISqlSettings settings, bool includePrivate);
        Task<LookupData> GetByCode(ISqlSettings settings, string code);
        Task<LookupData> Get(ISqlSettings settings, Guid id);
    }
}
