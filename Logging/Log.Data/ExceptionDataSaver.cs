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
    public class ExceptionDataSaver : IExceptionDataSaver
    {
        private IDbProviderFactory _providerFactory;

        public ExceptionDataSaver(IDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public async Task Create(ISqlTransactionHandler transactionHandler, ExceptionData exception)
        {
            if (exception.Manager.GetState(exception) == DataState.New)
            {
                await _providerFactory.EstablishTransaction(transactionHandler, exception);
                using (DbCommand command = transactionHandler.Connection.CreateCommand())
                {
                    command.CommandText = "[lg].[CreateException]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transactionHandler.Transaction.InnerTransaction;

                    IDataParameter id = DataUtil.CreateParameter(_providerFactory, "id", DbType.Guid);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);

                    DataUtil.AddParameter(_providerFactory, command.Parameters, "parentExceptionId", DbType.Guid, exception.ParentExceptionId);
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "message", DbType.String, exception.Message);
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "typeName", DbType.String, exception.TypeName);
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "source", DbType.String, exception.Source);
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "appDomain", DbType.String, exception.AppDomain);
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "targetSite", DbType.String, exception.TargetSite);
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "stackTrace", DbType.String, exception.StackTrace);
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "data", DbType.String, exception.Data);
                    DataUtil.AddParameter(_providerFactory, command.Parameters, "timestamp", DbType.DateTime2, exception.Timestamp);

                    await command.ExecuteNonQueryAsync();
                    exception.ExceptionId = (Guid)id.Value;
                }
            }
        }
    }
}
