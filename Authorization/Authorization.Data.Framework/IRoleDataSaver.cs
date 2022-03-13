using AquaFlaim.Authorization.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Data.Framework
{
    public interface IRoleDataSaver
    {
        Task Create(ISqlTransactionHandler transactionHandler, RoleData role);
        Task Update(ISqlTransactionHandler transactionHandler, RoleData role);
    }
}
