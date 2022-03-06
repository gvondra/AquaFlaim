using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Framework
{
    public interface IUser
    {
        Guid UserId { get; set; }
        string ReferenceId { get; set; }
        Guid EmailAddressId { get; set; }
        string Name { get; set; }
        DateTime CreateTimestamp { get; set; }
        DateTime UpdateTimestamp { get; set; }
    }
}
