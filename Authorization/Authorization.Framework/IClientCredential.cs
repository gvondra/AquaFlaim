using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Framework
{
    public interface IClientCredential
    {
        Guid ClientCredentialId { get; set; }
        Guid ClientId { get; set; }
        byte[] Secret { get; set; }
        bool IsActive { get; set; }
        DateTime CreateTimestamp { get; set; }
        DateTime UpdateTimestamp { get; set; }
    }
}
