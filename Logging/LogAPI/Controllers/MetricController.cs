using AquaFlaim.CommonAPI;
using AquaFlaim.CommonCore;
using AquaFlaim.Interface.Log.Models;
using AquaFlaim.Log.Data.Framework;
using AquaFlaim.Log.Data.Framework.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricController : CommonControllerBase
    {
        private readonly IOptions<Settings> _settings;
        private readonly ISettingsFactory _settingsFactory;
        private readonly IMapper _mapper;
        private readonly IMetricDataSaver _dataSaver;

        public MetricController(IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            IMapper mapper,
            IMetricDataSaver dataSaver)
        {
            _settings = settings;
            _settingsFactory = settingsFactory;
            _mapper = mapper;
            _dataSaver = dataSaver;
        }

        [HttpPost()]
        [Authorize()]
        public async Task<IActionResult> Create([FromBody] Metric[] metrics)
        {
            if (metrics != null && metrics.Length > 0)
            {
                MetricData[] metricData = new MetricData[metrics.Length];
                for (int i = 0; i < metrics.Length; i += 1)
                {
                    metricData[i] = _mapper.Map<MetricData>(metrics[i]);
                }
                Saver saver = new Saver();
                await saver.Save(
                    new TransactionHandler(_settingsFactory.CreateCore(_settings.Value)),
                    (th) => Save(th, metricData)
                    );
            }
            return Ok();
        }

        [NonAction]
        private async Task Save(ITransactionHandler transactionHandler, MetricData[] metricData)
        {
            for (int i = 0; i < metricData.Length; i += 1)
            {
                await _dataSaver.Create(transactionHandler, metricData[i]);
            }
        }
    }
}
