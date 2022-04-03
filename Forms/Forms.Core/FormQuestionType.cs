using AquaFlaim.CommonCore;
using AquaFlaim.Forms.Data.Framework;
using AquaFlaim.Forms.Data.Framework.Models;
using AquaFlaim.Forms.Framework;
using AquaFlaim.Forms.Framework.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Core
{
    public class FormQuestionType : IFormQuestionType
    {
        private readonly FormQuestionTypeData _data;
        private readonly IFormQuestionTypeDataSaver _dataSaver;
        private readonly IFormSectionType _sectionType;        

        public FormQuestionType(FormQuestionTypeData data,
            IFormQuestionTypeDataSaver dataSaver,
            IFormSectionType formSectionType)
        {
            _data = data;   
            _dataSaver = dataSaver;
            _sectionType = formSectionType;
        }

        public int FormQuestionTypeId => _data.FormQuestionTypeId;

        public int FormTypeId {  get => _data.FormTypeId; private set => _data.FormTypeId = value; }

        public int FormSectionTypeId { get => _data.FormSectionTypeId; private set => _data.FormSectionTypeId = value; }

        public string Code => _data.Code;

        public string Text { get => _data.Text; set => _data.Text = value; }
        public ResponseType ResponseType { get => (ResponseType)_data.ResponseType; set => _data.ResponseType = (short)value; }
        public List<string> ResponseList 
        {
            get
            {
                if (string.IsNullOrEmpty(_data.ResponseList))
                    return new List<string>();
                else
                    return JsonConvert.DeserializeObject<List<string>>(_data.ResponseList, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
            }
            set
            {
                if (value == null)
                    _data.ResponseList = string.Empty;
                else
                    _data.ResponseList = JsonConvert.SerializeObject(value, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
            }
        }
        public short? ResponseMaxLength { get => _data.ResponseMaxLength; set => _data.ResponseMaxLength = value; }
        public bool IsRequired { get => _data.IsRequired; set => _data.IsRequired = value; }
        public string ResponseValidationExpression { get => _data.ResponseValidationExpression; set => _data.ResponseValidationExpression = value; }
        public bool Hidden { get => _data.Hidden; set => _data.Hidden = value; }
        public short Order { get => _data.Order; set => _data.Order = value; }

        public DateTime CreateTimestamp => _data.CreateTimestamp;

        public DateTime UpdateTimestamp => _data.UpdateTimestamp;

        public Task Create(ITransactionHandler transactionHandler)
        {
            FormTypeId = _sectionType.FormTypeId;
            FormSectionTypeId = _sectionType.FormSectionTypeId;
            return _dataSaver.Create(transactionHandler, _data);
        }
            

        public Task Update(ITransactionHandler transactionHandler)
            => _dataSaver.Update(transactionHandler, _data);
    }
}
