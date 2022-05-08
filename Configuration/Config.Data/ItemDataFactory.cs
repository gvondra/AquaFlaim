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
    public class ItemDataFactory : IItemDataFactory
    {
        private readonly ISqlDbProviderFactory _dbProviderFactory;
        private readonly GenericDataFactory<ItemData> _genericDataFactory = new GenericDataFactory<ItemData>();

        public ItemDataFactory(ISqlDbProviderFactory providerFactory)
        {
            _dbProviderFactory = providerFactory;
        }

        private static ItemData Create() => new ItemData();

        public async Task<ItemData> Get(ISqlSettings settings, Guid id)
        {
            IDataParameter[] parameters = new IDataParameter[]
            {
                DataUtil.CreateParameter(_dbProviderFactory, "id", DbType.Guid, DataUtil.GetParameterValue(id))
            };
            return (await _genericDataFactory.GetData(
                settings,
                _dbProviderFactory,
                "[cfg].[GetItem]",
                Create,
                DataUtil.AssignDataStateManager,
                parameters
                )).FirstOrDefault();
        }

        public async Task<IEnumerable<string>> GetAllCodes(ISqlSettings settings, bool includePrivate)
        {
            List<string> codes = new List<string>();
            using (DbConnection connection = await _dbProviderFactory.OpenConnection(settings))
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "[cfg].[GetAllItemCodes]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(
                        DataUtil.CreateParameter(_dbProviderFactory, "includePrivate", DbType.Boolean, DataUtil.GetParameterValue(includePrivate))
                        );
                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        int ordinal = reader.GetOrdinal("code");
                        while (await reader.ReadAsync())
                        {
                            codes.Add(reader.GetFieldValue<string>(ordinal));
                        }
                        reader.Close();
                    }
                }
            }
            return codes;
        }

        public async Task<ItemData> GetByCode(ISqlSettings settings, string code)
        {
            IDataParameter[] parameters = new IDataParameter[]
            {
                DataUtil.CreateParameter(_dbProviderFactory, "code", DbType.AnsiString, DataUtil.GetParameterValue(code))
            };
            return (await _genericDataFactory.GetData(
                settings, 
                _dbProviderFactory, 
                "[cfg].[GetItemByCode]", 
                Create, 
                DataUtil.AssignDataStateManager, 
                parameters
                )).FirstOrDefault();
        }
    }
}
