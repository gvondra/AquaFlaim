using AquaFlaim.CommonAPI;
using AquaFlaim.CommonCore;
using AquaFlaim.Interface.Log;
using AquaFlaim.Interface.Log.Models;
using AquaFlaim.Log.Data.Framework;
using AquaFlaim.Log.Data.Framework.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraceController : LogControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITraceDataSaver _dataSaver;

        public TraceController(IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            IMetricService metricService,
            IExceptionService exceptionService,
            IMapper mapper,
            ITraceDataSaver dataSaver)
            : base(settings, settingsFactory, metricService, exceptionService)
        {
            _mapper = mapper;
            _dataSaver = dataSaver;
        }

        [HttpPost()]
        [Authorize(Constants.POLICY_LOG_WRITE)]
        public async Task<IActionResult> Create([FromBody] Trace[] traces)
        {
            try
            {
                if (traces != null && traces.Length > 0)
                {
                    TraceData[] traceData = new TraceData[traces.Length];
                    for (int i = 0; i < traces.Length; i += 1)
                    {
                        traceData[i] = _mapper.Map<TraceData>(traces[i]);
                    }
                    Saver saver = new Saver();
                    await saver.Save(
                        new TransactionHandler(_settingsFactory.CreateCore(_settings.Value)),
                        (th) => Save(th, traceData)
                        );
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                await WriteException(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [NonAction]
        private async Task Save(ITransactionHandler transactionHandler, TraceData[] traceData)
        {
            for (int i = 0; i < traceData.Length; i += 1)
            {
                await _dataSaver.Create(transactionHandler, traceData[i]);
            }
        }
    }
}
