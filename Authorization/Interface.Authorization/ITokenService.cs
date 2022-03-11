﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Authorization
{
    public interface ITokenService
    {
        Task<string> Create(ISettings settings);
    }
}
