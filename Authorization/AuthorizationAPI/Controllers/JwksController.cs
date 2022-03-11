﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwksController : AuthorizationControllerBase
    {
        private readonly IOptions<Settings> _settings;

        public JwksController(IOptions<Settings> settings)
        {
            _settings = settings;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 150)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var jsonWebKeySet = new { Keys = new List<object>() };
                RsaSecurityKey securityKey = RsaSecurityKeySerializer.GetSecurityKey(_settings.Value.TknCsp);
                JsonWebKey jsonWebKey = JsonWebKeyConverter.ConvertFromRSASecurityKey(securityKey);
                jsonWebKey.Alg = "RS512";
                jsonWebKey.Use = "sig";
                jsonWebKeySet.Keys.Add(jsonWebKey);
                return Content(JsonConvert.SerializeObject(jsonWebKeySet, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }), "appliation/json");
            }
            catch (Exception ex)
            {
                // todo await LogException(ex, _exceptionService.Value, _settingsFactory, _settings.Value);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}