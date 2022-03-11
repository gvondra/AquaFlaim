using AquaFlaim.Authorization.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Data.Framework
{
    public interface IUserDataFactory
    {
        Task<UserData> Get(ISqlSettings settings, Guid id);
        Task<UserData> GetByReferenceId(ISqlSettings settings, string referenceId);
        Task<IEnumerable<UserData>> GetByEmailAddress(ISqlSettings settings, string emailAddress);
    }
}
