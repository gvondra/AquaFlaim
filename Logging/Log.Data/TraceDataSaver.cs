using AquaFlaim.Log.Data.Framework;
using AquaFlaim.Log.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Log.Data
{
    public class TraceDataSaver : ITraceDataSaver
    {
        private IDbProviderFactory _providerFactory;

        public TraceDataSaver(IDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public async Task Create(ISqlTransactionHandler transactionHandler, TraceData trace)
        {
            if (trace.Manager.GetState(trace) == DataState.New)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, trace);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[lg].[CreateTrace]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter id = DataUtil.CreateParameter(_providerFactory, "id", DbType.Guid);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);

                    DataUtil.AddParameter(_providerFactory, command.Parameters, "eventCode", DbType.AnsiString, trace.EventCode);
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "message", DbType.String, trace.Message);
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "data", DbType.String, trace.Data);
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "timestamp", DbType.DateTime2, trace.Timestamp);

                    await command.ExecuteNonQueryAsync();
                    trace.TraceId = (Guid)id.Value;
                }
            }
        }
    }
}
