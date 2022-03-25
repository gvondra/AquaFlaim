﻿using AquaFlaim.Interface.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support
{
    public class LogSettings : ISettings
    {
        public string BaseAddress { get; set; }
        public string Token { get; set; }

        public Task<string> GetToken()
        {
            return Task.FromResult(Token);
        }
    }
}
