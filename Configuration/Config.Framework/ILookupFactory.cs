using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Framework
{
    public interface ILookupFactory
    {
        ILookup Create();
        Task<IEnumerable<string>> GetAllCodes(ISettings settings, bool includePrivate = false);
        Task<ILookup> Get(ISettings settings, Guid id);
        Task<ILookup> GetByCode(ISettings settings, string code);
    }
}
