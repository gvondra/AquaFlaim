using AquaFlaim.Interface.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Authorization
{
    public interface IUserService
    {
        Task<User> Get(ISettings settings);
        Task<User> GetByEmailAddress(ISettings settings, string emailAddress);
        Task<User> Update(ISettings settings, User user);
    }
}
