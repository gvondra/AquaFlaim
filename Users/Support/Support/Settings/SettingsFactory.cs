using InterfaceAuthorization = AquaFlaim.Interface.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace AquaFlaim.User.Support
{
    public class SettingsFactory : ISettingsFactory
    {
        public InterfaceAuthorization.ISettings CreateAuthorization()
        {
            if (string.IsNullOrEmpty(AccessToken.Token))
                throw new ArgumentNullException("Access Token");
            return CreateAuthorization(AccessToken.Token);
        }

        public InterfaceAuthorization.ISettings CreateAuthorization(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));
            return new AuthorizationSettings
            {
                BaseAddress = Properties.Settings.Default.AuthorizationApiBaseAddress,
                Token = token
            };
        }
    }
}
