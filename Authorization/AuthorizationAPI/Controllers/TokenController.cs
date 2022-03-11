﻿using AquaFlaim.Authorization.Framework;
using AquaFlaim.CommonAPI;
using AquaFlaim.CommonCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : AuthorizationControllerBase
    {
        private readonly IOptions<Settings> _settings;
        private readonly ISettingsFactory _settingsFactory;
        public readonly IUserFactory _userFactory;
        public readonly IUserSaver _userSaver;
        public readonly IEmailAddressFactory _emailAddressFactory;
        public readonly IEmailAddressSaver _emailAddressSaver;

        public TokenController(IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            IUserFactory userFactory,
            IUserSaver userSaver,
            IEmailAddressFactory emailAddressFactory,
            IEmailAddressSaver emailAddressSaver)
        {
            _settings = settings;
            _settingsFactory = settingsFactory;
            _userFactory = userFactory;
            _userSaver = userSaver;
            _emailAddressFactory = emailAddressFactory;
            _emailAddressSaver = emailAddressSaver;
        }

        [HttpPost]
        [Authorize(Constants.POLICY_TOKEN_CREATE)]
        public async Task<IActionResult> Create()
        {
            try
            {
                IUser user = await GetUser();
                return Ok(await CreateToken(user));
            }
            catch (Exception ex)
            {
                //todo await LogException(ex, _exceptionService.Value, _settingsFactory, _settings.Value);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /*
        [HttpPost("ClientCredential")]
        public async Task<IActionResult> CreateClientCredential([FromBody] ClientCredential clientCredential)
        {
            IActionResult result = null;
            try
            {
                if (result == null && clientCredential == null)
                    result = BadRequest("Missing request data");
                if (result == null && (!clientCredential.ClientId.HasValue || clientCredential.ClientId.Value.Equals(Guid.Empty)))
                    result = BadRequest("Missing client id value");
                if (result == null && string.IsNullOrEmpty(clientCredential.Secret))
                    result = BadRequest("Missing secret value");
                if (result == null)
                {
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    IClient client = await _clientFactory.Get(settings, clientCredential.ClientId.Value);
                    if (client == null)
                        result = StatusCode(StatusCodes.Status401Unauthorized);
                    if (result == null)
                    {
                        if (!_secretProcessor.Verify(clientCredential.Secret, await client.GetSecretHash(settings)))
                            result = StatusCode(StatusCodes.Status401Unauthorized);
                    }
                    if (result == null)
                    {
                        result = Content(await CreateToken(client), "text/plain");
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
        */
        [NonAction]
        private async Task<IUser> GetUser()
        {
            IUser user;
            IEmailAddress emailAddress = null;
            string subscriber = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            ISettings coreSettings = _settingsFactory.CreateCore(_settings.Value);
            user = await _userFactory.GetByReferenceId(coreSettings, subscriber);
            if (user == null)
            {
                string email = User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
                emailAddress = await _emailAddressFactory.GetByAddress(coreSettings, email);
                if (emailAddress == null)
                {
                    emailAddress = _emailAddressFactory.Create(email);
                    await _emailAddressSaver.Create(coreSettings, emailAddress);
                }
                user = _userFactory.Create(subscriber, emailAddress);
                user.Name = User.Claims.First(c => string.Equals(c.Type, ClaimTypes.Name, StringComparison.OrdinalIgnoreCase)).Value;
                //SetSuperUser(user);
                await _userSaver.Create(coreSettings, user);
            }
            else
            {
                user.Name = User.Claims.First(c => string.Equals(c.Type, ClaimTypes.Name, StringComparison.OrdinalIgnoreCase) || string.Equals(c.Type, ClaimTypes.Name, StringComparison.OrdinalIgnoreCase)).Value;
                //SetSuperUser(user);
                await _userSaver.Update(coreSettings, user);
            }
            return user;
        }

        //[NonAction]
        //private void SetSuperUser(IUser user)
        //{
        //    string email = User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        //    if (!string.IsNullOrEmpty(_settings.Value.SuperUser) && string.Equals(email, _settings.Value.SuperUser, StringComparison.OrdinalIgnoreCase))
        //    {
        //        user.Roles = user.Roles |
        //            UserRole.SystemAdministrator |
        //            UserRole.AccountAdministrator
        //            ;
        //    }
        //}

        [NonAction]
        private async Task<string> CreateToken(IUser user)
        {
            RsaSecurityKey securityKey = RsaSecurityKeySerializer.GetSecurityKey(_settings.Value.TknCsp, true);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha512);
            List<Claim> claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
                };
            Claim claim = User.Claims.FirstOrDefault(c => string.Equals(_settings.Value.ExternalIdIssuer, c.Issuer, StringComparison.OrdinalIgnoreCase) && string.Equals(ClaimTypes.NameIdentifier, c.Type, StringComparison.OrdinalIgnoreCase));
            if (claim != null)
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, claim.Value));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, (await user.GetEmailAddress(_settingsFactory.CreateCore(_settings.Value))).Address));
            //if ((user.Roles & UserRole.SystemAdministrator) == UserRole.SystemAdministrator)
            //    claims.Add(new Claim("role", "sysadmin"));
            //if ((user.Roles & UserRole.AccountAdministrator) == UserRole.AccountAdministrator)
            //    claims.Add(new Claim("role", "actadmin"));
            JwtSecurityToken token = new JwtSecurityToken(
                _settings.Value.InternalIdIssuer,
                _settings.Value.InternalIdIssuer,
                claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [NonAction]
        private Task<string> CreateToken(IClient client)
        {
            RsaSecurityKey securityKey = RsaSecurityKeySerializer.GetSecurityKey(_settings.Value.TknCsp, true);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha512);
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, client.ClientId.ToString("N")));
            JwtSecurityToken token = new JwtSecurityToken(
                _settings.Value.InternalIdIssuer,
                _settings.Value.InternalIdIssuer,
                claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: credentials
                );
            return Task.FromResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
