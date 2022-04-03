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
    public class FormQuestionTypeDataSaver : IFormQuestionTypeDataSaver
    {
        private readonly ISqlDbProviderFactory _providerFactory;

        public FormQuestionTypeDataSaver(ISqlDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public async Task Create(ISqlTransactionHandler transactionHandler, FormQuestionTypeData formQuestionTypeData)
        {
            if (formQuestionTypeData.Manager.GetState(formQuestionTypeData) == DataState.New)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, formQuestionTypeData);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[frm].[CreateFormQuestionType]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter id = DataUtil.CreateParameter(_providerFactory, "id", DbType.Int32);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);

                    IDataParameter timestamp = DataUtil.CreateParameter(_providerFactory, "timestamp", DbType.DateTime2);
                    timestamp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(timestamp);

                    DataUtil.AddParameter(_providerFactory, command.Parameters, "formTypeId", DbType.Int32, formQuestionTypeData.FormTypeId);
                    AddCommonParameters(command, formQuestionTypeData);

                    await command.ExecuteNonQueryAsync();
                    formQuestionTypeData.FormQuestionTypeId = (int)id.Value;
                    formQuestionTypeData.CreateTimestamp = (DateTime)timestamp.Value;
                    formQuestionTypeData.UpdateTimestamp = (DateTime)timestamp.Value;
                }
            }
        }

        public async Task Update(ISqlTransactionHandler transactionHandler, FormQuestionTypeData formQuestionTypeData)
        {
            if (formQuestionTypeData.Manager.GetState(formQuestionTypeData) == DataState.Updated)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, formQuestionTypeData);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[frm].[UpdateFormQuestionType]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter timestamp = DataUtil.CreateParameter(_providerFactory, "timestamp", DbType.DateTime2);
                    timestamp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(timestamp);

                    DataUtil.AddParameter(_providerFactory, command.Parameters, "id", DbType.Int32, formQuestionTypeData.FormQuestionTypeId);
                    AddCommonParameters(command, formQuestionTypeData);

                    await command.ExecuteNonQueryAsync();
                    formQuestionTypeData.UpdateTimestamp = (DateTime)timestamp.Value;
                }
            }
        }

        private void AddCommonParameters(DbCommand command, FormQuestionTypeData formQuestionTypeData)
        {
            DataUtil.AddParameter(_providerFactory, command.Parameters, "formSectionTypeId", DbType.Int32, formQuestionTypeData.FormSectionTypeId);
            DataUtil.AddParameter(_providerFactory, command.Parameters, "code", DbType.AnsiString, formQuestionTypeData.Code);
            DataUtil.AddParameter(_providerFactory, command.Parameters, "text", DbType.AnsiString, formQuestionTypeData.Text);
            DataUtil.AddParameter(_providerFactory, command.Parameters, "responseType", DbType.Int16, formQuestionTypeData.ResponseType);
            DataUtil.AddParameter(_providerFactory, command.Parameters, "responseList", DbType.AnsiString, formQuestionTypeData.ResponseList);
            DataUtil.AddParameter(_providerFactory, command.Parameters, "responseMaxLength", DbType.Int16, formQuestionTypeData.ResponseMaxLength);
            DataUtil.AddParameter(_providerFactory, command.Parameters, "isRequired", DbType.Boolean, formQuestionTypeData.IsRequired);
            DataUtil.AddParameter(_providerFactory, command.Parameters, "responseValidationExpression", DbType.AnsiString, formQuestionTypeData.ResponseValidationExpression);
            DataUtil.AddParameter(_providerFactory, command.Parameters, "hidden", DbType.Boolean, formQuestionTypeData.Hidden);
            DataUtil.AddParameter(_providerFactory, command.Parameters, "order", DbType.Int16, formQuestionTypeData.Order);
        }
    }
}
