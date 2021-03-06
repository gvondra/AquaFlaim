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
    public class ExceptionController : LogControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExceptionDataSaver _dataSaver;

        public ExceptionController(IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            IMetricService metricService,
            IExceptionService exceptionService,
            IMapper mapper,
            IExceptionDataSaver dataSaver)
            : base(settings, settingsFactory, metricService, exceptionService)
        {
            _mapper = mapper;
            _dataSaver = dataSaver;
        }

        [HttpPost()]
        [Authorize(Constants.POLICY_LOG_WRITE)]
        public async Task<IActionResult> Create([FromBody] Exception[] exceptions)
        {
            try
            {
                if (exceptions != null && exceptions.Length > 0)
                {
                    Saver saver = new Saver();
                    for (int i = 0; i < exceptions.Length; i += 1)
                    {
                        await saver.Save(new TransactionHandler(_settingsFactory.CreateCore(_settings.Value)),
                            (th) => Save(th, exceptions[i])
                            );
                    }
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [NonAction]
        private async Task Save(ITransactionHandler transactionHandler, Exception exception, System.Guid? parentId = null)
        {
            ExceptionData data = _mapper.Map<ExceptionData>(exception);
            data.ParentExceptionId = parentId;
            await _dataSaver.Create(transactionHandler, data);
            if (exception.InnerException != null)
                await Save(transactionHandler, exception.InnerException, data.ExceptionId);
        }
    }
}
