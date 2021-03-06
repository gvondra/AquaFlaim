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
        AquaFlaim.Interface.Forms.ISettings CreateForms();
        AquaFlaim.Interface.Forms.ISettings CreateForms(string token);
        AquaFlaim.Interface.Configuration.ISettings CreateConfiguration();
        AquaFlaim.Interface.Configuration.ISettings CreateConfiguration(string token);
    }
}
