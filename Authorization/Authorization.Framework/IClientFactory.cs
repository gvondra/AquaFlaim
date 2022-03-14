using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Framework
{
    public interface IClientFactory
    {
        IClient Create(string secret);
        Task<IClient> Get(ISettings settings, Guid id);
        Task<IEnumerable<IClient>> GetAll(ISettings settings);
    }
}
