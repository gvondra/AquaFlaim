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
    public class MetricDataFactory : IMetricDataFactory
    {
        private readonly IDbProviderFactory _providerFactory;
        private readonly GenericDataFactory<MetricData> _genericDataFactory;

        public MetricDataFactory(IDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
            _genericDataFactory = new GenericDataFactory<MetricData>();
        }

        protected MetricData CreateData() => new MetricData();

        public async Task<IEnumerable<string>> GetEventCodes(ISqlSettings settings)
        {
            List<string> result = new List<string>();
            using (DbConnection connection = await _providerFactory.OpenConnection(settings))
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "[lg].[GetMetricEvents]";
                command.CommandType = CommandType.StoredProcedure;
                using (DbDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetFieldValue<string>(0)); // expectin the data set to only contain 1 column
                    }
                    reader.Close();
                }
            }
            return result;
        }

        public Task<IEnumerable<MetricData>> GetTopMetricsByTimestamp(ISqlSettings settings, DateTime maxTimestamp)
        {
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(DataUtil.CreateParameter(_providerFactory, "maxTimestamp", DbType.DateTime2, maxTimestamp));
            return _genericDataFactory.GetData(settings,
                _providerFactory,
                "[lg].[GetTopMetricByTimestamp]",
                CreateData,
                DataUtil.AssignDataStateManager,
                parameters);
        }
    }
}
