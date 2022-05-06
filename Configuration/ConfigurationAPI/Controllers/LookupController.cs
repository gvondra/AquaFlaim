using AquaFlaim.CommonAPI;
using AquaFlaim.CommonCore;
using AquaFlaim.Config.Framework;
using AquaFlaim.Interface.Configuration.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace ConfigurationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ConfigControllerBase
    {
        private readonly ILookupFactory _lookupFactory;
        private readonly ILookupSaver _lookupSaver;

        public LookupController(
            IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            AquaFlaim.Interface.Log.IMetricService metricService,
            AquaFlaim.Interface.Log.IExceptionService exceptionService,
            ILookupFactory lookupFactory,
            ILookupSaver lookupSaver
            ) : base(settings, settingsFactory, metricService, exceptionService)
        {
            _lookupFactory = lookupFactory;
            _lookupSaver = lookupSaver;
        }

        [Authorize(Constants.POLICY_CONFIG_READ)]
        [HttpGet("/api/LookupCode")]
        [ProducesResponseType(typeof(string[]), 200)]
        public async Task<IActionResult> GetCodes()
        {
            DateTime start = DateTime.UtcNow;
            IActionResult result = null;
            try
            {
                ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                result = Ok(await _lookupFactory.GetAllCodes(settings, true));
            }
            catch (Exception ex)
            {
                await WriteException(ex);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _ = WriteMetrics("get-all-lookup-codes", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }

        [Authorize(Constants.POLICY_CONFIG_READ)]
        [HttpGet("{code}")]
        [ProducesResponseType(typeof(Lookup[]), 200)]
        public async Task<IActionResult> Get(string code)
        {
            DateTime start = DateTime.UtcNow;
            IActionResult result = null;
            try
            {
                if (result == null && string.IsNullOrEmpty(code))
                    result = BadRequest("Missing code value");
                if (result == null)
                {
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    ILookup innerLookup = await _lookupFactory.GetByCode(settings, code);
                    if (innerLookup == null)
                        result = NotFound();
                    else
                    {
                        IMapper mapper = MapperConfiguration.CreateMapper();
                        result = Ok(mapper.Map<Lookup>(innerLookup));
                    }                        
                }
            }
            catch (Exception ex)
            {
                await WriteException(ex);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _ = WriteMetrics("get-lookup", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }

        [Authorize(Constants.POLICY_CONFIG_READ)]
        [HttpPost]
        [ProducesResponseType(typeof(Lookup), 200)]
        public async Task<IActionResult> Create([FromBody] Lookup lookup)
        {
            DateTime start = DateTime.UtcNow;
            IActionResult result = null;
            try
            {
                if (result == null && lookup == null)
                    result = BadRequest("Missing item data");
                if (result == null && string.IsNullOrEmpty(lookup.Code))
                    result = BadRequest("Missing item code value");
                if (result == null)
                {
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    ILookup innerLookup = _lookupFactory.Create();
                    IMapper mapper = MapperConfiguration.CreateMapper();
                    mapper.Map(lookup, innerLookup);
                    await _lookupSaver.Create(settings, innerLookup);
                    result = Ok(mapper.Map<Lookup>(innerLookup));
                }
            }
            catch (Exception ex)
            {
                await WriteException(ex);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _ = WriteMetrics("create-config-lookup", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }

        [Authorize(Constants.POLICY_CONFIG_READ)]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Lookup), 200)]
        public async Task<IActionResult> Update([FromRoute] Guid? id, [FromBody] Lookup lookup)
        {
            DateTime start = DateTime.UtcNow;
            IActionResult result = null;
            try
            {
                if (result == null && lookup == null)
                    result = BadRequest("Missing item data");
                if (result == null && string.IsNullOrEmpty(lookup.Code))
                    result = BadRequest("Missing item code value");
                if (result == null && (!id.HasValue || id.Value.Equals(Guid.Empty)))
                    result = BadRequest("Missing id parameter value");
                if (result == null)
                {
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    ILookup innerLookup = await _lookupFactory.Get(settings, id.Value);
                    if (innerLookup == null)
                        result = NotFound();
                    else
                    {
                        IMapper mapper = MapperConfiguration.CreateMapper();
                        mapper.Map(lookup, innerLookup);
                        await _lookupSaver.Update(settings, innerLookup);
                        result = Ok(mapper.Map<Lookup>(innerLookup));
                    }
                }
            }
            catch (Exception ex)
            {
                await WriteException(ex);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _ = WriteMetrics("update-config-lookup", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }
    }
}
