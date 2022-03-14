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
    public class TraceController : CommonControllerBase
    {
        private readonly IOptions<Settings> _settings;
        private readonly ISettingsFactory _settingsFactory;
        private readonly IMapper _mapper;
        private readonly ITraceDataSaver _dataSaver;

        public TraceController(IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            IMapper mapper,
            ITraceDataSaver dataSaver)
        {
            _settings = settings;
            _settingsFactory = settingsFactory;
            _mapper = mapper;
            _dataSaver = dataSaver;
        }

        [HttpPost()]
        [Authorize()]
        public async Task<IActionResult> Create([FromBody] Trace[] traces)
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
