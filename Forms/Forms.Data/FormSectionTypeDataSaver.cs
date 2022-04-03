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
    public class FormSectionTypeDataSaver : IFormSectionTypeDataSaver
    {
        private readonly ISqlDbProviderFactory _providerFactory;

        public FormSectionTypeDataSaver(ISqlDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public async Task Create(ISqlTransactionHandler transactionHandler, FormSectionTypeData formSectionTypeData)
        {
            if (formSectionTypeData.Manager.GetState(formSectionTypeData) == DataState.New)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, formSectionTypeData);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[frm].[CreateFormSectionType]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter id = DataUtil.CreateParameter(_providerFactory, "id", DbType.Int32);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);

                    IDataParameter timestamp = DataUtil.CreateParameter(_providerFactory, "timestamp", DbType.DateTime2);
                    timestamp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(timestamp);

                    DataUtil.AddParameter(_providerFactory, command.Parameters, "formTypeId", DbType.Int32, formSectionTypeData.FormTypeId);
                    AddCommonParameters(command, formSectionTypeData);

                    await command.ExecuteNonQueryAsync();
                    formSectionTypeData.FormSectionTypeId = (int)id.Value;
                    formSectionTypeData.CreateTimestamp = (DateTime)timestamp.Value;
                    formSectionTypeData.UpdateTimestamp = (DateTime)timestamp.Value;
                }
            }
        }

        public async Task Update(ISqlTransactionHandler transactionHandler, FormSectionTypeData formSectionTypeData)
        {
            if (formSectionTypeData.Manager.GetState(formSectionTypeData) == DataState.Updated)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, formSectionTypeData);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[frm].[UpdateFormSectionType]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter timestamp = DataUtil.CreateParameter(_providerFactory, "timestamp", DbType.DateTime2);
                    timestamp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(timestamp);

                    DataUtil.AddParameter(_providerFactory, command.Parameters, "id", DbType.Int32, formSectionTypeData.FormSectionTypeId);
                    AddCommonParameters(command, formSectionTypeData);

                    await command.ExecuteNonQueryAsync();
                    formSectionTypeData.UpdateTimestamp = (DateTime)timestamp.Value;
                }
            }
        }

        private void AddCommonParameters(DbCommand command, FormSectionTypeData formSectionTypeData)
        {
            DataUtil.AddParameter(_providerFactory, command.Parameters, "title", DbType.AnsiString, formSectionTypeData.Title);
            DataUtil.AddParameter(_providerFactory, command.Parameters, "order", DbType.Int16, formSectionTypeData.Order);
            DataUtil.AddParameter(_providerFactory, command.Parameters, "hidden", DbType.Boolean, formSectionTypeData.Hidden);
        }
    }
}
