using AquaFlaim.Log.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Log.Data.Framework
{
    public interface ITraceDataSaver
    {
        Task Create(ISqlTransactionHandler transactionHandler, TraceData trace);
    }
}
