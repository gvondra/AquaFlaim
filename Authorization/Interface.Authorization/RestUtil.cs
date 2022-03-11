using BrassLoon.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Authorization
{
    public sealed class RestUtil
    {
        internal RestUtil() { }

        public async Task<T> Send<T>(IService service, IRequest request)
        {
            IResponse<T> response = await service.Send<T>(request);
            CheckSuccess(response);
            return response.Value;
        }

        public async Task<T> Post<T>(IService service, Uri uri, object body)
        {
            IResponse<T> response = await service.Post<T>(uri, body);
            CheckSuccess(response);
            return response.Value;
        }

        internal void CheckSuccess(IResponse response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error {(int)response.StatusCode} {response.StatusCode}");
        }

        public string AppendPath(string basePath, params string[] segments)
        {
            List<string> path = basePath.Split('/').Where(p => !string.IsNullOrEmpty(p)).ToList();
            return string.Join("/",
                path.Concat(segments.Where(s => !string.IsNullOrEmpty(s)))
                );
        }
    }
}
