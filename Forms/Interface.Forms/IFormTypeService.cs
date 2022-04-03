using AquaFlaim.Interface.Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Forms
{
    public interface IFormTypeService
    {
        Task<FormType> Get(ISettings settings, int formTypeId);
        Task<IEnumerable<FormType>> GetAll(ISettings settings);
        Task<FormType> Create(ISettings settings, FormType formType);
        Task<FormType> Update(ISettings settings, FormType formType);
    }
}
