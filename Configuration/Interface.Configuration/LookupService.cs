using AquaFlaim.Interface.Configuration.Models;
using BrassLoon.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Configuration
{
    public class LookupService : ILookupService
    {
        private readonly IService _service;
        private readonly RestUtil _restUtil;

        public LookupService(IService service,
            RestUtil restUtil)
        {
            _service = service;
            _restUtil = restUtil;
        }

        public async Task<Lookup> Create(ISettings settings, Lookup lookup)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "Lookup");
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Post, lookup);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<Lookup> response = await _service.Send<Lookup>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<Lookup> GetPublicByCode(ISettings settings, string code)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "Lookup", code);
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Get);
            IResponse<Lookup> response = await _service.Send<Lookup>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<Lookup> GetByCode(ISettings settings, string code)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "r", "Lookup", code);
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Get);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<Lookup> response = await _service.Send<Lookup>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<IEnumerable<string>> GetCodes(ISettings settings)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "LookupCode");
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Get);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<List<string>> response = await _service.Send<List<string>>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<Lookup> Update(ISettings settings, Lookup lookup)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "Lookup", lookup.LookupId.ToString("N"));
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Put, lookup);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<Lookup> response = await _service.Send<Lookup>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }
    }
}
