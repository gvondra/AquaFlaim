using AquaFlaim.CommonCore;
using AquaFlaim.Config.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Core
{
    public class LookupSaver : ILookupSaver
    {
        public async Task Create(ISettings settings, ILookup lookup)
        {
            Saver saver = new Saver();
            await saver.Save(new TransactionHandler(settings), lookup.Create);
        }

        public async Task Update(ISettings settings, ILookup lookup)
        {
            Saver saver = new Saver();
            await saver.Save(new TransactionHandler(settings), lookup.Update);
        }
    }
}
