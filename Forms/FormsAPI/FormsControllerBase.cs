using AquaFlaim.CommonAPI;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAPI
{
    public class FormsControllerBase : CommonControllerBase
    {
        protected readonly ISettingsFactory _settingsFactory;
        protected readonly IOptions<Settings> _settings;

        protected FormsControllerBase(
            IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            AquaFlaim.Interface.Log.IMetricService metricService,
            AquaFlaim.Interface.Log.IExceptionService exceptionService)
            : base(metricService, exceptionService)
        {
            _settings = settings;
            _settingsFactory = settingsFactory;
        }

        protected Task WriteMetrics(string eventCode, double? magnitude, Dictionary<string, string> data = null)
            => base.WriteMetrics(_settingsFactory.CreateLog(_settings.Value, GetUserToken()), eventCode, magnitude, data);

        protected Task WriteException(Exception exception)
            => base.WriteException(_settingsFactory.CreateLog(_settings.Value, GetUserToken()), exception);
    }
}
