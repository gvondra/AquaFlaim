using AquaFlaim.Interface.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Configuration
{
    public interface ILookupService
    {
        Task<IEnumerable<string>> GetCodes(ISettings settings);
        Task<Lookup> GetPublicByCode(ISettings settings, string code);
        Task<Lookup> GetByCode(ISettings settings, string code);
        Task<Lookup> Create(ISettings settings, Lookup lookup);
        Task<Lookup> Update(ISettings settings, Lookup lookup);
    }
}
