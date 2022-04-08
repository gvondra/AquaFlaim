using AquaFlaim.Interface.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support
{
    public class FormsSettings : ISettings
    {
        public string BaseAddress { get; set; }
        public string Token { get; set; }

        public Task<string> GetToken()
        {
            return Task.FromResult(Token);
        }
    }
}
