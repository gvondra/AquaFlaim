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
    public class FormQuestionTypeDataFactory : IFormQuestionTypeDataFactory
    {
        private readonly ISqlDbProviderFactory _providerFactory;
        private readonly GenericDataFactory<FormQuestionTypeData> _genericDataFactory = new GenericDataFactory<FormQuestionTypeData>();

        public FormQuestionTypeDataFactory(ISqlDbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public Task<IEnumerable<FormQuestionTypeData>> GetByFormSectionTypeId(ISqlSettings settings, int formSectionTypeId)
        {
            List<IDataParameter> parameters = new List<IDataParameter>
            {
                DataUtil.CreateParameter(_providerFactory, "formSectionTypeId", DbType.Int32, formSectionTypeId)
            };
            return _genericDataFactory.GetData(
                settings,
                _providerFactory,
                "[frm].[GetFormQuestionType_by_FormSectionTypeId]",
                () => new FormQuestionTypeData(),
                DataUtil.AssignDataStateManager,
                parameters
                );
        }

        public Task<IEnumerable<FormQuestionTypeData>> GetByFormTypeId(ISqlSettings settings, int formTypeId)
        {
            List<IDataParameter> parameters = new List<IDataParameter>
            {
                DataUtil.CreateParameter(_providerFactory, "formTypeId", DbType.Int32, formTypeId)
            };
            return _genericDataFactory.GetData(
                settings,
                _providerFactory,
                "[frm].[GetFormQuestionType_by_FormTypeId]",
                () => new FormQuestionTypeData(),
                DataUtil.AssignDataStateManager,
                parameters
                );
        }
    }
}
