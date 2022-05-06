using AquaFlaim.Config.Data.Framework;
using AquaFlaim.Config.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Data
{
    public class LookupDataSaver : ILookupDataSaver
    {
        private readonly ISqlDbProviderFactory _providerFactory;

        public LookupDataSaver(ISqlDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public async Task Create(ISqlTransactionHandler transactionHandler, LookupData lookupData)
        {
            if (lookupData.Manager.GetState(lookupData) == DataState.New)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, lookupData);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[cfg].[CreateLookup]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter id = DataUtil.CreateParameter(_providerFactory, "id", DbType.Guid);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);

                    IDataParameter timestamp = DataUtil.CreateParameter(_providerFactory, "timestamp", DbType.DateTime2);
                    timestamp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(timestamp);

                    AddCommonParameters(command, lookupData);

                    await command.ExecuteNonQueryAsync();
                    lookupData.LookupId = (Guid)id.Value;
                    lookupData.CreateTimestamp = (DateTime)timestamp.Value;
                    lookupData.UpdateTimestamp = (DateTime)timestamp.Value;
                }
            }
        }

        public async Task Update(ISqlTransactionHandler transactionHandler, LookupData lookupData)
        {
            if (lookupData.Manager.GetState(lookupData) == DataState.Updated)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, lookupData);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[cfg].[UpdateLookup]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter timestamp = DataUtil.CreateParameter(_providerFactory, "timestamp", DbType.DateTime2);
                    timestamp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(timestamp);

                    DataUtil.AddParameter(_providerFactory, command.Parameters, "id", DbType.Guid, DataUtil.GetParameterValue(lookupData.LookupId));
                    AddCommonParameters(command, lookupData);

                    await command.ExecuteNonQueryAsync();
                    lookupData.UpdateTimestamp = (DateTime)timestamp.Value;
                }
            }
        }

        private void AddCommonParameters(DbCommand command, LookupData lookupData)
        {
            DataUtil.AddParameter(_providerFactory, command.Parameters, "isPublic", DbType.Boolean, DataUtil.GetParameterValue(lookupData.IsPublic));
            DataUtil.AddParameter(_providerFactory, command.Parameters, "code", DbType.AnsiString, DataUtil.GetParameterValue(lookupData.Code));
            DataUtil.AddParameter(_providerFactory, command.Parameters, "data", DbType.AnsiString, DataUtil.GetParameterValue(lookupData.Data));
        }
    }
}
