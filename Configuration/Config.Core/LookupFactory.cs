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
    public class LookupFactory : ILookupFactory
    {
        private readonly ILookupDataFactory _dataFactory;
        private readonly ILookupDataSaver _dataSaver;

        public LookupFactory(ILookupDataFactory dataFactory,
            ILookupDataSaver lookupDataSaver)
        {
            _dataFactory = dataFactory;
            _dataSaver = lookupDataSaver;
        }

        private Lookup Create(LookupData data) => new Lookup(data, _dataSaver);

        public Task<IEnumerable<string>> GetAllCodes(ISettings settings, bool includePrivate = false)
        {
            return _dataFactory.GetAllCodes(new DataSettings(settings), includePrivate);
        }

        public async Task<ILookup> GetByCode(ISettings settings, string code)
        {
            Lookup lookup = null;
            LookupData data = await _dataFactory.GetByCode(new DataSettings(settings), code);
            if (data != null)
                lookup = Create(data);
            return lookup;
        }
    }
}
