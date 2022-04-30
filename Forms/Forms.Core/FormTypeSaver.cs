using AquaFlaim.CommonCore;
using AquaFlaim.Forms.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Core
{
    public class FormTypeSaver : IFormTypeSaver
    {
        public async Task Create(ISettings settings, IFormType formType)
        {
            Saver saver = new Saver();
            await saver.Save(new TransactionHandler(settings), formType.Create);
        }

        public async Task Update(ISettings settings, IFormType formType)
        {
            Saver saver = new Saver();
            await saver.Save(new TransactionHandler(settings), formType.Update);
        }
    }
}
