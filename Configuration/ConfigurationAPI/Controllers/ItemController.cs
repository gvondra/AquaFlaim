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
using System.Threading.Tasks;

namespace ConfigurationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ConfigControllerBase
    {
        private readonly IItemFactory _itemFactory;
        private readonly IItemSaver _itemSaver;

        public ItemController(
            IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            AquaFlaim.Interface.Log.IMetricService metricService,
            AquaFlaim.Interface.Log.IExceptionService exceptionService,
            IItemFactory itemFactory,
            IItemSaver itemSaver
            ) : base(settings, settingsFactory, metricService, exceptionService)
        {
            _itemFactory = itemFactory;
            _itemSaver = itemSaver;
        }

        [Authorize(Constants.POLICY_CONFIG_READ)]
        [HttpGet("/api/ItemCode")]
        [ProducesResponseType(typeof(string[]), 200)]
        public async Task<IActionResult> GetCodes()
        {
            DateTime start = DateTime.UtcNow;
            IActionResult result = null;
            try
            {
                ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                result = Ok(await _itemFactory.GetAllCodes(settings, true));
            }
            catch (Exception ex)
            {
                await WriteException(ex);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _ = WriteMetrics("get-all-item-codes", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }

        [Authorize(Constants.POLICY_CONFIG_READ)]
        [HttpGet("{code}")]
        [ProducesResponseType(typeof(Item), 200)]
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
                    IItem innerItem = await _itemFactory.GetByCode(settings, code);
                    if (innerItem == null)
                        result = NotFound();
                    else
                    {
                        IMapper mapper = MapperConfiguration.CreateMapper();
                        result = Ok(mapper.Map<Item>(innerItem));
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
                _ = WriteMetrics("get-config-item", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }

        [Authorize(Constants.POLICY_CONFIG_READ)]
        [HttpPost]
        [ProducesResponseType(typeof(Item), 200)]
        public async Task<IActionResult> Create([FromBody] Item item)
        {
            DateTime start = DateTime.UtcNow;
            IActionResult result = null;
            try
            {
                if (result == null && item == null)
                    result = BadRequest("Missing item data");
                if (result == null && string.IsNullOrEmpty(item.Code))
                    result = BadRequest("Missing item code value");
                if (result == null)
                {
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    IItem innerItem = _itemFactory.Create();
                    IMapper mapper = MapperConfiguration.CreateMapper();
                    mapper.Map(item, innerItem);
                    await _itemSaver.Create(settings, innerItem);
                    result = Ok(mapper.Map<Item>(innerItem));
                }
            }
            catch (Exception ex)
            {
                await WriteException(ex);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _ = WriteMetrics("create-config-item", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }

        [Authorize(Constants.POLICY_CONFIG_READ)]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Item), 200)]
        public async Task<IActionResult> Update([FromRoute] Guid? id, [FromBody] Item item)
        {
            DateTime start = DateTime.UtcNow;
            IActionResult result = null;
            try
            {
                if (result == null && item == null)
                    result = BadRequest("Missing item data");
                if (result == null && string.IsNullOrEmpty(item.Code))
                    result = BadRequest("Missing item code value");
                if (result == null && (!id.HasValue || id.Value.Equals(Guid.Empty)))
                    result = BadRequest("Missing id parameter value");
                if (result == null)
                {
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    IItem innerItem = await _itemFactory.Get(settings, id.Value);
                    if (innerItem == null)
                        result = NotFound();
                    else
                    {
                        IMapper mapper = MapperConfiguration.CreateMapper();
                        mapper.Map(item, innerItem);
                        await _itemSaver.Update(settings, innerItem);
                        result = Ok(mapper.Map<Item>(innerItem));
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
                _ = WriteMetrics("update-config-item", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }
    }
}
