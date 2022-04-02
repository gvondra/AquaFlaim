using AquaFlaim.CommonAPI;
using AquaFlaim.Interface.Log;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LogAPI
{
    public class LogControllerBase : CommonControllerBase
    {
        protected readonly IOptions<Settings> _settings;
        protected readonly ISettingsFactory _settingsFactory;

        protected LogControllerBase(
            IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            IMetricService metricService,
            IExceptionService exceptionService)
            : base(metricService, exceptionService)
        { 
            _settings = settings;
            _settingsFactory = settingsFactory;
        }

        protected Task WriteException(System.Exception exception)
            => base.WriteException(_settingsFactory.CreateLog(_settings.Value, GetUserToken()), exception);
    }
}
