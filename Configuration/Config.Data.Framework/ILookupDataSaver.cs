using AquaFlaim.Config.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Data.Framework
{
    public interface ILookupDataSaver
    {
        Task Create(ISqlTransactionHandler transactionHandler, LookupData lookupData);
        Task Update(ISqlTransactionHandler transactionHandler, LookupData lookupData);
    }
}
