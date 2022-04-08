using InterfaceAuthorization = AquaFlaim.Interface.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using AquaFlaim.Interface.Log;

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
                BaseAddress = Settings.AuthorizationApiBaseAddress,
                Token = token
            };
        }

        public ISettings CreateLog(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));
            return new LogSettings
            {
                BaseAddress = Settings.LogApiBaseAddress,
                Token = token
            };
        }

        public ISettings CreateLog()
        {
            if (string.IsNullOrEmpty(AccessToken.Token))
                throw new ArgumentNullException("Access Token");
            return CreateLog(AccessToken.Token);
        }

        public AquaFlaim.Interface.Forms.ISettings CreateForms()
        {
            if (string.IsNullOrEmpty(AccessToken.Token))
                throw new ArgumentNullException("Access Token");
            return CreateForms(AccessToken.Token);
        }

        public AquaFlaim.Interface.Forms.ISettings CreateForms(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));
            return new FormsSettings()
            {
                BaseAddress = Settings.FormsApiBaseAddress,
                Token = token
            };
        }
    }
}
