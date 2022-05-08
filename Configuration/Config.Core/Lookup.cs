using AquaFlaim.CommonCore;
using AquaFlaim.Config.Data.Framework;
using AquaFlaim.Config.Data.Framework.Models;
using AquaFlaim.Config.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Core
{
    public class Lookup : ILookup
    {
        private static JsonSerializerSettings serializeSettings = new JsonSerializerSettings()
        {
            ContractResolver = null,
            Formatting = Formatting.None
        };
        private readonly LookupData _data;
        private readonly ILookupDataSaver _dataSaver;

        public Lookup(LookupData data,
            ILookupDataSaver dataSaver)
        {
            _data = data;
            _dataSaver = dataSaver;
        }

        public Guid LookupId => _data.LookupId;

        public bool IsPublic { get => _data.IsPublic; set => _data.IsPublic = value; }
        public string Code { get => _data.Code.ToLower(); set => _data.Code = value.ToLower(); }

        public Dictionary<string, string> Data 
        { 
            get => !string.IsNullOrEmpty(_data.Data) ? JsonConvert.DeserializeObject<Dictionary<string, string>>(_data.Data, serializeSettings) : new Dictionary<string, string>(); 
            set => _data.Data = value != null ? JsonConvert.SerializeObject(value, serializeSettings) : string.Empty; 
        }

        public DateTime CreateTimestamp => _data.CreateTimestamp;

        public DateTime UpdateTimestamp => _data.UpdateTimestamp;

        public Task Create(ITransactionHandler transationHandler)
            => _dataSaver.Create(transationHandler, _data);

        public Task Update(ITransactionHandler transationHandler)
            => _dataSaver.Update(transationHandler, _data);
    }
}
