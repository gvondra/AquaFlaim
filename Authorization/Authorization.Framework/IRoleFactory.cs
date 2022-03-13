using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Framework
{
    public interface IRoleFactory
    {
        IRole Create(string policyName);
        Task<IRole> Get(ISettings settings, int id);
        Task<IEnumerable<IRole>> GetAll(ISettings settings);
        Task<IEnumerable<IRole>> GetByUserId(ISettings settings, Guid userId);
    }
}
