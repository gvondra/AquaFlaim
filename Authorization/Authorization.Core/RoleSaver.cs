using AquaFlaim.Authorization.Framework;
using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Core
{
    public class RoleSaver : IRoleSaver
    {
        public async Task Create(ISettings settings, IRole role)
        {
            RoleFactory.ClearCache();
            Saver saver = new Saver();
            await saver.Save(new TransactionHandler(settings), role.Create);
        }

        public async Task Update(ISettings settings, IRole role)
        {
            RoleFactory.ClearCache();
            Saver saver = new Saver();
            await saver.Save(new TransactionHandler(settings), role.Update);
        }
    }
}
