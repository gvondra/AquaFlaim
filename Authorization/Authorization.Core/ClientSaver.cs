using AquaFlaim.Authorization.Framework;
using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Core
{
    public class ClientSaver : IClientSaver
    {
        public async Task Create(ISettings settings, IClient client)
        {
            Saver saver = new Saver();
            await saver.Save(new TransactionHandler(settings), client.Create);
        }

        public async Task Update(ISettings settings, IClient client)
        {
            Saver saver = new Saver();
            await saver.Save(new TransactionHandler(settings), client.Update);
        }
    }
}
