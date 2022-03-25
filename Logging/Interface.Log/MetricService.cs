using AquaFlaim.Interface.Log.Models;
using BrassLoon.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Log
{
    public class MetricService : IMetricService
    {
        private readonly RestUtil _restUtil;
        private readonly IService _service;

        public MetricService(RestUtil restUtil,
            IService service)
        {
            _restUtil = restUtil;
            _service = service;
        }

        public async Task Create(ISettings settings, params Metric[] metrics)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "Metric");
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Post, metrics);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse response = await _service.Send(request);
            await _restUtil.CheckSuccess(response);
        }

        public Task Create(ISettings settings, string eventCode, double? magnitude, string status = null, Dictionary<string, string> data = null)
        => Create(
            settings, 
            new Metric
            {
                EventCode = eventCode,
                Magnitude = magnitude,
                Status = status,
                Data = data,
                Timestamp = DateTime.UtcNow
            });

        public async Task<IEnumerable<string>> GetEventCodes(ISettings settings)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "MetricEventCode");
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Get);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<List<string>> response = await _service.Send<List<string>>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<IEnumerable<Metric>> GetTopByTimestamp(ISettings settings, DateTime? maxTimestamp)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "Metric");
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Get);
            request.AddJwtAuthorizationToken(settings.GetToken);
            if (maxTimestamp.HasValue)
                request.AddQueryParameter("maxTimestamp", maxTimestamp.Value.ToString("O"));
            IResponse<List<Metric>> response = await _service.Send<List<Metric>>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }
    }
}
