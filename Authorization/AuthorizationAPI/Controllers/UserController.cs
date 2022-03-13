﻿using AquaFlaim.Authorization.Framework;
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
    public class UserController : AuthorizationControllerBase
    {
        private readonly IOptions<Settings> _settings;
        private readonly ISettingsFactory _settingsFactory;
        private readonly IMapper _mapper;
        private readonly IUserFactory _userFactory;
        private readonly IUserSaver _userSaver;

        public UserController(IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            IMapper mapper,
            IUserFactory userFactory,
            IUserSaver userSaver)
        {
            _settings = settings;
            _settingsFactory = settingsFactory;
            _mapper = mapper;
            _userFactory = userFactory;
            _userSaver = userSaver;
        }

        [HttpGet()]
        [Authorize(Constants.POLICY_USER_READ)]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> GetAll([FromQuery] string emailAddress)
        {
            IActionResult result = null;
            try
            {
                IUser innerUser = null;
                ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                if (result == null && !string.IsNullOrEmpty(emailAddress))
                    innerUser = (await _userFactory.GetByEmailAddress(settings, emailAddress)).FirstOrDefault();
                if (result == null && innerUser == null)
                    innerUser = await _userFactory.GetByReferenceId(settings, GetCurrentUserReferenceId());
                if (result == null && innerUser == null)
                    result = NotFound();
                if (result == null && innerUser != null)
                {
                    User user = _mapper.Map<User>(innerUser);
                    user.Roles = await innerUser.GetRoles(settings);
                    result = Ok(user);
                }
            }
            catch (Exception ex)
            {
                //todo await LogException(ex, _exceptionService.Value, _settingsFactory, _settings.Value);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Constants.POLICY_USER_READ)]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> Get([FromRoute] Guid? id)
        {
            IActionResult result = null;
            try
            {
                if (result == null && (!id.HasValue || id.Value.Equals(Guid.Empty)))
                    result = BadRequest("Missing user id value");
                if (result == null)
                {
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    IUser innerUser = await _userFactory.Get(settings, id.Value);
                    if (result == null && innerUser == null)
                        result = NotFound();
                    if (result == null && innerUser != null)
                    {
                        User user = _mapper.Map<User>(innerUser);
                        user.Roles = await innerUser.GetRoles(settings);
                        result = Ok(user);
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

        [HttpPut("{id}")]
        [Authorize(Constants.POLICY_USER_EDIT)]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> Update([FromRoute] Guid? id, [FromBody] User user)
        {
            IActionResult result = null;
            try
            {
                if (result == null && (!id.HasValue || id.Value.Equals(Guid.Empty)))
                    result = BadRequest("Missing user id value");
                if (result == null && user == null)
                    result = BadRequest("Missing user data");
                if (result == null && string.IsNullOrEmpty(user.Name))
                    result = BadRequest("Missing user name value");
                if (result == null)
                {
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    IUser innerUser = await _userFactory.Get(settings, id.Value);
                    if (result == null && innerUser == null)
                        result = NotFound();
                    if (result == null && innerUser != null)
                    {
                        _mapper.Map<User, IUser>(user, innerUser);
                        if (user.Roles != null)
                        {
                            foreach (string key in user.Roles.Keys)
                            {
                                await innerUser.AddRole(settings, key);
                            }
                        }
                        foreach (string key in ( await innerUser.GetRoles(settings)).Keys)
                        {
                            if (user.Roles == null || !user.Roles.ContainsKey(key))
                                await innerUser.RemoveRole(settings, key);
                        }
                        await _userSaver.Update(settings, innerUser); 
                        _mapper.Map<IUser, User>(innerUser, user);
                        user.Roles = await innerUser.GetRoles(settings);
                        result = Ok(user);
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