using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Framework
{
    public interface IEmailAddress
    {
        Guid EmailAddressId { get; set; }
        string Address { get; set; }
        DateTime CreateTimestamp { get; set; }
    }
}
