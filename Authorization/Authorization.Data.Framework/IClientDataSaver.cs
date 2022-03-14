using AquaFlaim.Authorization.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Data.Framework
{
    public interface IClientDataSaver
    {
        Task Create(ISqlTransactionHandler transactionHandler, ClientData client);
        Task Update(ISqlTransactionHandler transactionHandler, ClientData client);
        Task Create(ISqlTransactionHandler transactionHandler, ClientCredentialData clientCredentail);
        Task DeactivateClient(ISqlTransactionHandler transactionHandler, Guid clientId);
    }
}
