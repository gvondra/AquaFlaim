using AquaFlaim.Authorization.Framework;
using AquaFlaim.Interface.Authorization.Models;
using AquaFlaim.CommonCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AquaFlaim.CommonAPI;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : AuthorizationControllerBase
    {
        private readonly IOptions<Settings> _settings;
        private readonly ISettingsFactory _settingsFactory;
        private readonly IMapper _mapper;
        private readonly IClientFactory _clientFactory;
        private readonly IClientSaver _clientSaver;
        private readonly IClientSecretProcessor _clientSecretProcessor;

        public ClientController(IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            IMapper mapper,
            IClientFactory clientFactory,
            IClientSaver clientSaver,
            IClientSecretProcessor clientSecretProcessor)
        {
            _settings = settings;
            _settingsFactory = settingsFactory;
            _mapper = mapper;
            _clientFactory = clientFactory;
            _clientSaver = clientSaver;
            _clientSecretProcessor = clientSecretProcessor;
        }

        [HttpGet()]
        [Authorize(Constants.POLICY_CLIENT_READ)]
        [ProducesResponseType(typeof(Client), 200)]
        public async Task<IActionResult> GetAll()
        {
            IActionResult result = null;
            try
            {
                IEnumerable<IClient> innerClients = await _clientFactory.GetAll(_settingsFactory.CreateCore(_settings.Value));
                result = Ok(innerClients.Select<IClient, Client>(c => _mapper.Map<Client>(c)));
            }
            catch (Exception ex)
            {
                //todo await LogException(ex, _exceptionService.Value, _settingsFactory, _settings.Value);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Constants.POLICY_CLIENT_READ)]
        [ProducesResponseType(typeof(Client), 200)]
        public async Task<IActionResult> GetAll([FromRoute] Guid? id)
        {
            IActionResult result = null;
            try
            {
                if (result == null && (!id.HasValue || id.Value.Equals(Guid.Empty)))
                    result = BadRequest("Missing id parameter value");
                if (result == null)
                {
                    IClient innerClient = await _clientFactory.Get(_settingsFactory.CreateCore(_settings.Value), id.Value);
                    if (innerClient == null)
                        result = NotFound();
                    if (result == null)
                    {
                        result = Ok(_mapper.Map<Client>(innerClient));
                    }
                }
            }
            catch (Exception ex)
            {
                //todo await LogException(ex, _exceptionService.Value, _settingsFactory, _settings.Value);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return result;
        }

        [HttpGet("/api/ClientCredentialSecret")]
        [Authorize(Constants.POLICY_CLIENT_EDIT)]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetClientCredentialSecret()
        {
            IActionResult result;
            try
            {
                result = Ok(_clientSecretProcessor.Create());
            }
            catch (Exception ex)
            {
                //todo await LogException(ex, _exceptionService.Value, _settingsFactory, _settings.Value);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return result;
        }

        [HttpPost()]
        [Authorize(Constants.POLICY_CLIENT_EDIT)]
        [ProducesResponseType(typeof(Client), 200)]
        public async Task<IActionResult> Create(ClientSaveRequest request)
        {
            IActionResult result = null;
            try
            {
                if (result == null && request.Client == null)
                    result = BadRequest("Missing client data");
                if (result == null && string.IsNullOrEmpty(request.Client?.Name))
                    result = BadRequest("Missing client name value");
                if (result == null && string.IsNullOrEmpty(request.Secret))
                    result = BadRequest("Missing secret value");
                if (result == null)
                {
                    IClient innerClient = _clientFactory.Create(request.Secret);
                    _mapper.Map<Client, IClient>(request.Client, innerClient);
                    await _clientSaver.Create(_settingsFactory.CreateCore(_settings.Value), innerClient);
                    result = Ok(_mapper.Map<Client>(innerClient));
                }
            }
            catch (Exception ex)
            {
                //todo await LogException(ex, _exceptionService.Value, _settingsFactory, _settings.Value);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Constants.POLICY_CLIENT_EDIT)]
        [ProducesResponseType(typeof(Client), 200)]
        public async Task<IActionResult> Update([FromRoute] Guid? id, ClientSaveRequest request)
        {
            IActionResult result = null;
            try
            {
                if (result == null && (!id.HasValue || id.Value.Equals(Guid.Empty)))
                    result = BadRequest("Missing client id value");
                if (result == null && request.Client == null)
                    result = BadRequest("Missing client data");
                if (result == null && string.IsNullOrEmpty(request.Client?.Name))
                    result = BadRequest("Missing client name value");
                if (result == null)
                {
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    IClient innerClient = await _clientFactory.Get(settings, id.Value);
                    if (innerClient == null)
                        result = NotFound();
                    if (result == null)
                    {
                        _mapper.Map<Client, IClient>(request.Client, innerClient);
                        if (!string.IsNullOrEmpty(request.Secret))
                            innerClient.SetSecret(request.Secret);
                        await _clientSaver.Update(_settingsFactory.CreateCore(_settings.Value), innerClient);
                        result = Ok(_mapper.Map<Client>(innerClient));
                    }
                }
            }
            catch (Exception ex)
            {
                //todo await LogException(ex, _exceptionService.Value, _settingsFactory, _settings.Value);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return result;
        }

    }
}
