using AquaFlaim.CommonCore;
using AquaFlaim.Config.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Core
{
    public class ItemSaver : IItemSaver
    {
        public async Task Create(ISettings settings, IItem item)
        {
            Saver saver = new Saver();
            await saver.Save(new TransactionHandler(settings), item.Create);
        }

        public async Task Update(ISettings settings, IItem item)
        {
            Saver saver = new Saver();
            await saver.Save(new TransactionHandler(settings), item.Update);
        }
    }
}
