using BrassLoon.RestClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Log
{
    public class ExceptionService : IExceptionService
    {
        private readonly RestUtil _restUtil;
        private readonly IService _service;

        public ExceptionService(RestUtil restUtil,
            IService service)
        {
            _restUtil = restUtil;
            _service = service;
        }

        public async Task Create(ISettings settings, params Models.Exception[] exceptions)
        {
            UriBuilder builder = new UriBuilder(settings.BaseAddress);
            builder.Path = _restUtil.AppendPath(builder.Path, "api", "Exception");
            IRequest request = _service.CreateRequest(builder.Uri, HttpMethod.Post, exceptions);
            request.AddJwtAuthorizationToken(settings.GetToken);
            IResponse response = await _service.Send(request);
            await _restUtil.CheckSuccess(response);
        }

        public Task Create(ISettings settings, params System.Exception[] exceptions) => Create(settings, Map(exceptions));

        private Models.Exception[] Map(System.Exception[] exceptions)
        {
            Models.Exception[] result = new Models.Exception[exceptions.Length];
            for (int i = 0; i < exceptions.Length; i+=1)
            {
                result[i] = Map(exceptions[i]);
            }
            return result;
        }

        private Models.Exception Map(System.Exception exception)
        {
            Models.Exception result = new Models.Exception
            {
                Message = exception.Message,
                AppDomain = AppDomain.CurrentDomain.FriendlyName,
                Source = exception.Source,
                StackTrace = exception.StackTrace,
                TargetSite = exception.TargetSite.ToString(),
                Timestamp = DateTime.UtcNow,
                TypeName = exception.GetType().FullName,
                Data = GetData(exception.Data)
            };
            if (exception.InnerException != null)
                result.InnerException = Map(exception.InnerException);            
            return result;
        }

        private Dictionary<string, string> GetData(IDictionary data)
        {
            Dictionary<string, string> result = null;
            if (data != null)
            {
                result = new Dictionary<string, string>();
                IDictionaryEnumerator enumerator = data.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    string key = (enumerator.Key ?? string.Empty).ToString();
                    string value = (enumerator.Value ?? string.Empty).ToString();
                    result.Add(key, value);
                }
            }
            return result;
        }
    }
}
