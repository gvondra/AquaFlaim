using AquaFlaim.Forms.Data.Framework.Models;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Data.Framework
{
    public interface IFormTypeDataFactory
    {
        Task<FormTypeData> Get(ISqlSettings settings, int id);
        Task<IEnumerable<FormTypeData>> GetAll(ISqlSettings settings);
    }
}
