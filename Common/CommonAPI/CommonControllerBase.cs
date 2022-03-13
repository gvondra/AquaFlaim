using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.CommonAPI
{
    public abstract class CommonControllerBase : Controller
    {
        protected string GetCurrentUserReferenceId() 
            => User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        protected string GetCurrentUserEmailAddress()
            => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    }
}
