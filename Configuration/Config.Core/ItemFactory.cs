using AquaFlaim.CommonCore;
using AquaFlaim.Config.Data.Framework;
using AquaFlaim.Config.Data.Framework.Models;
using AquaFlaim.Config.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Core
{
    public class ItemFactory : IItemFactory
    {
        private readonly IItemDataFactory _dataFactory;
        private readonly IItemDataSaver _dataSaver;

        public ItemFactory(IItemDataFactory dataFactory,
            IItemDataSaver dataSaver)
        {
            _dataFactory = dataFactory;
            _dataSaver = dataSaver;
        }

        private Item Create(ItemData data) => new Item(data, _dataSaver);

        public Task<IEnumerable<string>> GetAllCodes(ISettings settings, bool includePrivate = false)
        {
            return _dataFactory.GetAllCodes(new DataSettings(settings), includePrivate);
        }

        public async Task<IItem> GetByCode(ISettings settings, string code)
        {
            Item item = null;
            ItemData data = await _dataFactory.GetByCode(new DataSettings(settings), code);
            if (data != null)
                item = Create(data);
            return item;
        }

        public IItem Create() => Create(new ItemData() { IsPublic = false });

        public async Task<IItem> Get(ISettings settings, Guid id)
        {
            Item item = null;
            ItemData data = await _dataFactory.Get(new DataSettings(settings), id);
            if (data != null)
                item = Create(data);
            return item;
        }
    }
}
