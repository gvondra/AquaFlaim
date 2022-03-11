using AquaFlaim.Authorization.Framework;
using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Core
{
    public class EmailAddressSaver : IEmailAddressSaver
    {        
        public Task Create(ISettings settings, IEmailAddress emailAddress)
        {
            Saver saver = new Saver();
            return saver.Save(new TransactionHandler(settings), emailAddress.Create);
        }
    }
}
