using AquaFlaim.Forms.Data.Framework;
using AquaFlaim.Forms.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Data
{
    public class FormTypeDataFactory : IFormTypeDataFactory
    {
        private readonly ISqlDbProviderFactory _providerFactory;
        private readonly GenericDataFactory<FormTypeData> _genericDataFactory = new GenericDataFactory<FormTypeData>();

        public FormTypeDataFactory(ISqlDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public async Task<FormTypeData> Get(ISqlSettings settings, int id)
        {
            List<IDataParameter> parameters = new List<IDataParameter>
            {
                DataUtil.CreateParameter(_providerFactory, "id", DbType.Int32, id)
            };
            return (await _genericDataFactory.GetData(
                settings,
                _providerFactory,
                "[frm].[GetFormType]",
                () => new FormTypeData(),
                DataUtil.AssignDataStateManager,
                parameters
                )).FirstOrDefault();
        }

        public Task<IEnumerable<FormTypeData>> GetAll(ISqlSettings settings)
        {
            return _genericDataFactory.GetData(
                settings, 
                _providerFactory,
                "[frm].[GetFormTypeAll]", 
                () => new FormTypeData(),
                DataUtil.AssignDataStateManager
                );
        }
    }
}
