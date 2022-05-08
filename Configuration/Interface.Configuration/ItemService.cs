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
    public class ItemService : IItemService
    {
        private readonly IService _service;
        private readonly RestUtil _restUtil;

        public ItemService(IService service,
            RestUtil restUtil)
        {
            _service = service;
            _restUtil = restUtil;
        }

        public async Task<Item> Create(ISettings settings, Item item)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "Item");
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Post, item);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<Item> response = await _service.Send<Item>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<Item> GetPublicByCode(ISettings settings, string code)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "Item", code);
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Get);
            IResponse<Item> response = await _service.Send<Item>(request);            
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<Item> GetByCode(ISettings settings, string code)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "r", "Item", code);
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Get);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<Item> response = await _service.Send<Item>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<IEnumerable<string>> GetCodes(ISettings settings)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "ItemCode");
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Get);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<List<string>> response = await _service.Send<List<string>>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }

        public async Task<Item> Update(ISettings settings, Item item)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "Item", item.ItemId.ToString("N"));
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Put, item);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse<Item> response = await _service.Send<Item>(request);
            _restUtil.CheckSuccess(response);
            return response.Value;
        }
    }
}
