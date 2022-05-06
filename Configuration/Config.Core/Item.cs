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
    public class Item : IItem
    {
        private static JsonSerializerSettings serializeSettings = new JsonSerializerSettings()
        {
            ContractResolver = null,
            Formatting = Formatting.None
        };
        private readonly ItemData _data;
        private readonly IItemDataSaver _dataSaver;

        public Item(ItemData data,
            IItemDataSaver dataSaver)
        {
            _data = data;
            _dataSaver = dataSaver;
        }

        public Guid ItemId => _data.ItemId;

        public bool IsPublic { get => _data.IsPublic; set => _data.IsPublic = value; }
        public string Code { get => _data.Code.ToLower(); set => _data.Code = value.ToLower(); }
        public dynamic Data 
        { 
            get => JsonConvert.DeserializeObject(_data.Data, serializeSettings); 
            set => _data.Data = JsonConvert.SerializeObject(value, serializeSettings); 
        }

        public DateTime CreateTimestamp => _data.CreateTimestamp;

        public DateTime UpdateTimestamp => _data.UpdateTimestamp;

        public Task Create(ITransactionHandler transationHandler)
            => _dataSaver.Create(transationHandler, _data);

        public Task Update(ITransactionHandler transationHandler)
            => _dataSaver.Update(transationHandler, _data);
    }
}
