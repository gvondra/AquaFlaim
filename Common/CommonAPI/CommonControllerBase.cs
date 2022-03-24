using LogAPI = AquaFlaim.Interface.Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AquaFlaim.CommonAPI
{
    public abstract class CommonControllerBase : Controller
    {
        private readonly LogAPI.IMetricService _metricService;

        protected CommonControllerBase(LogAPI.IMetricService metricService)
        {
            _metricService = metricService;
        }

        protected string GetCurrentUserReferenceId() 
            => User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        protected string GetCurrentUserEmailAddress()
            => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        protected string GetUserToken()
        {
            string token = null;
            KeyValuePair<string, StringValues>? header = Request.Headers.FirstOrDefault(h => string.Equals(h.Key, "Authorization", StringComparison.OrdinalIgnoreCase));
            if (header.HasValue && header.Value.Value.Count == 1)
            {
                token = header.Value.Value[0];
                Match match = Regex.Match(token, @"bearer\s+(\S+)", RegexOptions.IgnoreCase);
                if (match != null && match.Success && match.Groups != null && match.Groups.Count == 2)
                {
                    token = match.Groups[1].Value;
                }
            }
            return token;
        }

        protected async Task WriteMetrics(LogAPI.ISettings settings, string eventCode, double? magnitude, Dictionary<string, string> data = null)
        {
            string status = string.Empty;
            if (Response != null)
                status = ((int)Response.StatusCode).ToString();
            await _metricService.Create(settings, eventCode, magnitude, status, data);
        }
    }
}
