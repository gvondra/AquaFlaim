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
    public class MetricDataSaver : IMetricDataSaver
    {
        private IDbProviderFactory _providerFactory;

        public MetricDataSaver(IDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public async Task Create(ISqlTransactionHandler transactionHandler, MetricData metric)
        {
            if (metric.Manager.GetState(metric) == DataState.New)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, metric);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[lg].[CreateMetric]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter id = DataUtil.CreateParameter(_providerFactory, "id", DbType.Guid);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);

                    DataUtil.AddParameter(_providerFactory, command.Parameters, "eventCode", DbType.AnsiString, DataUtil.GetParameterValue(metric.EventCode));
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "magnitude", DbType.Double, DataUtil.GetParameterValue(metric.Magnitude));
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "status", DbType.String, DataUtil.GetParameterValue(metric.Status));
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "data", DbType.String, DataUtil.GetParameterValue(metric.Data));
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "timestamp", DbType.DateTime2, DataUtil.GetParameterValue(metric.Timestamp));

                    await command.ExecuteNonQueryAsync();
                    metric.MetricId = (Guid)id.Value;
                }
            }
        }
    }
}
