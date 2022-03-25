using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support
{
    public interface ISettingsFactory
    {
        AquaFlaim.Interface.Authorization.ISettings CreateAuthorization();
        AquaFlaim.Interface.Authorization.ISettings CreateAuthorization(string token);
        AquaFlaim.Interface.Log.ISettings CreateLog();
        AquaFlaim.Interface.Log.ISettings CreateLog(string token);
    }
}
