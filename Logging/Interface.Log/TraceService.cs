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
    public class TraceService : ITraceService
    {
        private readonly RestUtil _restUtil;
        private readonly IService _service;

        public TraceService(RestUtil restUtil,
            IService service)
        {
            _restUtil = restUtil;
            _service = service;
        }

        public Task Creaet(ISettings settings, string eventCode, string message, Dictionary<string, string> data = null)
        => Create(
            settings,
            new Trace
            {
                EventCode = eventCode,
                Message = message,
                Data = data,
                Timestamp = DateTime.UtcNow
            });

        public async Task Create(ISettings settings, params Trace[] traces)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "Trace");
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Post, traces);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse response = await _service.Send(request);
            await _restUtil.CheckSuccess(response);
        }
    }
}
