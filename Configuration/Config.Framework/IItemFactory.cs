using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Framework
{
    public interface IItemFactory
    {
        IItem Create();
        Task<IEnumerable<string>> GetAllCodes(ISettings settings, bool includePrivate = false);
        Task<IItem> Get(ISettings settings, Guid id);
        Task<IItem> GetByCode(ISettings settings, string code);
    }
}
