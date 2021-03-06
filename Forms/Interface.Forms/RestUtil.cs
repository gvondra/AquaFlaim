using BrassLoon.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Forms
{
    public class RestUtil
    {
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

        internal async Task CheckSuccess(IResponse response)
        {
            if (!response.IsSuccessStatusCode)
            {
                ApplicationException exception = new ApplicationException($"Error {(int)response.StatusCode} {response.StatusCode}");
                exception.Data["RequestAddress"] = response.Message.RequestMessage.RequestUri.ToString();
                if (response.Message.Headers
                    .Any(h => string.Equals("content-type", h.Key, StringComparison.OrdinalIgnoreCase)
                    && string.Equals("text/plain", h.Value.FirstOrDefault() ?? string.Empty, StringComparison.OrdinalIgnoreCase)))
                {
                    exception.Data["Text"] = await response.Message.Content.ReadAsStringAsync();
                }
                throw exception;
            }
        }

        internal void CheckSuccess<T>(IResponse<T> response)
        {
            if (!response.IsSuccessStatusCode)
            {
                ApplicationException exception = new ApplicationException($"Error {(int)response.StatusCode} {response.StatusCode}");
                exception.Data["RequestAddress"] = response.Message.RequestMessage.RequestUri.ToString();
                if (!string.IsNullOrEmpty(response.Text))
                    exception.Data["Text"] = response.Text;
                throw exception;
            }
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
