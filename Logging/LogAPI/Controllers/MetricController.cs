using AquaFlaim.CommonAPI;
using AquaFlaim.CommonCore;
using AquaFlaim.Interface.Log.Models;
using AquaFlaim.Log.Data.Framework;
using AquaFlaim.Log.Data.Framework.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricController : Controller
    {
        private readonly IOptions<Settings> _settings;
        private readonly ISettingsFactory _settingsFactory;
        private readonly IMapper _mapper;
        private readonly IMetricDataFactory _dataFactory;
        private readonly IMetricDataSaver _dataSaver;

        public MetricController(IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            IMapper mapper,
            IMetricDataFactory dataFactory,
            IMetricDataSaver dataSaver)
        {
            _settings = settings;
            _settingsFactory = settingsFactory;
            _mapper = mapper;
            _dataFactory = dataFactory;
            _dataSaver = dataSaver;
        }

        [HttpGet("/api/MetricEventCode")]
        [Authorize(Constants.POLICY_LOG_READ)]
        [ProducesResponseType(typeof(string[]), 200)]
        public async Task<IActionResult> GetEventCodes()
        {
            return Ok(
                await _dataFactory.GetEventCodes(_settingsFactory.CreateData(_settings.Value))
                );
        }

        [HttpGet()]
        [Authorize(Constants.POLICY_LOG_READ)]
        [ProducesResponseType(typeof(Metric[]), 200)]
        public async Task<IActionResult> Get([FromQuery] DateTime? maxTimestamp = null)
        {
            IActionResult result = null;
            if (!maxTimestamp.HasValue)
                maxTimestamp = DateTime.UtcNow;
            maxTimestamp = maxTimestamp.Value.ToUniversalTime();
            IEnumerable<MetricData> innerMetric = await _dataFactory.GetTopMetricsByTimestamp(_settingsFactory.CreateData(_settings.Value), maxTimestamp.Value);
            result = Ok(
                innerMetric.Select<MetricData, Metric>(data => _mapper.Map<Metric>(data))
                );
            return result;
        }

        [HttpPost()]
        [Authorize(Constants.POLICY_LOG_WRITE)]
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
