using AquaFlaim.Config.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Data.Framework
{
    public interface IItemDataSaver
    {
        Task Create(ISqlTransactionHandler transactionHandler, ItemData itemData);
        Task Update(ISqlTransactionHandler transactionHandler, ItemData itemData);
    }
}
