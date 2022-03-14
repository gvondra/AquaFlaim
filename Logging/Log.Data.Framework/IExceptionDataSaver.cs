using AquaFlaim.Log.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Log.Data.Framework
{
    public interface IExceptionDataSaver
    {
        Task Create(ISqlTransactionHandler transactionHandler, ExceptionData exception);
    }
}
