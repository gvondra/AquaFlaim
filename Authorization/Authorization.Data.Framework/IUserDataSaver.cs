using AquaFlaim.Authorization.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Data.Framework
{
    public interface IUserDataSaver
    {
        Task Create(ISqlTransactionHandler transactionHandler, UserData user);
        Task Update(ISqlTransactionHandler transactionHandler, UserData user);
    }
}
