using AquaFlaim.Authorization.Framework;
using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Core
{
    public class UserSaver : IUserSaver
    {
        public Task Create(ISettings settings, IUser user)
        {
            Saver saver = new Saver();
            return saver.Save(new TransactionHandler(settings), user.Create);
        }

        public Task Update(ISettings settings, IUser user)
        {
            Saver saver = new Saver();
            return saver.Save(new TransactionHandler(settings), user.Update);
        }
    }
}
