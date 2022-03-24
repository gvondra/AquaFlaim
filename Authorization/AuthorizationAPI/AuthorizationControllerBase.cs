using LogAPI = AquaFlaim.Interface.Log;
using AquaFlaim.CommonAPI;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace AuthorizationAPI
{
    public abstract class AuthorizationControllerBase : CommonControllerBase
    {
        protected readonly ISettingsFactory _settingsFactory;
        protected readonly IOptions<Settings> _settings;

        protected AuthorizationControllerBase(
            IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            LogAPI.IMetricService metricService)
            : base(metricService)
        { 
            _settings = settings;
            _settingsFactory = settingsFactory;
        }

        protected Task WriteMetrics(string eventCode, double? magnitude, Dictionary<string, string> data = null)
            => base.WriteMetrics(_settingsFactory.CreateLog(_settings.Value, GetUserToken()), eventCode, magnitude, data);
    }
}
