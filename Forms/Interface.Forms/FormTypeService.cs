using AquaFlaim.Interface.Forms.Models;
using BrassLoon.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Forms
{
    public class FormTypeService : IFormTypeService
    {
        private readonly IService _service;
        private readonly RestUtil _restUtil;

        public FormTypeService(IService service,
            RestUtil restUtil)
        {
            _service = service;
            _restUtil = restUtil;
        }

        public async Task<FormType> Create(ISettings settings, FormType formType)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "FormType");
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Post, formType);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<FormType> response = await _service.Send<FormType>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<FormType> Get(ISettings settings, int formTypeId)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "FormType", formTypeId.ToString());
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Get);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<FormType> response = await _service.Send<FormType>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<IEnumerable<FormType>> GetAll(ISettings settings)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "FormType");
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Get);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<List<FormType>> response = await _service.Send<List<FormType>>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<FormType> Update(ISettings settings, FormType formType)
        {
            if (!formType.FormTypeId.HasValue)
                throw new ArgumentException("Form type id not set");
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "FormType", formType.FormTypeId.Value.ToString());
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Put, formType);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<FormType> response = await _service.Send<FormType>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }
    }
}
