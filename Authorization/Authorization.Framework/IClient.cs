using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Framework
{
    public interface IClient
    {
        Guid ClientId { get; set; }
        string Name { get; set; }
        DateTime CreateTimestamp { get; set; }
        DateTime UpdateTimestamp { get; set; }
    }
}
