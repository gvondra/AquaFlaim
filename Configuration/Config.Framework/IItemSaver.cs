using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Framework
{
    public interface IItemSaver
    {
        Task Create(ISettings settings, IItem item);
        Task Update(ISettings settings, IItem item);
    }
}
