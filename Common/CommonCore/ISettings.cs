using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.CommonCore
{
    public interface ISettings
    {
        Task<string> GetConnetionString();
        Func<Task<string>> GetDatabaseAccessToken();
    }
}
