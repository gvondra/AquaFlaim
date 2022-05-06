using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Framework
{
    public interface ILookupSaver
    {
        Task Create(ISettings settings, ILookup lookup);
        Task Update(ISettings settings, ILookup lookup);
    }
}
