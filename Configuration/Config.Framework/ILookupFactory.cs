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
        Task<IEnumerable<string>> GetAllCodes(ISettings settings, bool includePrivate = false);
        Task<ILookup> GetByCode(ISettings settings, string code);
    }
}
