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
    public class ItemDataSaver : IItemDataSaver
    {
        private readonly ISqlDbProviderFactory _providerFactory;

        public ItemDataSaver(ISqlDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public async Task Create(ISqlTransactionHandler transactionHandler, ItemData itemData)
        {
            if (itemData.Manager.GetState(itemData) == DataState.New)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, itemData);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[cfg].[CreateItem]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter id = DataUtil.CreateParameter(_providerFactory, "id", DbType.Guid);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);

                    IDataParameter timestamp = DataUtil.CreateParameter(_providerFactory, "timestamp", DbType.DateTime2);
                    timestamp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(timestamp);

                    AddCommonParameters(command, itemData);

                    await command.ExecuteNonQueryAsync();
                    itemData.ItemId = (Guid)id.Value;
                    itemData.CreateTimestamp = (DateTime)timestamp.Value;
                    itemData.UpdateTimestamp = (DateTime)timestamp.Value;
                }
            }
        }

        public async Task Update(ISqlTransactionHandler transactionHandler, ItemData itemData)
        {
            if (itemData.Manager.GetState(itemData) == DataState.Updated)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, itemData);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[cfg].[UpdateItem]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter timestamp = DataUtil.CreateParameter(_providerFactory, "timestamp", DbType.DateTime2);
                    timestamp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(timestamp);

                    DataUtil.AddParameter(_providerFactory, command.Parameters, "id", DbType.Guid, DataUtil.GetParameterValue(itemData.ItemId));
                    AddCommonParameters(command, itemData);

                    await command.ExecuteNonQueryAsync();
                    itemData.UpdateTimestamp = (DateTime)timestamp.Value;
                }
            }
        }

        private void AddCommonParameters(DbCommand command, ItemData itemData)
        {
            DataUtil.AddParameter(_providerFactory, command.Parameters, "isPublic", DbType.Boolean, DataUtil.GetParameterValue(itemData.IsPublic));
            DataUtil.AddParameter(_providerFactory, command.Parameters, "code", DbType.AnsiString, DataUtil.GetParameterValue(itemData.Code));
            DataUtil.AddParameter(_providerFactory, command.Parameters, "data", DbType.AnsiString, DataUtil.GetParameterValue(itemData.Data));
        }
    }
}
