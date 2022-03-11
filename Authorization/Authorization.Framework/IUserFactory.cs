using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Framework
{
    public interface IUserFactory
    {
        Task<IUser> Get(ISettings settings, Guid id);
        Task<IUser> GetByReferenceId(ISettings settings, string referenceId);
        Task<IEnumerable<IUser>> GetByEmailAddress(ISettings settings, string emailAddress);
        IUser Create(string referenceId, IEmailAddress emailAddress);
    }
}
