using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Framework
{
    public interface IFormTypeSaver
    {
        Task Create(ISettings settings, IFormType formType);
        Task Update(ISettings settings, IFormType formType);
    }
}
