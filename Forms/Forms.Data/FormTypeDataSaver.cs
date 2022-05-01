using AquaFlaim.Forms.Data.Framework;
using AquaFlaim.Forms.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Data
{
    public class FormTypeDataSaver : IFormTypeDataSaver
    {
        private readonly ISqlDbProviderFactory _providerFactory;

        public FormTypeDataSaver(ISqlDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public async Task Create(ISqlTransactionHandler transactionHandler, FormTypeData formTypeData)
        {
            if (formTypeData.Manager.GetState(formTypeData) == DataState.New)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, formTypeData);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[frm].[CreateFormType]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter id = DataUtil.CreateParameter(_providerFactory, "id", DbType.Int32);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);

                    IDataParameter timestamp = DataUtil.CreateParameter(_providerFactory, "timestamp", DbType.DateTime2);
                    timestamp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(timestamp);

                    DataUtil.AddParameter(_providerFactory, command.Parameters, "title", DbType.AnsiString, DataUtil.GetParameterValue(formTypeData.Title));

                    await command.ExecuteNonQueryAsync();
                    formTypeData.FormTypeId = (int)id.Value;
                    formTypeData.CreateTimestamp = (DateTime)timestamp.Value;
                    formTypeData.UpdateTimestamp = (DateTime)timestamp.Value;
                }
            }
        }

        public async Task Update(ISqlTransactionHandler transactionHandler, FormTypeData formTypeData)
        {
            if (formTypeData.Manager.GetState(formTypeData) == DataState.Updated)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, formTypeData);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[frm].[UpdateFormType]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter timestamp = DataUtil.CreateParameter(_providerFactory, "timestamp", DbType.DateTime2);
                    timestamp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(timestamp);

                    DataUtil.AddParameter(_providerFactory, command.Parameters, "id", DbType.Int32, DataUtil.GetParameterValue(formTypeData.FormTypeId));
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "title", DbType.AnsiString, DataUtil.GetParameterValue(formTypeData.Title));

                    await command.ExecuteNonQueryAsync();
                    formTypeData.UpdateTimestamp = (DateTime)timestamp.Value;
                }
            }
        }
    }
}
