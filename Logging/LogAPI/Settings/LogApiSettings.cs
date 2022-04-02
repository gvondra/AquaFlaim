using AquaFlaim.Interface.Log;
using System.Threading.Tasks;

namespace LogAPI
{
    public class LogApiSettings : ISettings
    {
        public string BaseAddress { get; set; }
        public string Token { get; set; }

        public Task<string> GetToken()
        {
            return Task.FromResult(Token);
        }
    }
}
