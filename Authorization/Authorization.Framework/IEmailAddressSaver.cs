using AquaFlaim.CommonCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Framework
{
    public interface IEmailAddressSaver
    {
        Task Create(ISettings settings, IEmailAddress emailAddress);
    }
}
