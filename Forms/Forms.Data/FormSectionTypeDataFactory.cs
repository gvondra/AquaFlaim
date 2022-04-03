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
    public class FormSectionTypeDataFactory : IFormSectionTypeDataFactory
    {
        private readonly ISqlDbProviderFactory _providerFactory;
        private readonly GenericDataFactory<FormSectionTypeData> _genericDataFactory = new GenericDataFactory<FormSectionTypeData>();

        public FormSectionTypeDataFactory(ISqlDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public Task<IEnumerable<FormSectionTypeData>> GetByFormTypeId(ISqlSettings settings, int formTypeId)
        {
            List<IDataParameter> parameters = new List<IDataParameter>
            {
                DataUtil.CreateParameter(_providerFactory, "formTypeId", DbType.Int32, formTypeId)
            };
            return _genericDataFactory.GetData(
                settings,
                _providerFactory,
                "[frm].[GetFormSectionType_by_FormTypeId]",
                () => new FormSectionTypeData(),
                DataUtil.AssignDataStateManager,
                parameters
                );
        }
    }
}
