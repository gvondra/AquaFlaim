using AquaFlaim.Interface.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Configuration
{
    public interface IItemService
    {
        Task<IEnumerable<string>> GetCodes(ISettings settings);
        Task<Item> GetPublicByCode(ISettings settings, string code);
        Task<Item> GetByCode(ISettings settings, string code);
        Task<Item> Create(ISettings settings, Item item);
        Task<Item> Update(ISettings settings, Item item
            );
    }
}
